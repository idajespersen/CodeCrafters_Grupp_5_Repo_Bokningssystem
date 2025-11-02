using Grupp_5_Bokningssystem.Screens;

namespace Grupp_5_Bokningssystem
{
    internal class Program
    {
        //private static bool _runProgram = true;

        // Implement persistency using a file to store room names
        // Implement persistency using a file to store developers names
        // Read from and write to these files as needed
        // Read when program starts 

        static void Main(string[] args)
        {
            ScreenManager screenManager = new ScreenManager();

            BookingApp app = new BookingApp(screenManager);

            // Add MainMenuScreen to be the root screen.
            screenManager.Add(new MainMenuScreen());

            // Add LanguageSelectionScreen to be the active screen.
            screenManager.Add(new LanguageSelectionScreen());

            // Run the app. The app can be stopped by calling Stop() method.
            app.Run();

            /*while (_runProgram == true)
            {
                MainMenu();
            }*/
        }

        // Method for menu
        /*public static void MainMenu()
        {
            Print.MainMenu();

            if (!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                Print.UserInputNumberError(1, 3);

                MainMenu();
            }

            switch (userChoice)
            {
                case 1:
                    Console.Clear();
                    BookingMenu();
                    break;
                case 2:
                    Console.Clear();
                    RoomMenu();
                    break;
                case 3:
                    Console.Clear();
                    AboutInfoscreen();
                    break;
                case 0:
                    //_runProgram = false;
                    break;
                default:
                    Print.UserInputNumberError(1, 3);
                    break;
            }
        }

        public static void AboutInfoscreen()
        {
            Print.AboutInfo();
        }

        public static void BookingMenu()
        {
            Print.BookingMenu();

            if (!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                Print.UserInputNumberError(1, 4);
            }

            switch (userChoice)
            {
                case 1:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en NewBooking finnas!");
                    Console.ReadKey();
                    NewBooking();
                    break;
                case 2:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en CancelBooking finnas!");
                    Console.ReadKey();
                    CancelBooking();
                    break;
                case 3:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en UpdateBooking finnas!");
                    Console.ReadKey();
                    UpdateBooking();
                    break;
                case 4:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en ListBookings finnas!");
                    Console.ReadKey();
                    ListBookings();
                    break;
                case 0:
                    Console.Clear();
                    return;
                default:
                    Print.UserInputNumberError(1, 4);
                    break;
            }
        }

        public static void RoomMenu()
        {
            Print.RoomMenu();

            if (!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                Print.UserInputNumberError(1, 2);
            }

            switch (userChoice)
            {
                case 1:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en SearchRoom finnas!");
                    Console.ReadKey();
                    SearchRoom();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en NewRoom finnas!");
                    Console.ReadKey();
                    NewRoom();
                    Console.Clear();
                    break;
                case 0:
                    Console.Clear();
                    return;
                default:
                    Print.UserInputNumberError(1, 2);
                    break;
            }
        }

        // - Create new bookings
        public static void NewBooking()
        {
        }

        // - Cancel a booking
        public static void CancelBooking()
        {
        }

        // - Update an existing booking
        public static void UpdateBooking()
        {
        }

        // - List all bookings
        // Handle sorting of bookings
        public static void ListBookings()
        {
        }

        // - List bookings from a specific year
        // Will be inside the ListBookings in menu
        public static void ListBookingsByYear()
        {
        }

        // - List rooms with specific properties (e.g., capacity)
        // Implement operations for filtering and searching rooms based on criteria such as capacity and availability
        public static void SearchRoom()
        {
        }

        // - Ability to make new rooms with error handling for duplicate names
        public static void NewRoom()
        {
        }

        // Handle start and stop times for bookings
        // Format date and time appropriately
        // TimeSpan for duration of booking

        // Use interface as return type where relevant
        */
    }
}