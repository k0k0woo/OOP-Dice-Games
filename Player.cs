using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace OOP_Dice_Games
{
    internal class Player
    {
        public List<Die> diceList = new List<Die>();
        public bool RealPerson;
        public int points = 0;
        public string name = "";
        public bool HoldDie = false;

        public Player(List<Die> inputDieList,string newName,bool inputRealPerson) 
        {
            diceList = inputDieList;
            name = newName;
            RealPerson = inputRealPerson;

        }
        public void displayDice(Die die)
        {
            Console.WriteLine("\n\n{0}\n - - -\n|     |\n|  {1}  |\n|     |\n - - -\n", die.name, die.Value);
        }

        public void RollDice(bool hold)
        {
            int holdDieOf = whichMostCommon();
            foreach(Die dice in diceList)
            {
                if(hold)
                {
                    if(dice.Value != holdDieOf)
                    {
                        dice.Roll();
                        
                    }
                    
                    displayDice(dice);
                }
                else
                {
                    dice.Roll();
                    displayDice(dice);
                }


            }
        }


        public List<int> DiceValues()
        {
            List<int> values = new List<int>();
            foreach (Die dice in diceList)
            {
                values.Add(dice.Value);
            }
            return values;
        }

        public int HowMuchOfAKind()
        {
            IGrouping<int, int> duplicate =
              DiceValues().GroupBy(n => n)
              .OrderByDescending(g => g.Count())
              .First();

            return duplicate.Count();
        }

        public int whichMostCommon()
        {
            IGrouping<int, int> duplicate =
              DiceValues().GroupBy(n => n)
              .OrderByDescending(g => g.Count())
              .First();

            return duplicate.First();
        }

        public void PlayerHold(bool RealPersoncheck)
        {
            if (RealPersoncheck)
            {
                Console.WriteLine("\nDo you want to hold the 2-of-a-kind? y/n");
                string DieHold = Console.ReadLine().ToString();

                while (DieHold != "y" && DieHold != "n")
                {
                    Console.WriteLine("\nInvalid input....");
                    Console.WriteLine("\nDo you want to hold the 2-of-a-kind? y/n");
                    DieHold = Console.ReadLine().ToString();
                }

                if (DieHold == "y")
                {
                    HoldDie = true;
                }
                else
                {
                    HoldDie = false;
                }

            }
            else
            {
                HoldDie = true;
                Console.WriteLine("The bot held a 2-of-a-kind.");
            }
        }

        public void Playerturn(bool Player)
        {
            if (Player)
            {
                Console.WriteLine("\n{0}'s current points = {1}",name, points);
                RollDice(HoldDie);
                if(HowMuchOfAKind() > 1)
                {
                    Console.WriteLine("\n\nA {0}-of-a-kind is rolled",HowMuchOfAKind());

                    if (HowMuchOfAKind() == 2)
                    {
                        PlayerHold(RealPerson);
                    }
                    else
                    {
                        HoldDie = false;
                        points += (HowMuchOfAKind() - 2) * 3;
                    }
                }
                Console.WriteLine("\n{0}'s new points = {1}", name, points);

            }
            else
            {
                Console.WriteLine("\n{0}'s current points = {1}", name, points);
                RollDice(HoldDie);
                if (HowMuchOfAKind() > 1)
                {
                    Console.WriteLine("\n\nA {0}-of-a-kind is rolled", HowMuchOfAKind());

                    if (HowMuchOfAKind() == 2)
                    {
                        PlayerHold(RealPerson);
                    }
                    else
                    {
                        HoldDie = false;
                        points += (HowMuchOfAKind() - 2) * 3;
                    }
                }
                Console.WriteLine("\n{0}'s new points = {1}", name, points);
            }
        }
    }
}
