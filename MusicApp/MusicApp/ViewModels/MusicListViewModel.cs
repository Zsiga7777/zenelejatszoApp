namespace MusicApp.ViewModels;

public partial class MusicListViewModel(IFileService fileService, IMusicService musicService) : MusicModel
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    [ObservableProperty]
    public ObservableCollection<MusicModel> musics = new ObservableCollection<MusicModel>();

    private async Task OnAppearingAsync()
    {
        await GetPermissionsAsync();
        SetAppTheme();
#if ANDROID
        List<MusicModel> musics = fileService.ReadAllMusicFromDirectories(new List<string>());
        await musicService.UpdateMusicsAsync(musics);
        Musics = musics.ToObservableCollection();
#else
        List<string> directories = fileService.ReadAllFilePathFromPreference();
        if (directories == null || directories.Count == 0) 
        {
            await Shell.Current.DisplayAlertAsync("Betöltési hiba", "A beállításokban adjon meg egy mappát", "ok"); 
        }
        else
        {
            List<MusicModel> musics = fileService.ReadAllMusicFromDirectories(directories);
           await musicService.UpdateMusicsAsync(musics);
               Musics = musics.ToObservableCollection();
        }
#endif
    }

private async Task OnDisappearingAsync()
    {
        Musics.Clear();
    }

    private void SetAppTheme()
    {
        string appTheme = Preferences.Default.Get("apptheme", "");
        if (appTheme == "dark")
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else if (appTheme == "light")
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    private async Task GetPermissionsAsync()
    {
        var res =await Permissions.RequestAsync<Permissions.Media>();

        PermissionStatus storageReadstatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        PermissionStatus storageWritestatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        PermissionStatus mediaStatus = await Permissions.CheckStatusAsync<Permissions.Media>();

        if(storageReadstatus != PermissionStatus.Granted)
        {
            await Permissions.RequestAsync<Permissions.StorageRead>();
        }
        if (storageWritestatus != PermissionStatus.Granted)
        {
            await Permissions.RequestAsync<Permissions.StorageWrite>();
        }
        if (mediaStatus != PermissionStatus.Granted || mediaStatus == PermissionStatus.Unknown)
        {
            await Permissions.RequestAsync<Permissions.Media>();
        }
    }

}
