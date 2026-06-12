namespace MusicApp.ViewModels;

[ObservableObject]
public partial class SettingViewModel(IFileService fileService, IMusicService musicService, IServiceProvider serviceProvider)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);

    public IAsyncRelayCommand DeleteFolderFromListCommand => new AsyncRelayCommand<string>((folderName) => OnDeleteFolderFromListAsync(folderName));

    public IAsyncRelayCommand AddFolderToListCommand => new AsyncRelayCommand(OnAddFolderToListAsync);

    public IAsyncRelayCommand AppThemePickerTappedCommand => new AsyncRelayCommand(AppThemePickerTappedAsync);


    [ObservableProperty]
    public ObservableCollection<FolderModel> folders = new ObservableCollection<FolderModel>();

    [ObservableProperty]
    private bool isAddFolderButtonVisible = false;

    [ObservableProperty]
    private string selectedAppTheme;

    private async Task OnAppearingAsync()
    {
        SetSelectedAppTheme();

        if (Folders.Count == 0)
        {
            LoadFolders();
        }
    }

    private async Task OnDeleteFolderFromListAsync(string folderName)
    {
        Folders.Remove(Folders.First(x => x.FolderName == folderName));
        List<string> folderPaths = Folders.Select(x => x.FolderPath).ToList();
        fileService.WriteAllFilePathtoPreference(folderPaths);
        await DeleteSongsInDeletedFolderAsync();
    }

    private async Task DeleteSongsInDeletedFolderAsync()
    {

List<string> directories = fileService.ReadAllFilePathFromPreference();
        if (directories == null || directories.Count == 0) 
        {
            await musicService.RemoveAllAsync(); 
        }
        else
        {
            List<MusicModel> musics = fileService.ReadAllMusicFromDirectories(directories);
           await musicService.UpdateMusicsAsync(musics);
        }
    }

    private async Task OnAddFolderToListAsync()
    {
        var result = await FolderPicker.Default.PickAsync();
        if (result.IsSuccessful)
        {
            Folders.Add(new FolderModel(result.Folder.Name, result.Folder.Path));
            List<string> folderPaths = Folders.Select(x => x.FolderPath).ToList();
            fileService.WriteAllFilePathtoPreference(folderPaths);
            await AddSongsToDbAsync();
        }
    }

    private async Task AddSongsToDbAsync()
    {
        List<string> directories = fileService.ReadAllFilePathFromPreference();
            List<MusicModel> musics = fileService.ReadAllMusicFromDirectories(directories);
            await musicService.UpdateMusicsAsync(musics);
    }

    private async Task AppThemePickerTappedAsync()
    {
        await Shell.Current.ShowPopupAsync<string>(serviceProvider.GetRequiredService<ThemePickerPopup>());
    }

    private void SetSelectedAppTheme()
    {
        string apptheme = Preferences.Default.Get("apptheme", "");
        if (apptheme == "dark")
        {
            SelectedAppTheme = "sötét";
        }
        else if (apptheme == "light")
        {
            SelectedAppTheme = "világos";
        }
        else
        {
            SelectedAppTheme = "rendszer";
        }
    }

    private void LoadFolders()
    {
        List<string> temp = fileService.ReadAllFilePathFromPreference();
        if (temp.Count > 0)
        {
            foreach (var item in temp)
            {
                Folders.Add(new FolderModel(item.Split("\\").Last().ToString(), item));
            }
        }
    }
}
