using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryAppV2
{
    class Program
    {
        public static void Lotto(int whiteAmount, int whiteStart, int whiteEnd, int redEnd = 0, string redballName = "Redball")
        {
            /*Lotto method generates and prints the numbers for Powerball, Mega Millions, Lucky 4 Life, Keno, Pick 3, and Pick 4
             * whiteAmount - the amount of whiteballs that need to be drawn
             * whiteStart - the beginning value for the whiteball number range
             * whiteEnd - the end value for the whiteball number range
             * redEnd - the end number for the redball range.  Set to zero if there is no redball drawn.  (redStart doesn't exist because the redball range always starts at 1)
             * redballName - the name of the redball for a specific lottery.  (Powerball = Powerball, Mega Millions = Megaball, Lucky 4 Life = Luckyball
             */

            int[] whiteArray = new int[whiteAmount];
            int redball;
            var rnd = new Random();

            //For loop to generate numbers for the lottery.
            for (int i = 0; i < whiteAmount;)
            {
                int temp = rnd.Next(whiteStart, whiteEnd);
                if (whiteStart > 0 && whiteArray.Contains(temp) == false) //Weeds out repeating numbers.  For Powerball, Mega Millions, Lucky for Life, and Keno.  Ranges start at 1.
                {
                    whiteArray[i] = temp;
                    i++;
                }
                else if (whiteStart < 1) //Allows for repeating numbers.  For Pick 3 and Pick 4.  Ranges start at 0.
                {
                    whiteArray[i] = temp;
                    i++;
                }
            }

            //Sorting and printing the whiteballs
            Array.Sort(whiteArray);
            Console.Write("Your numbers: ");
            foreach (int number in whiteArray)
            {
                Console.Write($"{number} ");
            }

            //If statement used to print the redball for Powerball, Mega Millions, and Lucky 4 Life.  Zero prevents Pick 3, Pick4, and Keno from printing a redball
            if (redEnd > 0)
            {
                redball = rnd.Next(1, redEnd);
                Console.Write($"{redballName}: {redball}");
            }

        }
        static void Main(string[] args)
        {

            //Method tester
            //while (true)
            //{
            //    Lotto(5, 1, 70, 27, "Powerball");
            //    Console.WriteLine("\n");
            //    Task.Delay(3000).Wait();
            //}

            bool againBool = true, lottoBool = false;
            int selection = 0;
            string yesNo = "";

            //Program is wrapped in a while loop to let the user generate as many sets of numbers as they want.
            while (againBool == true)
            {
                //Asking the user to select a game.  Uses a while loop and TryParse to make sure user inputs an integer between 1 and 6
                while (lottoBool == false || selection < 1 || selection > 6)
                {
                    Console.Write("\n1 Powerball \n2 MegaMillions \n3 Keno \n4 Lucky4Life \n5 Pick 3 \n6 Pick 4 \n\nPlease input an integer from 1 - 6 to select the lottery you would like to play: ");
                    lottoBool = int.TryParse(Console.ReadLine(), out selection);
                    Console.WriteLine("\n");
                }

                //Switch statement to select the game the user wants to play
                switch (selection)
                {
                    case 1: //Powerball
                        Lotto(5, 1, 70, 27, "Powerball");
                        break;
                    case 2: //Mega Millions
                        Lotto(5, 1, 71, 26, "Megaball");
                        break;
                    case 3: //Keno
                        int amount = 0;
                        bool kenoBool = false;
                        while (kenoBool == false || amount < 1 || amount > 10)//Get the user to input an integer between 1 and 10 for the amount of Keno numbers to play
                        {
                            Console.Write("Please enter an integer between 1 - 10 for how many Keno numbers you would like to play: ");
                            kenoBool = int.TryParse(Console.ReadLine(), out amount);
                        }
                        Lotto(amount, 1, 80);
                        break;
                    case 4: //Lucky For Life
                        Lotto(5, 1, 49, 19, "Luckyball");
                        break;
                    case 5: //Pick 3
                        Lotto(3, 0, 10);
                        break;
                    case 6: //Pick 4
                        Lotto(4, 0, 10);
                        break;
                }

                //Asks user if they would like to generate more lotto numbers.  Uses a while loop to get the correct Y or N input.
                while (yesNo != "N" && yesNo != "Y")
                {
                    Console.Write("\n\n\nWould you like to generate more numbers? Input 'Y' for yes or 'N' for no: ");
                    yesNo = Console.ReadLine().ToUpper().TrimStart();
                    Console.WriteLine("\n");


                    if (yesNo == "N")
                    {
                        againBool = false;
                    }
                    else if (yesNo == "Y")
                    {
                        lottoBool = false;
                        selection = 0;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }

                //Resets the yesNo variable so the program will re-enter the yesNo while loop
                yesNo = "";

            }
        }
    }
}
