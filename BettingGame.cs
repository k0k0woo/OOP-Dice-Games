using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class BettingGame:Game
    {
        //Change path for each game otherwise it will overide the file
        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\BettingLeaderboard.txt";
        }
        public BettingGame()
        {
            UpdateFilePath();
            _NewDie(10, 6);

            var total = 0;

            foreach (Die die in _diceList)
            {
                die.Roll();
                total += die.Value;
                Console.WriteLine("{0} Roll = {1}", die.name, die.Value);
            }

            Console.WriteLine("The sum of dice is {0}", total);

            UpdateLeaderboard("fe", total);
        }
    }
}
