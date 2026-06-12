namespace MusicApp.ViewModels;

public class AppShellViewModel(IFileService fileService, IMusicService musicService)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);

private async Task OnAppearingAsync()
    {
        ConfigureShellNavigation();
        await GetPermissionsAsync();
        SetAppTheme();
        await CheckAndUpdateMusics();
    }

    private async Task GetPermissionsAsync()
    {
        var res = await Permissions.RequestAsync<Permissions.Media>();

        PermissionStatus storageReadstatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        PermissionStatus storageWritestatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        PermissionStatus mediaStatus = await Permissions.CheckStatusAsync<Permissions.Media>();

        if (storageReadstatus != PermissionStatus.Granted)
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

    private async Task CheckAndUpdateMusics()
    {
#if ANDROID
        List<MusicModel> musics = fileService.ReadAllMusicFromDirectories(new List<string>());
        await musicService.UpdateMusicsAsync(musics);
#else
        List<string> directories = fileService.ReadAllFilePathFromPreference();
        if (directories != null && directories.Count != 0) 
        {
        List<MusicModel> musics = fileService.ReadAllMusicFromDirectories(directories);
           await musicService.UpdateMusicsAsync(musics);
        }

#endif
    }

    private static void ConfigureShellNavigation()
    {
        Routing.RegisterRoute(MusicListView.Name, typeof(MusicListView));
        Routing.RegisterRoute(SettingView.Name, typeof(SettingView));
        Routing.RegisterRoute(PlayListsView.Name, typeof(PlayListsView));
        Routing.RegisterRoute(PlaylistMusicListView.Name, typeof(PlaylistMusicListView));
    }
}
