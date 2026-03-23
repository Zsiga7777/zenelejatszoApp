namespace MusicApp.Views;

public partial class PlayListsView : ContentPage
{
    public PlaylistViewModel ViewModel => BindingContext as PlaylistViewModel;

    public static string Name => nameof(PlayListsView);
    public PlayListsView(PlaylistViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}