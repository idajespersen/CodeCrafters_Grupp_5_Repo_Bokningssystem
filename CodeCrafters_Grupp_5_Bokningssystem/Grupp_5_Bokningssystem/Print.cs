using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem
{
    public enum Language
    {
        Swedish,
        English
    }

    public static class Print
    {
        private static Language _currentLanguage = Language.Swedish;

        public static Language CurrentLanguage 
        {
            get { return _currentLanguage; }
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                }
            }
        }

        /// <summary>
        /// Prints the main menu message
        /// </summary>
        public static void MainMenu()
        {
            Console.Clear();

            if (_currentLanguage == Language.Swedish)
            {
                Console.WriteLine(" - Bokningssystemet - ");
                Console.WriteLine();
                Console.WriteLine("Här kan du göra bokningar av rum och ändra dessa vid behov.");
                Console.WriteLine();
                Console.WriteLine(" - Huvudmeny - ");
                Console.WriteLine(" [1] - Bokningshantering");
                Console.WriteLine(" [2] - Rumshantering");
                Console.WriteLine(" [3] - Om");
                Console.WriteLine();
                Console.WriteLine(" [0] - Avsluta programmet");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" - Booking system - ");
                Console.WriteLine();
                Console.WriteLine("Here you can book rooms and change them if needed.");
                Console.WriteLine();
                Console.WriteLine(" - Main menu - ");
                Console.WriteLine(" [1] - Booking manager");
                Console.WriteLine(" [2] - Room manager");
                Console.WriteLine(" [3] - About");
                Console.WriteLine();
                Console.WriteLine(" [0] - Exit the program");
                Console.WriteLine();
            }
        }

        public static void RoomMenu()
        {
            Console.Clear();

            if(_currentLanguage == Language.Swedish)
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

            if(_currentLanguage == Language.Swedish)
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

            if (_currentLanguage == Language.Swedish)
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

            Console.Clear();

            if (_currentLanguage == Language.Swedish)
            {
                Console.WriteLine("Felmeddelande:");
                Console.WriteLine();
                Console.WriteLine("Du skrev inte en giltig siffra.");
                Console.WriteLine($"Vänligen skriv in en siffra mellan [{min}]-[{max}].");
                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att återgå till menyn.");
            }
            else
            {
                Console.WriteLine("Error message:");
                Console.WriteLine();
                Console.WriteLine("The given input is invalid.");
                Console.WriteLine($"Please enter a number between [{min}]-[{max}].");
                Console.WriteLine();
                Console.Write("Press [ENTER] to return to the menu.");
            }

            Console.ReadKey();
        }
    }
}
