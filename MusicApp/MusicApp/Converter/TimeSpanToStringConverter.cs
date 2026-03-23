namespace MusicApp.Converter;

public class TimeSpanToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is TimeSpan ts ? $"{Math.Floor((double)ts.TotalSeconds/(double)60)}:{Math.Floor(ts.TotalSeconds - Math.Floor((double)ts.TotalSeconds / (double)60) * 60).ToString().PadLeft(2,'0')}" : 0.0; ;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
