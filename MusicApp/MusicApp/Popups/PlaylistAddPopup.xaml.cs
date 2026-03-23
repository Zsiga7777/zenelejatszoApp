namespace MusicApp.Popups;

public partial class PlaylistAddPopup : Popup
{
    public PlaylistAddPopupViewModel ViewModel => BindingContext as PlaylistAddPopupViewModel;

    public static string Name => nameof(PlaylistAddPopup);
    public PlaylistAddPopup(PlaylistAddPopupViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}