using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public abstract class SelectionScreen : Screen
    {
        protected SelectionScreen(int minChoice, int maxChoice)
        {
            if (maxChoice < minChoice)
                throw new ArgumentException("Max choice cannot be lesser than min choice.", nameof(maxChoice));
            
            MinChoice = minChoice;
            MaxChoice = maxChoice;
        }

        public int MinChoice
        {
            get;
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
            if(int.TryParse(inputString, out int choice) && MinChoice <= choice && choice <= MaxChoice)
            {
                HandleValidChoice(choice);
            }
            else
            {
                HandleWrongInput();
            } 
        }

        /// <summary>
        /// Handle input error by clearing console, display error message and await key.
        /// </summary>
        protected void HandleWrongInput()
        {
            Console.Clear();

            DisplayWrongInputMessage(BookingApp.Instance.Language);

            Console.ReadKey();
        }

        protected virtual void DisplayWrongInputMessage(Language language)
        {
            if (language == Language.Swedish)
            {
                Console.WriteLine("Felmeddelande:");
                Console.WriteLine();
                Console.WriteLine("Du skrev inte en giltig siffra.");
                Console.WriteLine($"Vänligen skriv in en siffra mellan [{MinChoice}]-[{MaxChoice}].");
                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att återgå till menyn.");
            }
            else
            {
                Console.WriteLine("Error message:");
                Console.WriteLine();
                Console.WriteLine("The given input is invalid.");
                Console.WriteLine($"Please enter a number between [{MinChoice}]-[{MaxChoice}].");
                Console.WriteLine();
                Console.Write("Press [ENTER] to return to the menu.");
            }
        }
    }
}
