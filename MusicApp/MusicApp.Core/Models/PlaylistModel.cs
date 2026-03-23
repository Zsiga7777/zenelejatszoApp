namespace MusicApp.Core.Models;

public class PlaylistModel
{
    public uint Id { get;  set; }
    public string Title { get;  set; }

    public List<MusicModel>? Musics { get; set; }

    public PlaylistModel()
    {

    }

    public PlaylistModel( string title)
    {
        Title = title;
    }

    public PlaylistModel(PlaylistEntity entity)
    {
        this.Id = entity.Id;
        this.Title = entity.Title;
    }

    public PlaylistEntity ModelToEntity()
    {
        return new PlaylistEntity
        {
            Id = this.Id,
            Title = this.Title
        };
    }
}
