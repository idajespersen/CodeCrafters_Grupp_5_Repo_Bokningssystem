using Bokningssystem.Logic.Helpers;
using Bokningssystem.Logic.Rooms;
using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic;
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
    // Handle start and stop times for bookings
    // Format date and time appropriately
    // TimeSpan for duration of booking
    // Use interface as return type where relevant 
}