/*
 * CustomCoinToss - A coin toss simulation program with 
 * customizable names for the coin faces and a 
 * customizable number of coins flipped.
 * Edited to allow for reset of Heads & Tails names
 * and random number of coins flipped.
 * Author: Jamie Pollock
 * Date: 4/12/2018
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace CustomCoinToss
{
    class CustomCoinToss
    {
        public static Random coinFace = new Random(); // Creates a random coin object for randomized results

        public static int numToss, h = 0, t = 0, difference;
        public static string head, tail, winner;
        public static string decoration = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";

        static void Main()
        {
            Title = "Custom Coin Toss"; // Makes the program title of the console window read as "Custom Coin Toss"
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Black;
            Clear();

            printHeader();

            coinsToFlip();
        }

        public static void coinsToFlip()
        {
            /* Gives the user the option to flip customized number of coins
             * If no number is chosen, gives the option for random number or option to quit the program
            */

            Random number = new Random();

            WriteLine("How many coins would you like to flip?");
            WriteLine("\nEnter a whole number,\n\"Random\" to flip a random number of coins between 1 and 100,000,\nor 0 or Q to quit.");
            string ans = ReadLine().ToUpper();

            if (ans == "RANDOM")
            {
                numToss = number.Next(1, 100000); // Creates a random number between 1 and 100,000
                WriteLine("\nWe'll flip " + numToss + " coins!");
            }
            else if (ans == "Q" || ans == "0")
            {
                exitConditions();
            }
            else if (!Int32.TryParse(ans, out numToss))
            {
                WriteLine("Please enter a whole number, \"Random\", or 0 or Q to quit");
                ans = ReadLine().ToUpper();
            }
            else
                numToss = Int32.Parse(ans);

            coinFaceNames();
        }

        public static void coinFaceNames()
        {
            /* Gives the user the option to use customized coin face names
             * If no face name is chosen, uses default Heads or Tails
            */

            string name;
            do
            {
                WriteLine(decoration);
                WriteLine("\nWhat are you flipping a coin to decide?\nIf nothing is entered, the default of \"Heads\" and \"Tails\" will be used.");
                Write("Heads:  ");
                head = ReadLine();
                if (head == "")
                {
                    head = "Heads";
                }
                Write("Tails:  ");
                tail = ReadLine();
                if (tail == "")
                {
                    tail = "Tails";
                }

                WriteLine("\nWould you like to change the coin names?");
                Write("Y/N >>  ");
                name = ReadLine();

                if (name.ToUpper() == "N")
                {
                    flipCoins();
                }
            }
            while (name.ToUpper() == "Y" && name.ToUpper() != "N");

            flipCoins();
        }

        public static void flipCoins()
        {
            // Method for proccessing the user-defined number of coin flips

            int flip, face;
            string faceName;

            do
            {
                for (flip = 1; flip <= numToss; flip++)
                {
                    face = coinFace.Next(1, 3);

                    switch (face)
                    {
                        // Displays the name of each coin face result along with the custom name
                        case 1:
                            faceName = "Heads";
                            if (head == "Heads")
                            {
                                WriteLine(faceName);
                            }
                            else
                                WriteLine(faceName + " - " + head);
                            h++;
                            break;
                        case 2:
                            faceName = "Tails";
                            if (tail == "Tails")
                            {
                                WriteLine(faceName);
                            }
                            else
                                WriteLine(faceName + " - " + tail);
                            t++;
                            break;
                    }
                }
            }
            while (flip < numToss);

            WriteLine(decoration);
            WriteLine("\n\nPress any key to get your winner!");
            ReadKey();

            winConditions();
        }

        public static void winConditions()
        {
            // Handles winning conditions

            if (h > t)
            {
                winner = head;
                difference = h - t;
            }
            if (h < t)
            {
                winner = tail;
                difference = t - h;
            }
            if (h == t)
            {
                winner = "Neither! It's a tie!";
                difference = 0;
            }

            Clear(); // Clears the console window to prevent clutter & scrolling

            WriteLine(head + " - " + h + " out of " + numToss);
            WriteLine(tail + " - " + t + " out of " + numToss);

            if (difference != 0)
            {
                WriteLine("There was a difference of " + difference + " coins between heads and tails!");
            }

            WriteLine(decoration);
            WriteLine("\n\nAnd the winner is...\n\t" + winner + "!");

            repeatConditions();
        }


        public static void repeatConditions()
        {
            // Repeat conditions with a call for error handling

            WriteLine(decoration);
            Write("\n\nWould you like to play again? Y/N >>  ");
            string replay = ReadLine().ToUpper();

            replay = handleInvalidEntry(replay);

            if (replay.ToUpper() == "Y")
            {
                // Reset variables
                h = 0;
                t = 0;

                Clear();
                Main();
            }
            else
                exitConditions();
        }

        public static void exitConditions()
        {
            // Called to close the program.

            WriteLine(decoration);
            WriteLine("Thanks for playing!\nPress any key to close the program.");
            ReadKey();
            Environment.Exit(1); // Closes program only after user presses a key.

        }

        public static string handleInvalidEntry(string handler)
        {
            /* 
             * Prevents user from entering anything other than
             * Y or N to either continue or close the program.
            */

            while (handler.ToUpper() != "Y" && handler.ToUpper() != "N")
            {
                WriteLine("Invalid entry. Please enter Y or N\nY/N >>  ");
                handler = ReadLine().ToUpper();
            }

            return handler;
        }

        public static void printHeader()
        {
            /*
             * Prints the program header at the beginning of the program.
            */

            WriteLine("\t\tCustom Coin Toss\n\t\t~~~~~~~~~~~~~~\n\n");
        }
    }
}