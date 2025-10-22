
namespace Grupp_5_Bokningssystem
{
    internal class Program
    {
        public static bool runProgram = true;

        // Create List<T> to manage bookings and rooms
        List<IBookable> BookingsAndRooms = new List<IBookable>();

        // Implement persistency using a file to store room names
        // Implement persistency using a file to store developers names
        // Read from and write to these files as needed
        // Read when program starts 

        static void Main(string[] args)
        {
            while (runProgram == true)
            {
                Console.WriteLine("Här är vårat framtida projekt!");

                MainMenu();

            }
        }
        // Method for menu
        public static void MainMenu()
        {
            Console.WriteLine("xXx - Welcome - xXx");
            Console.WriteLine("[1] - Booking menu");
            Console.WriteLine("[2] - Room menu");
            Console.WriteLine("[3] - About");
            Console.WriteLine("[0] - Exit");

            if (int.TryParse(Console.ReadLine(), out int choice)) { }
          
            else
            {
                Console.WriteLine("Please enter a number between 1-3. " +
                "0 to exit the program.");
            }

            switch (choice)
            {
                case 1:
                    BookingMenu();
                    break;

                case 2:
                    Console.WriteLine("Case 2 - Main Menu");
                    break;
                case 3:
                    Console.WriteLine("Case 3 - Main Menu");
                    break;
                case 0:
                    runProgram = false;
                    break;
                default:
                    Console.WriteLine("Invalid val, försök again");
                    break;
            }
        }
        public static void AboutMenu()
        {
            Console.WriteLine($"This program is created by xxx, xxx, xxx and xxx.");
        }
        public static void BookingMenu()
        {
            Console.WriteLine("[1] - Create new booking");
            Console.WriteLine("[2] - Cancel booking");
            Console.WriteLine("[3] - Update booking");
            Console.WriteLine("[4] - List all bookings");
            Console.WriteLine("[0] - Return to main menu");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine(choice);
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number between 1-4. " +
                "0 to return to main menu.");
            }

            switch (choice)
            {
                case 1:
                    IBookable.NewBooking();
                    break;
                case 2:
                    IBookable.CancelBooking();
                                      
                    break;                    
                case 3:                    
                    IBookable.UpdateBooking();  
                    break;
                case 4:
                    IBookable.ListBooking();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid val, försök again");
                    break;

            }
        }
        public static void RoomMenu()
        {
            Console.WriteLine("[1] - Search room by properties");
            Console.WriteLine("[2] - Create new room");
            Console.WriteLine("[0] - Return to main menu");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine(choice);
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number between 1-3. " +
                "0 to exit the program.");
            }


            switch (choice)
            {
                case 1:
                    
                    break;

                case 2:
                    
                    break;

                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid val, försök again");
                    break;
            }
        }



        // Create a IBookable interface with methods for booking and cancelling bookings
        public interface IBookable
        {
            // Handle start and stop times for bookings
            // Format date and time appropriately
            // TimeSpan for duration of booking

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
            public static void ListBooking()
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
        }
        // Use interface as return type where relevant

        // Create a parent class Room
        // Implement the IBookable interface in the class
        public class Room
        {

        }
        // Create a child class GroupRoom
        // Implement the IBookable interface in the class
        public class GroupRoom : Room
        {


        }
        // Create a child class ClassRoom
        // Implement the IBookable interface in the class
        public class ClassRoom : Room
        {


        }
    }
}


