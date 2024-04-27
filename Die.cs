using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class Die
    {
        /// <summary>
        /// A Dice that will roll and store a random number
        /// </summary>


        // Variables
        private int _Minroll = 1;
        private int _Maxroll = 7;
        private string _name = "Die";
        private int _Value;


        // Properties
        public int Value { get { return _Value; } set { _Value = value; } }
        public string name { get { return _name; } set { _name = value; } }

        public int Minroll { get { return _Minroll; } set { _Minroll = value; } }
        public int Maxroll { get { return _Maxroll; } set { _Maxroll = value; } }

        // Init function
        public Die(int NumberOfsides,string newname)
        {
            Maxroll = NumberOfsides+1;
            name = newname;
        } 

        // Roll function
        public void Roll()
        {
            Value = new Random(Guid.NewGuid().GetHashCode()).Next(Minroll, Maxroll);
        }
    }
}
