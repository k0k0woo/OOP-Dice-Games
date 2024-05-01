using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Add a new die upgrade
    /// </summary>
    internal class UpgradeAddDie:Upgrade
    {
        public override void SetCost()
        {
            BaseCost = 35;
            costMulti = 0.0035D;
        }
        public override void SetName()
        {
            Name = "Add new Die";
        }

        public override void SetDescription()
        {
            description = "Adds a new die";
        }
        public override void ApplyUpgrade(Rollgame game)
        {
            game.NewDie(6, 1);
            Console.WriteLine("Added new die");
        }

    }
}
