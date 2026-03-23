namespace MusicApp.Core.Models;

public class FolderModel
{
    public string FolderName { get;  set; }
    public string FolderPath { get;  set; }

    public FolderModel()
    {
        
    }

    public FolderModel(string folderName, string folderPath)
    {
        FolderName = folderName;
        FolderPath = folderPath;
    }
}
