using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class BettingGame:Game
    {
        //Change path for each game otherwise it will overide the file
        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\BettingLeaderboard.txt";
        }

        private int _money;


        private int betType;
        private int betAmount;
        private int betNumber;
        public int Money { get { return _money; } set { _money = value; } }
        private bool GameLoopCheck = true;
        private void SetStartingMoney()
        {
            Money = 100;
        }

        private void SinglesOdds()
        {
            Console.WriteLine("\nBetting on a specific number to show up is 2/1 ($100 = $200)");
            Console.WriteLine("With a 4x bonus if a double of your selected number\n");
        }

        private void DoublesOdds()
        {
            Console.WriteLine("\nBetting on any double is 5/1 ($100 = $500)");
            Console.WriteLine("Betting on a specific double is 20/1 ($100 = $2000)\n");
        }

        private void ShowOdds()
        {
            SinglesOdds();

            DoublesOdds();
        }

        public void GetBetAmount()
        {
            Console.WriteLine("\nCurrent monet = ${0}", Money);
            Console.WriteLine("\nPlease input betting amount:");
            var tempBetAmount = Console.ReadLine();
            
            bool IsDouble =  Int32.TryParse(tempBetAmount, out betAmount);

            if(IsDouble == false || betAmount > Money || betAmount < 1)
            {
                Console.WriteLine("\n{0} is an invalid bet.", betAmount);
                GetBetAmount();
            }
            else
            {
                Money -= betAmount;
            }
        }

        public void GetBetType()
        {
            Console.WriteLine("\nEnter type: \n| Single number (1) | Any Double (2) | A specific Double (3) |");

            var num = Console.ReadLine();


            bool CheckInt = int.TryParse(num, out betType);

            if (CheckInt == false || betType < 1 || betType > 3)
            {
                Console.WriteLine("{0} is an invalid input",betType);
                GetBetType();
            }


        }

        public void GetBetNumber()
        {
            if (betType == 1)
            {
                Console.WriteLine("Enter number (1-6)");

                int InpNumber = CheckInputInt(Console.ReadLine());

                while (InpNumber < 1 || InpNumber > 6)
                {
                    InpNumber = CheckInputInt(Console.ReadLine());
                }

                betNumber = InpNumber;

                Console.WriteLine("You will win if any die has the number {0}", InpNumber);

                RollDice();

                if (BetNumberCheck())
                {
                    AddWinning(2);
                }
                else
                {
                    Loser();
                }
            }
            else if(betType == 2)
            {
                // check if double
                RollDice();

                if (DiceDoubleCheck())
                {
                    AddWinning(5);
                }
                else
                {
                    Loser() ;
                }
            }
            else if(betType == 3)
            {
                Console.WriteLine("Enter number (1-6)");

                int InpNumber = CheckInputInt(Console.ReadLine());

                while (InpNumber < 1 || InpNumber > 6)
                {
                    InpNumber = CheckInputInt(Console.ReadLine());
                }
                betNumber = InpNumber;
                Console.WriteLine("You will win if 2 die have the number {0}", InpNumber);

                RollDice();

                if(BetNumberCheck() && DiceDoubleCheck()) 
                {
                    AddWinning(20);
                }
                else
                {
                    Loser();
                }
            }
        
        }

        public bool DiceDoubleCheck()
        {
            List<int> dice = GetDieValue();

            if (dice[0] == dice[1])
            {
                return true;
            }else { return false; }
            
        }

        public bool BetNumberCheck()
        {
            List<int> dice = GetDieValue();

            foreach(int die in dice)
            {
                if(die == betNumber) return true;
            }
            return false;
        }

        public void AddWinning(int multi)
        {
            Console.WriteLine("You won.");

            Money += betAmount * multi;

            Console.WriteLine("You won {0}. Your new balance is {1}",betAmount*multi,Money);
        }

        public void Loser()
        {
            Console.WriteLine("You lost your bet");

            if (BankruptCheck())
            {
                Console.WriteLine("You have lost all money.");
                GameLoopCheck = false;
            }
        }
        public bool BankruptCheck()
        {
            if (Money <= 0) { return true; } else {  return false; }
        }




        public override void GameSetup()
        {
            SetStartingMoney();

            NewDie(6, 2);

            ShowOdds();

        }

        public void ContinueCheck()
        {
            Console.WriteLine("Do you want to continue y/n?");

            string response = Console.ReadLine();

            while(response != "y" && response != "n")
            {
                Console.WriteLine("invalid input try again:");
                response = Console.ReadLine();
            }

            if(response == "n" || response == "N")
            {
                GameLoopCheck = false;

            }
        }
        public void GameLoop()
        {
            GetBetAmount();

            GetBetType();

            GetBetNumber();
            if (GameLoopCheck) 
            {
            ContinueCheck();            
            }



        }

        public BettingGame()
        {

            // Change leaderboard path
            UpdateFilePath();

            //ClearLeaderboard();

            // Gameplay

            GameSetup();

            while (GameLoopCheck)
            {
                GameLoop();
            }

            GameOver(Money);



            

            
        }
    }
}
