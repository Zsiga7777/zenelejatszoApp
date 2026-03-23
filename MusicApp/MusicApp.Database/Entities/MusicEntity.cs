namespace MusicApp.Database.Entities;

[Table("Musics")]
public class MusicEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    public string Path { get;  set; }

    [Required]
    public double Length { get;  set; }

    [Required]
    public string Title { get;  set; }

    public virtual ICollection<MusicPlaylistConnectionEntity> MusicPlaylistConnections { get; set; }

}
