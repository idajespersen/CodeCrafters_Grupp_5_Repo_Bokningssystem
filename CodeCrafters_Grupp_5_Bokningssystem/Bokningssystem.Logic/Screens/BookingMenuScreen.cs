using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Screens
{
    public sealed class BookingMenuScreen : NumberSelectionScreen
    {
        public BookingMenuScreen()
            : base(4)
        {
        }

        public override void DisplayMessage(Language language)
        {
            if (language == Language.Swedish)
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

        public override void HandleChoice(int choice)
        {
            switch (choice)
            {
                case 0:
                    ScreenManager.Instance.Pop();
                    return;
                case 1:
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en NewBooking finnas!");
                    Console.ReadKey();
                    //NewBooking();
                    break;
                case 2:
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en CancelBooking finnas!");
                    Console.ReadKey();
                    //CancelBooking();
                    break;
                case 3:
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en UpdateBooking finnas!");
                    Console.ReadKey();
                    //UpdateBooking();
                    break;
                case 4:
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en ListBookings finnas!");
                    Console.ReadKey();
                    //ListBookings();
                    break;
            }
        }
    }
}