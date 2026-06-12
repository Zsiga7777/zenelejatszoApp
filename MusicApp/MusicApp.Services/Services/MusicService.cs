namespace MusicApp.Services.Services;

public class MusicService(AppDBContext dBContext) : IMusicService
{
    public async Task UpdateMusicsAsync(List<MusicModel> musics)
    {
            await dBContext.Database.EnsureCreatedAsync();

        List<MusicEntity> musicEntities = musics.Select(x => x.ModelToEntity()).ToList();
        List<MusicEntity> databaseMusics = await dBContext.Musics.AsNoTracking().ToListAsync();

        foreach (MusicEntity entity in databaseMusics)
        {
            if (!musicEntities.Any(x => x.Title == entity.Title))
            {
                dBContext.Musics.Remove(entity);
            }
        }
        await dBContext.SaveChangesAsync();
        foreach (MusicEntity entity in musicEntities)
        {
            if (!databaseMusics.Any(x => x.Title == entity.Title))
            {
               await dBContext.Musics.AddAsync(entity);
            }
        }
        await dBContext.SaveChangesAsync();
    }

    public async Task<List<MusicModel>> GetMusicsByPlaylistIdAsync(uint playlistId)
    {
        return await dBContext.MusicPlaylistConnections.AsNoTracking().Include(x => x.Music).Where(x => x.PlayListId == playlistId).Select(x => new MusicModel(x.Music)).ToListAsync();
    }

    public async Task<List<uint>> GetMusicIdsByPlaylistIdAsync(uint playlistId)
    {
        return await dBContext.MusicPlaylistConnections.AsNoTracking().Where(x => x.PlayListId == playlistId).Select(x => x.MusicId).ToListAsync();
    }

    public async Task<List<MusicModel>> GetAllMusicAsync()
    {
        await dBContext.Database.EnsureCreatedAsync();
        return await dBContext.Musics.AsNoTracking().Select(x =>new MusicModel(x)).ToListAsync();
    }

    public async Task RemoveAllAsync()
    {
        dBContext.Musics.RemoveRange((await dBContext.Musics.ToListAsync()));
        await dBContext.SaveChangesAsync();
    }
}
