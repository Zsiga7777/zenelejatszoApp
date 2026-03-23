namespace MusicApp.Views;

public partial class PlaylistMusicListView : ContentPage
{
    public PlaylistMusicListViewModel ViewModel => BindingContext as PlaylistMusicListViewModel;
    
    public static string Name => nameof(PlaylistMusicListView);
    public PlaylistMusicListView(PlaylistMusicListViewModel viewModel)
	{
        BindingContext = viewModel;
      
        InitializeComponent();
    }
}