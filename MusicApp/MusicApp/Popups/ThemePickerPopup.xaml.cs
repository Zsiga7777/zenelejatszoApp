namespace MusicApp.Popups;

public partial class ThemePickerPopup : Popup
{
    public ThemePickerPopupViewModel ViewModel => this.BindingContext as ThemePickerPopupViewModel;
    public ThemePickerPopup(ThemePickerPopupViewModel viewModel)
	{
        this.BindingContext = viewModel;
        InitializeComponent();
	}
}