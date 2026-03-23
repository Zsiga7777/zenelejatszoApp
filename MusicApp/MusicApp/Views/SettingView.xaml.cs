namespace MusicApp.Views;

public partial class SettingView : ContentPage
{
    public SettingViewModel ViewModel => BindingContext as SettingViewModel;

    public static string Name => nameof(SettingView);
  
    public SettingView(SettingViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}