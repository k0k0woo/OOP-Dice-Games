using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Add a duplicate global multiplier upgrade
    /// </summary>
    internal class UpgradeDuplicate:Upgrade
    {


        public override void SetCost()
        {
            BaseCost = 50;
            costMulti = 0.001D;
            GlobalMulti = 0.03M;
        }
        public override void SetName()
        {
            Name = "Duplicate multi";
        }

        public override void SetDescription()
        {
            description = "Multiply score by 1.30(30%) if a duplicate roll occurs(e.g double 6's) only occurs for highest duplicate.";
        }
        public override void ApplyUpgrade(Rollgame game)
        {
            Console.WriteLine("....Completed.....");

            game.GlobalUpgrades.Add(this);
        }
    }
}
