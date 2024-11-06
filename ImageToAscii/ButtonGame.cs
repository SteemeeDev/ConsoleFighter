using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;


class ButtonGame
{
    public async Task<int> pressKey(char targetKey, int timeLimit, int damage)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Press the {targetKey} key within {timeLimit} milliseconds");
        Task Delay = Task.Delay(timeLimit);
        var getKey = Task.Run(() => {
            while (true) {
                if (Console.KeyAvailable)
                {
                    char key = Console.ReadKey(intercept: true).KeyChar;

                    if (key == targetKey)
                    {
                        return true;
                    }

                    if (!Delay.IsCompleted)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Wrong key! You pressed: {key}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
            }
        });
        Task completed = null;
        completed = await Task.WhenAny(Delay, getKey);
        //await Task.WhenAll(Delay, getKey);

        if (completed == getKey)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You pressed the key in time!");
            Console.ForegroundColor = ConsoleColor.White;
            return damage;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You missed the key!");
            Console.ForegroundColor = ConsoleColor.White;
            return -damage;
        }
    }
    public void ShowMenu(int width, int height, string name, int health, string lore)
    {
        ShowImage(name + ".bmp", width);

        StringBuilder sb = new StringBuilder();
        //Top line
        sb.Append('|');
        for (int i = 0; i < width * 2; i++)
        {
            sb.Append("^");
        }
        sb.Append("|\n");

        //Black magic for showing name and health
        string name_ = " NAME: " + name;
        string health_ = " HEALTH: " + health;
        string lore_ = " Description: " + lore;
        sb.Append("¦" + name_ + health_.PadLeft(width * 2 - 1 - name_.Length) + " ¦\n");
        sb.Append("¦" +"\u001b[90;3m" + lore_.PadRight(width * 2) + "\u001b[0m" + "¦\n");

        //Bottom line
        sb.Append('|');
        for (int i = 0; i < width * 2; i++)
        {
            sb.Append('.');
        }
        sb.Append("|\n");


        Console.Write(sb);
    }
    public void ShowImage(string name, int resolution)
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
 