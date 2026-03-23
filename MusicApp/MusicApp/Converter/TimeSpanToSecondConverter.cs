namespace MusicApp.Converter;

public class TimeSpanToSecondConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        double temp = value is TimeSpan ts ? Math.Floor(ts.TotalSeconds) : 0.0;
        return temp;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        TimeSpan temp = value is double seconds ? TimeSpan.FromSeconds(Math.Floor(seconds)) : TimeSpan.Zero;
        return temp;
    }
}
