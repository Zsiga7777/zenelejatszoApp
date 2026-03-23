namespace MusicApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            ConfigureShellNavigation();
        }
        private static void ConfigureShellNavigation()
        {
            Routing.RegisterRoute(MusicListView.Name, typeof(MusicListView));
            Routing.RegisterRoute(SettingView.Name, typeof(SettingView));
            Routing.RegisterRoute(PlayListsView.Name, typeof(PlayListsView));
            Routing.RegisterRoute(PlaylistMusicListView.Name, typeof(PlaylistMusicListView));
        }
    }
}
