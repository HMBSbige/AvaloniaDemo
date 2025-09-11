using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System.Globalization;

namespace AvaloniaDemo.ValueConverters;

public class ToBitmapConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		try
		{
			if (value is byte[] { Length: > 0 } bytes)
			{
				using MemoryStream ms = new(bytes);
				return new Bitmap(ms);
			}

			return null;
		}
		catch
		{
			return null;
		}
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
