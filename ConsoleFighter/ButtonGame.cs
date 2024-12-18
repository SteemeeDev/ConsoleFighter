﻿using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;


class ButtonGame
{
    public async Task<int> pressKey(char targetKey, int timeLimit, int damage)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Press the \u001b[0;96m{targetKey}\u001b[97m key within {timeLimit} milliseconds");
        bool hitKey = false;

        using (var cts = new CancellationTokenSource())
        {
            Task Delay = Task.Delay(timeLimit, cts.Token);
            var getKey = Task.Run(() => {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        char key = Console.ReadKey(intercept: true).KeyChar;

                        if (key == targetKey)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"You did {damage} damage!");
                            Console.ForegroundColor = ConsoleColor.White;
                            hitKey = true;
                            cts.Cancel();
                        }

                        if (!Delay.IsCompleted)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Wrong key! You pressed: {key}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    }
                }
                return false;
            }, cts.Token);
            //Important to have getKey first as it defaults to that
            Task completed = await Task.WhenAny(getKey, Delay);


            if (hitKey == true)
            {
                return damage;
            }
            else if (hitKey == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You missed the key and took damage!");
                Console.ForegroundColor = ConsoleColor.White;
                return -1;
            }
            else
            {
                Console.WriteLine("Some error happened idk async programming is hard!");
                return 0;
            }

        }
    }

    public void ShowMenu(int width, int height, string name, int health, string lore)
    {
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
        //Dictionary med luminance værdier, som korresponderer til en ascii karakter
        Dictionary<float, char> lumToChar = new Dictionary<float, char>() {
            {0.0f,' '},
            {0.2f,','},
            {0.4f,'+'},
            {0.6f,'&'},
            {0.8f,'#'},
            {1.0f,'■'}
        };

        //Find image path
        string images = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Images");
        //Jeg bruger en stringbuilder, og bruger kun 1 draw call så det er hurtigere
        StringBuilder sb = new StringBuilder();
        //Vi finder det givne bitmap billede
        Bitmap img = new Bitmap($"{images}/{name}.bmp"); ;
        //Vi skipper nogle pixels så billedet passer i konsollen
        int resX = img.Width / resolution;
        int resY = img.Height / resolution;
        /// Her looper vi gennem hver x-ende pixel i billedet, 
        /// regner pixel'ens brightness og bruger vores dictionary
        /// til at lookup den givne ascii karakter
        for (int i = 0; i < img.Height; i += resY)
        {
            sb.Append('\n');
            for (int j = 0; j < img.Width; j += resX)
            {
                Color color = img.GetPixel(j, i);
                // Brightness af en pixel giver en værdi mellem 0 og 1 som beskriver hvor lys pixellen er 
                float luminance = color.GetBrightness();
                // Vi runder brightness værdien til nærmeste 0.2'er så den passer til dictionary'et
                float roundedLum = (float)Math.Round(luminance / 0.2f) * 0.2f;
                if (lumToChar.TryGetValue(roundedLum, out char value))
                {
                    sb.Append($" {value}");
                }
            }
        }
        // Til sidst printer vi vores billede
        Console.WriteLine(sb);
    }

    public void showFightSummary(int enemyStartHealth, int enemyHealth, int playerStartHealth, int playerHealth)
    {
        Console.WriteLine("Fight summary:");
        Console.WriteLine($"You did \u001b[91m{enemyStartHealth - enemyHealth}\u001b[97m damage");
        Console.WriteLine($"You took \u001b[91m{playerStartHealth - playerHealth}\u001b[97m damage");
        Console.WriteLine($"You have \u001b[92m{playerHealth}\u001b[97m health");
        Console.WriteLine($"The enemy has \u001b[92m{enemyHealth}\u001b[97m health");
    }

    public bool checkPlrHealth(int playerHealth)
    {
        if (playerHealth <= 0)
        {
            Console.WriteLine("You died!");
            Thread.Sleep(1000);
            Console.WriteLine("Press enter to restart...");
            Console.ReadLine();
            Console.Clear();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkEnemyHealth(int enemyHealth)
    {
        if (enemyHealth <= 0)
        {
            Console.WriteLine("You won!");
            Thread.Sleep(1000);
            Console.WriteLine("Press enter to go to next stage...");
            Console.ReadLine();
            Console.Clear();
            return true;
        }
        else
        {
            return false;
        }
    }
}
 