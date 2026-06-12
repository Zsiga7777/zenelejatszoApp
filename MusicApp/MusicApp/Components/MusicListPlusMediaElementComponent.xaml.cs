namespace MusicApp.Components;

public partial class MusicListPlusMediaElementComponent : ContentView
{
    #region Properties
    public static readonly BindableProperty MusicsProperty = BindableProperty.Create(
       propertyName: nameof(Musics),
       returnType: typeof(ObservableCollection<MusicModel>),
       declaringType: typeof(MusicListPlusMediaElementComponent),
       defaultValue: null,
       defaultBindingMode: BindingMode.OneWay
       );

    public ObservableCollection<MusicModel> Musics
    {
        get => (ObservableCollection<MusicModel>)GetValue(MusicsProperty);
        set => SetValue(MusicsProperty, value);
    }

    public static readonly BindableProperty IsMediaPlayerVisibleProperty = BindableProperty.Create(
       propertyName: nameof(IsMediaPlayerVisible),
       returnType: typeof(bool),
       declaringType: typeof(MusicListPlusMediaElementComponent),
       defaultValue: null,
       defaultBindingMode: BindingMode.OneWay
       );
    public bool IsMediaPlayerVisible
    {
        get => (bool)GetValue(IsMediaPlayerVisibleProperty);
        set => SetValue(IsMediaPlayerVisibleProperty, value);
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

    public static readonly BindableProperty SelectedMusicProperty = BindableProperty.Create(
       propertyName: nameof(SelectedMusic),
       returnType: typeof(MusicModel),
       declaringType: typeof(MusicListPlusMediaElementComponent),
       defaultValue: null,
       defaultBindingMode: BindingMode.OneWay
       );

    public MusicModel SelectedMusic
    {
        get => (MusicModel)GetValue(SelectedMusicProperty);
        set => SetValue(SelectedMusicProperty, value);
    }

    private List<string> _playedMusics = new List<string>();

    private List<MusicModel> _nextSongs = new List<MusicModel>();
    #endregion

    #region Commands
    public IRelayCommand OnMusicTappedCommand => new RelayCommand(OnMusicTapped);
    public IRelayCommand OnMusicEndedCommand => new RelayCommand(NextMusic);
    public IRelayCommand OnPreviousTrackTappedCommand => new RelayCommand(PreviousMusic);
    public IRelayCommand OnNextTrackTappedCommand => new RelayCommand(NextMusic);

    public IRelayCommand OnMusicAddToQueueCommand => new RelayCommand<string>((path) => OnMusicAddToQueue(path));

    private void OnMusicTapped()
    {
        if (SelectedMusic?.Title != null)
        {
            SaveToPlayedSongs(SelectedMusic.Title);
        }
        IsMediaPlayerVisible = true;
        if (!_nextSongs.Contains(SelectedMusic))
        {
            _nextSongs.Insert(0, SelectedMusic);
        }
        PlayButtonVisible = false;
        PauseButtonVisible = true;
    }

    private void SaveToPlayedSongs(string title)
    {
        if (_playedMusics.Contains(title))
        {
            _playedMusics.Remove(title);
        }
        _playedMusics.Insert(0, SelectedMusic.Title);
    }

    private void NextMusic()
    {
        if (_nextSongs.Count == 1)
        {
            _nextSongs.RemoveAt(0);
            _nextSongs.Add(GenerateRandomMusic());
        }
        else if (_nextSongs.Count > 1)
        {
            _nextSongs.RemoveAt(0);
        }
        else _nextSongs.Add(GenerateRandomMusic());
        SaveToPlayedSongs(SelectedMusic.Title);
        SelectedMusic = _nextSongs[0]; 
        PlayButtonVisible = false;
        PauseButtonVisible = true;
        Preferences.Set("CurrentSongTitle", SelectedMusic?.Title);
    }


    private void PreviousMusic()
    {
        if (_playedMusics.Count == 0)
        {
            _nextSongs.Insert(0, GenerateRandomMusic());
            SelectedMusic = _nextSongs[0];
        }
        else if (_playedMusics[0] == SelectedMusic.Title)
        {
            _playedMusics.Remove(SelectedMusic.Title);
            SelectedMusic = _playedMusics.Count == 0 ? GenerateRandomMusic() :  Musics.First(x => x.Title == _playedMusics[0]);
        }
        else
        {
            SelectedMusic =Musics.First(x => x.Title == _playedMusics[0]);
            _playedMusics.Remove(SelectedMusic.Title);
        }
        PlayButtonVisible = false;
        PauseButtonVisible = true;
        Preferences.Set("CurrentSongTitle", SelectedMusic?.Title);
    }
    private MusicModel GenerateRandomMusic()
    {
        Random random = new Random();
        MusicModel music;
        do
        {
            music = Musics[random.Next(0, Musics.Count)];
        } while (music.Title == SelectedMusic.Title);

        return music;
    }

    private void OnMusicAddToQueue(string musicPath)
    {
        _nextSongs.Add(Musics.First(x => x.Path.ToString() == musicPath));
    }

    private void LoadLastMusic()
    {
        string currentSongTitle = Preferences.Get("CurrentSongTitle", "");
        if (currentSongTitle != "" && Musics.Count > 0)
        {
            IsMediaPlayerVisible = true;
            SelectedMusic = Musics.FirstOrDefault(x => x.Title == currentSongTitle);
            if (!_nextSongs.Contains(SelectedMusic))
            {
                _nextSongs.Add(SelectedMusic);
            }
        }
    }

    #endregion

    private void UnloadedEvent(object sender, EventArgs e)
    {
        Musics.Clear();
    }

    private void LoadedEvent(object sender, EventArgs e)
    {
        LoadLastMusic();
    }
    public MusicListPlusMediaElementComponent()
	{
		InitializeComponent();
	}
}