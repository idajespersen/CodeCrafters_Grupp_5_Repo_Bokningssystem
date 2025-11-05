using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Screens
{
    [Flags]
    public enum InputErrorAction
    {
        None = 0,
        ClearConsole = 1,
        AwaitEnterKey = 2,
        ClearAndAwaitKey = ClearConsole | AwaitEnterKey
    }

    public abstract class NumberSelectionScreen : Screen
    {
        protected readonly InputErrorAction _errorAction;

        protected NumberSelectionScreen(int maxChoice, InputErrorAction errorAction = InputErrorAction.ClearAndAwaitKey)
        {
            if (maxChoice < 1)
                throw new ArgumentException("maxChoice must be equal to or greater than 1");

            MaxChoice = maxChoice;
            _errorAction = errorAction;
        }

        public int MaxChoice
        {
            get;
        }

        /// <summary>
        /// Handle a choice inbetween 0 and MaxChoice.
        /// </summary>
        /// <param name="choice">Number between 0 and MaxChoice.</param>
        public abstract void HandleChoice(int choice);

        protected override void HandleInput(string inputString)
        {
            if (!int.TryParse(inputString, out int choice) || choice < 0 || choice > MaxChoice)
            {
                HandleInputError();
                return;
            }

            HandleChoice(choice);
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

        protected void HandleInputError()
        {
            if ((_errorAction & InputErrorAction.ClearConsole) != 0)
                Console.Clear();

            DisplayInputErrorMessage(DisplayLanguage.Selected);

            if ((_errorAction & InputErrorAction.AwaitEnterKey) != 0)
                Console.ReadKey();
        }
    }
}