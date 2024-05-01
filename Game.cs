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


        /// <summary>
        /// Add a new die to list
        /// </summary>
        /// <param name="Sides">The number of sides the die should have (6 = normal die)</param>
        /// <param name="numberOfDie">The number of die you would like to add</param>
        public void NewDie(int Sides,int numberOfDie)
        {
            for(int i = 0; i < numberOfDie; i++) // for the number of die selected
            {
                string name = "Die - " + (diceList.Count+1); // create new die name based on current number of dice in the game

                diceList.Add(new Die(Sides,name)); // Add the new die to the dieList array
            }

        }



        /// <summary>
        /// Get the scores from the leaderboard file
        /// </summary>
        /// <returns>List of scores from leaderboard file</returns>
        private List<string> GetLeaderboard()
        {
            List<string> leaderboardlist = new List<string> ();// create list to store scores

            // Get .txt of leaderboard

            if (File.Exists(path))// if file found
            {
                List<string> leaderboardfile = File.ReadAllLines(path).ToList();// get all scores and save to list
                if(leaderboardfile.Count()  > 0)// if scores in file
                {
                    foreach(string player in leaderboardfile)// for each score
                    {

                    leaderboardlist.Add(player);// add score to output list

                    }

                }
                else// if no scores
                {
                    leaderboardlist.Add("No High Scores");// add no high scores to output
                }
            }
            else // if file not found
            {
                File.Create(path).Close();// create file then close it.
                leaderboardlist.Add("No High Scores");// add no high scores to output
            }


            return leaderboardlist;// return leaderboard
        }


        /// <summary>
        /// Update leaderboard with a new score
        /// </summary>
        /// <param name="name">Username</param>
        /// <param name="score">Score</param>
        public void UpdateLeaderboard(string name, Int64 score)
        {
            List<string> leaderboard = GetLeaderboard(); // get current leaderboard

            string newPlayer = name + " "+ score; // combine name and score
            if (leaderboard.Count() > 0 && leaderboard.Count() < 10 && leaderboard[0] != "No High Scores")// while leaderboard length between 1-10 and has no no high score string
            {
                var breakcheck = true;
                foreach (string player in leaderboard.ToList())// for each player in leaderboard
                {
                    string[] splitPlayer = player.Split(' ');// split back into name and score
                    if (Int64.Parse(splitPlayer[1]) < score) // if current leaderboard player has lower score than input player
                    {
                        leaderboard.Insert(leaderboard.IndexOf(player), newPlayer);// Insert player into leaderboard here
                        File.WriteAllLines(path, leaderboard);//write to leaderboard file
                        breakcheck = false;
                        break;
                    }
                }
                if (breakcheck) // if not aldready incerted into leaderboard
                {
                    leaderboard.Add(newPlayer);// add onto end of leaderboard
                    File.WriteAllLines(path, leaderboard);// write leaderboard to file
                }
                
            }
            else if(leaderboard.Count() == 10)// if leaderboard full
            {
                foreach (string player in leaderboard.ToList()) // for each player
                {
                    string[] splitPlayer = player.Split(' '); // split back into name and score
                    if (Int32.Parse(splitPlayer[1]) < score) // if current leaderboard player has lower score than input player
                    {
                        leaderboard.Insert(leaderboard.IndexOf(player), newPlayer);// insert into leaderboard
                        leaderboard.RemoveAt(leaderboard.Count()-1);// remove last score from leaderboard
                        File.WriteAllLines(path, leaderboard);// write to file
                        break;
                    }
                }
            }
            else // if no high scores
            {
                leaderboard.Insert(0, newPlayer); // add score
                leaderboard.Remove("No High Scores"); // remove txt

                File.WriteAllLines(path, leaderboard);// write to file

            }
            
        }


        /// <summary>
        /// Display the leaderboard
        /// </summary>
        public void OutputLeaderboard()
        {
            List<string> leaderboard = GetLeaderboard();// get leaderboard

            Console.WriteLine("\n\n---------Leaderboard----------");// create seperation


            foreach(string player in leaderboard)// for each player
            {
                Console.WriteLine("{0} : {1}",leaderboard.IndexOf(player)+1,player);// output name and score
            }
        }

        /// <summary>
        /// remove leaderboard file
        /// </summary>
        public void ClearLeaderboard()
        {
            File.Delete(path);// delete file
        }


        /// <summary>
        /// Runs things like UpdatePath() called on class creation.
        /// </summary>
        abstract public void GameSetup();

        /// <summary>
        /// Add user score to leaderboard and output to leaderboard
        /// </summary>
        /// <param name="score">Users score</param>
        public void GameOver(Int64 score)
        {
            GetName();
            UpdateLeaderboard(name, score);

            Console.WriteLine("Game Saved...");

            OutputLeaderboard();
        }


        /// <summary>
        /// Roll all dice in game
        /// </summary>
        /// <param name="display">set true to display dice</param>
        public void RollDice(bool display)
        {
            foreach (Die die in diceList)// for each die
            {
                die.Roll();// roll the die
                if (display)// if wanting to display
                {
                    // display die
                    Console.WriteLine("{0}\n - - -\n|     |\n|  {1}  |\n|     |\n - - -\n", die.name, die.Value); 
                }
            }
        }

        /// <summary>
        /// Get die values
        /// </summary>
        /// <returns>A list of the current die values</returns>
        public List<int> GetDieValue()
        {
            List<int> dieValue = new List<int>();// create list

            foreach (Die die in diceList)// for each die in game
            {
                dieValue.Add(die.Value);// add value to list
            }

            return dieValue;// return list
        }



    }
}
