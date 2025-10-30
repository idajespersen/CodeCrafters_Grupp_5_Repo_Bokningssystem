using System.Globalization;

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
            new ClassRoom("Klassrum 1", 45, true, true);
            new ClassRoom("Klassrum 2", 20, true, true);
            new ClassRoom("Klassrum 3", 30, true, true);
            new ClassRoom("Klassrum 4", 15, false, false);
            new GroupRoom("Grupprum 1", 6, false, false);
            new GroupRoom("Grupprum 2", 8, true, true);
            new GroupRoom("Grupprum 3", 4, false, false);

            while (runProgram)
            {

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
            // Switch meny baserat på användarens val i bokningsmenyn.
            switch (userChoice)
            {
                case 1: // Om användaren väljer "Gör en ny bokning". Gjorts av Sara.
                    Console.Clear();
                    bool bookingMenuActive = true; // Variabel som styr loopen för bokningsmenyn.
                    // Loop som fortsätter så länge användaren inte väljer att återgå till huvudmenyn.
                    while (bookingMenuActive)
                    {
                        Console.WriteLine("Vilken typ av bokning vill du göra?");
                        Console.WriteLine("[1] Klassrum");
                        Console.WriteLine("[2] Grupprum");
                        Console.WriteLine("[0] Återgå till bokningsmenyn");

                        // Läser in användarens val och försöker konvertera till int.
                        // Då det inte lyckas skrivs felmeddelande ut.
                        if (!int.TryParse(Console.ReadLine(), out int choice))
                        {
                            Console.WriteLine("Du måste skriva in en siffra 1-2!");
                            continue; // Skickar tillbaka användaren till början av loopen.
                        }
                        // Switch meny för att välja klassrum eller grupprum.
                        switch (choice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                // Filtrerar alla rum i RoomRegistry och lägger endast ClassRoom-objekt i en lista.
                                var classRooms = RoomRegistry.AllRooms.OfType<ClassRoom>().ToList();
                                // Visar alla klassrum i listan.
                                Console.WriteLine("\nTillgängliga klassrum:");
                                for (int i = 0; i < classRooms.Count; i++)
                                    Console.WriteLine($"[{i + 1}] {classRooms[i].RoomName} Kapacitet: {classRooms[i].RoomCapacity}");

                                int classRoomChoice; // Variabel som ska hålla användarens val av rum.
                                while (true) // Loop för att säkerställa giltigt val av rum.
                                {
                                    Console.Write("Välj rum: ");
                                    // Läser in användarens val och försöker konvertera till int.
                                    // Kontrollerar så att användaren väljer ett rumsnummer som finns.
                                    if (int.TryParse(Console.ReadLine(), out classRoomChoice) &&
                                        classRoomChoice >= 1 && classRoomChoice <= classRooms.Count)
                                        break; // Vid giltigt val bryts loopen.
                                    // Ifall det inte är giltigt skickas ett felmeddelande.
                                    Console.WriteLine($"Felaktigt val. Ange ett tal mellan 1 och {classRooms.Count}.");
                                }
                                // Skapar en bokning för det valda rummet med metoden NewBooking.
                                classRooms[classRoomChoice - 1].NewBooking();
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                // Filtrerar alla rum i RoomRegistry och lägger endast GroupRoom-objekt i en lista.
                                var groupRooms = RoomRegistry.AllRooms.OfType<GroupRoom>().ToList();

                                // Visar alla grupprum i listan.
                                Console.WriteLine("\nTillgängliga grupprum:");
                                for (int i = 0; i < groupRooms.Count; i++)
                                    Console.WriteLine($"[{i + 1}] {groupRooms[i].RoomName}");

                                int groupRoomChoice;// Variabel som ska hålla användarens val av rum.
                                while (true) // Loop för att säkerställa giltigt val av rum.
                                {
                                    Console.Write("Välj rum: ");
                                    // Läser in användarens val och försöker konvertera till int.
                                    // Kontrollerar så att användaren väljer ett rumsnummer som finns.
                                    if (int.TryParse(Console.ReadLine(), out groupRoomChoice) &&
                                        groupRoomChoice >= 1 && groupRoomChoice <= groupRooms.Count)
                                        break; // Vid giltigt val bryts loopen.
                                    // Ifall det inte är giltigt skickas ett felmeddelande.
                                    Console.WriteLine($"Felaktigt val. Ange ett tal mellan 1 och {groupRooms.Count}.");
                                }
                                // Skapar en bokning för det valda rummet med metoden NewBooking.
                                groupRooms[groupRoomChoice - 1].NewBooking();
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                bookingMenuActive = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.WriteLine("Du måste skriva in en siffra 1-2!"); // Felmeddelande skrivs ut.
                                break;
                        }

                    }
                    break;
                case 2:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en CancelBooking finnas!");
                    Console.ReadKey();
                    //  CancelBooking();
                    break;
                case 3:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en UpdateBooking finnas!");
                    Console.ReadKey();
                    //    UpdateBooking();
                    break;
                case 4:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en ListBookings finnas!");
                    Console.ReadKey();
                    //  ListBookings();
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
                    //SearchRoom();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en NewRoom finnas!");
                    Console.ReadKey();
                    // NewRoom();
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

        // Klass som håller reda på alla rum som skapats. Gjord av Sara.
        public static class RoomRegistry
        {
            public static List<Room> AllRooms { get; private set; } = new();

            public static void RegisterRoom(Room room)
            {
                AllRooms.Add(room);
            }
        }
        // Interface som definierar funktionalitet som alla bokningsbara objekt måste implementera.
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

        // Basklass som implementerar IBookable interface.
        public class Room : IBookable
        {
            public List<Booking> bookings = new List<Booking>();
            // Rummets namn.
            public string RoomName { get; }
            // Rummets kapacitet.
            public int RoomCapacity { get; }
            // Rummets tillgänglighet.
            bool RoomIsAvailable { get; }
            // Parent class accepts name, capacity and availability as parameters
            // Methods for IBookable interface implemented with NotImplementedException
            // Konstruktor för att skapa rum.
            public Room(string roomName, int roomCapacity, bool RoomIsAvailable)
            {
                RoomName = roomName;
                RoomCapacity = roomCapacity;
                this.RoomIsAvailable = RoomIsAvailable;
                // Registrerar rummet i den globala listan över rum
                RoomRegistry.RegisterRoom(this);

            }
            // Metod för att skapa bokning. Gjord av Sara.
            public virtual void NewBooking()
            {
                // Användaren skriver in sitt namn
                Console.Write("Ange namn på den som bokar: ");
                string? bookerName = Console.ReadLine();
                // Kontrollerar om namnet är tomt/whitespace. 
                if (string.IsNullOrWhiteSpace(bookerName))
                {
                    // Skriver ut felmeddelande och användaren skickas tillbaka till bokningsmenyn.
                    Console.WriteLine("Du måste ange ett namn för att boka ett rum!");
                    return;
                }

                DateTime bookingDate;
                while (true) // Loop för att säkerställa ett giltigt datum.
                {
                    // Användaren skriver in datum för bokning.
                    Console.Write("Ange datum för bokning (ÅÅÅÅ-MM-DD): ");
                    string dateInput = Console.ReadLine();
                    // Kontrollerar om användaren skrivit in rätt format på datum. Enligt svensk standard.
                    // Skickar ut bookingDate.
                    if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd", new CultureInfo("sv-SE"), System.Globalization.DateTimeStyles.AllowWhiteSpaces, out bookingDate))
                    {
                        // Kontrollerar så att datumet inte har passerat.
                        if (bookingDate.Date < DateTime.Today)
                        {
                            Console.WriteLine("Du kan inte boka ett datum som passerat!");
                            continue; // Skickar tillbaka användaren till början av loopen.
                        }
                        // Om datum är giltigt sparas det endast med datumdelen inte tid. Loopen bryts sedan.
                        bookingDate = bookingDate.Date;
                        break;
                    }

                    else
                    {
                        // Om användaren inte skriver datum i rätt format får de ett felmeddelande.
                        Console.WriteLine("Felaktigt datumformat! Försök igen (Korrekt format: 2025-10-22).");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                }

                TimeSpan bookingStartTime, bookingEndTime;
                while (true) // Loop för att säkerställa giltig starttid.
                {
                    // Användaren skriver in bokningens starttid.
                    Console.Write("Ange bokningens starttid (HH:MM): ");
                    string startTimeInput = Console.ReadLine().Replace(" ", "");
                    // Kontrollerar om användaren skrivit in rätt format på tid. Enligt svensk standard.
                    // Skickar ut bookingStartTime.
                    if (!TimeSpan.TryParseExact(startTimeInput, "hh\\:mm", new CultureInfo("sv-SE"), out bookingStartTime))
                    {
                        Console.WriteLine("Felaktigt tidsformat! Försök igen (Korrekt format: 09:30).");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    // Skapar DateTime objekt med datum och starttid för jämförelse.
                    DateTime startDateTime = bookingDate + bookingStartTime;
                    // Kontrollerar så att starttiden inte passerat.
                    if (startDateTime < DateTime.Now)
                    {
                        Console.WriteLine("Du kan inte boka en tid som passerat!");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    break; // Lämnar loopen då vi har en giltig tid.

                }

                while (true) // Loop för att säkerställa giltig sluttid.
                {
                    // Användaren skriver in bokningens sluttid.
                    Console.Write("Ange sluttid (format: HH:MM): ");
                    string endTimeInput = Console.ReadLine().Replace(" ", "");
                    // Kontrollerar om användaren skrivit in rätt format på tid. Enligt svensk standard.
                    // Skickar ut bookingEndTime.
                    if (!TimeSpan.TryParseExact(endTimeInput, "hh\\:mm", new CultureInfo("sv-SE"), out bookingEndTime))
                    {
                        Console.WriteLine("Felaktigt tidsformat! Försök igen (Korrekt format: 09:30).");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    // Kontrollerar så att sluttiden inte är före starttiden.
                    if (bookingEndTime <= bookingStartTime)
                    {
                        Console.WriteLine("Sluttiden måste vara efter starttiden!");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }

                    break; // Lämnar loopen då vi har en giltig tid.
                }
                // Kombinerar datum och tider till DateTime objekt för bokningen.
                DateTime startTime = bookingDate + bookingStartTime;
                DateTime endTime = bookingDate + bookingEndTime;
                // Skapar nytt Booking objekt.
                Booking newBooking = new Booking(bookerName, RoomName, startTime, endTime);
                // Kontrollerar så att den nya bokningen inte överlappar med befintliga bokningar.
                bool overlap = bookings.Any(b => b.BookingsOverlap(newBooking));
                if (overlap)
                {
                    Console.WriteLine("Tiden är redan bokad. Din bokning kunde inte genomföras.");
                    Console.ReadKey();
                    return; // Avbryter bokningen. Returnerar tillbaka till bokningsmenyn.
                }
                // Om bokningen inte överlappar med någon annan läggs den till i bookings listan.
                bookings.Add(newBooking);
                Console.WriteLine($"Bokning skapad: {bookerName} har bokat {RoomName} {startTime:yyyy-MM-dd HH:mm}–{endTime:HH:mm}");
            }

            public void CancelBooking()
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

        // Child class GroupRoom, inherits from Room and IBookable
        // Paramenters for constructor include smartboard availability
        public class GroupRoom : Room
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
        public class ClassRoom : Room
        {
            bool hasProjector;
            public ClassRoom(string iName, int iCapacity, bool iIsAvailable, bool iHasProjector)
                : base(iName, iCapacity, iIsAvailable)
            {
                hasProjector = iHasProjector;
            }
        }
    }
    // Klass som hanterar bokningar. Gjord av Sara.
    public class Booking
    {
        // Namn på personen som bokar.
        public string BookerName { get; set; }
        // Namn på rummet som bokas.
        public string RoomName { get; set; }
        // Starttid för bokning.
        public DateTime StartTime { get; set; }
        // Sluttid för bokning.
        public DateTime EndTime { get; set; }
        // Längd av bokningen.
        public TimeSpan Duration => EndTime - StartTime;
        // Konstruktor för att skapa ny bokning.
        public Booking(string bookerName, string roomName, DateTime startTime, DateTime endTime)
        {
            BookerName = bookerName;
            RoomName = roomName;
            StartTime = startTime;
            EndTime = endTime;
            // Kontrollerar att sluttiden är efter starttiden annars kastas ett undantag.
            if (endTime <= startTime)
                throw new ArgumentException("Sluttid måste vara efter starttid.");
        }

        //Metod i Booking klassen för att kolla om bokningar överlappar.
        public bool BookingsOverlap(Booking otherBooking)
        {
            // Om den andra bokningen är null så kan den inte överlappa och den returnerar då false.
            if (otherBooking == null) return false;

            // Kollar om en boknings start och sluttid överlappar med en annan bokning.
            bool noOverlap = StartTime >= otherBooking.EndTime || EndTime <= otherBooking.StartTime;

            // Om noOverlap är true så överlappar de inte och returnerar false.
            // Om noOverlap är false så överlappar de inte och returnerar true.
            return !noOverlap;
        }

    }
}

