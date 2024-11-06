using System.Drawing;
using System.Text;


class MainLoop
{

    
    static async Task Main()
    {
        void showFightSummary(int enemyStartHealth, int EnemyHealth, int playerStartHealth, int playerHealth)
        {
            Console.WriteLine("Fight summary:");
            Console.WriteLine($"You did \u001b[91m{enemyStartHealth - EnemyHealth}\u001b[97m damage");
            Console.WriteLine($"You took \u001b[91m{playerStartHealth - playerHealth}\u001b[97m damage");
            Console.WriteLine($"You have \u001b[92m{playerHealth}\u001b[97m health");
        }

        bool checkHealth(int playerHealth)
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

        int playerHealth = 10;

        ButtonGame game = new();
        Enemy jonas = new Enemy("Jonas", 10, "The weakest enemy");
        int enemyStartHealth = jonas.health;
        int playerStartHealth = playerHealth;

        game.ShowMenu(32, 5, jonas.name, jonas.health, jonas.lore);
        Console.WriteLine("A wild jonas approaches!");
        Console.WriteLine("Press enter to start fight...");
        Console.ReadLine();

        int dmg = await game.pressKey('a', 3000, 10);

        // We handle negative damage as enemy attack
        if (dmg > 0) { jonas.health -= dmg; }
        else { playerHealth += dmg; }

        dmg = await game.pressKey('b', 2000, 1);
        if (dmg > 0) { jonas.health -= dmg; }
        else { playerHealth += dmg; }

        showFightSummary(enemyStartHealth, jonas.health, playerStartHealth, playerHealth);
        if (checkHealth(playerHealth))
        {
            await Main();
        }

        enemyStartHealth = jonas.health;
        playerStartHealth = playerHealth;

        Thread.Sleep(2000);
        game.ShowMenu(32, 5, jonas.name, jonas.health, jonas.lore);
        Console.WriteLine("Press enter to start fight...");
        Console.ReadLine();

        dmg = await game.pressKey('h', 1500, 1);
        if (dmg > 0) { jonas.health -= dmg; }
        else { playerHealth += dmg; }

        dmg = await game.pressKey('j', 1500, 1);
        if (dmg > 0) { jonas.health -= dmg; }
        else { playerHealth += dmg; }

        dmg = await game.pressKey('k', 1500, 1);
        if (dmg > 0) { jonas.health -= dmg; }
        else { playerHealth += dmg; }

        dmg = await game.pressKey('l', 1500, 1);
        if (dmg > 0) { jonas.health -= dmg; }
        else { playerHealth += dmg; }


        Console.WriteLine("Fight summary:");
        Console.WriteLine($"You did \u001b[91m{enemyStartHealth - jonas.health}\u001b[97m damage");
        Console.WriteLine($"You took \u001b[91m{playerStartHealth - playerHealth}\u001b[97m damage");
        Console.WriteLine($"You have \u001b[92m{playerHealth}\u001b[97m health");
 
        if (playerHealth <= 0)
        {
            Console.WriteLine("You died!");
            await Main();
        }

        // a.ShowMenu(22, 5, "Duck", 100);
        Console.ReadLine();



    }


}



