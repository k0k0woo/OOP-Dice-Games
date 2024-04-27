﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Games
{
    internal class Rollgame:Game
    {

        public Int16 rollsLeft = 100;

        public Int64 Score = 0;
        public Int64 HighestScore = 0;

        public Int16 totalRolls = 0;

        public UpgradeShop shop = new UpgradeShop();


        private void PlayerRoll()
        {
            if (CheckRollsValid())
            {
                RollDice(false);
                UpdateScore(GetDiceTotal());
                DisplayDice();
                rollsLeft -= 1;
                totalRolls += 1;

            }
        }

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


        private void DisplayRollsAndScore()
        {
            Console.WriteLine("You have {0} rolls left | Current Score{1}", rollsLeft, Score);
        }

        private void DisplayUserActions()
        {
            Console.WriteLine("1-Roll | 2-Shop | 3-Upgrades | 4-Dice\n");
        }
        private Int64 GetDiceTotal()
        {
            Int64 total = 0;
            foreach (Die die in diceList)
            {
                total += die.Value;
            }

            return total;
        }

        private void UpdateScore(Int64 scoreMod)
        {
            Score += scoreMod;

            if (Score > HighestScore)
            {
                HighestScore = Score;
            }
        }

        public void DisplayDice()
        {
            foreach(Die die in diceList)
            {
                Console.WriteLine("{0}\n - - -\n|     |\n|  {1}  |\n|     |\n - - -\nMin:{2}    Max:{3}\n", die.name, die.Value,die.Minroll,die.Maxroll-1);
            }

        }
        public override void UpdateFilePath()
        {
            path = @"C:\Users\Joshi\Documents\RollGameLeaderboard.txt";
        }

        public void PlayerTurn()
        {
            DisplayRollsAndScore();
            DisplayUserActions();
            

            int playerAction = CheckInputInt(Console.ReadLine());

            while(playerAction < 1 || playerAction > 4) 
            {
                Console.WriteLine("Invalid input");
                playerAction = CheckInputInt(Console.ReadLine());
            }


            if(playerAction == 1)
            { 
                PlayerRoll();
                shop.UpdateShop(this);
            }else if(playerAction == 2)
            {
                ShopOpen();
            }
        }

        public void setUpShop()
        {
            shop.RefreshUpgardes(this);

        }

        public void ShopOpen()
        {
            shop.OutputShop(this);

            var userInp = Console.ReadLine();

            int intUserInp = CheckInputInt(userInp);

            while (intUserInp > shop.shopslots || intUserInp < 0)
            {
                intUserInp = CheckInputInt(Console.ReadLine());
            }

            if (intUserInp == 0)
            {
                // Exit shop
            }

            else
            {
                Upgrade upgrade = shop.upgrades[intUserInp-1];

                if(Score > upgrade.Cost)
                {
                    Score -= upgrade.Cost;
                    upgrade.ApplyUpgrade(this);
                }else if (Score < upgrade.Cost)
                {
                    Console.WriteLine("\nNot enough score!");
                }
            }
        }


        public override void GameSetup()
        {
            Console.WriteLine("Game set up....\n");

            NewDie(6, 1);


            setUpShop();
            




        }



        public Rollgame()
        {
            UpdateFilePath();

            GameSetup();

            while(CheckRollsValid())
            {
            PlayerTurn();
            }
            GameOver(Score);

        }
    }
}
