namespace MusicApp.Components;

public partial class MediaElementComponent : ContentView
{
    #region Properties
    public static readonly BindableProperty SelectedMusicProperty = BindableProperty.Create(
        propertyName: nameof(SelectedMusic),
        returnType: typeof(MusicModel),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay
        );

    public MusicModel SelectedMusic
    {
        get => (MusicModel)GetValue(SelectedMusicProperty);
        set => SetValue(SelectedMusicProperty, value);
    }

    public static readonly BindableProperty IsMediaPlayerVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsMediaPlayerVisible),
        returnType: typeof(bool),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay
        );

    public bool IsMediaPlayerVisible
    {
        get => (bool)GetValue(IsMediaPlayerVisibleProperty);
        set => SetValue(IsMediaPlayerVisibleProperty, value);
    }

    public static readonly BindableProperty CurrentPlayerStandingProperty = BindableProperty.Create(
        propertyName: nameof(CurrentPlayerStanding),
        returnType: typeof(TimeSpan),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    public TimeSpan CurrentPlayerStanding
    {
        get => (TimeSpan)GetValue(CurrentPlayerStandingProperty);
        set => SetValue(CurrentPlayerStandingProperty, value);
    }

    public static readonly BindableProperty PlayButtonVisibleProperty = BindableProperty.Create(
        propertyName: nameof(PlayButtonVisible),
        returnType: typeof(bool),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    public bool PlayButtonVisible
    {
        get => (bool)GetValue(PlayButtonVisibleProperty);
        set => SetValue(PlayButtonVisibleProperty, value);
    }

    public static readonly BindableProperty PauseButtonVisibleProperty = BindableProperty.Create(
        propertyName: nameof(PauseButtonVisible),
        returnType: typeof(bool),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    public bool PauseButtonVisible
    {
        get => (bool)GetValue(PauseButtonVisibleProperty);
        set => SetValue(PauseButtonVisibleProperty, value);
    }

    public static readonly BindableProperty MusicDurationProperty = BindableProperty.Create(
        propertyName: nameof(MusicDuration),
        returnType: typeof(TimeSpan),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay
        );

    public TimeSpan MusicDuration
    {
        get => (TimeSpan)GetValue(MusicDurationProperty);
        set => SetValue(MusicDurationProperty, value);
    }

    #endregion

    #region Commands

    public static readonly BindableProperty OnMusicEndedCommandProperty = BindableProperty.Create(
        propertyName: nameof(OnMusicEndedCommand),
        returnType: typeof(IRelayCommand),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IRelayCommand OnMusicEndedCommand
    {
        get => (IRelayCommand)GetValue(OnMusicEndedCommandProperty);
        set => SetValue(OnMusicEndedCommandProperty, value);
    }

    public static readonly BindableProperty OnPreviousTrackTappedCommandProperty = BindableProperty.Create(
        propertyName: nameof(OnPreviousTrackTappedCommand),
        returnType: typeof(IRelayCommand),
        declaringType: typeof(MediaElementComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IRelayCommand OnPreviousTrackTappedCommand
    {
        get => (IRelayCommand)GetValue(OnPreviousTrackTappedCommandProperty);
        set => SetValue(OnPreviousTrackTappedCommandProperty, value);
    }

    public static readonly BindableProperty OnNextTrackTappedCommandProperty = BindableProperty.Create(
       propertyName: nameof(OnNextTrackTappedCommand),
       returnType: typeof(IRelayCommand),
       declaringType: typeof(MediaElementComponent),
       defaultValue: null,
       defaultBindingMode: BindingMode.OneWay);

    public IRelayCommand OnNextTrackTappedCommand
    {
        get => (IRelayCommand)GetValue(OnNextTrackTappedCommandProperty);
        set => SetValue(OnNextTrackTappedCommandProperty, value);
    }

    public IRelayCommand OnPlayPausedTappedCommand => new RelayCommand(OnPlayPausedTapped);

    public IRelayCommand OnMusicProgressingCommand => new RelayCommand<TimeSpan>((currentStanding) => OnMusicProgress(currentStanding));
    private void OnPlayPausedTapped()
    {
        if (PlayButtonVisible)
        {
            mediaElement.Play();
            PlayButtonVisible = false;
            PauseButtonVisible = true;
        }

        else
        {
            mediaElement.Pause();
            PlayButtonVisible = true;
            PauseButtonVisible = false;
        }

    }
    private void OnMusicProgress(TimeSpan currentStanding)
    {
        CurrentPlayerStanding = currentStanding;
    }

    #endregion

    public MediaElementComponent()
	{
        InitializeComponent();
        audioProgressSlider.DragCompleted += (sender, args) =>
        {
            this.mediaElement.SeekTo(CurrentPlayerStanding);
        };
    }

    private void UnloadedEvent(object sender, EventArgs e)
    {
        mediaElement.Stop();
        mediaElement.Source = null;
        mediaElement.DisconnectHandlers();
        mediaElement.Dispose();
        
        if (BindingContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    private void LoadedEvent(object sender, EventArgs e)
    {
        try
        {
            if (mediaElement != null)
            {
                if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.MediaElementState.None)
                {
                    PauseButtonVisible = true;
                    PlayButtonVisible = false;
                    MusicDuration = mediaElement.Duration;
                }
                else
                {
                    PauseButtonVisible = false;
                    PlayButtonVisible = true;
                }

            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message); }
    }

    private void SetMusicDuration(object sender, CommunityToolkit.Maui.Core.MediaStateChangedEventArgs e)
    {
        MusicDuration = mediaElement.Duration;
    }
}