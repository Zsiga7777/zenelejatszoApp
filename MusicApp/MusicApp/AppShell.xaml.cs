namespace MusicApp
{
    public partial class AppShell : Shell
    {
        public AppShellViewModel viewModel => BindingContext as AppShellViewModel;
        public AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        
    }
}
