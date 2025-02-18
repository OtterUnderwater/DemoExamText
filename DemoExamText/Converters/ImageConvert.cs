using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;


namespace DemoExamText.Converters
{
	internal class ImageConvert : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			return (value == null || value == "") ?
				new Bitmap(AssetLoader.Open(new Uri("avares://DemoExamText/Assets/picture.jpg"))):
				new Bitmap(AssetLoader.Open(new Uri($"avares://DemoExamText/Assets/{System.Convert.ToString(value).Trim()}")));
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
