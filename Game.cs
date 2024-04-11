using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace OOP_Dice_Games
{
    abstract class Game
    {
        /// <summary>
        /// Generic game class contain things all games will need or may only vary
        /// </summary>


        public List<Die> diceList = new List<Die>();


        public string path = @"C:\Users\Joshi\Documents\Leaderboard.txt";

        public string name = "";

        // Will be used to change the path
        public abstract void UpdateFilePath();

        public void GetName()
        {
            Console.WriteLine("Enter Your Name (Max-16-Character):");

            string INPname = Console.ReadLine();

            if (ValidateName(INPname))
            {
                name = INPname;
            }
            else
            {
                Console.WriteLine("{0} is an invalid name.", INPname);
                GetName();
            }
        }

        private bool ValidateName(string VALname)
        {
            if(VALname.Length > 16 || VALname.Length < 1)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        
        // Add new die to the list
        public void NewDie(int Sides,int numberOfDie)
        {
            for(int i = 0; i < numberOfDie; i++) 
            {
                string name = "Die" + (i + 1);

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
                File.Create(path);
                leaderboardlist.Add("No High Scores");
            }


            return leaderboardlist;
        }
        // Update leaderboard
        public void UpdateLeaderboard(string name, int score)
        {
            List<string> leaderboard = GetLeaderboard();

            string newPlayer = name + " "+ score;
            if (leaderboard.Count() > 0 && leaderboard.Count() < 10 && leaderboard[0] != "No High Scores")
            {
                var breakcheck = true;
                foreach (string player in leaderboard.ToList())
                {
                    string[] splitPlayer = player.Split(' ');
                    if (Int32.Parse(splitPlayer[1]) < score)
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


        // Run on game creation
        /*
        public Game()
        {

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
