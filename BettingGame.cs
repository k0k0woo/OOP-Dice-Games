using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    /// <summary>
    /// A betting game child class (base class = Game)
    /// 
    /// player aims to get as much money as possible by betting on the result of 2 dice.
    /// </summary>
    internal class BettingGame:Game
    {
        /// <summary>
        /// Update leaderboard path
        /// </summary>
        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\BettingLeaderboard.txt";
        }

        // Variables

        private int _money;
        private int betType;
        private int betAmount;
        private int betNumber;
        private bool GameLoopCheck = true;

        // Properties
        public int Money { get { return _money; } set { _money = value; } }

        /// <summary>
        /// Allocate the starting amount of money
        /// </summary>
        private void SetStartingMoney()
        {
            Money = 100;// set money to 100
        }


        /// <summary>
        /// Output the odds and return of singles betting
        /// </summary>
        private void SinglesOdds()
        {
            Console.WriteLine("\nBetting on a specific number to show up is 2/1 ($100 = $200)");
        }

        /// <summary>
        /// Output the odds and returns of betting that there will be a double
        /// </summary>
        private void DoublesOdds()
        {
            Console.WriteLine("\nBetting on any double is 5/1 ($100 = $500)");
            Console.WriteLine("Betting on a specific double is 20/1 ($100 = $2000)\n");
        }

        /// <summary>
        /// Call to show all odds
        /// </summary>
        private void ShowOdds()
        {
            SinglesOdds();

            DoublesOdds();
        }


        /// <summary>
        /// Tell user current balance and ask for a bet input
        /// </summary>
        public void GetBetAmount()
        {
            Console.WriteLine("\nCurrent monet = ${0}", Money);// tell user current balance
            Console.WriteLine("\nPlease input betting amount:");
            var tempBetAmount = Console.ReadLine();// get and store input
            
            bool IsDouble =  Int32.TryParse(tempBetAmount, out betAmount);// validate input is integer

            if(IsDouble == false || betAmount > Money || betAmount < 1)// validate input and ensure user can afford bet
            {
                Console.WriteLine("\n{0} is an invalid bet.", betAmount);// tell user invalid input
                GetBetAmount();// call recursively
            }
            else// if valid input
            {
                Money -= betAmount;// balance loses bet amount
            }
        }

        /// <summary>
        /// Get type of bet user wants
        /// </summary>
        public void GetBetType()
        {
            Console.WriteLine("\nEnter type: \n| Single number (1) | Any Double (2) | A specific Double (3) |");

            var num = Console.ReadLine();


            bool CheckInt = int.TryParse(num, out betType);// output to betType

            if (CheckInt == false || betType < 1 || betType > 3) // Ensure user inputted integer that is 1,2 or 3
            {
                Console.WriteLine("{0} is an invalid input",betType);
                GetBetType();// call recursively
            }


        }

        /// <summary>
        /// Depending on bet type will ask for desired die result for bet then roll die and payout result. 
        /// </summary>
        public void GetBetNumber()
        {
            if (betType == 1) // if bet on singles
            {
                // Ask for desired die result
                Console.WriteLine("Enter number (1-6)");

                int InpNumber = CheckInputInt(Console.ReadLine());

                while (InpNumber < 1 || InpNumber > 6)
                {
                    InpNumber = CheckInputInt(Console.ReadLine());
                }

                betNumber = InpNumber; // save to betnumber

                Console.WriteLine("You will win if any die has the number {0}", InpNumber); // send confirmation message to user

                RollDice(true); // roll dice with display

                if (BetNumberCheck()) // if number guessed on a die
                {
                    AddWinning(2);// add bet to balance with x2.0 multiplier
                }
                else// if bet lost
                {
                    Loser();// call loser
                }
            }
            else if(betType == 2)// if any double bet selected
            {
                
                RollDice(true); // roll dice

                if (DiceDoubleCheck())// if dice have same result
                {
                    AddWinning(5); // add winings to balance x5.0 multiplier
                }
                else // if no double
                {
                    Loser() ;// call loser
                }
            }
            else if(betType == 3)// if a specific double predicted
            {
                // get the desired number that will be double
                Console.WriteLine("Enter number (1-6)");

                int InpNumber = CheckInputInt(Console.ReadLine());

                while (InpNumber < 1 || InpNumber > 6)
                {
                    InpNumber = CheckInputInt(Console.ReadLine());
                }

                
                betNumber = InpNumber;// save to betnumber


                Console.WriteLine("You will win if 2 die have the number {0}", InpNumber);

                RollDice(true);// roll dice

                if(BetNumberCheck() && DiceDoubleCheck()) // if die roll is betnumber and both dice have same result
                {
                    AddWinning(20);// add winings to balance x20.0 multiplier
                }
                else // if lost bet
                {
                    Loser(); // call loser
                }
            }
        
        }

        /// <summary>
        /// Checks if die1 and die2 have same result
        /// </summary>
        /// <returns>true if dice are doubles</returns>
        public bool DiceDoubleCheck()
        {
            List<int> dice = GetDieValue();// get die

            if (dice[0] == dice[1])// if die values the same
            {
                return true;// return true
            }else { return false; }
            
        }

        /// <summary>
        /// Check if any die result equal prediction
        /// </summary>
        /// <returns>true if prediction correct</returns>
        public bool BetNumberCheck()
        {
            List<int> dice = GetDieValue();// get die value

            foreach(int die in dice)// for each die
            {
                if(die == betNumber) return true;// if value = bet prediction then return true
            }
            return false;
        }

        /// <summary>
        /// Add bet amount to money
        /// </summary>
        /// <param name="multi">The multiple of betAmount</param>
        public void AddWinning(int multi)
        {
            Console.WriteLine("You won.");

            Money += betAmount * multi;// add bet amount X multi

            Console.WriteLine("You won {0}. Your new balance is {1}",betAmount*multi,Money);// tell user new balance and how much they won from the bet
        }

        /// <summary>
        /// If a bet is lost tell user and ensure not bankrupt
        /// </summary>
        public void Loser()
        {
            Console.WriteLine("You lost your bet");// tell user

            if (BankruptCheck())// check if money is 0
            {
                Console.WriteLine("You have lost all money.");// tell user
                GameLoopCheck = false;// break game loop
            }
        }

        /// <summary>
        /// Check if player is bankrupt
        /// </summary>
        /// <returns>true if player money is 0</returns>
        public bool BankruptCheck()
        {
            if (Money <= 0) { return true; } else {  return false; }
        }



        /// <summary>
        /// Call on game start
        /// </summary>
        public override void GameSetup()
        {
            SetStartingMoney(); // set money

            NewDie(6, 2);// add dice to game

            ShowOdds();// tell user betting odds

        }

        /// <summary>
        /// Ask if user wants to bank score or risk for another bet
        /// </summary>
        public void ContinueCheck()
        {
            // Ask if user wants to bet again

            Console.WriteLine("Do you want to continue y/n?");

            string response = Console.ReadLine();

            while(response != "y" && response != "n")
            {
                Console.WriteLine("invalid input try again:");
                response = Console.ReadLine();
            }

            
            if(response == "n")// if user wants to save
            {
                GameLoopCheck = false;// break game loop

            }
        }
       
        /// <summary>
        /// Call each (round)
        /// </summary>
        public void GameLoop()
        {
            GetBetAmount();// get bet amount

            GetBetType();// get type of bet

            GetBetNumber();// get prediction number and roll

            if (GameLoopCheck) // if game still continues (user not bankrupt)
            {
            ContinueCheck();// ask if they want to bank the score       
            }



        }


        /// <summary>
        /// Runs on class creation
        /// </summary>
        public BettingGame()
        {

            
            UpdateFilePath();// Change leaderboard path

            // ClearLeaderboard();

            // Gameplay

            GameSetup();// call before 'game started'

            // create the game loop

            while (GameLoopCheck)
            {
                GameLoop();
            }

            GameOver(Money);// Saves score to leaderboard and outputs leaderboard



            

            
        }
    }
}
