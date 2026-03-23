namespace MusicApp.Components;

public partial class PlaylistListItemComponent : ContentView
{
    public static readonly BindableProperty PlaylistProperty = BindableProperty.Create(
       propertyName: nameof(Playlist),
       returnType: typeof(PlaylistModel),
       declaringType: typeof(PlaylistListItemComponent),
       defaultValue: null,
       defaultBindingMode: BindingMode.OneWay
       );

    public PlaylistModel Playlist
    {
        get => (PlaylistModel)GetValue(PlaylistProperty);
        set => SetValue(PlaylistProperty, value);
    }

    public static readonly BindableProperty UpdatePlaylistCommadProperty = BindableProperty.Create(
        propertyName: nameof(UpdatePlaylistCommad),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(PlaylistListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand UpdatePlaylistCommad
    {
        get => (IAsyncRelayCommand)GetValue(UpdatePlaylistCommadProperty);
        set => SetValue(UpdatePlaylistCommadProperty, value);
    }

    public static readonly BindableProperty DeletePlaylistCommadProperty = BindableProperty.Create(
        propertyName: nameof(DeletePlaylistCommad),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(PlaylistListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand DeletePlaylistCommad
    {
        get => (IAsyncRelayCommand)GetValue(DeletePlaylistCommadProperty);
        set => SetValue(DeletePlaylistCommadProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(uint),
        declaringType: typeof(PlaylistListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    public uint CommandParameter
    {
        get => (uint)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public PlaylistListItemComponent()
	{
		InitializeComponent();
	}
}