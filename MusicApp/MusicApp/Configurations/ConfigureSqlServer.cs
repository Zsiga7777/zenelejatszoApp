namespace MusicApp.Configurations;

public static class ConfigureSqlServer
{
    public static MauiAppBuilder UseMsSqlServer(this MauiAppBuilder builder)
    {
        string dbPath = Path.Combine(Environment.CurrentDirectory, "songs.db");

        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseSqlite(dbPath)
        );
        return builder;
    }
}
