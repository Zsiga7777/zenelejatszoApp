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
                    TagLib.File TagFile = TagLib.File.Create(file.FullName);
                    double duration = TagFile.Properties.Duration.TotalSeconds;
                    songs.Add(new MusicModel($"{file.FullName}", duration, $"{file.Name.Remove(file.Name.IndexOf('(') == -1 ? file.Name.Length - 4 : file.Name.IndexOf('('))}"));
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
        result = result.Remove(result.Length-1);
        Preferences.Set("ListOfFolders", result);
    }
}