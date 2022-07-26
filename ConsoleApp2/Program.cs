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
                            Console.WriteLine("1 - Bet a new fate - 3zł [{0}/8]", tempts + 1);
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
            throw new NotImplementedException();
        }

        private static int[] BetFate()
        {
            throw new NotImplementedException();
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
