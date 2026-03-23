namespace MusicApp.ViewModels;

[ObservableObject]
public partial class PlaylistViewModel(IMusicService musicService, IPlaylistService playlistService, IMusicPlaylistConnectionService musicPlaylistConnectionService)
{
    public IAsyncRelayCommand AddPlaylistCommand => new AsyncRelayCommand(AddPlaylistAsync);
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand UpdatePlaylistCommad => new AsyncRelayCommand<uint>((playlistId) => UpdatePlaylistAsync(playlistId));
    public IAsyncRelayCommand DeletePlaylistCommad => new AsyncRelayCommand<uint>((playlistId) => DeletePlaylistAsync(playlistId));
    public IAsyncRelayCommand OnPlaylistTappedCommand => new AsyncRelayCommand<PlaylistModel>((playlist) => OnPlaylistTappedAsync(playlist));

    [ObservableProperty]
    public ObservableCollection<PlaylistModel> playlists = new ObservableCollection<PlaylistModel>();
    private async Task OnAppearingAsync()
    {
        Playlists = new ObservableCollection<PlaylistModel>();
        List<PlaylistModel> temp = await playlistService.GetAllAsync();

        temp.ForEach(playlist => { Playlists.Add(playlist); }); 
    }

    private async Task AddPlaylistAsync()
    {
        var viewmodel = new PlaylistAddPopupViewModel(musicService,playlistService, musicPlaylistConnectionService );
        var popup = new PlaylistAddPopup(viewmodel);
        await Shell.Current.ShowPopupAsync(popup);
    }

    private async Task UpdatePlaylistAsync(uint playlistId)
    {
        var viewmodel = new PlaylistAddPopupViewModel(musicService, playlistService, musicPlaylistConnectionService, playlistId);
        var popup = new PlaylistAddPopup(viewmodel);
       
        await Shell.Current.ShowPopupAsync(popup);
    }

    private async Task DeletePlaylistAsync(uint playlistId)
    {
        await playlistService.DeletePlaylistAsync(playlistId);
        Playlists.Remove(Playlists.First(x => x.Id == playlistId));
    }

    private async Task OnPlaylistTappedAsync(PlaylistModel playlist)
    {
        ShellNavigationQueryParameters NavigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Playlist" , playlist}
        };
        await Shell.Current.GoToAsync(PlaylistMusicListView.Name, NavigationQueryParameter);
    }
}
