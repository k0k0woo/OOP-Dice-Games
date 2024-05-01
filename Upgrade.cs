using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Generic Upgrade class
    /// </summary>
    abstract class Upgrade
    {
        // Variables
        private Int64 _cost;
        public Int64 Cost { get { return _cost; } set { _cost = value; } }
        public double costMulti = 0.001D;
        public int BaseCost = 10;
        public decimal GlobalMulti = 0M;        
        private string _description;
        private string _name;

        // Properties
        public string Name { get { return _name; } set { _name = value; } }
        public string description { get { return _description; } set { _description = value; } }

        /// <summary>
        /// Used for function of upgrade
        /// e.g add 1 max roll to die
        /// </summary>
        /// <param name="game">The game being called from</param>
        abstract public void ApplyUpgrade(Rollgame game);

        /// <summary>
        /// Set upgrade name
        /// </summary>
        abstract public void SetName();

        /// <summary>
        /// Set upgrade description
        /// </summary>
        abstract public void SetDescription();

        /// <summary>
        /// Set upgrade base cost,cost multi and global multi if needed.
        /// </summary>
        abstract public void SetCost();

        /// <summary>
        /// Calculate upgrade cost for the shop
        /// </summary>
        /// <param name="HighScore">Current highest score</param>
        /// <param name="totalRolls">Total rolls completed in the game</param>
        public void CalcCost(Int64 HighScore, Int16 totalRolls)
        {
            // Cost = base value then scales with highscore and totalrolls
            Cost = Convert.ToInt64(BaseCost+Math.Pow(costMulti * HighScore/2.5 * totalRolls,1.5));
        }
        
        /// <summary>
        /// Call on upgrade creation
        /// </summary>
        public Upgrade()
        {
            SetName(); // set name
            SetDescription();// set description
            SetCost(); // set cost associated values

        }
        
    }
}
