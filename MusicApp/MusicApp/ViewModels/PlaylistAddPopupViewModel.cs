namespace MusicApp.ViewModels;

[ObservableObject]
public partial class PlaylistAddPopupViewModel(IMusicService musicService, IPlaylistService playlistService, IMusicPlaylistConnectionService musicPlaylistConnectionService, uint playlistId = 0)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IRelayCommand OnMusicTappedCommand => new RelayCommand<MusicModel>((music) => OnMusicTapped(music));
    public IAsyncRelayCommand SavePlaylistCommand => new AsyncRelayCommand(SavePlaylistAsync);

    [ObservableProperty]
    public string playlistTitle = "";

    [ObservableProperty]
    public ObservableCollection<MusicModel> musics = new ObservableCollection<MusicModel>();

    private List<uint> selectedMusics = new List<uint>();
    private async Task OnAppearingAsync()
    {
        List<MusicModel> temp = await musicService.GetAllMusicAsync();
        if (playlistId != 0)
        {
            PlaylistTitle = (await playlistService.GetByIdNotrackingAsync(playlistId)).Title;
            selectedMusics = await musicService.GetMusicIdsByPlaylistIdAsync(playlistId);
            selectedMusics.ForEach(id => { temp.First(x => x.Id == id).Bgcolor = Color.FromRgb(200, 200, 200); });
            
        }
        temp.ForEach(music => { Musics.Add(music); });

    }
    private void OnMusicTapped(MusicModel music)
    {
        if(!selectedMusics.Contains(music.Id))
        {
            selectedMusics.Add(music.Id);
            Musics.First(x => x.Id  == music.Id).Bgcolor = Color.FromRgb(168,167,167);
        }
        else
        {
            selectedMusics.Remove(music.Id);
            Musics.First(x => x.Id == music.Id).Bgcolor = Colors.Transparent;
        }
    }

    private async Task SavePlaylistAsync()
    {
        if (playlistId == 0)
        {
            await SaveNewPlaylistAsync();
        }
        else
        {
            await UpdatePlaylistAsync();
        }
        await Shell.Current.ClosePopupAsync();
    }

    private async Task SaveNewPlaylistAsync()
    {
        uint playlistId = await playlistService.SavePlaylistAsync(new PlaylistModel(PlaylistTitle));
        List<MusicPlaylistConnectionModel> musicPlaylistConnections = new List<MusicPlaylistConnectionModel>();
        foreach (uint selecttedId in selectedMusics)
        {
            musicPlaylistConnections.Add(new MusicPlaylistConnectionModel(selecttedId, playlistId));
        }
        await musicPlaylistConnectionService.SaveMusicPlaylistConnectionsAsync(musicPlaylistConnections);
    }

    private async Task UpdatePlaylistAsync()
    {
        await musicPlaylistConnectionService.UpdateMusicPlaylistConnectionsAsync(playlistId, selectedMusics);
        PlaylistEntity playlist = await playlistService.GetByIdAsync(playlistId);
        playlist.Title = PlaylistTitle;
        await playlistService.UpdatePlaylistAsync(playlist);
    }
}
