namespace MusicApp.ViewModels;

public partial class PlaylistMusicListViewModel(IMusicService musicService) : MusicModel, IQueryAttributable
{
    public IAsyncRelayCommand BackCommand => new AsyncRelayCommand(BackToPlaylistsAsync);

    [ObservableProperty]
    public ObservableCollection<MusicModel> musics = new ObservableCollection<MusicModel>();

    [ObservableProperty]
    public string playlistTitle;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Playlist", out object result);
        PlaylistModel playlist = result as PlaylistModel;
        Musics = (await musicService.GetMusicsByPlaylistIdAsync(playlist.Id)).ToObservableCollection();
        PlaylistTitle = playlist.Title;
    }
    
    private async Task BackToPlaylistsAsync()
    {
        await Shell.Current.GoToAsync(PlayListsView.Name);
    }
}
