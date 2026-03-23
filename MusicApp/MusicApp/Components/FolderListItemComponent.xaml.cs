namespace MusicApp.Components;

public partial class FolderListItemComponent : ContentView
{
    public static readonly BindableProperty FolderProperty = BindableProperty.Create(
        propertyName: nameof(Folder),
        returnType: typeof(FolderModel),
        declaringType: typeof(FolderListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay
        );

    public FolderModel Folder
    {
        get => (FolderModel)GetValue(FolderProperty);
        set => SetValue(FolderProperty, value);
    }

    public static readonly BindableProperty DeleteFolderFromListCommadProperty = BindableProperty.Create(
        propertyName: nameof(DeleteFolderFromListCommad),
        returnType: typeof(IRelayCommand),
        declaringType: typeof(FolderListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IRelayCommand DeleteFolderFromListCommad
    {
        get => (IRelayCommand)GetValue(DeleteFolderFromListCommadProperty);
        set => SetValue(DeleteFolderFromListCommadProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(string),
        declaringType: typeof(FolderListItemComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public FolderListItemComponent()
	{
		InitializeComponent();
	}
}