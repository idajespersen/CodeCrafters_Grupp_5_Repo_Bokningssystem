using Bokningssystem.Logic;
using Grupp_5_Bokningssystem;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml.Serialization;
using static RoomRegistry;
using static RoomRegistry.Program;


namespace Grupp_5_Bokningssystem
{
    // !!! TO-DO !!!
    // Implement persistency using a file to store room names
    // Implement persistency using a file to store developers names
    // Read from and write to these files as needed
    // Read when program starts



    // ----------------------------------------------------------------
    // 2. Parent / Base class
    // ----------------------------------------------------------------

    // Parent Class: Room inherits from interface IBookable
    public class Room : IBookable
    {
        // Unique empty list for each created room
        public List<Booking> bookings = new List<Booking>();
        // Variable to handle unique ID per room
        public string RoomId { get; }
        // Variable for room name from user input
        public string RoomName { get; }
        // Variable for room capacity
        public int RoomCapacity { get; }
        // Variable for room availability
        bool RoomIsAvailable { get; }
        // Standardvärde för max antal timmar ett rum kan bokas.
        // Kan göra override för att ändra individuellt för olika typer rum.
        public virtual int MaxBookingHours => 8;
        // Skapar ett cultureinfo objekt för att läsa av lokal kultur för datum.
        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        // Room Constructor
        // Parent class accepts id, name, capacity and availability as parameters
        public Room(string iId, string iName, int iCapacity, bool iIsAvailable)
        {
            RoomId = iId;
            RoomName = iName;
            RoomCapacity = iCapacity;
            RoomIsAvailable = iIsAvailable;
            // Registrerar rummet i den globala listan över rum.
            RoomRegistry.RegisterRoom(this);
        }
        // Metod för att skapa bokning. Gjord av Sara.
        public void NewBooking()
        {   // Använder ReadName metod för att läsa in användarens namn.
            string bookerName = Helper.ReadName("Ange namn på den som bokar: ");
            // Använder ParseDateTime metod för att läsa in bokningens datum.
            DateTime bookingDate = Helper.ParseDateTime($"\nAnge datum för bokning", currentCulture);
            // Använder ReadTimeSpan metod för att läsa in bokningens start- och sluttid.
            var (bookingStartTime, bookingEndTime) = Helper.ReadTimeSpan("Ange starttid", "Ange sluttid", bookingDate, MaxBookingHours, currentCulture);
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
                return; // Avbryter bokningen. Skickar tillbaka användaren till föregående meny.
            }
            // Om bokningen inte överlappar med någon annan läggs den till i bookings listan.
            bookings.Add(newBooking);
            Console.Clear();
            Console.WriteLine($"Bokning skapad:\n{bookerName} har bokat {RoomName} - {startTime.Day} {startTime:MMMM} {startTime.Year} kl {startTime:HH\\:mm}-{endTime:HH\\:mm}.");
        }
        // Metod för att ta bort bokning. Gjord av Sara.
        public void CancelBooking()
        {
            Console.Clear();
            // Felmeddelande om det inte gjorts några bokningar på det valda rummet.
            if (bookings.Count == 0)
            {
                Console.WriteLine($"Det finns inga bokningar för {RoomName}");
                return; // Skickar tillbaka användaren till föregående meny.
            }
            // Visar alla bokningar på det valda rummet.
            Console.WriteLine($"Bokningar för {RoomName}:");
            for (int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {bookings[i].BookerName} - {bookings[i].StartTime.Day} {bookings[i].StartTime:MMMM} {bookings[i].StartTime.Year} kl {bookings[i].StartTime:HH\\:mm} - {bookings[i].EndTime:HH\\:mm}");
            }
            // Använder metod ParseInt för att läsa in användarens val av bokning att ta bort.
            int removeInput = Helper.ParseInt("Ange vilken bokning du vill ta bort: ", 1, bookings.Count);
            // Variabel för att ta bort bokning. - 1 för att nå rätt index.
            Booking bookingToRemove = bookings[removeInput - 1];
            // Ber användaren bekräfta borttagning av bokning. Om de svarar ja så tas bokning bort.
            if (Helper.ConfirmAction($"ta bort bokningen för {bookingToRemove.BookerName} - {bookingToRemove.StartTime.Day} {bookingToRemove.StartTime:MMMM} {bookingToRemove.StartTime.Year} kl {bookingToRemove.StartTime:HH\\:mm}-{bookingToRemove.EndTime:HH\\:mm}", "Borttagning har avbrutits"))
            {
                bookings.Remove(bookingToRemove);
                Console.WriteLine($"Bokningen har tagits bort");
            }
        }

        public void UpdateBooking()
        {
            bool updateMenuActive = true; // Variabel som styr loop för menyn.
            while (updateMenuActive)
            {
                Console.Clear();
                // Felmeddelande om det inte gjorts några bokningar på det valda rummet.
                if (bookings.Count == 0)
                {
                    Console.WriteLine($"Det finns inga bokningar för {RoomName}");
                    return; // Skickar tillbaka användaren till föregående meny.
                }

                Console.WriteLine($"Välj bokningen du vill uppdatera:");
                // Visar alla bokningar på det valda rummet.
                Console.WriteLine($"Bokningar för {RoomName}:");
                for (int i = 0; i < bookings.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {bookings[i].BookerName} - {bookings[i].StartTime.Day} {bookings[i].StartTime:MMMM} {bookings[i].StartTime.Year} kl {bookings[i].StartTime:HH\\:mm} - {bookings[i].EndTime:HH\\:mm}");
                }
                // Använder metod ParseInt för att läsa in användarens val av bokning att uppdatera.
                int updateInput = Helper.ParseInt("Ange vilken bokning du vill uppdatera: ", 1, bookings.Count);
                // Variabel för att uppdatera bokning. - 1 för att nå rätt index.
                Booking bookingToUpdate = bookings[updateInput - 1];

                Console.WriteLine("Vad vill du ändra?");
                Console.WriteLine("[1] Ändra namn på bokningen");
                Console.WriteLine("[2] Ändra datum/tid på bokningen");
                Console.WriteLine("[0] Återgå till bokningsmenyn");
                // Använder metod ParseInt för att läsa in användarens val av vad som ska ändras.
                int updateMenuChoice = Helper.ParseInt("", 0, 2);
                switch (updateMenuChoice)
                {
                    case 1: // Om användaren väljer att ändra namn.
                        Console.Clear();
                        // Använder metod ReadName för att läsa in nytt namn.
                        string newBookerName = Helper.ReadName("Ange nytt namn på den som bokar: ");
                        // Ber användaren bekräfta uppdatering av bokning. Om de svarar ja så uppdateras namnet.
                        if (Helper.ConfirmAction($"ändra namnet för bokning {bookingToUpdate.BookerName} - {bookingToUpdate.StartTime.Day} {bookingToUpdate.StartTime:MMMM} {bookingToUpdate.StartTime.Year}  kl {bookingToUpdate.StartTime:HH\\:mm}  {bookingToUpdate.EndTime:HH\\:mm} till {newBookerName}", "Uppdatering av namn har avbrutits"))
                        {
                            bookingToUpdate.BookerName = newBookerName;
                            Console.WriteLine("Namnet på bokningen har uppdaterats!");
                        }
                        break;
                    case 2: // Om användaren väljer att ändra tid/datum.
                            // Skapar nya DateTime objekt med samma värde som bokningen som ska uppdateras.
                        DateTime newStartTime = bookingToUpdate.StartTime;
                        DateTime newEndTime = bookingToUpdate.EndTime;
                        Console.Clear();
                        Console.WriteLine("Vad vill du ändra?");
                        Console.WriteLine("[1] Ändra datum på bokningen");
                        Console.WriteLine("[2] Ändra tid på bokningen");
                        Console.WriteLine("[0] Återgå till bokningsmenyn");
                        // Använder metod ParseInt för att läsa in användarens val av vad som ska ändras.
                        int updateMenuChoice2 = Helper.ParseInt("", 0, 2);
                        switch (updateMenuChoice2)
                        {
                            case 1: // Om användaren väljer att ändra datum.
                                Console.Clear();
                                // Använder ParseDateTime metod för att läsa in nytt datum.
                                DateTime newBookingDate = Helper.ParseDateTime($"\nAnge nytt datum för bokning:", currentCulture);
                                // Kombinerar datum och tider till ett nytt DateTime objekt för bokningen.
                                newStartTime = newBookingDate + newStartTime.TimeOfDay;
                                newEndTime = newBookingDate + newEndTime.TimeOfDay;
                                // Skapar temporärt Booking objekt för att kolla om ny bokning överlappar med befintliga bokningar.
                                Booking tempBooking = new Booking(bookingToUpdate.BookerName, RoomName, newStartTime, newEndTime);
                                bool overlap = bookings.Any(b => b != bookingToUpdate && b.BookingsOverlap(tempBooking));
                                if (overlap)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                                    Helper.BackToMenu();
                                }
                                // Ber användaren bekräfta uppdatering av bokning. Om de svarar ja så uppdateras datumet.
                                if (Helper.ConfirmAction($"ändra datum för bokning {bookingToUpdate.BookerName} - {bookingToUpdate.StartTime.Day} {bookingToUpdate.StartTime:MMMM} {bookingToUpdate.StartTime.Year} kl {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm} till {newStartTime.Day} {newStartTime:MMMM} {newStartTime.Year}", "Uppdatering av datum har avbrutits"))
                                {
                                    bookingToUpdate.StartTime = newStartTime;
                                    bookingToUpdate.EndTime = newEndTime;
                                    Console.WriteLine("Datumet på bokningen har uppdaterats!");
                                }
                                break;
                            case 2: // Om användaren väljer att ändra tid.
                                Console.Clear();
                                // Använder ReadTimeSpan metod för att läsa in ny start- och sluttid.
                                var (newbookingStartTime, newBookingEndTime) = Helper.ReadTimeSpan("Ange ny starttid", "Ange ny sluttid", bookingToUpdate.StartTime.Date, MaxBookingHours, currentCulture);
                                // Kombinerar datum och tider till ett nytt DateTime objekt för bokningen.
                                newStartTime = newStartTime.Date + newbookingStartTime;
                                newEndTime = newEndTime.Date + newBookingEndTime;
                                // Skapar temporärt Booking objekt för att kolla om ny bokning överlappar med befintliga bokningar.
                                Booking tempBooking2 = new Booking(bookingToUpdate.BookerName, RoomName, newStartTime, newEndTime);
                                overlap = bookings.Any(b => b != bookingToUpdate && b.BookingsOverlap(tempBooking2));
                                if (overlap)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nTiden är redan bokad. Din bokning kunde inte genomföras.");
                                    Helper.BackToMenu();
                                }
                                // Ber användaren bekräfta uppdatering av bokning. Om de svarar ja så uppdateras tiden.
                                if (Helper.ConfirmAction($"ändra tiden för bokning {bookingToUpdate.BookerName} - {bookingToUpdate.StartTime.Day} {bookingToUpdate.StartTime:MMMM} {bookingToUpdate.StartTime.Year} kl {bookingToUpdate.StartTime:HH\\:mm}-{bookingToUpdate.EndTime:HH\\:mm} till  {newStartTime} - {newEndTime}", "Uppdatering av tid har avbrutits"))
                                {
                                    bookingToUpdate.StartTime = newStartTime;
                                    bookingToUpdate.EndTime = newEndTime;

                                    Console.WriteLine("Tiden på bokningen har uppdaterats!");
                                }
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                updateMenuActive = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.Clear();
                                Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                                Helper.BackToMenu();
                                break;
                        }
                        break;
                    case 0: // Om användaren vill återgå till huvudmenyn
                        updateMenuActive = false; // While loopen avslutas.
                        break;
                    default: // Om användaren skriver in något annat än 1, 2 eller 0.
                        Console.Clear();
                        Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                        Helper.BackToMenu();
                        break;
                }
                break;
            }
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
}

// Klass som hanterar bokningar. Gjord av Sara.
public class Booking
{
    public string BookerName { get; set; }
    public string RoomName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
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

    public override int MaxBookingHours => 6;

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
// Klass som håller reda på alla rum som skapats. Gjord av Sara.
public static class RoomRegistry
{
    public static List<IBookable> AllRooms { get; private set; } = new();

    public static void RegisterRoom(IBookable room)
    {
        AllRooms.Add(room);
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
            // Hár gjort hjälpmetod för int tryparse vill du använda det?
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
                        Helper.DisplayMessage();
                        NewRoom();
                        continue;
                }
            }
            else
            {
                Helper.DisplayMessage();
                Helper.BackToMenu();
                continue;
            }
            // Name the room
            Console.Write("\nNamnge rummet: ");
            var newRoomName = "000";
            newRoomName = Console.ReadLine()?.Trim() ?? "";
            // As long as string isnt null/empty, loops through list and compare room names
            if (!string.IsNullOrEmpty(newRoomName))
            {
                foreach (IBookable item in RoomRegistry.AllRooms)
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
                        Helper.DisplayMessage();
                        Helper.BackToMenu();
                        NewRoom();
                        continue;
                }
            }
            else
            {
                Helper.DisplayMessage();
                Helper.BackToMenu();
                NewRoom();
                continue;
            }
            // Add room to the list once user didn't trigger errors and assign ID
            string newRoomId = Guid.NewGuid().ToString("N");

            if (newRoomType == "ClassRoom")
            {
                bool newRoomIsAvailable = true;
                ClassRoom newRoom = new ClassRoom(newRoomId, newRoomName, newRoomCapacity, newRoomEquipment, newRoomIsAvailable);
                RoomRegistry.AllRooms.Add(newRoom);
            }
            if (newRoomType == "GroupRoom")
            {
                bool newRoomIsAvailable = true;
                GroupRoom newRoom = new GroupRoom(newRoomId, newRoomName, newRoomCapacity, newRoomEquipment, newRoomIsAvailable);
                RoomRegistry.AllRooms.Add(newRoom);
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

        // ----------------------------------------------------------------
        // 5. Main
        // ----------------------------------------------------------------
        public static bool runProgram = true;
        static void Main(string[] args)
        {
            // Hardcoded a few rooms to have something to work with
            new ClassRoom("AA9BF90B94804053B746950491256241", "Klassrum 101", 45, true, true);
            new ClassRoom("6FCCFE8B37DF473B9598D08461B7AFF7", "Klassrum 102", 20, true, true);
            new ClassRoom("32EE3ED541C44576BC175D13418BC557", "Klassrum 103", 30, true, true);
            new ClassRoom("D5B43EDB95EB40B799B7D6B857CA8740", "Klassrum 104", 15, false, false);
            new GroupRoom("E057DD50FC7F40539D01A9988185A923", "Grupprum 201", 6, false, false);
            new GroupRoom("21E8740CF52648B587EB9FEBCC8C3C59", "Grupprum 202", 8, true, true);
            new GroupRoom("247256E4889F4F0F8836141736C14163", "Grupprum 203", 4, false, false);
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

                // Använder metod ParseInt för att läsa in användarens menyval.
                int userChoice = Helper.ParseInt("", 0, 4);

                // Filtrerar alla rum i RoomRegistry och lägger endast ClassRoom/GroupRoom objekt i en lista.
                var classRooms = RoomRegistry.AllRooms.OfType<ClassRoom>().ToList();
                var groupRooms = RoomRegistry.AllRooms.OfType<GroupRoom>().ToList();
                switch (userChoice)
                {
                    case 1: // Om användaren väljer "Gör en ny bokning". Gjorts av Sara.
                        Helper.TypeOfRoomMenu("\nVilken typ av bokning vill du göra?");
                        // Använder metod ParseInt för att läsa in användarens val av typ av rum.
                        int newBookingChoice = Helper.ParseInt("", 0, 2);
                        switch (newBookingChoice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                Helper.ShowAvailableRooms("klassrum", classRooms);
                                // Använder metod ParseInt för att läsa in användarens val av rum.
                                int classRoomChoice = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                // Skapar en bokning för det valda rummet med metoden NewBooking. -1 för att nå rätt index.
                                classRooms[classRoomChoice - 1].NewBooking();
                                Helper.BackToMenu();
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                Helper.ShowAvailableRooms("grupprum", groupRooms);
                                int groupRoomChoice = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);
                                groupRooms[groupRoomChoice - 1].NewBooking();
                                Helper.BackToMenu();
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                isRunningMenu = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                                Helper.BackToMenu();
                                break;
                        }
                        break;

                    case 2: // Om användaren väljer "Ta bort bokning". Gjorts av Sara.
                        Helper.TypeOfRoomMenu("\nVad vill du avboka?");
                        // Använder metod ParseInt för att läsa in användarens val av typ av rum.
                        int removeBookingChoice = Helper.ParseInt("", 0, 2);
                        switch (removeBookingChoice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                Console.Clear();
                                Helper.ShowAvailableRooms("klassrum", classRooms);
                                // Använder metod ParseInt för att läsa in användarens val av rum.
                                int classRoomChoiceCancel = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                // Avbryter en bokning för det valda rummet med metoden CancelBooking. -1 för att nå rätt index.
                                classRooms[classRoomChoiceCancel - 1].CancelBooking();
                                Helper.BackToMenu();
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                Console.Clear();
                                Helper.ShowAvailableRooms("grupprum", groupRooms);
                                int groupRoomChoiceCancel = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);
                                groupRooms[groupRoomChoiceCancel - 1].CancelBooking();
                                Helper.BackToMenu();
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn.
                                isRunningMenu = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                                Helper.BackToMenu();
                                break;
                        }
                        break;

                    case 3: // Om användaren väljer "Ändra bokning". Gjorts av Sara.
                        bool updateMenuActive = true; // Variabel som styr loop för menyn.
                        while (updateMenuActive)
                        {
                            Helper.TypeOfRoomMenu("\nVilken typ av bokning vill du uppdatera?");
                            // Använder metod ParseInt för att läsa in användarens val av typ av rum.
                            int updateBookingChoice = Helper.ParseInt("", 0, 2);
                            switch (updateBookingChoice)
                            {
                                case 1: // Om användaren väljer Klassrum.
                                    Helper.ShowAvailableRooms("klassrum", classRooms);
                                    // Använder metod ParseInt för att läsa in användarens val av rum.
                                    int classRoomChoice = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                    // Uppdaterar en bokning för det valda rummet med metoden UpdateBooking. - 1 för att nå rätt index.
                                    classRooms[classRoomChoice - 1].UpdateBooking();
                                    Helper.BackToMenu();
                                    break;
                                case 2: // Om användaren väljer Grupprum.
                                    Helper.ShowAvailableRooms("grupprum", groupRooms);
                                    int groupRoomChoice = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);
                                    groupRooms[groupRoomChoice - 1].UpdateBooking();
                                    Helper.BackToMenu();
                                    break;
                                case 0: // Om användaren vill återgå till huvudmenyn.
                                    updateMenuActive = false;
                                    isRunningMenu = false; // While looperna avslutas.
                                    break;
                                default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                    Console.Clear();
                                    Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                                    Helper.BackToMenu();
                                    break;
                            }
                        }
                        break;
                    case 4:
                        Console.Clear();
                        // Temporary comment for testing
                        Console.WriteLine("Här kommer en ListBookings finnas!");
                        Console.ReadKey();
                        // ListBookings();
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

                int userChoice = Helper.ParseInt("", 0, 2);

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
                        RoomRegistry.NewRoom();
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

        public static class Helper
        {
            // Metod för att ändra string till int. (Används för menyer) Gjord av Sara.
            public static int ParseInt(string userPrompt, int min, int max)
            {
                while (true) // Loop för att säkerställa att vi får ut ett giltigt val.
                {
                    Console.WriteLine(userPrompt);
                    string? input = Console.ReadLine()?.Trim();
                    // Kontrollerar så att användaren skriver in något.
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Du måste skriva in en siffra! Försök igen.");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    // Läser in användarens val och försöker konvertera till int.
                    // Kontrollerar så att användaren väljer ett giltigt nummer
                    if (int.TryParse(input, out int val) && val >= min && val <= max)
                    {
                        return val; // Returnerar användarens val i en int.
                    }
                    else
                    {
                        // Ifall det inte är giltigt skickas ett felmeddelande.
                        Console.WriteLine($"Du måste skriva in en siffra mellan {min}-{max}! Försök igen.");
                        continue;
                    }

                }
            }
            // Metod för att läsa in datum på bokningar. Gjord av Sara.
            public static DateTime ParseDateTime(string userPrompt, CultureInfo culture)
            {
                Console.Clear();
                while (true) // Loop för att säkerställa att vi får ut ett giltigt datum.
                {
                    Console.WriteLine($"{userPrompt} (Format: {culture.DateTimeFormat.ShortDatePattern}): ");
                    string? input = Console.ReadLine().Trim().Replace(" ", "");
                    // Kontrollerar så att användaren skriver in något.
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Du måste skriva in ett datum! Försök igen.");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    // Läser in användarens val och försöker konvertera till DateTime objekt.
                    // Läser in DateTime utefter kulturellt format.
                    if (DateTime.TryParse(input, culture, DateTimeStyles.None, out DateTime date))
                    {
                        // Kontrollerar så att datumet ej passerat.
                        if (date.Date < DateTime.Today)
                        {
                            Console.WriteLine("Du kan inte boka ett datum som passerat! Försök igen.");
                            continue;
                        }
                        // Kontrollerar så att datumet är på en helgdag.
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            Console.WriteLine("Du kan inte göra en bokning på en helgdag! Försök igen.");
                            continue;
                        }
                        return date.Date; // Returnerar användarens val i ett DateTime objekt med endast datum.
                    }
                    else
                    {
                        // Ifall användarens input är ogiltigt/fel format skickas ett felmeddelande.
                        // Visar exempel på rätt format utefter kultur.
                        Console.WriteLine($"Felaktigt datumformat! Försök igen (Korrekt format: {DateTime.Now.ToString(culture.DateTimeFormat.ShortDatePattern)}).");
                        continue;
                    }
                }
            }
            // Metod för att läsa in start- och sluttid på bokningar. Gjord av Sara.
            public static (TimeSpan StartTime, TimeSpan EndTime) ReadTimeSpan(string userPrompt, string userPrompt2, DateTime bookingDate, int maxBookingHours, CultureInfo culture)
            {
                Console.Clear();
                TimeSpan bookingStartTime, bookingEndTime;

                while (true) // Loop för att säkerställa att vi får ut en giltig starttid.
                {   // Användaren skriver in starttid. Visar exempelformat beroende på kultur.
                    Console.Write($"\n{userPrompt} (Format: ({DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)}): ");
                    string startTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");
                    // Kontrollerar så att användaren skriver in något.
                    if (string.IsNullOrWhiteSpace(startTimeInput))
                    {
                        Console.WriteLine("Du måste skriva in en tid! Försök igen.");
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                    // Läser in användarens val och försöker konvertera till ett DateTime objekt.
                    // Läser in DateTime utefter kulturellt format.
                    if (!DateTime.TryParse(startTimeInput, culture, DateTimeStyles.None, out DateTime startTimeDateTime))
                    {
                        Console.WriteLine($"Felaktigt tidsformat! Försök igen (Korrekt format: ({DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)})");
                        continue;
                    }
                    // Ändrar DateTime objekt till TimeSpan.
                    bookingStartTime = startTimeDateTime.TimeOfDay;
                    // Skapar nytt DateTime objekt med datum och starttid för jämförelse.
                    DateTime startDateTime = bookingDate + bookingStartTime;
                    // Kontrollerar så att starttiden inte passerat.
                    if (startDateTime < DateTime.Now)
                    {
                        Console.WriteLine("Du kan inte boka en tid som passerat!");
                        continue;
                    }
                    // Tidigast tid rum kan bokas (kl 8). String som skriver ut tiden utefter kultur.
                    TimeSpan earliestBookingTime = TimeSpan.FromHours(8);
                    string earliestBookingTimeCurrentCulture = DateTime.Today.Add(earliestBookingTime).ToString("t", culture);
                    if (bookingStartTime < earliestBookingTime)
                    {
                        Console.WriteLine($"Du kan inte boka rum innan kl {earliestBookingTimeCurrentCulture}");
                        continue;
                    }
                    break; // Lämnar loopen då vi har en giltig tid.
                }
                while (true) // Loop för att säkerställa giltig sluttid.
                {
                    Console.Write($"\n{userPrompt2} (Format:{DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)}: ");
                    string endTimeInput = Console.ReadLine().Trim().Replace(".", ":").Replace(" ", "");

                    if (!DateTime.TryParse(endTimeInput, culture, DateTimeStyles.None, out DateTime endTimeDateTime))
                    {
                        Console.WriteLine($"Felaktigt tidsformat! Försök igen (Korrekt format: {DateTime.Today.Add(TimeSpan.FromHours(9)).ToString("t", culture)}).");
                        continue;
                    }
                    bookingEndTime = endTimeDateTime.TimeOfDay;
                    // Kontrollerar så att sluttiden inte är före starttiden.
                    if (bookingEndTime <= bookingStartTime)
                    {
                        Console.WriteLine("Sluttiden måste vara efter starttiden!");
                        continue;
                    }
                    // Senast tid rum kan bokas (kl 17). String som skriver ut tiden utefter kultur.
                    TimeSpan latestBookingTime = TimeSpan.FromHours(17);
                    string latestBookingTimeCurrentCulture = DateTime.Today.Add(latestBookingTime).ToString("t", culture);
                    if (bookingEndTime > latestBookingTime)
                    {
                        Console.WriteLine($"Du kan inte boka rum efter kl {latestBookingTimeCurrentCulture}");
                        continue;
                    }
                    // Jämför bokningens längd med maxtid för bokning (6 timmar för grupprum, 8 timmar för klassrum).
                    TimeSpan bookingDuration = bookingEndTime - bookingStartTime;
                    if (bookingDuration.TotalHours > maxBookingHours)
                    {
                        Console.WriteLine($"Du kan inte boka rummet mer än {maxBookingHours} timmar!");
                        continue;
                    }

                    break; // Lämnar loopen då vi har en giltig tid.
                }
                return (bookingStartTime, bookingEndTime); // Returnerar start- och sluttid.
            }
            // Metod för att visa alla klassrum/grupprum. Gjord av Sara.
            public static void ShowAvailableRooms<T>(string type, List<T> typeOfRoom) where T : Room
            {
                Console.Clear();
                // Visar varje rum med namn och kapacitet.
                // Beroende på vilken rumslista man skickar in (grupprum/klassrum).
                Console.WriteLine($"Tillgängliga {type}:");
                for (int i = 0; i < typeOfRoom.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {typeOfRoom[i].RoomName} Kapacitet: {typeOfRoom[i].RoomCapacity}");
                }
            }
            // Metod för att läsa in namn. Gjord av Sara.
            public static string ReadName(string userPrompt)
            {
                Console.Clear();
                string bookerNameInput;
                while (true)
                {
                    // Användaren skriver in sitt namn
                    Console.WriteLine($"{userPrompt}");
                    bookerNameInput = Console.ReadLine().Trim();
                    // Kontrollerar så att namnet inte är tomt/whitespace. 
                    if (string.IsNullOrWhiteSpace(bookerNameInput))
                    {
                        // Skriver ut felmeddelande och användaren skickas tillbaka till bokningsmenyn.
                        Console.WriteLine("Du måste ange ett namn för att boka ett rum!");
                        continue;
                    }
                    // Kontrollerar så att namnet inte innehåller siffror eller specialtecken (förutom t.ex &, / eller +).
                    if (bookerNameInput.Contains('1') || bookerNameInput.Contains('2') || bookerNameInput.Contains('3')
                        || bookerNameInput.Contains('4') || bookerNameInput.Contains('5') || bookerNameInput.Contains('6')
                        || bookerNameInput.Contains('7') || bookerNameInput.Contains('8') || bookerNameInput.Contains('9')
                        || bookerNameInput.Contains('!') || bookerNameInput.Contains('@') || bookerNameInput.Contains('£')
                        || bookerNameInput.Contains('%') || bookerNameInput.Contains('#') || bookerNameInput.Contains('¤')
                        || bookerNameInput.Contains('=') || bookerNameInput.Contains('?') || bookerNameInput.Contains('^')
                        || bookerNameInput.Contains('*') || bookerNameInput.Contains('<') || bookerNameInput.Contains('>')
                        || bookerNameInput.Contains(':') || bookerNameInput.Contains(';') || bookerNameInput.Contains('|')
                        || bookerNameInput.Contains('{') || bookerNameInput.Contains('}') || bookerNameInput.Contains('[')
                        || bookerNameInput.Contains(']') || bookerNameInput.Contains('½') || bookerNameInput.Contains('/')
                        || bookerNameInput.Contains('(') || bookerNameInput.Contains(')') || bookerNameInput.Contains('\\'))
                    {
                        Console.WriteLine("Namnet får endast innehålla bokstäver!");
                        continue;
                    }
                    break;
                }
                // Formaterar namn så första bokstaven är stor och resten små.
                string bookerName = char.ToUpper(bookerNameInput[0]) + bookerNameInput.Substring(1).ToLower();
                return bookerName;
            }
            // Metod för att låta användaren bekräfta val. Gjord av Sara.
            public static bool ConfirmAction(string userPrompt, string actionCanceled)
            {
                bool confirmLoop = true;
                while (confirmLoop) // Loop för att få ut ett giltigt svar.
                {
                    Console.Clear();
                    Console.WriteLine($"Är du säker på att du vill {userPrompt}? (Ja/Nej)");
                    string confirmAction = Console.ReadLine().Trim().ToLower();
                    // Om användaren skriver ja eller j tas bokningen bort.
                    // Skriver de nej eller n avbryts det.
                    if (confirmAction == "j" || confirmAction == "ja")
                    {
                        return true;
                    }
                    else if (confirmAction == "n" || confirmAction == "nej")
                    {
                        Console.WriteLine($"{actionCanceled}");

                        return false;
                    }
                    else
                    {
                        Console.WriteLine($"Du måste bekräfta Ja eller Nej!");
                        BackToMenu();
                        continue; // Skickar tillbaka användaren till början av loopen.
                    }
                }
                return false;
            }

            public static void TypeOfRoomMenu(string userPrompt)
            {
                Console.Clear();
                Console.WriteLine($"{userPrompt}");
                Console.WriteLine("[1] Klassrum");
                Console.WriteLine("[2] Grupprum");
                Console.WriteLine("\n[0] Återgå till huvudmenyn");
            }
            public static void BackToMenu()
            {
                Thread.Sleep(1000);
                Console.WriteLine("\nTryck ENTER för att återgå till menyn");
                Console.ReadKey();
                Console.Clear();
            }
            public static void DisplayMessage()
            {
                Console.Clear();
                Console.WriteLine("Felmeddelande:\n\n" +
                                    "Du skrev inte en giltig siffra.\n" +
                                    "Vänligen försök igen.\n\n");
            }
        }
    }
}
