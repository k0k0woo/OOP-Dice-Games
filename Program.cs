using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
        /// <summary>
        /// Class containing the main function.
        /// Starts the game.
        /// </summary>
    internal class Program
    {  



        /// <summary>
        /// Main function calls startgame with parameter of choosegame.
        /// </summary>

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my project..\n\n"); // tell the user programme started

            StartGame(ChooseGame()); // call the game function

            Console.WriteLine("Press any key to exit..\n\n"); // tell user to press key to exit when game ended
            Console.ReadKey();// get key input


        }

        /// <summary>
        /// Ask user for a numerical input corresponding to a game.
        /// </summary>
        /// <returns>The input number</returns>
        static int ChooseGame()
        {

            Console.WriteLine("Enter | 1 - BettingGame | 2 - RollingGame | 3-Sevens Out | 4-Three or more | 5 - Statistics");// tell user input choices

            string inp = Console.ReadLine(); // get and store input
            int intINP;
            bool check = int.TryParse(inp, out intINP); // validate input

            while (check == false || (intINP < 1 || intINP > 5)) // while input is a number not 1 or 2
            {
                Console.WriteLine("\nEnter | 1 - BettingGame | 2 - RollingGame | 3-Sevens Out | 4-Three or more | 5 - Statistics"); // tell user input choices again
                Console.WriteLine("\nInvalid input try again..\n");// tell user they inputed incorrect number
                inp = Console.ReadLine();
                check = int.TryParse(inp, out intINP); // re-validate input
            }

            return intINP; // return number

        }

        /// <summary>
        /// Create the game class dependant of input
        /// </summary>
        /// <param name="Game">An integer (1/2)</param>
        static void StartGame(int Game)
        {
            
            if (Game == 1) // if input == 1
            {
                // start betting game

                Console.WriteLine("Betting game selected....\n\n");//tell user the game that is selected
                BettingGame bettingGame = new BettingGame();// create the class to start the game
            }
            else if (Game == 2)
            {
                Console.WriteLine("Rolling game selected....\n\n");//tell user the game that is selected
                Rollgame rollgame = new Rollgame();// create the class to start the game

            }else if (Game == 3)
            {
                Console.WriteLine("Sevens Out Selected ........\n\n");
                SevensOut sevensOut = new SevensOut();
                sevensOut.Start();
            }
            else if (Game == 4)
            {
                Console.WriteLine("Three or more Selected ........\n\n");
                ThreeOrMore threeOrMore = new ThreeOrMore();
                threeOrMore.Start();
            }
            else if(Game == 5)
            {
                statisitcs stats = new statisitcs();

            }
            else
            {
                Console.WriteLine("Invalid game selected....\n\n");//tell user the game that is selected
            }
        }
    }
}
