namespace MusicApp.Core.Interfaces;

public interface IPlaylistService
{
    Task DeletePlaylistAsync(uint playlistId);
    Task<List<PlaylistModel>> GetAllAsync();
    Task<PlaylistEntity> GetByIdAsync(uint id);
    Task<PlaylistModel> GetByIdNotrackingAsync(uint id);
    Task<uint> SavePlaylistAsync(PlaylistModel playList);
    Task UpdatePlaylistAsync(PlaylistEntity playlist);
}
