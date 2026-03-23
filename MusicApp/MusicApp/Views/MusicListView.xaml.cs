namespace MusicApp.Views;

public partial class MusicListView : ContentPage
{
    public MusicListViewModel ViewModel => BindingContext as MusicListViewModel;
    
    public static string Name => nameof(MusicListView);
    public MusicListView(MusicListViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
    }
}