using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my project..\n\n");

            StartGame(ChooseGame());

            Console.WriteLine("Press any key to exit..\n\n");
            Console.ReadKey();


        }

        static int ChooseGame()
        {

            Console.WriteLine("Enter | 1 - BettingGame | 2 - RollingGame |");

            string inp = Console.ReadLine();
            int intINP;
            bool check = int.TryParse(inp, out intINP);

            while (check == false || (intINP < 1 || intINP > 2)) 
            {
                Console.WriteLine("\nEnter | 1 - BettingGame | 2 - RollingGame |");
                Console.WriteLine("\nInvalid input try again..\n");
                inp = Console.ReadLine();
                check = int.TryParse(inp, out intINP);
            }

            return intINP;

        }


        static void StartGame(int Game)
        {
            if (Game == 1)
            {
                Console.WriteLine("Betting game selected....\n\n");
                BettingGame bettingGame = new BettingGame();
            }
            else if (Game == 2)
            {
                Console.WriteLine("Rolling game selected....\n\n");
                Rollgame rollgame = new Rollgame();

            }
        }
    }
}
