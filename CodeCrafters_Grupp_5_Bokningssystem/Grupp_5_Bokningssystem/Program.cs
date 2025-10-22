
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
                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

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
            void NewBooking();
            // - Cancel a booking
            void CancelBooking();

            // - Update an existing booking
            void UpdateBooking();
            // - List all bookings
            // Handle sorting of bookings
            void ListBooking();
            // - List bookings from a specific year
            // Will be inside the ListBookings in menu
            void ListBookingsByYear();

            // - List rooms with specific properties (e.g., capacity)
            // Implement operations for filtering and searching rooms based on criteria such as capacity and availability
            void SearchRoom();

            // - Ability to make new rooms with error handling for duplicate names
            void NewRoom();
        }
        // Use interface as return type where relevant

        // Create a parent class Room
        // Implement the IBookable interface in the class
        public class Room : IBookable
        {
            public string RoomName { get; private set; }
            public int Capacity { get; private set; }

            //List<IBookable> BookingsAndRooms = new List<IBookable>();

            public Room(string roomName, int capacity)
            {
                RoomName = roomName;
                Capacity = capacity;
            }
            public virtual void NewBooking()
            {
                Console.WriteLine($"Skapar ny bokning för rum: {RoomName}");

                Console.Write("Ange namn på den som bokar: ");
                string? userName = Console.ReadLine();

                DateTime bookingDate;
                while (true)
                {
                    Console.Write("Ange datum för bokning (ÅÅÅÅ-MM-DD): ");
                    string dateInput = Console.ReadLine();

                    if (DateTime.TryParse(dateInput, out bookingDate))
                    {
                        bookingDate = bookingDate.Date;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Felaktigt datumformat! Försök igen (Korrekt format: 2025-10-22).");
                    }
                }

                TimeSpan bookingStartTime;
                while (true)
                {
                    Console.Write("Ange bokningens starttid (HH:MM): ");
                    if (TimeSpan.TryParse(Console.ReadLine(), out bookingStartTime))
                        break;
                    else
                        Console.WriteLine("Felaktigt tidsformat! Försök igen (Korrekt format: 09:30).");
                }
                TimeSpan bookingEndTime;
                while (true)
                {
                    Console.Write("Ange sluttid (format: HH:MM): ");
                    string endTimeInput = Console.ReadLine();

                    if (!TimeSpan.TryParse(endTimeInput, out bookingEndTime))
                    {
                        Console.WriteLine("Felaktigt tidsformat! Försök igen (Korrekt format: 09:30).");
                        continue;
                    }

                    if (bookingEndTime <= bookingStartTime)
                    {
                        Console.WriteLine("Sluttiden måste vara efter starttiden! Försök igen.");
                        continue;
                    }

                    break;
                }
                DateTime startTime = bookingDate + bookingStartTime;
                DateTime endTime = bookingDate + bookingEndTime;

                //IBookable b = new IBookable(userName, Name, startTime, endTime);

                //bool overlap = BookingsAndRooms.Any(x => x.OverlapsWith(b));
                //if (overlap)
                //{
                //    Console.WriteLine("⚠️ Den här bokningen överlappar med en befintlig bokning.");
                //        Console.WriteLine("Bokning avbruten.");
                //        return;

                //}

                //BookingsAndRooms.Add(b);
                //Console.WriteLine($"✅ Bokning skapad: {userName} har bokat {Name} {startTime:yyyy-MM-dd HH:mm}–{endTime:HH:mm}");
            }

            public void CancelBooking()
            {
                throw new NotImplementedException();
            }

            public void ListBooking()
            {
                throw new NotImplementedException();
            }

            public void ListBookingsByYear()
            {
                throw new NotImplementedException();
            }


            public void NewRoom()
            {
                throw new NotImplementedException();
            }

            public void SearchRoom()
            {
                throw new NotImplementedException();
            }

            public void UpdateBooking()
            {
                throw new NotImplementedException();
            }
        }
        // Create a child class GroupRoom
        // Implement the IBookable interface in the class
        public class GroupRoom : Room
        {
            public GroupRoom(string roomName, int capacity) : base(roomName, capacity)
            {
            }
            public override void NewBooking()
            {
                Console.WriteLine($"Skapar bokning för grupprum: {RoomName}");
                base.NewBooking();
            }

        }
        // Create a child class ClassRoom
        // Implement the IBookable interface in the class
        public class ClassRoom : Room
        {
            public ClassRoom(string roomName, int capacity) : base(roomName, capacity)
            {
            }
            public override void NewBooking()
            {
                Console.WriteLine($"Skapar bokning för klassrum: {RoomName}");
                base.NewBooking();
            }

        }
    }


}
