namespace MusicApp.Core.Interfaces;

public interface IFileService
{
    List<MusicModel> ReadAllMusicFromDirectories(List<string> directories);
    void WriteAllFilePathtoPreference(List<string> paths);
    List<string> ReadAllFilePathFromPreference();
    Task WriteLogsAsync(string exceptionMessage);
}
