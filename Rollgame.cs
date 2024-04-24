using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class Rollgame:Game
    {

        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\RollGameLeaderboard.txt";
        }

        public override void GameSetup()
        {
            Console.WriteLine("Game set up....\n");
        }



        public Rollgame()
        {
            UpdateFilePath();

            GameSetup();
        }
    }
}
