using System;

namespace OOP_Dice_Games
{
    /// <summary>
    /// Die class
    /// function to roll die and variable to store result
    /// </summary>
    internal class Die
    {
        // Variable
        private int _Minroll = 1; // min value the die can roll

        private int _Maxroll = 7; // 1 higher than the max value a die can roll

        private string _name = "Die"; // the die's name

        private int _Value; // the stored value from a roll


        // Properties
        public int Value { get { return _Value; } set { _Value = value; } }
        public string name { get { return _name; } set { _name = value; } }

        public int Minroll { get { return _Minroll; } set { _Minroll = value; } }
        public int Maxroll { get { return _Maxroll; } set { _Maxroll = value; } }

        /// <summary>
        /// Run of die creation
        /// </summary>
        /// <param name="NumberOfsides">integer (max-value a die can roll)</param>
        /// <param name="newname">The die's name</param>
        public Die(int NumberOfsides,string newname)
        {
            Maxroll = NumberOfsides+1; // update max roll
            name = newname;// update name
        } 

        /// <summary>
        /// Update value to new random number within limits of max-roll and min-roll
        /// </summary>
        public void Roll()
        {
            Value = new Random(Guid.NewGuid().GetHashCode()).Next(Minroll, Maxroll);// get new random number
        }
    }
}
