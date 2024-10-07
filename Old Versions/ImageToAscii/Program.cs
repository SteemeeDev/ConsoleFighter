using System.Diagnostics;
using System.Drawing;
using System.Text;

Dictionary<float, char> lumToChar = new Dictionary<float, char>() {
    {0f,' '},
    {0.2f,','},
    {0.4f,'+'},
    {0.6f,'&' },
    {0.8f,'#'},
    {1f, '■'}
    };


string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Images");
Console.WriteLine("PATH: " + path);


while (true)
{
    Console.WriteLine("Enter filename, press enter for default: ");

    string input = Console.ReadLine();
    if (input == "")
    {
        Console.WriteLine("Choosing duck image");
        input = "Duck.bmp";
    }
    else
    {
        if (!File.Exists(path+@$"\{input}")) {
            Console.WriteLine("Error File Not Found!");
            continue;
        }

    }
  
    Bitmap image = new Bitmap($"{path}/{input}");


    Console.WriteLine("Enter a resolution:");
    string res = Console.ReadLine();
    int resolution = 32;
    try
    {
        resolution = int.Parse(res);
        if (resolution > image.Width || resolution > image.Height) {
            Console.WriteLine("Invalid resoution! Resolution is bigger than image size!");
            continue;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Resolution! Using default of " + resolution);
    }

    int resX = image.Width/resolution;
    int resY = image.Height/resolution; 

    StringBuilder sb = new StringBuilder();
    void ShowImgInConsole(Bitmap img)
    {
        sb.Clear();
        for (int i = 0; i < img.Height; i += resY)
        {
            for (int j = 0; j < img.Width; j += resX)
            {
                Color color = img.GetPixel(j, i);
                float luminance = color.GetBrightness();
                float roundedLum = (float)Math.Round(luminance / 0.2f) * 0.2f;
                if (lumToChar.TryGetValue(roundedLum, out char value))
                {
                    sb.Append($" {value}");
                }
            }
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }

    ShowImgInConsole(image);
}
