using System.Drawing;
using System.Text;


namespace ImgToAscii
{
    class Program
    {
        static async Task Main()
        {
            int res = 32;
            ShowMenu(res, 5,"jonas",3);
            ButtonGame a = new ButtonGame();
            await a.pressKey('a', 3000);
            await a.pressKey('b', 2000);
            ShowMenu(res, 5, "Duck", 100);
        }

        static void ShowMenu(int width, int height, string name, int health)
        {
            ShowImage(name+".bmp", width);

            StringBuilder sb = new StringBuilder();
            //Top line
            sb.Append('|');
            for (int i = 0; i < width * 2; i++)
            {
                sb.Append("^");
            }
            sb.Append("|\n");

            //Black magic for showing name and health
            string name_ = " NAME: "+name;
            string health_ = " HEALTH: "+health;
            sb.Append("¦" + name_+health_.PadLeft(width*2-name_.Length-1)+ " ¦" + "\n");

            //Bottom line
            sb.Append('|');
            for (int i = 0; i < width*2; i++)
            {
                sb.Append('.');
            }
            sb.Append("|\n");


            Console.Write(sb);
        }
        static void ShowImage(string name, int resolution)
        {
            Dictionary<float, char> lumToChar = new Dictionary<float, char>() {
                {0.0f,' '},
                {0.2f,','},
                {0.4f,'+'},
                {0.6f,'&'},
                {0.8f,'#'},
                {1.0f,'■'}
            };
            string images = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Images");
            StringBuilder sb = new StringBuilder();
            Bitmap img = new Bitmap($"{images}/{name}"); ;
            int resX = img.Width / resolution;
            int resY = img.Height / resolution;
            for (int i = 0; i < img.Height; i += resY)
            {
                sb.Append('\n');
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
            }
            Console.WriteLine(sb);
        }
    }
}
