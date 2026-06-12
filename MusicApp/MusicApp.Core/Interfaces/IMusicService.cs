namespace MusicApp.Core.Interfaces;

public interface IMusicService
{
    Task<List<MusicModel>> GetAllMusicAsync();
    Task<List<uint>> GetMusicIdsByPlaylistIdAsync(uint playlistId);
    Task<List<MusicModel>> GetMusicsByPlaylistIdAsync(uint playlistId);
    Task RemoveAllAsync();
    Task UpdateMusicsAsync(List<MusicModel> musics);
}
