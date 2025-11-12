namespace Grupp_5_Bokningssystem
    {
    internal class Program
        {
        public static bool runProgram = true;

        // Hardcoded developer names, to be replaced with file persistency
        static string devName1 = "Daniel Skalk";
        static string devName2 = "Ida Hägglund";
        static string devName3 = "Sara Sundqvist";
        static string devName4 = "Hajdar";

        // Implement persistency using a file to store room names
        // Implement persistency using a file to store developers names
        // Read from and write to these files as needed
        // Read when program starts 

        static void Main(string[] args)
            {
            while (runProgram == true)
                {

                // Testing Git issues 2025-11-12 // Doesnt work
                //try again

                MainMenu();

                }
            }
        // Method for menu
        public static void MainMenu()
            {
            Console.Clear();
            Console.WriteLine("\n - Bokningssystemet -\n\n" +
                    "Här kan du göra bokningar av rum och ändra dessa vid behov.\n");
            Console.WriteLine("\n - Huvudmeny -\n");
            Console.WriteLine(" [1] - Bokningshantering");
            Console.WriteLine(" [2] - Rumshantering");
            Console.WriteLine(" [3] - Om\n");
            Console.WriteLine(" [0] - Avsluta programmet\n");

            if (int.TryParse(Console.ReadLine(), out int userChoice)) { }
            else
                {
                Console.Clear();
                Console.WriteLine("Felmeddelande:\n\n" +
                        "Du skrev inte en giltig siffra.\n" +
                        "Vänligen skriv in en siffra mellan [1]-[3].\n" +
                        "Skriv [0] för att avsluta programmet.");
                Console.ReadKey();
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
                    runProgram = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Felmeddelande:\n\n" +
                        "Du skrev inte en giltig siffra.\n" +
                        "Vänligen skriv in en siffra mellan [1]-[3].\n" +
                        "Tryck [ENTER] för att återgå till menyn.");
                    Console.ReadKey();
                    break;
                }
            }
        public static void AboutInfoscreen()
            {
            Console.Clear();
            Console.WriteLine($"Det här programmet skapades av:\n\n" +
                $" - CodeCrafters-Teamet - \n\n   {devName1}\n\n   {devName2}\n\n   {devName3}\n\n   {devName4}\n\n\n" +
                $"Tryck på [ENTER] för att återgå till menyn.");
            Console.ReadKey();
            Console.Clear();
            }
        public static void BookingMenu()
            {
            Console.Clear();
            Console.WriteLine("\n - Bokningsmenyn -\n\n");
            Console.WriteLine(" [1] - Gör en ny bokning");
            Console.WriteLine(" [2] - Ta bort bokning");
            Console.WriteLine(" [3] - Ändra bokning");
            Console.WriteLine(" [4] - Visa alla bokningar\n");
            Console.WriteLine(" [0] - Återgå till huvudmenyn\n");

            if (int.TryParse(Console.ReadLine(), out int userChoice)) { }
            else
                {
                Console.Clear();
                Console.WriteLine("Felmeddelande:\n\n" +
                        "Du skrev inte en giltig siffra.\n" +
                        "Vänligen skriv in en siffra mellan [1]-[4].\n" +
                        "Tryck [ENTER] för att återgå till menyn.");
                Console.ReadKey();
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
                    Console.Clear();
                    Console.WriteLine("Felmeddelande:\n\n" +
                        "Du skrev inte en giltig siffra.\n" +
                        "Vänligen skriv in en siffra mellan [1]-[4].\n" +
                        "Tryck [ENTER] för att återgå till menyn.");
                    Console.ReadKey();
                    break;

                }
            }
        public static void RoomMenu()
            {
            Console.Clear();
            Console.WriteLine("\n - Rumshantering - \n\n");
            Console.WriteLine(" [1] - Sök efter rum");
            Console.WriteLine(" [2] - Skapa ett nytt rum\n");
            Console.WriteLine(" [0] - Återgå till menyn\n");
            if (int.TryParse(Console.ReadLine(), out int userChoice)) { }
            else
                {
                Console.Clear();
                Console.WriteLine("Felmeddelande:\n\n" +
                        "Du skrev inte en giltig siffra.\n" +
                        "Vänligen skriv in en siffra mellan [1]-[2].\n" +
                        "Tryck [ENTER] för att återgå till menyn.");
                Console.ReadKey();
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
                    Console.Clear();
                    Console.WriteLine("Felmeddelande:\n\n" +
                        "Du skrev inte en giltig siffra.\n" +
                        "Vänligen skriv in en siffra mellan [1]-[2].\n" +
                        "Tryck [ENTER] för att återgå till menyn.");
                    Console.ReadKey();
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



        // Create a IBookable interface with methods for booking and cancelling bookings
        public interface IBookable
            {
            void NewBooking();
            void CancelBooking();
            void UpdateBooking();
            void ListBookings();
            void ListBookingsByYear();
            void SearchRoom();
            void NewRoom();
            }
        // Handle start and stop times for bookings
        // Format date and time appropriately
        // TimeSpan for duration of booking

        // Use interface as return type where relevant

        // Create a parent class Room
        // Implement the IBookable interface in the class
        public class Room : IBookable
            {
            // Put variables here
            // Variable for room name
            public string roomName { get; }
            // Variable for room capacity
            int roomCapacity { get; }
            // Variable for room availability
            bool roomIsAvailable { get; }
            // Create List<T> to manage bookings and rooms            
            List<IBookable> BookingsAndRooms = new List<IBookable>
            {
                // Hardcoded a few rooms to have something to work with
                new ClassRoom("Klassrum 1", 45, true, true),
                new ClassRoom("Klassrum 2", 20, true, true),
                new ClassRoom("Klassrum 3", 30, true, true),
                new ClassRoom("Klassrum 4", 15, false, false),
                new GroupRoom("Grupprum 1", 6, false, false),
                new GroupRoom("Grupprum 2", 8, true, true),
                new GroupRoom("Grupprum 3", 4, false, false),
            };

            // Parent class accepts name, capacity and availability as parameters
            // Methods for IBookable interface implemented with NotImplementedException
            public Room(string name, int capacity, bool isAvailable)
                {
                roomName = name;
                roomCapacity = capacity;
                roomIsAvailable = isAvailable;
                }

            public void NewBooking()
                {
                throw new NotImplementedException();
                }

            public void CancelBooking()
                {
                throw new NotImplementedException();
                }

            public void UpdateBooking()
                {
                throw new NotImplementedException();
                }

            public void ListBookings()
                {
                throw new NotImplementedException();
                }

            public void ListBookingsByYear()
                {
                throw new NotImplementedException();
                }

            public void SearchRoom()
                {
                throw new NotImplementedException();
                }

            public void NewRoom()
                {
                throw new NotImplementedException();
                }
            }

        // Child class GroupRoom, inherits from Room and IBookable
        // Paramenters for constructor include smartboard availability
        public class GroupRoom : Room, IBookable
            {
            bool hasSmartBoard;
            public GroupRoom(string iName, int iCapacity, bool iIsAvailable, bool iHasSmartboard)
                : base(iName, iCapacity, iIsAvailable)
                {
                hasSmartBoard = iHasSmartboard;
                }
            }
        // Child class ClassRoom, inherits from Room and IBookable
        // Paramenters for constructor include projector availability
        public class ClassRoom : Room, IBookable
            {
            bool hasProjector;
            public ClassRoom(string iName, int iCapacity, bool iIsAvailable, bool iHasProjector)
                : base(iName, iCapacity, iIsAvailable)
                {
                hasProjector = iHasProjector;
                }
            }
        }
    }