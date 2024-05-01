using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Class designed to act as a shop for user to buy upgrades
    /// </summary>
    internal class UpgradeShop
    {
        // Variables

        public int shopslots = 3;// amount of upgrades on shop inventory

        public int MaxShopslots = 3;// used to ensure correct amount of upgrades always in inventory

        public List<Upgrade> upgrades = new List<Upgrade>();//Shop upgrade inventory

        /// <summary>
        /// Randomly generate new upgrade and add to shop inventory
        /// </summary>
        public void GetNewUpgrade()
        {
            int num = new Random(Guid.NewGuid().GetHashCode()).Next(0, 101); // Out of 100 represent percentage.

            if (num <= 20) // 20% chance
            {
                upgrades.Add(new UpgradeAddDie()); // add die upgrade

            }else if (num >20 && num <= 45) // 25% chance
            {
                upgrades.Add(new UpgradeIncreaseMaxRoll()); // increase die max possible roll
            }else if (num >45 && num <= 70) // 25% chance
            {
                upgrades.Add(new UpgradeIncreaseMinRoll()); // increase minimum roll a die can have
            }else if(num >70 && num <= 85) // 15% chance
            {
                upgrades.Add(new UpgradeQuantityOfDieMulti()); // add a global multiplier based on current number of dice
            }
            else if (num >85 && num <= 100) // 15% chance
            {
                upgrades.Add(new UpgradeDuplicate()); // add a global multiplier based on the highest number duplicate result
            }

        }

        /// <summary>
        /// Update the max number of shop slots
        /// </summary>
        /// <param name="game">the game with shop in</param>
        public void NewShopslot(Rollgame game)
        {
            if(game.totalRolls % 10 == 0)// every 10 rolls
            {
                shopslots = 3 + (game.totalRolls/10);// increase shopslots
                MaxShopslots = 3 + (game.totalRolls / 10); // increase maxshopslots
            }
            else if(game.totalRolls % 5 == 0)// every 5 roll (when just shop refresh)
            {
                shopslots = MaxShopslots; // update shoplots to max shoplots
            }
        }


        /// <summary>
        /// Change upgrades in shop (shop refresh)
        /// </summary>
        /// <param name="game">game class is in</param>
        public void RefreshUpgardes(Rollgame game) 
        {
            upgrades = new List<Upgrade>();// clear shop inventory
            for (int i = 0;i < shopslots; i++)// for each shopslot
            {
                GetNewUpgrade();// generate upgrade
            }
            calCost(game);// calculate costs for upgrades

        }

        /// <summary>
        /// Update upgrade costs
        /// </summary>
        /// <param name="game">game class is in</param>
        public void calCost(Rollgame game)
        {
            foreach(Upgrade upgrade in upgrades)// for each upgrade in shop
            {
                upgrade.CalcCost(game.HighestScore,game.totalRolls);// update cost
            }
        }

        /// <summary>
        /// Called after each player roll
        /// </summary>
        /// <param name="game">game class is in</param>
        public void UpdateShop(Rollgame game)
        {
            NewShopslot(game);// update shoplots

            if(game.totalRolls % 5 == 0)// every 5 rolls
            {
                // Refresh shop

                Console.WriteLine("*** Refreshed shop ***");

                RefreshUpgardes(game);
            }

        }

        /// <summary>
        /// Get number of rolls until shop refresh
        /// </summary>
        /// <param name="game">game class is in</param>
        /// <returns>number of rolls left until shop refresh</returns>
        public int refreshRollsleft(Rollgame game)
        {
            if(game.totalRolls % 5 == 0)
            {
                return 5;

            }
            else
            {
                return 5-game.totalRolls % 5;
            }
        }

        /// <summary>
        /// Output the shop to user
        /// </summary>
        /// <param name="game">the game the class is in</param>
        public void OutputShop(Rollgame game)
        {
            // output shop

            Console.WriteLine("\n\n##################    Shop    ##################\n\n\n");

            Console.WriteLine(" 0 - EXIT      Current Score ({1})      Shop refresh in {0}", refreshRollsleft(game),game.Score);// output exit button, current score, and rolls until shop refresh
            for(int i = 0; i < shopslots; i++)// for each upgrade in shop inventory
            {
                Console.WriteLine("\n  {0} - {1}      :       Cost( {2} )", i+1, upgrades[i].Name, upgrades[i].Cost);// output upgrade number,name and cost
                Console.WriteLine("     ---     {0}     ---\n", upgrades[i].description);// description of upgrade
            }
            Console.WriteLine("\n\n\n##############################################\n");
        }

    }
}
