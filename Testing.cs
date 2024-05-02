using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class Testing
    {

        public void ValueCheck(int Val,int LowVal,int HighVal)
        {
                Debug.Assert(Val < LowVal || Val > HighVal,"Value incorrect.");
        }
    }
}
