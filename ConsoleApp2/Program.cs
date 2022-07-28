using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoGame

{
    class Program
    {

        static int Cumulation;
        static int Start = 30;
        static Random random = new Random();

        static void Main(string[] args)
        { 
        
            int money = Start;
            int day = 0;  
            do
            {
                money = Start;
                day = 0;
                ConsoleKey choose;
                do
                {
                    Cumulation = random.Next(2, 37) * 1000000;
                    day++;
                    int tempts = 0;
                    List<int[]> coupon = new List<int[]>();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("DAY: {0}", day);
                        Console.WriteLine("Welcome to the Lotto Game, today is {0} zł to win", Cumulation);
                        Console.WriteLine("\nAccount Balance: {0}zł", money);
                        DisplayCoupon(coupon);

                        //Beginning of the Menu
                        if (money >= 3 && tempts < 8) //Because we can only bet 8 tempts in the game, no more
                        {
                            Console.WriteLine("\n1 - Bet a new fate - 3zł [{0}/8]", tempts + 1);
                        }
                        Console.WriteLine("2 - Check coupon - Drawing");
                        Console.WriteLine("3 = End Game");
                        //End of the Menu
                        choose = Console.ReadKey().Key; // Load parameter selecter by the user
                        if (choose == ConsoleKey.D1 && money >= 3 && tempts < 8)
                        {
                            coupon.Add(BetFate());
                            money -= 3;
                            tempts++;
                        }
                    } while (choose == ConsoleKey.D1);
                    Console.Clear();
                    if (coupon.Count > 0)
                    {
                        int win = check(coupon);
                        if(win > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nCongratulations! You won {0} in this drawing!", win);
                            Console.ResetColor();
                            money += win;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nUnfortunately! You don't win anything!. ", win);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You did not bet any tempts in this draw. ");
                    }
                    Console.WriteLine("Press enter to continue. ");
                    Console.ReadKey();


                } while (money >= 3 && choose != ConsoleKey.D3); //every coupon costs us 3 zl, so when we have less than 3 zł - it's over game, break the game by press 3
                Console.Clear();
                Console.WriteLine("Day {0}. \n End Game, Your score is: {1} zł", day, money - Start);
                Console.WriteLine("Enter - Repeat the game. ");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        private static int check(List<int[]> coupon)
        {
            int YourWon = 0;
            int[] drawnNumbers = new int[6];
            for (int i = 0; i < drawnNumbers.Length; i++)
            {
                int tempt = random.Next(1, 50);
                if(!drawnNumbers.Contains(tempt))
                {
                    drawnNumbers[i] = tempt; //If drawnNumbers doesn't contains tempt, then we switch to true to perform if, and add i value to drawnNumbers, otherwise we should subtract i value

                }
                else
                {
                    i--;
                }
            }
            Array.Sort(drawnNumbers); //sort array
            Console.WriteLine("Drawn numbers:");
            foreach (int number in drawnNumbers)
            {
                Console.Write(number + ", ");
            }
            int[] hitNumbers /* trafione */ = checkCoupon(coupon, drawnNumbers);
            int value = 0;
            

            Console.WriteLine();
            if (hitNumbers[0] > 0)
            {
                value = hitNumbers[0] * 24;
                Console.WriteLine("3 hit: {0} + {1} zł", hitNumbers[0], value);
                YourWon += value;
            }
            if (hitNumbers[1] > 0)
            {
                value = hitNumbers[1] * random.Next(100, 301); //random value from 100 to 300, value from index one multiplied by value 24
                Console.WriteLine("4 hit: {0} + {1} zł", hitNumbers[1], value);
                YourWon += value;
            }
            if (hitNumbers[2] > 0)
            {
                value = hitNumbers[2] * random.Next(4000, 8001); 
                Console.WriteLine("5 hit: {0} + {1} zł", hitNumbers[2], value);
                YourWon += value;
            }
            if (hitNumbers[3] > 0)
            {
                value = (hitNumbers[3] * Cumulation) / (hitNumbers[3] + random.Next(0, 5));
                Console.WriteLine("6 hit: {0} + {1} zł", hitNumbers[2], value);
                YourWon += value;
            }

            return YourWon;
        }

        private static int[] checkCoupon(List<int[]> coupon, int[] drawnNumbers)
        {
            int[] won = new int[4];
            int i = 0;
            Console.WriteLine("\n\nYOUR COUPON: ");
            foreach (int[] fate in coupon)
            {
                i++;
                Console.Write(i + ": ");
                int howManyHits = 0;
                foreach (int number in fate)
                {
                    if (drawnNumbers.Contains(number))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(number + ", ");
                        Console.ResetColor();
                        howManyHits++; //trafien
                    }
                    else
                    {
                        Console.Write(number + ", ");
                    }

                }
                switch(howManyHits)
                {
                    case 3:
                        won[0]++;
                        break;
                    case 4:
                        won[1]++;
                        break;
                    case 5:
                        won[2]++;
                        break;
                    case 6:
                        won[3]++;
                        break;

                }
                Console.WriteLine(" -Hit {0}/6", howManyHits);
            }
            
            return won;
        }
        
        private static int[] BetFate()
        {
            int[] numbers = new int[6];
            int number = -1;
            for (int i = 0; i < numbers.Length; i++)
            {
                number = -1;
                Console.Clear();
                Console.Write("Bet numbers: \n");
                foreach(int n in numbers)
                {
                    if (n > 0)
                    {
                        Console.WriteLine(n + ", ");
                    }
                }
                Console.WriteLine("\nChoose number from 1 to 49:");
                Console.Write("{0}/6: ", i + 1);
                bool correct = int.TryParse(Console.ReadLine(), out number);
                if(correct && number >=1 && number <= 49 && !numbers.Contains(number))
                {
                    numbers[i] = number;
                }
                else
                {
                    Console.WriteLine("Unfortunately, incorrect number. ");
                    i--;
                    Console.ReadKey();
                }

            }
            Array.Sort(numbers);
            return numbers;
        }
        
        private static void DisplayCoupon(List<int[]> coupon)
        {
            if(coupon.Count == 0)
            {
                Console.WriteLine("You didn't bet any tempts yet. Please proceed with that. ");
            }
            else
            {
                int i = 0;
                Console.WriteLine("\nYour Coupon: ");
                foreach (int[] fate in coupon)
                {
                    i++;
                    Console.WriteLine(i + ": ");
                    foreach(int number in fate)
                    {
                        Console.Write(number + ", ");
                    }
                    Console.WriteLine();
     
                }
            }
        }
    }
}
