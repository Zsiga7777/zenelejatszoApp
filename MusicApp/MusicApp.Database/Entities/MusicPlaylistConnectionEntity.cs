namespace MusicApp.Database.Entities;

[Table("MusicPlaylistConnection")]
public class MusicPlaylistConnectionEntity
{
    public uint MusicId { get; set; }
    public virtual MusicEntity Music { get; set; }
    public uint PlayListId { get; set; }
    public virtual PlaylistEntity Playlist { get; set; }
}
