namespace MusicApp.Services.Services;

public class MusicPlaylistConnectionService(AppDBContext dBContext) :IMusicPlaylistConnectionService
{
    public async Task SaveMusicPlaylistConnectionsAsync(List<MusicPlaylistConnectionModel> models)
    {
        await dBContext.MusicPlaylistConnections.AddRangeAsync(models.Select(x => new MusicPlaylistConnectionEntity { PlayListId = x.PlaylistId, MusicId = x.MusicId }).ToList());
        await dBContext.SaveChangesAsync();
            }
    public async Task DeleteMusicPlaylistConnectionsAsync(MusicPlaylistConnectionModel model)
    {
        dBContext.MusicPlaylistConnections.Remove(new MusicPlaylistConnectionEntity { PlayListId = model.PlaylistId, MusicId = model.MusicId });
        await dBContext.SaveChangesAsync();
    }

    public async Task UpdateMusicPlaylistConnectionsAsync(uint playlistId, List<uint> musicIds)
    {
        List<MusicPlaylistConnectionEntity> connections = await dBContext.MusicPlaylistConnections.Where(x => x.PlayListId == playlistId).ToListAsync();
        foreach (MusicPlaylistConnectionEntity entity in connections)
        {
            if (!musicIds.Any(x => x == entity.MusicId))
            {
                dBContext.MusicPlaylistConnections.Remove(entity);
            }
        }
        foreach (uint id in musicIds)
        {
            if (!connections.Any(x => x.MusicId == id))
            {
                await dBContext.MusicPlaylistConnections.AddAsync(new MusicPlaylistConnectionEntity { MusicId = id, PlayListId = playlistId});
            }
        }
        await dBContext.SaveChangesAsync();
    }
}