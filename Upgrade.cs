using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    abstract class Upgrade
    {
        private Int64 _cost;
        public Int64 Cost { get { return _cost; } set { _cost = value; } }
        public double costMulti = 0.001D;
        public int BaseCost = 10;

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private string _description;
        public string description { get { return _description; } set { _description = value; } }


        abstract public void ApplyUpgrade(Rollgame game);

        abstract public void SetName();

        abstract public void SetDescription();

        abstract public void SetCost();


        public void CalcCost(Int64 HighScore, Int16 totalRolls)
        {
            Cost = Convert.ToInt64(BaseCost+Math.Pow(costMulti * HighScore * totalRolls,2));
        }
        
        public Upgrade()
        {
            SetName();
            SetDescription();
            SetCost();

        }
        
    }
}
