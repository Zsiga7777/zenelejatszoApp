namespace MusicApp.ViewModels;

[ObservableObject]
public partial class ThemePickerPopupViewModel
{
    public IAsyncRelayCommand OnSelectionChangeCommand => new AsyncRelayCommand(OnSelectionChangeAsync);

    [ObservableProperty]
    private ObservableCollection<string> elements = new ObservableCollection<string> { "rendszer", "sötét", "világos" };

    [ObservableProperty]
    private string selectedAppTheme = "";

    private async Task OnSelectionChangeAsync()
    {
        ChangeAppThemeToSelected();
        await Task.Yield();
        await Shell.Current.ClosePopupAsync();
    }

    private void ChangeAppThemeToSelected()
    {
        if (SelectedAppTheme == "rendszer")
        {
            Application.Current.UserAppTheme = AppTheme.Unspecified;
            Preferences.Default.Remove("apptheme");
        }
        else if (SelectedAppTheme == "sötét")
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            Preferences.Default.Set("apptheme", "dark");
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            Preferences.Default.Set("apptheme", "light");
        }
    }
}
