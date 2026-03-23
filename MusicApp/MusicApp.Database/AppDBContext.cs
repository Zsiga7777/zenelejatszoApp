namespace MusicApp.Database;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public DbSet<PlaylistEntity> Playlists { get; set; }
    public DbSet<MusicEntity> Musics { get; set; }
    public DbSet<MusicPlaylistConnectionEntity> MusicPlaylistConnections { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "songs.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}", option =>
        {
            option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MusicPlaylistConnectionEntity>(entity =>
        {
            entity.HasKey(musicPlaylistConnection => new { musicPlaylistConnection.MusicId, musicPlaylistConnection.PlayListId });

            entity.HasOne(musicPlaylistConnection => musicPlaylistConnection.Music)
                  .WithMany(music => music.MusicPlaylistConnections)
                  .HasForeignKey(musicPlaylistConnection => musicPlaylistConnection.MusicId);

            entity.HasOne(musicPlaylistConnection => musicPlaylistConnection.Playlist)
                  .WithMany(playlist => playlist.MusicPlaylistConnections)
                  .HasForeignKey(musicPlaylistConnection => musicPlaylistConnection.PlayListId);
        });
    }
}
