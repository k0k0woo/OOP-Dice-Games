using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    /// <summary>
    /// The Rollgame child class (parent = Game)
    /// 
    /// player has limited number of rolls and aim is to get as high score in the limit of 100 rolls.
    /// 
    /// player can use the current score to buy upgrades such as more die, multipliers or die upgrades
    /// 
    /// </summary>
    internal class Rollgame:Game
    {
        // Variables

        public Int16 rollsLeft = 100;

        public Int64 Score = 0;
        public Int64 HighestScore = 0;

        public Int16 totalRolls = 0;

        public UpgradeShop shop = new UpgradeShop(); // create upgrade shop

        public List<Upgrade> GlobalUpgrades = new List<Upgrade>(); // upgrades for global modifiers


        /// <summary>
        /// If player roll action selected roll the dice
        /// </summary>
        private void PlayerRoll()
        {
            if (CheckRollsValid())// if rolls are still left
            {
                RollDice(false);// roll the dice without display
                UpdateScore(GetDiceTotal()); // update score with die roll total
                DisplayDice();// display dice
                rollsLeft -= 1;// decrement from rolls left
                totalRolls += 1;// increment total rolls

            }
        }

        /// <summary>
        /// Check if rolls left > 0
        /// </summary>
        /// <returns></returns>
        private bool CheckRollsValid()
        {
            if (rollsLeft > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// display the rolls left and current user score
        /// </summary>
        private void DisplayRollsAndScore()
        {
            Console.WriteLine("You have {0} rolls left | Current Score = {1}", rollsLeft, Score);
        }

        /// <summary>
        /// Display user actions
        /// </summary>
        private void DisplayUserActions()
        {
            Console.WriteLine("1-Roll | 2-Shop | 3-Upgrades | 4-Dice | 5-Quit\n");
        }

        /// <summary>
        /// Gets the dice total value
        /// </summary>
        /// <returns></returns>
        private Int64 GetDiceTotal()
        {
            Int64 total = 0;
            List<int> values = new List<int>(); // die values
            decimal dieQuantityMuli = 1.000M;// die qauntity multiplier
            decimal duplicateResultMulti = 1.000M;// die duplicate multiplier

            foreach (Die die in diceList)// for each die
            {
                values.Add( die.Value );// store value in list
                total += die.Value;// add to total
            }

            // Get the duplicate with the highest count
            IGrouping<int, int> duplicate =
              values.GroupBy(n => n)
              .OrderByDescending(g => g.Count())
              .First();
            

            foreach (Upgrade upgrade in GlobalUpgrades)// for each global upgrade in game
            {
                if(upgrade.Name == "Quantity die Multi") // if upgrade is quantity
                {
                    if (diceList.Count > 1)// and more than 1 die
                    {
                        dieQuantityMuli += upgrade.GlobalMulti; // add multi to total quantity multi
                    }
                }else if(upgrade.Name == "Duplicate multi")// if upgrade is duplicate
                {
                    if (duplicate.Count() > 1)// the result contains a duplicate (doubles or triples)
                    {
                        duplicateResultMulti += upgrade.GlobalMulti;// add multi to total duplicate multi
                    }
                }
            }

            // To prevent multi from being an exponential

            decimal tempQunatmulti = dieQuantityMuli;// create temp var of multi
            decimal tempDupemulti = duplicateResultMulti;// create temp var of multi

            // Quantity die multiplier 

            foreach (Die die in diceList) // for each die
            {
                dieQuantityMuli = Math.Round(dieQuantityMuli * tempQunatmulti,3);// multiplier X temp multiplier
            }

            // Duplicate result multipler

            for(int i = 0; i < duplicate.Count(); i++)// for each result of highest duplicate (2 if doubles)
            {
                duplicateResultMulti =Math.Round(duplicateResultMulti * tempDupemulti,3);// multiplier X temp multiplier
            }

            // Get total multiplier

            decimal globalMulti = dieQuantityMuli * duplicateResultMulti;

            // Round all values
            Math.Round(globalMulti, 3);
            Math.Round(duplicateResultMulti, 3);
            Math.Round(dieQuantityMuli, 3);

            // Output values to player
            Console.WriteLine("\n\nThe roll x (total quantity multi x total duplicateMulti) = Total");
            Console.WriteLine("{0} x ({1} x {2}) = {3}\n\n",total, dieQuantityMuli, duplicateResultMulti, Convert.ToInt64(total * globalMulti));

            // Return the total result with multiplier in effect
            return Convert.ToInt64(total * globalMulti);
        }

        /// <summary>
        /// Add score to variable
        /// </summary>
        /// <param name="scoreMod">amount to be added</param>
        private void UpdateScore(Int64 scoreMod)
        {
            Score += scoreMod; // add to score

            if (Score > HighestScore)// update highest score to keep cost scaling
            {
                HighestScore = Score;
            }
        }

        /// <summary>
        /// Output dice with min and max roll.
        /// </summary>
        public void DisplayDice()
        {
            Console.WriteLine("\n\n#######     DICE    #######\n\n");

            foreach (Die die in diceList)
            {
                Console.WriteLine("{0}\n - - -\n|     |\n|  {1}  |\n|     |\n - - -\nMin:{2}    Max:{3}\n", die.name, die.Value,die.Minroll,die.Maxroll-1);
            }
            Console.WriteLine("\n\n##############################\n\n");
        }

        /// <summary>
        /// Output the global upgrades count and total multiplier
        /// </summary>
        public void DisplayUpgrades()
        {
            int quantUPgrades = 0;
            decimal quantmulti = 1;
            int dupeUPgrades = 0;
            decimal dupemulti = 1;


            foreach (Upgrade upgrade in GlobalUpgrades)// for each upgrade
            {
                if (upgrade.Name == "Quantity die Multi")// if quantity
                {
                    quantUPgrades ++;// increment quant upgrade count
                    quantmulti = 1 + (quantUPgrades * upgrade.GlobalMulti);// calculate total multi
                }
                else if (upgrade.Name == "Duplicate multi")
                {

                        dupeUPgrades++;// increment duplicate upgrade count
                    dupemulti = 1 + (dupeUPgrades * upgrade.GlobalMulti);// calculate total multi

                }
            }

            // Output to player

            Console.WriteLine("\n############## GLOBAL UPGRADES ##############\n");

            Console.WriteLine("Quantity die upgrade count = {0} this is a {1} multiplier per die.", quantUPgrades, quantmulti);
            Console.WriteLine("Duplicate die upgrade count = {0} this is a {1} multiplier for a duplicate result.\n", dupeUPgrades, dupemulti);

            Console.WriteLine("\n#############################################\n\n");

        }

        /// <summary>
        /// Update leaderboard file path
        /// </summary>
        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\RollGameLeaderboard.txt";
        }

        /// <summary>
        /// Get player action and respond acordingly
        /// </summary>
        public void PlayerTurn()
        {
            // Output score and actions to user

            DisplayRollsAndScore();
            DisplayUserActions();
            
            // get users action

            int playerAction = CheckInputInt(Console.ReadLine());

            while(playerAction < 1 || playerAction > 5) 
            {
                Console.WriteLine("Invalid input");
                playerAction = CheckInputInt(Console.ReadLine());
            }


            if(playerAction == 1) // if action roll
            { 
                PlayerRoll();// call player roll
                shop.UpdateShop(this);// update shop
            }else if(playerAction == 2) // if open shop
            {
                ShopOpen();// call open shop
            }
            else if(playerAction == 3)// if open upgrades
            {
                DisplayUpgrades();// display upgrades

            }
            else if(playerAction == 4)// if display dice
            {
                DisplayDice(); // call display dice



            }else if(playerAction == 5)// if quit
            {
                rollsLeft = 0; // set rollsLeft to 0 (simulate game over)
            }

        }

        /// <summary>
        /// ensure upgrades are in shop at game start
        /// </summary>
        public void setUpShop()
        {
            shop.RefreshUpgardes(this);// refresh shop

        }

        /// <summary>
        /// Open shop for user to buy upgrades
        /// </summary>
        public void ShopOpen()
        {
            shop.OutputShop(this);// display shop

            // Get input

            var userInp = Console.ReadLine();

            int intUserInp = CheckInputInt(userInp);

            while (intUserInp > shop.shopslots || intUserInp < 0)
            {
                intUserInp = CheckInputInt(Console.ReadLine());
            }



            // If exit do nothing this will close the shop and start new player turn
            if (intUserInp == 0)
            {
                // Exit shop
            }

            else// if upgrade inputted
            {
                
                Upgrade upgrade = shop.upgrades[intUserInp-1];// get upgrade selected

                
                if(Score >= upgrade.Cost)// if player can afford upgrade
                {
                    Score -= upgrade.Cost;// minus cost from score

                    upgrade.ApplyUpgrade(this);// apply upgrade

                    shop.upgrades.Remove(upgrade);// remove from shop inventory

                    shop.shopslots--;// remove a shoplot

                }else if (Score < upgrade.Cost)// if player can't afford upgrdae
                {
                    Console.WriteLine("\nNot enough score!");// tell user
                }
            }
        }

        /// <summary>
        /// Call when game chosen
        /// </summary>
        public override void GameSetup()
        {
            Console.WriteLine("Game set up....\n");

            NewDie(6, 1); // generate starting dice


            setUpShop();// setup shop
            




        }


        /// <summary>
        /// Call on class creation
        /// </summary>
        public Rollgame()
        {
            UpdateFilePath();// update leaderboard file path

            GameSetup();// call game setup

            while(CheckRollsValid())// will rolls left
            {
            PlayerTurn();// call for a player turn
            }
            GameOver(Score);// when player out of rolls call game over with score as input

        }
    }
}
