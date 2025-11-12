using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public sealed class LanguageSelectionScreen : SelectionScreen
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

        public override void HandleValidChoice(int choice)
        {
            switch(choice)
            {
                case 0:
                    HandleInputError();
                    return;
                case 1:
                    // Set the app language to Swedish
                    BookingApp.Instance.Language = Language.Swedish;
                    break;
                case 2:
                    // Set the app language to English
                    BookingApp.Instance.Language = Language.English;
                    break;
            }

            // Close this screen
            Close();
        }

        protected override void DisplayInputErrorMessage(Language language)
        {
            Console.WriteLine("Ogiltigt val. Försök igen.");
            Console.WriteLine("Tryck [ENTER] för att fortsätta.");
            Console.WriteLine();
            Console.WriteLine("Invalid choice. Try again.");
            Console.Write("Press [ENTER] to continue.");
        }
    }
}
