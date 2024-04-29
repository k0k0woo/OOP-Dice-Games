using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class UpgradeShop
    {
        public int shopslots = 3;
        public List<Upgrade> upgrades = new List<Upgrade>();

        public void GetNewUpgrade()
        {
            int num = new Random(Guid.NewGuid().GetHashCode()).Next(0, 101); // Out of 100 represent percentage.

            if (num <= 20)
            {
                upgrades.Add(new UpgradeAddDie());

            }else if (num >20 && num <= 45)
            {
                upgrades.Add(new UpgradeIncreaseMaxRoll());
            }else if (num >45 && num <= 70)
            {
                upgrades.Add(new UpgradeIncreaseMinRoll());
            }else if(num >70 && num <= 85)
            {
                upgrades.Add(new UpgradeQuantityOfDieMulti());
            }
            else if (num >85 && num <= 100)
            {
                upgrades.Add(new UpgradeDuplicate());
            }

        }

        public void NewShopslot(Rollgame game)
        {
            if(game.totalRolls % 10 == 0)
            {
                shopslots = 3 + (game.totalRolls/10);
            }
        }

        public void RefreshUpgardes(Rollgame game) 
        {
            upgrades = new List<Upgrade>();
            for(int i = 0;i < shopslots; i++)
            {
                GetNewUpgrade();
            }
            calCost(game);

        }

        public void calCost(Rollgame game)
        {
            foreach(Upgrade upgrade in upgrades)
            {
                upgrade.CalcCost(game.HighestScore,game.totalRolls);
            }
        }

        public void UpdateShop(Rollgame game)
        {
            NewShopslot(game);

            if(game.totalRolls % 5 == 0)
            {
                Console.WriteLine("*** Refreshed shop ***");
                RefreshUpgardes(game);
            }

        }

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
        public void OutputShop(Rollgame game)
        {
            Console.WriteLine("\n\n##################    Shop    ##################\n\n\n");
            Console.WriteLine(" 0 - EXIT      Current Score ({1})      Shop refresh in {0}", refreshRollsleft(game),game.Score);
            for(int i = 0; i < shopslots; i++)
            {
                Console.WriteLine("\n  {0} - {1}      :       Cost( {2} )", i+1, upgrades[i].Name, upgrades[i].Cost);
                Console.WriteLine("     ---     {0}     ---\n", upgrades[i].description);
            }
            Console.WriteLine("\n\n\n##############################################\n");
        }

    }
}
