namespace MusicApp.Database.Entities;

[Table("Playlist")]
public class PlaylistEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    public string Title { get;  set; }

    public virtual ICollection<MusicPlaylistConnectionEntity> MusicPlaylistConnections { get; set; }
}
