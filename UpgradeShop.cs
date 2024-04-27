using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class UpgradeShop
    {
        private int shopslots = 3;
        public List<Upgrade> upgrades = new List<Upgrade>();

        public void GetNewUpgrade()
        {
            int num = new Random(Guid.NewGuid().GetHashCode()).Next(0, 2); ;

            if (num == 0)
            {
                upgrades.Add(new UpgradeAddDie());

            }else if (num == 1)
            {
                upgrades.Add(new UpgradeIncreaseMaxRoll());
            }

        }

        public void NewShopslot(Rollgame game)
        {
            if(game.totalRolls % 10 == 0)
            {
                shopslots = 3 + game.totalRolls % 10;
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
                RefreshUpgardes(game);
            }

        }


        public void OutputShop(Rollgame game)
        {
            Console.WriteLine("\n\n##################    Shop    ##################\n\n\n");
            Console.WriteLine(" 0 - EXIT                 Shop refresh in {0}", game.totalRolls % 5);
            for(int i = 0; i < shopslots; i++)
            {
                Console.WriteLine("\n  {0} - {1}      :       Cost( {2} )", i, upgrades[i].Name, upgrades[i].Cost);
                Console.WriteLine("     ---     {0}     ---\n", upgrades[i].description);
            }
            Console.WriteLine("\n\n\n##############################################\n");
        }

    }
}
