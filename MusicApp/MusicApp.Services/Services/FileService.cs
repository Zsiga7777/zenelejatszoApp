using System.Text;

namespace MusicApp.Services.Services;

public class FileService : IFileService
{
    public List<MusicModel> ReadAllMusicFromDirectories(List<string> directories)
    {
        DirectoryInfo directory;
        FileInfo[] files;

        List<MusicModel> songs = new List<MusicModel>();
        foreach (string directoryPath in directories)
        {
            if (directoryPath == "") 
            { 
                continue;
            }

            directory = new DirectoryInfo(directoryPath);

            files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".mp3") 
                { 
                    continue; 
                }
                else
                {
                    try
                    {
                        TagLib.File TagFile = TagLib.File.Create(file.FullName);
                        double duration = TagFile.Properties.Duration.TotalSeconds;
                        songs.Add(new MusicModel($"{file.FullName}", duration, $"{file.Name.Remove(file.Name.IndexOf('(') == -1 ? file.Name.Length - 4 : file.Name.IndexOf('('))}"));
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        return songs;
    }

    public List<string> ReadAllFilePathFromPreference()
    {
        return Preferences.Get("ListOfFolders", "") != ""? Preferences.Get("ListOfFolders", "").Split(";").ToList() : new List<string>();
    }

    public void WriteAllFilePathtoPreference(List<string> paths)
    {
        string result = "";
        foreach (string path in paths)
        {
            result += path + ";";
        }
        if (result.Length > 0)
        {
            result = result.Remove(result.Length - 1);
        }
        Preferences.Set("ListOfFolders", result);
    }

    public async Task WriteLogsAsync(string exceptionMessage)
    {
        string folderPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Music player",
        "logs");
        Directory.CreateDirectory(folderPath);
        string path = Path.Combine(folderPath, $"log_{DateTime.Today:yyyy.MM.dd}.txt");

        using FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None, 128);
        using StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

        await sw.WriteLineAsync(exceptionMessage);
    }
}