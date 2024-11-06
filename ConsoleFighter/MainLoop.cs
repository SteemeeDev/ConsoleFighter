using System.Drawing;
using System.Text;


class MainLoop
{
    
    static async Task Main()
    {
        //Starting values
        int playerHealth = 10;
        ButtonGame game = new();

        // Game 1, Jonas the destroyer
        Enemy jonas = new Enemy("Jonas The Destroyer", 10, "The weakest enemy");
        await game1();
        async Task game1()
        {
            int enemyStartHealth = jonas.health;
            int playerStartHealth = playerHealth;

            game.ShowImage("Jonas", 32);
            game.ShowMenu(32, 5, jonas.name, jonas.health, jonas.lore);

            Console.WriteLine("A wild jonas approaches!");
            Console.WriteLine("Press enter to start fight...");
            Console.ReadLine();

            int dmg = await game.pressKey('a', 3000, 1);

            // We handle negative damage as enemy attack
            if (dmg > 0) { jonas.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('b', 2000, 1);
            if (dmg > 0) { jonas.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('c', 2000, 1);
            if (dmg > 0) { jonas.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('d', 2000, 1);
            if (dmg > 0) { jonas.health -= dmg; }
            else { playerHealth += dmg; }

            game.showFightSummary(enemyStartHealth, jonas.health, playerStartHealth, playerHealth);
            if (game.checkPlrHealth(playerHealth))
            {
                await Main();
            }
            if (game.checkEnemyHealth(jonas.health))
            {
                return;
            }
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();

            enemyStartHealth = jonas.health;
            playerStartHealth = playerHealth;

            game.ShowImage("Jonas", 32);
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

            game.showFightSummary(enemyStartHealth, jonas.health, playerStartHealth, playerHealth);
            if (game.checkPlrHealth(playerHealth))
            {
                await Main();
            }
            if (game.checkEnemyHealth(jonas.health))
            {
                return;
            }
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
            await game1();

        }

        // Game 2, Evil duck
        Enemy duck = new Enemy("Duck, Lord of evil", 100, "An unholy lord of all that is dark");
        await game2();
        async Task game2()
        {
            int enemyStartHealth = duck.health;
            int playerStartHealth = playerHealth;

            game.ShowImage("Duck", 32);
            game.ShowMenu(32, 5, duck.name, duck.health, duck.lore);

            Console.WriteLine("You feel a shiver down your spine");
            Console.WriteLine("Press enter to start fight...");
            Console.ReadLine();

            int dmg = await game.pressKey('q', 1000, 2);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('w', 1000, 2);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('n', 1000, 2);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('m', 1000, 2);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('g', 1000, 2);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('h', 1000, 2);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            game.showFightSummary(enemyStartHealth, duck.health, playerStartHealth, playerHealth);
            if (game.checkPlrHealth(playerHealth))
            {
                await Main();
            }
            if (game.checkEnemyHealth(duck.health))
            {
                return;
            }
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();



            enemyStartHealth = duck.health;
            playerStartHealth = playerHealth;

            game.ShowImage("Duck", 32);
            game.ShowMenu(32, 5, duck.name, duck.health, duck.lore);

            Console.WriteLine("Hint: Next pattern is d f j k");
            Console.WriteLine("Press enter to start fight...");
            Console.ReadLine();

            dmg = await game.pressKey('d', 500, 4);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('f', 500, 4);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('j', 500, 4);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }

            dmg = await game.pressKey('k', 500, 4);
            if (dmg > 0) { duck.health -= dmg; }
            else { playerHealth += dmg; }


            game.showFightSummary(enemyStartHealth, duck.health, playerStartHealth, playerHealth);
            if (game.checkPlrHealth(playerHealth))
            {
                await Main();
            }
            if (game.checkEnemyHealth(duck.health))
            {
                return;
            }
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();

            await game2();
        }

        //Finished game
        while (true)
        {
            for (int i = 0; i < 14; i++)
            {
                game.ShowImage($@"firework\{i}", 32);
                Thread.Sleep(200);
                Console.SetCursorPosition(0, 0);
            }
        }


    }


}



