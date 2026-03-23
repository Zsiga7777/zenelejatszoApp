namespace MusicApp.Core.Models;

public class MusicPlaylistConnectionModel
{
    public uint MusicId { get; set; }
    public uint PlaylistId { get; set; }

    public MusicPlaylistConnectionModel(uint musicId, uint playlistId)
    {
        MusicId = musicId;
        PlaylistId = playlistId;
    }
}
