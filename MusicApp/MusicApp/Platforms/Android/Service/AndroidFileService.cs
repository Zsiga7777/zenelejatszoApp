using Android.Content;
using Android.Database;
using Android.Provider;

namespace MusicApp.Platforms.Android.Service;

public class AndroidFileService : IFileService
{
    public List<MusicModel> ReadAllMusicFromDirectories(List<string> directories)
    {
        List<MusicModel> result = new List<MusicModel>();

        ContentResolver contentResolver = Platform.CurrentActivity.ContentResolver;
        string[] columns = {
    MediaStore.Audio.Media.InterfaceConsts.IsMusic,
    MediaStore.Audio.Media.InterfaceConsts.Id,
    MediaStore.Audio.Media.InterfaceConsts.Title,
     MediaStore.Audio.Media.InterfaceConsts.Duration,
};

            ICursor cursor = contentResolver.Query(MediaStore.Audio.Media.ExternalContentUri, columns, null, null, null);
            if (cursor is null)
            {
                return result;
            }
            while (cursor.MoveToNext())
            {
            if (int.Parse(cursor.GetString(0)) == 0)
            {
                continue;
            }
            if (!cursor.GetString(1).ToLower().EndsWith(".mp3"))
            {
                continue;
            }

            long id = cursor.GetLong(0);
            var contentUri = ContentUris.WithAppendedId(MediaStore.Audio.Media.ExternalContentUri, id);

            result.Add(new MusicModel(contentUri.ToString(), TimeSpan.FromMilliseconds(long.Parse(cursor.GetString(3))).TotalSeconds, cursor.GetString(2)));
            }
            cursor.Close();

            return result;
    }

    public List<string> ReadAllFilePathFromPreference()
    {
        return Preferences.Get("ListOfFolders", "") != "" ? Preferences.Get("ListOfFolders", "").Split(";").ToList() : new List<string>();
    }
    public void WriteAllFilePathtoPreference(List<string> paths)
    {
        string result = "";
        foreach (string path in paths)
        {
            result += path + ";";
        }
        result = result.Remove(result.Length - 1);
        Preferences.Set("ListOfFolders", result);
    }
}
