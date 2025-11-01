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
        protected readonly int _minChoice;
        protected readonly int _maxChoice;
        protected readonly InputErrorAction _errorAction;

        protected SelectionScreen(Screen? parent, int minChoice, int maxChoice, InputErrorAction errorAction = InputErrorAction.All) 
            : base(parent)
        {
            if (minChoice > maxChoice)
                throw new ArgumentException("minChoice must be lesser than or equal to maxChoice");

            _minChoice = minChoice;
            _maxChoice = maxChoice;
            _errorAction = errorAction;
        }

        protected virtual void InputErrorMessage(Language language)
        {
            if (DisplayLanguage.Selected == Language.Swedish)
            {
                Console.WriteLine("Felmeddelande:");
                Console.WriteLine();
                Console.WriteLine("Du skrev inte en giltig siffra.");
                Console.WriteLine($"Vänligen skriv in en siffra mellan [{_minChoice}]-[{_maxChoice}].");
                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att återgå till menyn.");
            }
            else
            {
                Console.WriteLine("Error message:");
                Console.WriteLine();
                Console.WriteLine("The given input is invalid.");
                Console.WriteLine($"Please enter a number between [{_minChoice}]-[{_maxChoice}].");
                Console.WriteLine();
                Console.Write("Press [ENTER] to return to the menu.");
            }
        }

        public void DisplayInputError()
        {
            if ((_errorAction & InputErrorAction.ClearConsole) != 0)
                Console.Clear();

            InputErrorMessage(DisplayLanguage.Selected);

            if ((_errorAction & InputErrorAction.AwaitEnterKey) != 0)
                Console.ReadKey();
        }
    }
}
