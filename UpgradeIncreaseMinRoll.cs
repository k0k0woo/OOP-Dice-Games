using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class UpgradeIncreaseMinRoll:Upgrade
    {
        private int addValue = new Random(Guid.NewGuid().GetHashCode()).Next(1, 10);
        public override void SetCost()
        {
            BaseCost = 4 * addValue-3;
            costMulti = 0.0005D;
        }
        public override void SetName()
        {
            Name = "Increases the minimal result a die can have by " + addValue + " (Will top out at die max roll).";
        }

        public override void SetDescription()
        {
            description = "For example a normal 6-sided die with a minimal roll increase of 1 now will roll 2-6 intstead.";
        }
        public override void ApplyUpgrade(Rollgame game)
        {
            Console.WriteLine("Which Die would you like to upgrade?:");

            game.DisplayDice();

            Console.WriteLine("Enter die number (die 1 = 1) :");

            string dieChoice = Console.ReadLine();

            int intDie = game.CheckInputInt(dieChoice);
            while (intDie < 1 || intDie > game.diceList.Count)
            {
                Console.WriteLine("Not valid die.");
                Console.WriteLine("Enter die number (die 1 = 1) :");
                dieChoice = Console.ReadLine();
                intDie = game.CheckInputInt(dieChoice);
            }
            if (game.diceList[intDie - 1].Minroll + addValue > game.diceList[intDie - 1].Maxroll)
            {
                Console.WriteLine("Taken min roll to max..");
                game.diceList[intDie - 1].Minroll = game.diceList[intDie - 1].Maxroll-1;
            }
            else
            {
                game.diceList[intDie - 1].Minroll += addValue;
            }
            Console.WriteLine("....Completed.....");

            Console.WriteLine("The updated die");

            game.DisplayDice();

        }
    }
}
