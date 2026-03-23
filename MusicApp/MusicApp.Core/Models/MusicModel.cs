using CommunityToolkit.Maui.Views;

namespace MusicApp.Core.Models;

[ObservableObject]
public partial class MusicModel
{
    public uint Id { get; private set; }
    public string Path { get;private set; }
    public MediaSource MediaSourcePath { get; private set; }
    public double Length { get; private set; }
    public string Title { get;  set; }

    [ObservableProperty]
    public Color bgcolor; 
    public MusicModel()
    {
        
    }

    public MusicModel(string path, double length, string title)
    {
        Path = path;
        MediaSourcePath = MediaSource.FromFile(path);
        Length = length;
        Title = title;
    }

    public MusicModel(MusicEntity entity)
    {
        this.Id = entity.Id;
        this.Path = entity.Path;
        this.MediaSourcePath = MediaSource.FromFile(entity.Path);
        this.Length = entity.Length;
        this.Title = entity.Title;
    }

    public MusicEntity ModelToEntity()
    {
        return new MusicEntity
        {
            Id = this.Id,
            Path = this.Path.ToString(),
            Length = this.Length,
            Title = this.Title
        };
    }
}
