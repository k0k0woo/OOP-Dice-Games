using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Generic class Game
    /// </summary>
    abstract class Game
    {
        // Variables

        public List<Die> diceList = new List<Die>(); // Create a list of dice


        public string path = @"C:\Users\Joshi\Documents\Leaderboard.txt"; // leaderboard folder path

        public string name = ""; // users name for leaderboard

        /// <summary>
        /// Updates the filepath (mainly so different games can use different leaderboards)
        /// </summary>
        public abstract void UpdateFilePath();

        /// <summary>
        /// Get the username for the leaderboard.
        /// </summary>
        public void GetName()
        {
            
            Console.WriteLine("Enter Your Name (Max-16-Character):");// tell user for input

            string INPname = Console.ReadLine();// store input

            if (ValidateName(INPname)) // ensure name fits criteria
            {
                name = INPname; // update name
            }
            else
            {
                Console.WriteLine("{0} is an invalid name.", INPname);// tell user that name invalid
                GetName();// recursive function
            }
        }

        /// <summary>
        /// Checks that string length is 1-16 characters long
        /// </summary>
        /// <param name="VALname">input string (leaderboard name)</param>
        /// <returns>bool</returns>
        private bool ValidateName(string VALname)
        {
            if(VALname.Length > 16 || VALname.Length < 1) // is string length between 1-16
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        /// <summary>
        /// Check that input is a number
        /// </summary>
        /// <param name="number">the string input</param>
        /// <returns>the number as integer</returns>
        public int CheckInputInt(string number)
        {
            
            int Output;

            bool CheckInt = int.TryParse(number, out Output);// check input is integer

            while (CheckInt == false) // while input is not integer
            {
                Console.WriteLine("{0} is an invalid input", number);// tell user invalid
                
                Console.WriteLine("Input: ");
                var num = Console.ReadLine();// store new input
                CheckInt = int.TryParse(num, out Output);// check new input is an integer

            }
            return Output;


        }


        // Add new die to the list
        public void NewDie(int Sides,int numberOfDie)
        {
            for(int i = 0; i < numberOfDie; i++) 
            {
                string name = "Die" + (diceList.Count+1);

                diceList.Add(new Die(Sides,name));
            }

        }

        // Leaderboard - top 10


        // Get leaderboard
        private List<string> GetLeaderboard()
        {
            List<string> leaderboardlist = new List<string> ();

            // Get .txt of leaderboard
            if (File.Exists(path))
            {
                List<string> leaderboardfile = File.ReadAllLines(path).ToList();
                if(leaderboardfile.Count()  > 0)
                {
                    foreach(string player in leaderboardfile)
                    {

                    leaderboardlist.Add(player);

                    }

                }
                else
                {
                    leaderboardlist.Add("No High Scores");
                }
            }
            else
            {
                File.Create(path).Close();
                leaderboardlist.Add("No High Scores");
            }


            return leaderboardlist;
        }
        // Update leaderboard
        public void UpdateLeaderboard(string name, Int64 score)
        {
            List<string> leaderboard = GetLeaderboard();

            string newPlayer = name + " "+ score;
            if (leaderboard.Count() > 0 && leaderboard.Count() < 10 && leaderboard[0] != "No High Scores")
            {
                var breakcheck = true;
                foreach (string player in leaderboard.ToList())
                {
                    string[] splitPlayer = player.Split(' ');
                    if (Int64.Parse(splitPlayer[1]) < score)
                    {
                        leaderboard.Insert(leaderboard.IndexOf(player), newPlayer);
                        File.WriteAllLines(path, leaderboard);
                        breakcheck = false;
                        break;
                    }
                }
                if (breakcheck)
                {
                    leaderboard.Add(newPlayer);
                    File.WriteAllLines(path, leaderboard);
                }
                
            }
            else if(leaderboard.Count() == 10)
            {
                foreach (string player in leaderboard.ToList())
                {
                    string[] splitPlayer = player.Split(' ');
                    if (Int32.Parse(splitPlayer[1]) < score)
                    {
                        leaderboard.Insert(leaderboard.IndexOf(player), newPlayer);
                        leaderboard.RemoveAt(leaderboard.Count()-1);
                        File.WriteAllLines(path, leaderboard);
                        break;
                    }
                }
            }
            else
            {
                leaderboard.Insert(0, newPlayer);
                leaderboard.Remove("No High Scores");

                foreach(string player in leaderboard)
                { Console.WriteLine(player); }

                File.WriteAllLines(path, leaderboard);

            }
            
        }
        // Output leaderboard
        public void OutputLeaderboard()
        {
            List<string> leaderboard = GetLeaderboard();

            Console.WriteLine("\n\n---------Leaderboard----------");


            foreach(string player in leaderboard)
            {
                Console.WriteLine("{0} : {1}",leaderboard.IndexOf(player)+1,player);
            }
        }

        public void ClearLeaderboard()
        {
            File.Delete(path);
        }

        abstract public void GameSetup();

        public void GameOver(Int64 score)
        {
            GetName();
            UpdateLeaderboard(name, score);

            Console.WriteLine("Game Saved...");

            OutputLeaderboard();
        }

        public void RollDice(bool display)
        {
            foreach (Die die in diceList)
            {
                die.Roll();
                if (display)
                {
                    Console.WriteLine("{0}\n - - -\n|     |\n|  {1}  |\n|     |\n - - -\n", die.name, die.Value);
                }
            }
        }

        public List<int> GetDieValue()
        {
            List<int> dieValue = new List<int>();

            foreach (Die die in diceList)
            {
                dieValue.Add(die.Value);
            }

            return dieValue;
        }


        // Run on class creation
        /*
        public Game() replace with classname
        {

            Game funcition

            _NewDie(6, 2);

            var total = 0;

            foreach(Die die in _diceList)
            {
                die.Roll();
                total += die.Value;
                Console.WriteLine("{0} Roll = {1}",die.name,die.Value);
            }

            Console.WriteLine("The sum of dice is {0}", total);

            UpdateLeaderboard("fe", total);
        }
        */
    }
}
