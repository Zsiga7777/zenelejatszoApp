namespace MusicApp.Configurations;

public static class ConfigureDI
{
    public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MusicListViewModel>();
        builder.Services.AddTransient<SettingViewModel>();
        builder.Services.AddTransient<PlaylistViewModel>();
        builder.Services.AddTransient<PlaylistAddPopupViewModel>();
        builder.Services.AddTransient<PlaylistMusicListViewModel>();
        builder.Services.AddTransient<ThemePickerPopupViewModel>();
        builder.Services.AddTransient<AppShellViewModel>();

        builder.Services.AddTransient<MusicListView>();
        builder.Services.AddTransient<SettingView>();
        builder.Services.AddTransient<PlayListsView>();
        builder.Services.AddTransient<PlaylistAddPopup>();
        builder.Services.AddTransient<PlaylistMusicListView>();
        builder.Services.AddTransient<ThemePickerPopup>();
        builder.Services.AddTransient<AppShell>();

#if ANDROID
            builder.Services.AddTransient<IFileService, Platforms.Android.Service.AndroidFileService>();
#else
            builder.Services.AddTransient<IFileService, FileService>();
#endif

        builder.Services.AddTransient<IMusicService, MusicService>();
        builder.Services.AddTransient<IPlaylistService, PlaylistService>();
        builder.Services.AddTransient<IMusicPlaylistConnectionService, MusicPlaylistConnectionService>();
        return builder; 
    }
}
