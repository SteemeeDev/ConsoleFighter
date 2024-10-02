using System.Drawing;

Bitmap image = new Bitmap("C:/ImageToAscii/ImageToAscii/ImageToAscii/Image.bmp");

Dictionary<float,char> lumToChar = new Dictionary<float, char>() {
	{0f,' '},
	{0.2f,','},
	{0.4f,'+'},
	{0.6f,'&' },
	{0.8f,'#'},
	{1f, '■'}
};

int resolution = 64;
void ShowImgInConsole(Bitmap img)
{
	for (int i = 0; i < img.Height; i+=img.Height/resolution)
	{
		for (int j = 0; j < img.Width; j += img.Height/resolution)
		{
			Color color = img.GetPixel(j, i);
			float luminance = color.GetBrightness();
			float roundedLum = (float)Math.Round(luminance / 0.2f) * 0.2f;
			if (lumToChar.TryGetValue(roundedLum, out char value))
			{
				Console.Write($".{value}");
			}
			else
			{
				Console.Write(' ');
			}
		}
		Console.WriteLine();
	}
}

ShowImgInConsole(image);