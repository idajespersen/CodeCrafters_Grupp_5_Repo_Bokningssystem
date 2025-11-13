using Bokningssystem.Logic;
using Bokningssystem.Logic.HelperMethods;
using Bokningssystem.Logic.RoomClasses;
using Grupp_5_Bokningssystem;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml.Serialization;



namespace Grupp_5_Bokningssystem
{
    // !!! TO-DO !!!
    // Implement persistency using a file to store room names
    // Implement persistency using a file to store developers names
    // Read from and write to these files as needed
    // Read when program starts
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

        
    }
}

    

