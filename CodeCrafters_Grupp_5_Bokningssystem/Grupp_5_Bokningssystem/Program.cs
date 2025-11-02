using Grupp_5_Bokningssystem.Helpers;
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
            Console.WriteLine(" - Huvudmeny -");
            Console.WriteLine(" [1] - Bokningshantering");
            Console.WriteLine(" [2] - Rumshantering");
            Console.WriteLine(" [3] - Om");
            Console.WriteLine(" [0] - Avsluta programmet");

            int userChoice = Helper.ParseInt("", 0, 3);

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
                    Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[3].\n");
                    Helper.BackToMenu();
                    break;
            }
        }
        public static void AboutInfoscreen()
        {
            Console.Clear();
            Console.WriteLine($"Det här programmet skapades av:\n\n" +
                $" - CodeCrafters-Teamet - \n\n   {devName1}\n\n   {devName2}\n\n   {devName3}\n\n   {devName4}\n\n\n");
            Helper.BackToMenu();
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

            int userChoice = Helper.ParseInt("", 0, 4);

            // Filtrerar alla rum i RoomRegistry och lägger endast ClassRoom/GroupRoom objekt i en lista.
            var classRooms = RoomRegistry.AllRooms.OfType<ClassRoom>().ToList();
            var groupRooms = RoomRegistry.AllRooms.OfType<GroupRoom>().ToList();
            bool bookingMenuActive = true; // Variabel som styr loop för bokningsmenyn.
            // Switch meny baserat på användarens val i bokningsmenyn.
            switch (userChoice)
            {
                case 1: // Om användaren väljer "Gör en ny bokning". Gjorts av Sara.
                    // Loop som fortsätter så länge användaren inte väljer att återgå till huvudmenyn.
                    while (bookingMenuActive)
                    {
                        Console.Clear();
                        Console.WriteLine("Vilken typ av bokning vill du göra?");
                        Console.WriteLine("[1] Klassrum");
                        Console.WriteLine("[2] Grupprum");
                        Console.WriteLine("[0] Återgå till bokningsmenyn");

                        // Använder metod ParseInt för att be användaren välja en typ av rum.
                        int choice = Helper.ParseInt("", 0, 2);
                        // Switch meny för att välja klassrum eller grupprum.
                        switch (choice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                    // Visar alla klassrum i listan classRooms.
                                Helper.ShowAvailableRooms("klassrum", classRooms);

                                // Använder metod ParseInt för att be användaren välja ett rum.
                                int classRoomChoice = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                // Skapar en bokning för det valda rummet med metoden NewBooking.
                                classRooms[classRoomChoice - 1].NewBooking();
                                Helper.BackToMenu();
                                break;
                            case 2: // Om användaren väljer Grupprum.

                                // Visar alla grupprum i listan.
                                Helper.ShowAvailableRooms("grupprum", groupRooms);

                                // Använder metod ParseInt för att be användaren välja en typ av rum.
                                int groupRoomChoice = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);

                                // Skapar en bokning för det valda rummet med metoden NewBooking.
                                groupRooms[groupRoomChoice - 1].NewBooking();
                                Helper.BackToMenu();

                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                bookingMenuActive = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.WriteLine("Du måste skriva in en siffra 1-2!"); // Felmeddelande skrivs ut.
                                Helper.BackToMenu();
                                break;
                        }

                    }
                    break;
                case 2:
                    while (bookingMenuActive)
                    {
                        Console.Clear();
                        Console.WriteLine("Vilken bokning vill du ta bort?");
                        Console.WriteLine("[1] Klassrum");
                        Console.WriteLine("[2] Grupprum");
                        Console.WriteLine("[0] Återgå till bokningsmenyn");

                        // Använder metod ParseInt för att be användaren välja en typ av rum.
                        int choice = Helper.ParseInt("", 0, 2);
                        // Switch meny för att välja klassrum eller grupprum.
                        switch (choice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                Console.Clear();
                                // Visar alla klassrum i listan.
                                Helper.ShowAvailableRooms("klassrum", classRooms);
                                // Använder metod ParseInt för att be användaren välja ett rum.
                                int classRoomChoiceCancel = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);

                                // Avbryter en bokning för det valda rummet med metoden CancelBooking.
                                classRooms[classRoomChoiceCancel - 1].CancelBooking();
                                Helper.BackToMenu();
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                Console.Clear();
                                // Visar alla klassrum i listan.
                                Helper.ShowAvailableRooms("grupprum", groupRooms);
                                // Använder metod ParseInt för att be användaren välja ett rum.
                                int groupRoomChoiceCancel = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);

                                // Avbryter en bokning för det valda rummet med metoden CancelBooking.
                                groupRooms[groupRoomChoiceCancel - 1].CancelBooking();
                                Helper.BackToMenu();
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                bookingMenuActive = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.WriteLine("Du måste skriva in en siffra 1-2!"); // Felmeddelande skrivs ut.
                                Helper.BackToMenu();
                                break;
                        }

                    }
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Här kommer en UpdateBooking finnas!");
                    Helper.BackToMenu();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Här kommer en ListBookings finnas!");
                    Helper.BackToMenu();
                    break;
                case 0:
                    Console.Clear();
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[4].\n");
                    Helper.BackToMenu();
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
            int userChoice = Helper.ParseInt("", 0, 2);

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
        public virtual int MaxBookingHours => 8;
        public Room(string roomName, int roomCapacity, bool RoomIsAvailable)
        {
            RoomName = roomName;
            RoomCapacity = roomCapacity;
            this.RoomIsAvailable = RoomIsAvailable;
            // Registrerar rummet i den globala listan över rum.
            RoomRegistry.RegisterRoom(this);

        }
        // Metod för att skapa bokning. Gjord av Sara.
        public virtual void NewBooking()
        {
            // Skapar ett cultureinfo objekt för att läsa av lokal kultur för datum.
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // Användaren skriver in sitt namn
            Console.Write("Ange namn på den som bokar: ");
            string? bookerNameInput = Console.ReadLine().Trim();
            // Kontrollerar om namnet är tomt/whitespace. 
            if (string.IsNullOrWhiteSpace(bookerNameInput))
            {
                // Skriver ut felmeddelande och användaren skickas tillbaka till bokningsmenyn.
                Console.WriteLine("Du måste ange ett namn för att boka ett rum!");
                return;
            }
            // Formaterar namn så första bokstaven är stor och resten små.
            string bookerName = char.ToUpper(bookerNameInput[0]) + bookerNameInput.Substring(1).ToLower();
            DateTime bookingDate = Helper.ParseDateTime($"\nAnge datum för bokning ({currentCulture.DateTimeFormat.ShortDatePattern}): ", currentCulture);
            // Använder ReadTimeSpan metod för att läsa in bokningens start- och sluttid.
            var (bookingStartTime, bookingEndTime) = Helper.ReadTimeSpan(bookingDate, MaxBookingHours);
         
          
            // Kombinerar datum och tider till DateTime objekt för bokningen.
            DateTime startTime = bookingDate + bookingStartTime;
            DateTime endTime = bookingDate + bookingEndTime;

            // Skapar nytt Booking objekt.
            Booking newBooking = new Booking(bookerName, RoomName, startTime, endTime);
            // Kontrollerar så att den nya bokningen inte överlappar med befintliga bokningar.
            bool overlap = bookings.Any(b => b.BookingsOverlap(newBooking));
            if (overlap)
            {
                Console.Clear();
                Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                Helper.BackToMenu();
                return; // Avbryter bokningen. Returnerar tillbaka till bokningsmenyn.
            }
            // Om bokningen inte överlappar med någon annan läggs den till i bookings listan.
            bookings.Add(newBooking);
            Console.Clear();
            Console.WriteLine($"\nBokning skapad: {bookerName} har bokat {RoomName} {startTime.ToString("g", currentCulture)}–{endTime.ToString("t", currentCulture)}");
        }
        // Metod för att ta bort bokning. Gjord av Sara.
        public void CancelBooking()
        {
            // Felmeddelande om det inte gjorts några bokningar på det valda rummet.
            if (bookings.Count == 0)
            {
                Console.WriteLine($"Det finns inga bokningar för {RoomName}");
                return; // Skickar tillbaka användaren till föregående meny.
            }
            // Visar bokningar på det valda rummet.
            Console.WriteLine($"Bokningar för {RoomName}:");
            for (int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {bookings[i].BookerName} - {bookings[i].StartTime} - {bookings[i].EndTime}");
            }
            // Använder metod ParseInt för att be användaren välja en bokning att ta bort.
            int removeBookingChoice = Helper.ParseInt("Ange vilken bokning du vill ta bort: ", 1, bookings.Count);
            // Variabel för att ta bort bokning. - 1 för att nå rätt index.
            Booking bookingToRemove = bookings[removeBookingChoice - 1];
            //Ber användaren bekräfta att bokningen ska tas bort.
            Console.WriteLine($"Är du säker på att du vill ta bort bokningen för {bookingToRemove.BookerName} - {bookingToRemove.StartTime} - {bookingToRemove.EndTime}? (Ja/Nej)");
            string confirmRemove = Console.ReadLine().Trim().ToLower();
            // Om användaren skriver ja eller j tas bokningen bort.
            // Annars avbryts det.
            if (confirmRemove == "j" || confirmRemove == "ja")
            {
                bookings.Remove(bookingToRemove);
                Console.WriteLine($"Bokningen har tagits bort");
            }
            else
            {
                Console.WriteLine($"Borttagning har avbrutits");
            }
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

        public override int MaxBookingHours => 6;

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