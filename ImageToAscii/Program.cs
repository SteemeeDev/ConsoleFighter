using System.Drawing;
using System.Drawing.Text;
using System.IO;
/*
Getfullpath removes ..\
@"" means literal string
..\ means go up folder
GetCurrenctDirectory gives the directory of the executable, not the project
*/
string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));

Console.WriteLine("PATH: " + path);


while (true)
{
    Console.WriteLine("Give filename, press enter for default");

    string input = Console.ReadLine();
    if (input == "")
    {
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
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Resolution! Using default of "+resolution);
    }

    Dictionary<float, char> lumToChar = new Dictionary<float, char>() {
    {0f,' '},
    {0.2f,','},
    {0.4f,'+'},
    {0.6f,'&' },
    {0.8f,'#'},
    {1f, '■'}
};


    void ShowImgInConsole(Bitmap img)
    {
        for (int i = 0; i < img.Height; i += img.Height / resolution)
        {
            for (int j = 0; j < img.Width; j += img.Height / resolution)
            {
                Color color = img.GetPixel(j, i);
                float luminance = color.GetBrightness();
                float roundedLum = (float)Math.Round(luminance / 0.2f) * 0.2f;
                if (lumToChar.TryGetValue(roundedLum, out char value))
                {
                    Console.Write($" {value}");
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine(input) ;
    }

    ShowImgInConsole(image);
}
