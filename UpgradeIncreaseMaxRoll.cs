using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Dice_Games
{
    internal class UpgradeIncreaseMaxRoll:Upgrade
    {
        public override void SetCost()
        {
            BaseCost = 5;
            costMulti = 0.0005D;
        }
        public override void SetName()
        {
            Name = "Add side to die";
        }

        public override void SetDescription()
        {
            description = "Allows die to roll 1 higher (turns 6-sided dice into 7-sided)";
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

            game.diceList[intDie-1].Maxroll += 1;

            Console.WriteLine("....Completed.....");

            Console.WriteLine("The updated die");

            game.DisplayDice();

        }
    }
}
