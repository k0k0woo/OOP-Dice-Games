using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class SevensOut:Game
    {
        public int DieTotal;
        public int score;

        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\SevensOut.txt";
        }

        public override void GameSetup()
        {
            NewDie(6, 2);
            UpdateFilePath();
        }

        public bool CheckIfSeven(int total)
        {

            if (total == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DoubleCheck()
        {
            List<int> die = GetDieValue();

            if (die[0] == die[1])
            {
                return true;
            }
            else { return false; }
        }
        public void Gameloop()
        {
            RollDice(true);
            
            DieTotal = GetDieValue().Sum();
            Console.WriteLine("The Total = {0}", DieTotal);
            if (CheckIfSeven(DieTotal) == false)
            {
                if (DoubleCheck() == true)
                {
                    score += DieTotal * 2;
                }
                else
                {
                    score += DieTotal;
                }
                Console.WriteLine("Your new score {0}", score);
            }
            else
            {
                GameOver(score);
            }
        }
        

        public void Start()
        { 
            while (CheckIfSeven(DieTotal) == false)
            {
                Gameloop();
                Console.WriteLine("\n\nPress key to continue.....");
                Console.ReadKey();
            }
        }
        public SevensOut()
        {
            GameSetup();

        }
    }
}
