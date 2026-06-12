namespace MusicApp.ViewModels;

public partial class MusicListViewModel(IMusicService musicService) : ObservableObject
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    [ObservableProperty]
    private ObservableCollection<MusicModel> musics = new ObservableCollection<MusicModel>();

    [ObservableProperty]
    private string errorMessage = "";

    [ObservableProperty]
    private bool isErrorVisible = false;

    private async Task OnAppearingAsync()
    {
        Musics = (await musicService.GetAllMusicAsync()).ToObservableCollection();
        if(Musics.Count == 0)
        {
            IsErrorVisible = true;
            if(Preferences.Get("ListOfFolders", "").Length == 0)
            {
                ErrorMessage = "Nincs kijelölt mappa. Jelöljön ki egy mappát a beállításokban.";
            }
            else
            {
                ErrorMessage = "A kijelölt mappa nem tartalmaz zenéket.";
            }
        }
    }

private async Task OnDisappearingAsync()
    {
        Musics.Clear();
    }
}
