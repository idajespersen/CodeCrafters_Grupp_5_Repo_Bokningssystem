using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem
{
    public static class Print
    {
        /// <summary>
        /// Prints the main menu message
        /// </summary>
        public static void MainMenu()
        {
            Console.Clear();

            
        }

        public static void RoomMenu()
        {
            Console.Clear();

            if(DisplayLanguage.Selected == Language.Swedish)
            {
                Console.WriteLine(" - Rumshantering - ");
                Console.WriteLine();
                Console.WriteLine(" [1] - Sök efter rum");
                Console.WriteLine(" [2] - Skapa ett nytt rum");
                Console.WriteLine();
                Console.WriteLine(" [0] - Återgå till menyn");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" - Rumshantering - ");
                Console.WriteLine();
                Console.WriteLine(" [1] - Sök efter rum");
                Console.WriteLine(" [2] - Skapa ett nytt rum");
                Console.WriteLine();
                Console.WriteLine(" [0] - Återgå till menyn");
                Console.WriteLine();
            }
        }

        public static void BookingMenu()
        {
            Console.Clear();

            if(DisplayLanguage.Selected == Language.Swedish)
            {
                Console.WriteLine(" - Bokningsmenyn - ");
                Console.WriteLine();
                Console.WriteLine(" [1] - Gör en ny bokning");
                Console.WriteLine(" [2] - Ta bort bokning");
                Console.WriteLine(" [3] - Ändra bokning");
                Console.WriteLine(" [4] - Visa alla bokningar");
                Console.WriteLine();
                Console.WriteLine(" [0] - Återgå till huvudmenyn");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" - Booking menu - ");
                Console.WriteLine();
                Console.WriteLine(" [1] - Make a new booking");
                Console.WriteLine(" [2] - Remove a booking");
                Console.WriteLine(" [3] - Change a booking");
                Console.WriteLine(" [4] - Show all bookings");
                Console.WriteLine();
                Console.WriteLine(" [0] - Return to the main menu");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the about info message
        /// </summary>
        public static void AboutInfo()
        {
            // Hardcoded developer names, to be replaced with file persistency
            string[] members =
            {
                "Daniel Skalk",
                "Ida Hägglund",
                "Sara Sundqvist",
                "Hajdar",
                "Tove Rosén"
            };

            void PrintMembers()
            {
                for (int i = 0; i < members.Length; i++)
                {
                    Console.WriteLine(members[i]);
                }
            }

            Console.Clear();

            if (DisplayLanguage.Selected == Language.Swedish)
            {
                Console.WriteLine("Det här programmet skapades av:");
                Console.WriteLine();
                Console.WriteLine(" - CodeCrafters-Teamet - ");

                PrintMembers();

                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att återgå till menyn.");
            }
            else
            {
                Console.WriteLine("This program was created by:");
                Console.WriteLine();
                Console.WriteLine(" - CodeCrafters-Team - ");

                PrintMembers();

                Console.WriteLine();
                Console.Write("Press [ENTER] to return to the menu.");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Prints an error message when the range of an input is incorrect
        /// </summary>
        /// <param name="min">Min input value</param>
        /// <param name="max">Max input value</param>
        public static void UserInputNumberError(int min, int max)
        {
            if (max < min)
                throw new ArgumentException("max must be greater than or equal to min.");

            
        }
    }
}
