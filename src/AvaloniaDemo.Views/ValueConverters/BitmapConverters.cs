using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace AvaloniaDemo.Views.ValueConverters;

public static class BitmapConverters
{
	private const int IconWidth = 64;

	public static readonly FuncValueConverter<byte[]?, Bitmap?> ToBitmap = new(bytes =>
	{
		try
		{
			if (bytes is not { Length: > 0 })
			{
				return null;
			}

			using MemoryStream ms = new(bytes);
			return Bitmap.DecodeToWidth(ms, IconWidth);
		}
		catch
		{
			return null;
		}
	});
}
