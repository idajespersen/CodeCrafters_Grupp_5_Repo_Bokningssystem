using Bokningssystem.Logic.HelperMethods;
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

        static void Main()
        {
            // Temporära klassrum/grupprum tills newroom metod är klar.
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
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n - Bokningssystemet -\n\n" +
                    "Här kan du göra bokningar av rum och ändra dessa vid behov.\n");
            Console.WriteLine(" - Huvudmeny -");
            Console.WriteLine(" [1] - Bokningshantering");
            Console.WriteLine(" [2] - Rumshantering");
            Console.WriteLine(" [3] - Om oss");
            Console.WriteLine(" [0] - Avsluta programmet");

            int mainMenuChoice = Helper.ParseInt("", 0, 3);

            switch (mainMenuChoice)
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
                default: // Om användaren skriver in något annat än 1-3 eller 0.
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
            bool bookingMenuActive = true; // Variabel som styr loop för bokningsmenyn.
            while (bookingMenuActive)
            {
                Console.Clear();
                Console.WriteLine("\n - Bokningsmenyn -\n\n");
                Console.WriteLine(" [1] - Gör en ny bokning");
                Console.WriteLine(" [2] - Ta bort bokning");
                Console.WriteLine(" [3] - Ändra bokning");
                Console.WriteLine(" [4] - Visa bokningar\n");
                Console.WriteLine(" [0] - Återgå till huvudmenyn\n");
                // Använder metod ParseInt för att läsa in användarens menyval.
                int bookingMenuChoice = Helper.ParseInt("", 0, 4);

                // Filtrerar alla rum i RoomRegistry och lägger endast ClassRoom/GroupRoom objekt i en lista.
                var classRooms = RoomRegistry.AllRooms.OfType<ClassRoom>().ToList();
                var groupRooms = RoomRegistry.AllRooms.OfType<GroupRoom>().ToList();

                switch (bookingMenuChoice)
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
                                bookingMenuActive = false; // While loopen avslutas.
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
                                bookingMenuActive = false; // While loopen avslutas.
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
                                    bookingMenuActive = false; // While looperna avslutas.
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
                        Console.WriteLine("Här kommer en ListBookings finnas!");
                        Console.WriteLine(" [1] - Visa alla bokningar\n");
                        Console.WriteLine(" [2] - Visa bokningar för specifikt år\n");
                        Console.WriteLine(" [0] - Återgå till huvudmenyn\n");

                        int listBookingsChoice = Helper.ParseInt("", 0, 2);
                        switch (listBookingsChoice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                Console.Clear();

                                Helper.BackToMenu();
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                Console.Clear();

                                Helper.BackToMenu();
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn.
                                bookingMenuActive = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                                Helper.BackToMenu();
                                break;
                        }

                        Helper.BackToMenu();
                        break;
                    case 0: // Om användaren vill återgå till huvudmenyn
                        bookingMenuActive = false; // While loopen avslutas.
                        return;
                    default: // Om användaren skriver in något annat än 1-4 eller 0.
                        Console.Clear();
                        Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[4].\n");
                        Helper.BackToMenu();
                        break;
                }
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
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    // Temporary comment for testing
                    Console.WriteLine("Här kommer en NewRoom finnas!");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 0:
                    Console.Clear();
                    return;
                default: // Om användaren skriver in något annat än 1, 2 eller 0.
                    Console.Clear();
                    Console.WriteLine("Vänligen skriv in en siffra mellan [1]-[2].\n");
                    Helper.BackToMenu();
                    break;
            }
        }

    }

    // Use interface as return type where relevant 
}