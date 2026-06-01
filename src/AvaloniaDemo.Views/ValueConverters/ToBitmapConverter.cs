using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System.Globalization;

namespace AvaloniaDemo.Views.ValueConverters;

public class ToBitmapConverter : IValueConverter
{
	private const int IconWidth = 64;

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		try
		{
			if (value is byte[] { Length: > 0 } bytes)
			{
				using MemoryStream ms = new(bytes);
				return Bitmap.DecodeToWidth(ms, IconWidth);
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
