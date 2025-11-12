using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Screens
{
    public sealed class MainMenuScreen : SelectionScreen
    {
        public MainMenuScreen() 
            : base(3)
        {
        }

        public override void DisplayMessage(Language language)
        {
            if (language == Language.Swedish)
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

        public override void HandleValidChoice(int choice)
        {
            switch(choice)
            {
                case 0:
                    Close();
                    return;
                case 1:
                    DisplayScreen(new BookingMenuScreen());
                    break;
                case 2:
                    DisplayScreen(new RoomMenuScreen());
                    break;
                case 3:
                    DisplayScreen(new AboutInfoScreen());
                    break;
            }
        }
    }
}
