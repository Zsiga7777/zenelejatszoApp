namespace MusicApp.Components;

public partial class MusicListItemComponent : ContentView
{
    public static readonly BindableProperty MusicProperty = BindableProperty.Create(
       propertyName: nameof(Music),
       returnType: typeof(MusicModel),
       declaringType: typeof(MusicListItemComponent),
       defaultValue: null,
       defaultBindingMode: BindingMode.OneWay
       );

    public MusicModel Music
    {
        get => (MusicModel)GetValue(MusicProperty);
        set => SetValue(MusicProperty, value);
    }

    public static readonly BindableProperty AddToQueueCommandProperty = BindableProperty.Create(
        propertyName: nameof(AddToQueueCommand),
        returnType: typeof(IRelayCommand),
        declaringType: typeof(MusicListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IRelayCommand AddToQueueCommand
    {
        get => (IRelayCommand)GetValue(AddToQueueCommandProperty);
        set => SetValue(AddToQueueCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(string),
        declaringType: typeof(MusicListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public MusicListItemComponent()
	{
		InitializeComponent();
	}
}