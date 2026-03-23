namespace MusicApp.ViewModels;

[ObservableObject]
public partial class SettingViewModel(IFileService fileService, IServiceProvider serviceProvider)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);

    public IRelayCommand DeleteFolderFromListCommand => new RelayCommand<string>((folderName) => OnDeleteFolderFromList(folderName));

    public IRelayCommand DeleteSaveFolderCommand => new RelayCommand(OnDeleteSaveFolder);

    public IAsyncRelayCommand AddFolderToListCommand => new AsyncRelayCommand(OnAddFolderToListAsync);

    public IAsyncRelayCommand AddSaveFolderCommand => new AsyncRelayCommand(OnAddSaveFolderAsync);

    public IAsyncRelayCommand AppThemePickerTappedCommand => new AsyncRelayCommand(AppThemePickerTappedAsync);


    [ObservableProperty]
    public ObservableCollection<FolderModel> folders = new ObservableCollection<FolderModel>();

    [ObservableProperty]
    private FolderModel saveFolder;

    [ObservableProperty]
    private bool isAddFolderButtonVisible = false;

    [ObservableProperty]
    private bool isSaveFolderVisible = false;

    [ObservableProperty]
    private string selectedAppTheme;

    private async Task OnAppearingAsync()
    {
        SetSelectedAppTheme();

        if (Folders.Count == 0)
        {
            LoadFolders();
            LoadSaveFolder();
        }
    }

    private void OnDeleteFolderFromList(string folderName)
    {
        Folders.Remove(Folders.First(x => x.FolderName == folderName));
        List<string> folderPaths = Folders.Select(x => x.FolderPath).ToList();
        fileService.WriteAllFilePathtoPreference(folderPaths);
    }

    private void OnDeleteSaveFolder()
    {
        SaveFolder = null;
        Preferences.Remove("outputDirectory");
        IsAddFolderButtonVisible = true;
        IsSaveFolderVisible = false;
    }
    private async Task OnAddFolderToListAsync()
    {
        var result = await FolderPicker.Default.PickAsync();
        if (result.IsSuccessful)
        {
            Folders.Add(new FolderModel(result.Folder.Name, result.Folder.Path));
            List<string> folderPaths = Folders.Select(x => x.FolderPath).ToList();
            fileService.WriteAllFilePathtoPreference(folderPaths);
        }
    }

    private async Task OnAddSaveFolderAsync()
    {
        var result = await FolderPicker.Default.PickAsync();
        if (result.IsSuccessful)
        {
            SaveFolder = new FolderModel(result.Folder.Name, result.Folder.Path);
            Preferences.Set("outputDirectory", result.Folder.Path);
            IsAddFolderButtonVisible = false;
            IsSaveFolderVisible = true;
        }

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

    private void LoadSaveFolder()
    {
        string outputFolderTemp = Preferences.Get("outputDirectory", "");
        if (outputFolderTemp != "")
        {
            SaveFolder = new FolderModel(outputFolderTemp.Split("\\").Last().ToString(), outputFolderTemp);
            IsSaveFolderVisible = true;
        }
        else
        {
            IsAddFolderButtonVisible = true;
            IsSaveFolderVisible = false;
        }
    }
}
