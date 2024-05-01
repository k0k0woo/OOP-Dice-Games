using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class statisitcs
    {

        static int ChooseGame()
        {

            Console.WriteLine("Enter 1-Sevens Out | 2-Three or more ");// tell user input choices

            string inp = Console.ReadLine(); // get and store input
            int intINP;
            bool check = int.TryParse(inp, out intINP); // validate input

            while (check == false || (intINP < 1 || intINP > 4)) // while input is a number not 1 or 2
            {
                Console.WriteLine("\nEnter 1-Sevens Out | 2-Three or more "); // tell user input choices again
                Console.WriteLine("\nInvalid input try again..\n");// tell user they inputed incorrect number
                inp = Console.ReadLine();
                check = int.TryParse(inp, out intINP); // re-validate input
            }

            return intINP; // return number

        }


        static void StartGame(int Game)
        {

            if (Game == 1)
            {
                Console.WriteLine("Sevens Out Selected ........\n\n");
                SevensOut sevensOut = new SevensOut();

                sevensOut.OutputLeaderboard();
            }
            else if (Game == 2)
            {
                Console.WriteLine("Three or more Selected ........\n\n");
                ThreeOrMore threeOrMore = new ThreeOrMore();
                threeOrMore.OutputLeaderboardStats();
            }
            else
            {
                Console.WriteLine("Invalid game selected....\n\n");//tell user the game that is selected
            }
        }

        public statisitcs()
        {
            StartGame(ChooseGame());
        }
    }
}
