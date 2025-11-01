using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    [Flags]
    public enum InputErrorAction
    {
        None            = 0,
        ClearConsole    = 1,
        AwaitEnterKey   = 2,
        All             = ClearConsole | AwaitEnterKey
    }

    public abstract class SelectionScreen : Screen
    {
        protected readonly InputErrorAction _errorAction;

        protected SelectionScreen(Screen? parent, int maxChoice, InputErrorAction errorAction = InputErrorAction.All) 
            : base(parent)
        {
            if (maxChoice < 1)
                throw new ArgumentException("minChoice must be lesser than or equal to maxChoice");

            MaxChoice = maxChoice;
            _errorAction = errorAction;
        }

        public int MaxChoice
        {
            get;
        }

        public void HandleInputError()
        {
            if ((_errorAction & InputErrorAction.ClearConsole) != 0)
                Console.Clear();

            DisplayInputErrorMessage(DisplayLanguage.Selected);

            if ((_errorAction & InputErrorAction.AwaitEnterKey) != 0)
                Console.ReadKey();
        }

        protected override bool HandleInput(string inputString)
        {
            if(!int.TryParse(inputString, out int choice) && (choice < 0 || choice > MaxChoice))
            {
                HandleInputError();
                return false;
            }

            if(choice > 0)
            {
                HandleValidChoice(choice);
            }

            return true;
        }

        /// <summary>
        /// Handle a valid choice inbetween 1 and a given maxChoice.
        /// </summary>
        /// <param name="choice"></param>
        public abstract void HandleValidChoice(int choice);

        protected virtual void DisplayInputErrorMessage(Language language)
        {
            if (language == Language.Swedish)
            {
                Console.WriteLine("Felmeddelande:");
                Console.WriteLine();
                Console.WriteLine("Du skrev inte en giltig siffra.");
                Console.WriteLine($"Vänligen skriv in en siffra mellan [1]-[{MaxChoice}].");
                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att återgå till menyn.");
            }
            else
            {
                Console.WriteLine("Error message:");
                Console.WriteLine();
                Console.WriteLine("The given input is invalid.");
                Console.WriteLine($"Please enter a number between [1]-[{MaxChoice}].");
                Console.WriteLine();
                Console.Write("Press [ENTER] to return to the menu.");
            }
        }      
    }
}
