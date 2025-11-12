using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public abstract class NumberSelectionScreen : Screen
    {
        protected NumberSelectionScreen(int maxChoice)
        {
            if (maxChoice < 1)
                throw new ArgumentException("maxChoice must be equal to or greater than 1");

            MaxChoice = maxChoice;
        }

        public int MaxChoice
        {
            get;
        }

        /// <summary>
        /// Handle a valid choice between 0 and MaxChoice.
        /// </summary>
        /// <param name="choice">Number between 0 and MaxChoice.</param>
        public abstract void HandleValidChoice(int choice);

        protected override void HandleInput(string inputString)
        {
            if (!int.TryParse(inputString, out int choice) || choice < 0 || choice > MaxChoice)
            {
                HandleInputError();
                return;
            }

            HandleValidChoice(choice);
        }

        /// <summary>
        /// Handle input error by clearing console, display error message and await key.
        /// </summary>
        protected void HandleInputError()
        {
            Console.Clear();

            DisplayInputErrorMessage(BookingApp.Instance.Language);

            Console.ReadKey();
        }

        protected virtual void DisplayInputErrorMessage(Language language)
        {
            if (language == Language.Swedish)
            {
                Console.WriteLine("Felmeddelande:");
                Console.WriteLine();
                Console.WriteLine("Du skrev inte en giltig siffra.");
                Console.WriteLine($"Vänligen skriv in en siffra mellan [0]-[{MaxChoice}].");
                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att återgå till menyn.");
            }
            else
            {
                Console.WriteLine("Error message:");
                Console.WriteLine();
                Console.WriteLine("The given input is invalid.");
                Console.WriteLine($"Please enter a number between [0]-[{MaxChoice}].");
                Console.WriteLine();
                Console.Write("Press [ENTER] to return to the menu.");
            }
        }
    }
}
