using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace OOP_Dice_Games
{
    internal class ThreeOrMore:Game
    {
        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\ThreeOrMore.txt";
        }

        public override void GameSetup()
        {
            UpdateFilePath();
        }

        public bool checkWin(Player player)
        {
            if(player.points >= 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<string> GetLeaderboard()
        {
            List<string> leaderboardlist = new List<string>();// create list to store scores

            // Get .txt of leaderboard

            if (File.Exists(path))// if file found
            {
                List<string> leaderboardfile = File.ReadAllLines(path).ToList();// get all scores and save to list
                if (leaderboardfile.Count() > 0)// if scores in file
                {
                    foreach (string player in leaderboardfile)// for each score
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



        public void UpdateLeaderboard(string name,bool Win)
        {
            List<string> leaderboard = GetLeaderboard(); // get current leaderboard
            
            int wins = 0;
            if (Win) { wins++; }
            int games = 1;

            string newPlayer = name+":"+wins+":"+games;
            
            if (leaderboard.Count() > 0 && leaderboard[0] != "No High Scores")// while leaderboard length between 1-10 and has no no high score string
            {
                var breakcheck = true;
                restart:
                foreach (string player in leaderboard.ToList())// for each player in leaderboard
                {
                    string[] splitPlayer = player.Split(':');// split back into name and score
                    if (splitPlayer[0] == name)
                    {
                        wins = int.Parse(splitPlayer[1]);
                        if (Win) { wins++; }
                        games = int.Parse(splitPlayer[2]) + 1;
                        newPlayer = name + ":" + wins + ":" + games;
                        leaderboard.Remove(player);
                        goto restart;
                    }
                    else
                    {
                        if (Int64.Parse(splitPlayer[1]) < wins) // if current leaderboard player has lower score than input player
                        {
                            leaderboard.Insert(leaderboard.IndexOf(player), newPlayer);// Insert player into leaderboard here
                            File.WriteAllLines(path, leaderboard);//write to leaderboard file
                            breakcheck = false;
                            break;
                        }
                    }
                }
                if (breakcheck) // if not already incerted into leaderboard
                {
                    leaderboard.Add(newPlayer);// add onto end of leaderboard
                    File.WriteAllLines(path, leaderboard);// write leaderboard to file
                }

            }
            
            else // if no high scores
            {
                leaderboard.Insert(0, newPlayer); // add score
                leaderboard.Remove("No High Scores"); // remove txt

                File.WriteAllLines(path, leaderboard);// write to file

            }

        }



        public new void OutputLeaderboard()
        {
            List<string> leaderboard = GetLeaderboard();// get leaderboard

            Console.WriteLine("\n\n---------Leaderboard----------");// create seperation


            foreach(string player in leaderboard)// for each player
            {
                if (leaderboard.IndexOf(player) + 1 <= 10)
                {
                    List<string> listplayer = player.Split(':').ToList();
                    Console.WriteLine("{0} : Player {1} :  Wins {2} : Games {3}", leaderboard.IndexOf(player) + 1, listplayer[0], listplayer[1], listplayer[2]);// output name and score
                }
            }
        }

        public void OutputLeaderboardStats()
        {
            List<string> leaderboard = GetLeaderboard();// get leaderboard

            Console.WriteLine("\n\n---------Leaderboard----------");// create seperation


            foreach (string player in leaderboard)// for each player
            {
                if (leaderboard.IndexOf(player) + 1 <= 10)
                {
                    List<string> listplayer = player.Split(':').ToList();
                    decimal winPercent = decimal.Parse(listplayer[1]) / decimal.Parse(listplayer[2])*100;
                    winPercent = Math.Round(winPercent, 2);

                    Console.WriteLine("{0} : Player {1} :  Wins {2} : Games {3} : Win Percent {4}", leaderboard.IndexOf(player) + 1, listplayer[0], listplayer[1], listplayer[2],winPercent.ToString()+"%");// output name and score
                }
            }
        }

        public void GameOver(string name,bool win)
        {
            UpdateLeaderboard(name, win);

        }

        public void Playerturn(Player player,bool human)
        {
            player.Playerturn(human);
            if (checkWin(player))
            {
                // Game over
                GameOver(player.name,true);
            }
            Console.WriteLine("\n\nPress key to continue.....");
            Console.ReadKey();
        }

        public bool AgainstAI()
        {
            Console.WriteLine("\nDo you want to play against a bot / a friend? y(bot)/n(friend)");
            string DieHold = Console.ReadLine().ToString();

            while (DieHold != "y" && DieHold != "n")
            {
                Console.WriteLine("\nInvalid input....");
                Console.WriteLine("\nDo you want to play against a bot / a friend? y(bot)/n(friend)");
                DieHold = Console.ReadLine().ToString();
            }

            if (DieHold == "y")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Start()
        {
            NewDie(6, 5);
            GetName();
            Player player1 = new Player(diceList, name, true);

            diceList = new List<Die>();
            
            NewDie(6, 5);
            bool ai = AgainstAI();
            if(ai == true)
            {
                GetName();
            }
            else
            {
                name = "AI BOT";
            }
            Player player2 = new Player(diceList, name, ai);

            diceList = new List<Die>();

            while (checkWin(player1) == false && checkWin(player2) == false)
            {
                Playerturn(player1, player1.RealPerson);

                if (checkWin(player1) == false)
                {
                    Playerturn(player2, player2.RealPerson);
                }

            }
            if (checkWin(player1) == false)
            {
                GameOver(player1.name, false);

            }
            else if (checkWin(player2) == false)
            {
                GameOver(player2.name, false);
            }

            OutputLeaderboard();
            Console.WriteLine("Game Saved...");
        }

        public ThreeOrMore()
        {

            GameSetup();

        }
    }
}
