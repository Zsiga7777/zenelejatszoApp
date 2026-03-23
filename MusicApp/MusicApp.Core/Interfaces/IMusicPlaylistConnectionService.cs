namespace MusicApp.Core.Interfaces;

public interface IMusicPlaylistConnectionService
{
    Task DeleteMusicPlaylistConnectionsAsync(MusicPlaylistConnectionModel model);
    Task SaveMusicPlaylistConnectionsAsync(List<MusicPlaylistConnectionModel> models);
    Task UpdateMusicPlaylistConnectionsAsync(uint playlistId, List<uint> musicIds);
}
