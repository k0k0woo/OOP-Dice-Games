using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Increase quantity die multiplier upgrade
    /// </summary>
    internal class UpgradeQuantityOfDieMulti:Upgrade
    {


        public override void SetCost()
        {
            BaseCost = 35;
            costMulti = 0.00125D;
            GlobalMulti = 0.01M;
        }
        public override void SetName()
        {
            Name = "Quantity die Multi";
        }

        public override void SetDescription()
        {
            description = "Multiply score by 1.10(10%) per dice";
        }
        public override void ApplyUpgrade(Rollgame game)
        {
            Console.WriteLine("....Completed.....");
            game.GlobalUpgrades.Add(this);
        }
    }
}
