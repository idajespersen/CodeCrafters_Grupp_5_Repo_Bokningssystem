using System.Security.Cryptography;

namespace Grupp_5_Bokningssystem
{
    // !!! TO-DO !!!
    // Implement persistency using a file to store room names
    // Implement persistency using a file to store developers names
    // Read from and write to these files as needed
    // Read when program starts

    // ----------------------------------------------------------------
    // 1. Interface - IBookable
    // ----------------------------------------------------------------
    public interface IBookable
    {
        void NewBooking();
        void CancelBooking();
        void UpdateBooking();
        void ListBookings();
        void ListBookingsByYear();
    }

    // ----------------------------------------------------------------
    // 2. Parent / Base class
    // ----------------------------------------------------------------

    // Parent Class: Room inherits from interface IBookable
    public class Room : IBookable
    {
        // Variable to handle unique ID per room
        public string RoomId { get; }

        // Variable for room name from user input
        public string RoomName { get; }

        // Variable for room capacity
        int RoomCapacity { get; }

        // Variable for room availability
        bool RoomIsAvailable { get; }

        // Unique empty list for each created room
        public List<Booking> BookingsList { get; } = new List<Booking>();

        // Room Constructor
        // Parent class accepts id, name, capacity and availability as parameters        
        public Room(string iId, string iName, int iCapacity, bool iIsAvailable)
        {
            // Unique ID per created room
            RoomId = iId;
            RoomName = iName;
            RoomCapacity = iCapacity;
            RoomIsAvailable = iIsAvailable;
        }

        // Method Logic 

        // - Create new bookings
        public void NewBooking()
        {
            // User needs to choose which room to book through a list?
            // List needs to be read, then printed out to console
            // User chooses room based on ID? (101, 203 etc)
            // Once room is selected, read from a list all bookings for that room
            // Alternatively show booked, and empty slots for the week?
            // User chooses slot(s), 30m each. How? between datetime to datetime? Limit to 30 min how?
            // Stop booking backwards in time!
            // Booking gets sent to the booking list, tied to room id.
            throw new NotImplementedException();
        }

        // - Cancel a booking
        public void CancelBooking()
        {
            throw new NotImplementedException();
        }

        // - Update an existing booking
        public void UpdateBooking()
        {
            throw new NotImplementedException();
        }

        // - List all bookings
        public void ListBookings()
        {
            throw new NotImplementedException();
        }

        // - List bookings from a specific year
        // Will be inside the ListBookings in menu
        public void ListBookingsByYear()
        {
            throw new NotImplementedException();
        }
    }

    // Booking Class
    public class Booking
    {
        // Notes:
        //
        // Handle start and stop times for bookings
        // Format date and time appropriately
        // TimeSpan for duration of booking?

        // Basic Functionality:
        // 
        // Flag room as unavailable during booked timeslot
        // What variables? Bool for available. DateTime for handling 30m slots?
        // Reference Room by ID; possible to combine info with Json?
        // Each booking need a unique ID? No?
        // inherit from IBookable?

        // Extras: 
        //
        // Make certain times unavailable to book 18.00 - 07.00 every day, all day saturday & sunday? Over the top?
        // Focus on basics first... 
    }

    // ----------------------------------------------------------------
    // 3. Child Classes
    // ----------------------------------------------------------------

    // Child class GroupRoom, inherits from Room and IBookable
    // Paramenters for constructor include smartboard availability
    public class GroupRoom : Room
    {
        bool hasSmartBoard;
        public GroupRoom(string iId, string iName, int iCapacity, bool iIsAvailable, bool iHasSmartboard)
            : base(iId, iName, iCapacity, iIsAvailable)
        {
            hasSmartBoard = iHasSmartboard;
        }
    }
    // Child class ClassRoom, inherits from Room and IBookable
    // Paramenters for constructor include projector availability
    public class ClassRoom : Room
    {
        bool hasProjector;
        public ClassRoom(string iId, string iName, int iCapacity, bool iIsAvailable, bool iHasProjector)
            : base(iId, iName, iCapacity, iIsAvailable)
        {
            hasProjector = iHasProjector;
        }
    }

    internal class Program
    {
        // ----------------------------------------------------------------
        // 4. Test Data
        // ----------------------------------------------------------------
        // Hardcoded developer names, to be replaced with file persistency
        public static string devName1 = "Daniel Skalk";
        public static string devName2 = "Ida Jespersen";
        public static string devName3 = "Sara Sundqvist";
        public static string devName4 = "Tove";

        // Create List<T> to manage rooms            
        static List<IBookable> RoomList = new List<IBookable>
        {
            // Hardcoded a few rooms to have something to work with
            new ClassRoom("AA9BF90B94804053B746950491256241", "Klassrum 101", 45, true, true),
            new ClassRoom("6FCCFE8B37DF473B9598D08461B7AFF7", "Klassrum 102", 20, true, true),
            new ClassRoom("32EE3ED541C44576BC175D13418BC557", "Klassrum 103", 30, true, true),
            new ClassRoom("D5B43EDB95EB40B799B7D6B857CA8740", "Klassrum 104", 15, false, false),
            new GroupRoom("E057DD50FC7F40539D01A9988185A923", "Grupprum 201", 6, false, false),
            new GroupRoom("21E8740CF52648B587EB9FEBCC8C3C59", "Grupprum 202", 8, true, true),
            new GroupRoom("247256E4889F4F0F8836141736C14163", "Grupprum 203", 4, false, false),
        };

        // ----------------------------------------------------------------
        // 5. Main
        // ----------------------------------------------------------------
        public static bool runProgram = true;
        static void Main(string[] args)
        {
            while (runProgram == true)
            {
                MainMenu();
            }
        }

        // ----------------------------------------------------------------
        // 6. Methods
        // ----------------------------------------------------------------
        public static void MainMenu()
        {
            bool isRunningMenu = true;

            while (isRunningMenu == true)
            {
                Console.Clear();
                Console.WriteLine("\n - Bokningssystemet -\n" +
                                    "\nHär kan du göra bokningar av rum och ändra dessa vid behov.\n" +
                                    "\n - Huvudmeny -\n" +
                                    " [1] - Bokningshantering\n" +
                                    " [2] - Rumshantering\n" +
                                    " [3] - Om\n\n" +
                                    " [0] - Avsluta programmet\n");

                if (int.TryParse(Console.ReadLine()?.Trim(), out int userChoice))
                {
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
                            DisplayMessage();
                            break;
                    }
                }
                else
                { DisplayMessage(); }
            }
        }

        public static void BookingMenu()
        {
            bool isRunningMenu = true;

            while (isRunningMenu)
            {

                Console.Clear();
                Console.WriteLine("\n - Bokningsmenyn -\n\n" +
                                    " [1] - Gör en ny bokning\n" +
                                    " [2] - Ta bort bokning\n" +
                                    " [3] - Ändra bokning\n" +
                                    " [4] - Visa alla bokningar\n\n" +
                                    " [0] - Återgå till huvudmenyn\n");

                if (int.TryParse(Console.ReadLine()?.Trim(), out int userChoice))
                {
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
                            isRunningMenu = false;
                            break;
                        default:
                            DisplayMessage();
                            break;
                    }
                }
                else { DisplayMessage(); }
            }
        }

        public static void RoomMenu()
        {
            bool isRunningMenu = true;

            while (isRunningMenu)
            {
                Console.Clear();
                Console.WriteLine("\n - Rumshantering - \n\n" +
                                    " [1] - Sök efter rum\n" +
                                    " [2] - Skapa ett nytt rum\n\n" +
                                    " [0] - Återgå till huvudmenyn\n");

                if (int.TryParse(Console.ReadLine()?.Trim(), out int userChoice))
                {
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
                            NewRoom();
                            Console.Clear();
                            break;
                        case 0:
                            Console.Clear();
                            isRunningMenu = false;
                            break;
                        default:
                            Console.Clear();
                            DisplayMessage();
                            continue;
                    }
                }
                else { DisplayMessage(); continue; }
            }
        }

        // Error message gets repeated too much. Making method for handling error messages
        public static void DisplayMessage()
        {
            Console.Clear();
            Console.WriteLine("Felmeddelande:\n\n" +
                                "Du skrev inte en giltig siffra.\n" +
                                "Vänligen försök igen.\n\n" +
                                "Tryck [ENTER] för att återgå till menyn.");
            Console.ReadKey();
        }

        public static void AboutInfoscreen()
        {
            Console.Clear();
            Console.WriteLine($"Det här programmet skapades av:\n\n" +
                                $" - CodeCrafters-Teamet - \n\n   {devName1}\n\n   {devName2}\n\n   {devName3}\n\n   {devName4}\n\n\n" +
                                $"Tryck på [ENTER] för att återgå till menyn.");
            Console.ReadKey();
        }
        // - List rooms with specific properties (e.g., capacity)
        // Implement operations for filtering and searching rooms based on criteria such as capacity and availability
        public static void SearchRoom()
        {
        }
        // - Ability to make new rooms with error handling for duplicate names
        public static void NewRoom()
        {
            Console.Clear();
            bool isRunningMenu = true;
            while (isRunningMenu)
            {
                Console.WriteLine("- Skapa ett nytt rum -\n");
                Console.WriteLine("Typ av rum:\n\n" +
                                    "[1] - Klassrum\n" +
                                    "[2] - Grupprum\n\n" +
                                    "[0] - Återgå till huvudmenyn\n");

                var newRoomType = "Room";
                if (int.TryParse(Console.ReadLine()?.Trim(), out int typeChoice))
                {
                    switch (typeChoice)
                    {
                        case 1:
                            newRoomType = "ClassRoom";
                            break;
                        case 2:
                            newRoomType = "GroupRoom";
                            break;
                        case 0:
                            isRunningMenu = false;
                            continue;
                        default:
                            DisplayMessage();
                            NewRoom();
                            continue;
                    }
                }
                else
                {
                    DisplayMessage();
                    continue;
                }
                // Name the room
                Console.Write("\nNamnge rummet: ");
                var newRoomName = "000";
                newRoomName = Console.ReadLine()?.Trim() ?? "";
                // As long as string isnt null/empty, loops through list and compare room names
                if (!string.IsNullOrEmpty(newRoomName))
                {
                    foreach (IBookable item in RoomList)
                    {
                        Room room = (Room)item;
                        // Compare new room name to existing rooms ignoring upper/lower case while doing so
                        if (string.Equals(room.RoomName, newRoomName, StringComparison.OrdinalIgnoreCase))
                        {
                            bool nameAlreadyExists = true;
                            if (nameAlreadyExists)
                            {
                                Console.WriteLine($"\nFel: Rummet '{newRoomName}' finns redan!\n\nTryck [ENTER] för att återvända.");
                                Console.ReadKey();
                                NewRoom();
                                continue;
                            }
                        }
                    }
                }
                // String was null/empty and error message is shown
                else
                {
                    Console.WriteLine("Rummet måste ha ett namn.\n\nTryck [ENTER] för att återvända.");
                    Console.ReadKey();
                    NewRoom();
                    continue;
                }
                // Room capacity
                Console.Write("\nAnge rummets kapacitet: ");
                var newRoomCapacity = 0;
                if (int.TryParse(Console.ReadLine()?.Trim() ?? "0", out int capacityInt) && (capacityInt > 0))
                {
                    newRoomCapacity = capacityInt;
                }
                else
                {
                    Console.WriteLine("\nVärdet måste vara större än 0.\n\nTryck [ENTER] för att återvända.");
                    Console.ReadKey();
                    NewRoom();
                    continue;
                }
                // Room equipment 
                Console.WriteLine("\nHar rummet utrustning?\n\n" +
                                    "[1] - Ja\n" +
                                    "[2] - Nej\n\n" +
                                    "[0] - Återgå till huvudmenyn");

                bool newRoomEquipment = false;
                if (int.TryParse(Console.ReadLine()?.Trim() ?? "", out int hasEquipment))
                {
                    switch (hasEquipment)
                    {
                        case 1:
                            newRoomEquipment = true;
                            break;
                        case 2:
                            newRoomEquipment = false;
                            break;
                        case 0:
                            isRunningMenu = false;
                            break;
                        default:
                            DisplayMessage();
                            NewRoom();
                            continue;
                    }
                }
                else
                {
                    DisplayMessage();
                    NewRoom();
                    continue;
                }
                // Add room to the list once user didn't trigger errors and assign ID
                string newRoomId = Guid.NewGuid().ToString("N");

                if (newRoomType == "ClassRoom")
                {
                    bool newRoomIsAvailable = true;
                    ClassRoom newRoom = new ClassRoom(newRoomId, newRoomName, newRoomCapacity, newRoomEquipment, newRoomIsAvailable);
                    RoomList.Add(newRoom);
                }
                if (newRoomType == "GroupRoom")
                {
                    bool newRoomIsAvailable = true;
                    GroupRoom newRoom = new GroupRoom(newRoomId, newRoomName, newRoomCapacity, newRoomEquipment, newRoomIsAvailable);
                    RoomList.Add(newRoom);
                }

                Console.WriteLine($"\nSammanfattning:\n\n" +
                                    $"Rumstyp: {newRoomType}\n" +
                                    $"Namn: {newRoomName}\n" +
                                    $"Kapacitet: {newRoomCapacity}\n" +
                                    $"Har utrustning: {newRoomEquipment}\n\n" +
                                    $"Rummet har lagts till.\n\n" +
                                    $"Tryck [ENTER] för att återgå till menyn.");
            }
        }
    }
}