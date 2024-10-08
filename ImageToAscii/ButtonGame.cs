using System;
using System.Threading.Tasks;


class ButtonGame
{
    public async Task<bool> pressKey(char targetKey, int timeLimit)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Press the {targetKey} key within {timeLimit} milliseconds");
        Task Delay = Task.Delay(timeLimit);
        var getKey = Task.Run(() =>
        {
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
        
        Task Completed = await Task.WhenAny(Delay, getKey);

        if (Completed == getKey)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You pressed the key in time!");
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You fail!");
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }
    }

    class Enemy
    {
        public string name;
        public int health;
        Enemy()
        {

        }
        void takeDamage(int  damage) 
        {
            health -= damage;
        }
    }
}
 