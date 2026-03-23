namespace MusicApp.Services.Services;

public class PlaylistService(AppDBContext dBContext) : IPlaylistService
{
    public async Task<uint> SavePlaylistAsync(PlaylistModel playList)
    {
        await dBContext.Playlists.AddAsync(playList.ModelToEntity());
        await dBContext.SaveChangesAsync();
        return (await dBContext.Playlists.AsNoTracking().FirstAsync(x => x.Title == playList.Title)).Id;
    }
    public async Task DeletePlaylistAsync(uint playlistId)
    {
        dBContext.Playlists.Remove(await dBContext.Playlists.FirstAsync(x => x.Id == playlistId));
        await dBContext.SaveChangesAsync();
    }

    public async Task UpdatePlaylistAsync(PlaylistEntity playlist)
    {
        dBContext.Playlists.Update(playlist);
        await dBContext.SaveChangesAsync();
    }

    public async Task<List<PlaylistModel>> GetAllAsync()
    {
        return await dBContext.Playlists.AsNoTracking().Select(x => new PlaylistModel(x)).ToListAsync();
    }
    public async Task<PlaylistModel> GetByIdNotrackingAsync(uint id) 
    {
        return new PlaylistModel( await dBContext.Playlists.AsNoTracking().FirstAsync(x =>x.Id == id));
    }
    public async Task<PlaylistEntity> GetByIdAsync(uint id)
    {
        return await dBContext.Playlists.FirstAsync(x => x.Id == id);
    }
}
