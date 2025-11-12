using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public sealed class LanguageSelectionScreen : SelectionScreen
    {
        public LanguageSelectionScreen() 
            : base(1, 2)
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

        protected override void DisplayWrongInputMessage(Language language, string input)
        {
            Console.WriteLine($"Ogiltigt val / Invalid choice: {input}");
            Console.WriteLine("Tryck [ENTER] för att fortsätta.");
            Console.Write("Press [ENTER] to continue.");
        }
    }
}
