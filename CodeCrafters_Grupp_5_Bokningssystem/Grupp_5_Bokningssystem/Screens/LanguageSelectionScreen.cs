using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public sealed class LanguageSelectionScreen : NumberSelectionScreen
    {
        public LanguageSelectionScreen() 
            : base(2)
        {
        }

        public override void DisplayMessage(Language language)
        {
            Console.WriteLine("Välj språk / Select language");
            Console.WriteLine();
            Console.WriteLine(" [1] - Svenska");
            Console.WriteLine(" [2] - English");
            Console.WriteLine();
        }

        public override void HandleChoice(int choice)
        {
            switch(choice)
            {
                case 0:
                    HandleInputError();
                    return;
                case 1:
                    DisplayLanguage.Selected = Language.Swedish;
                    break;
                case 2:
                    DisplayLanguage.Selected = Language.English;
                    break;
            }

            // Clear the screens and make MainMenuScreen the root screen
            ScreenManager.Instance.Clear();
            ScreenManager.Instance.Push(new MainMenuScreen());
        }

        protected override void DisplayInputErrorMessage(Language language)
        {
            Console.WriteLine("Försök igen / Try again");
            Console.WriteLine();
            Console.WriteLine("Tryck [ENTER] för att fortsätta.");
            Console.Write("Press [ENTER] to continue.");
        }
    }
}
