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
        // Test Data
        // ----------------------------------------------------------------
        // Hardcoded developer names, to be replaced with file persistency
        public static string devName1 = "Daniel Skalk";
        public static string devName2 = "Ida Jespersen";
        public static string devName3 = "Sara Sundqvist";
        public static string devName4 = "Tove";

        // ----------------------------------------------------------------
        // Main
        // ----------------------------------------------------------------
        public static bool runProgram = true;
        static void Main(string[] args)
        {
            // Hardcoded a few rooms to have something to work with
            RoomRegistry.RegisterRoom(new ClassRoom("AA9BF90B94804053B746950491256241", "Klassrum 101", 45, true, true));
            RoomRegistry.RegisterRoom(new ClassRoom("6FCCFE8B37DF473B9598D08461B7AFF7", "Klassrum 102", 20, true, true));
            RoomRegistry.RegisterRoom(new ClassRoom("32EE3ED541C44576BC175D13418BC557", "Klassrum 103", 30, true, true));
            RoomRegistry.RegisterRoom(new ClassRoom("D5B43EDB95EB40B799B7D6B857CA8740", "Klassrum 104", 15, false, false));
            RoomRegistry.RegisterRoom(new GroupRoom("E057DD50FC7F40539D01A9988185A923", "Grupprum 201", 6, false, false));
            RoomRegistry.RegisterRoom(new GroupRoom("21E8740CF52648B587EB9FEBCC8C3C59", "Grupprum 202", 8, true, true));
            RoomRegistry.RegisterRoom(new GroupRoom("247256E4889F4F0F8836141736C14163", "Grupprum 203", 4, false, false));
            while (runProgram == true)
            {
                MainMenu();
            }
        }

        // ----------------------------------------------------------------
        // Methods
        // ----------------------------------------------------------------
        public static void MainMenu()
        {
            bool isRunningMenu = true;

            while (isRunningMenu == true)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════╗");
                Console.WriteLine("║           Huvudmeny            ║");
                Console.WriteLine("╠════════════════════════════════╣");
                Console.WriteLine("║                                ║");
                Console.WriteLine("║ Här kan du boka och ändra rum. ║");
                Console.WriteLine("║                                ║");
                Console.WriteLine("╠════════════════════════════════╣");
                Console.WriteLine("║                                ║");
                Console.WriteLine("║   [1] Bokningshantering        ║");
                Console.WriteLine("║   [2] Rumshantering            ║");
                Console.WriteLine("║   [3] Om programmet            ║");
                Console.WriteLine("║   [0] Avsluta programmet       ║");
                Console.WriteLine("║                                ║");
                Console.WriteLine("╚════════════════════════════════╝");

                int userChoice = Helper.ParseInt("\nAnge menyval: ", 0, 3);
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
                        isRunningMenu = false;
                            break;
                        default:
                        Helper.DisplayMessage(0, 3);
                        break;
                    }
                }

            }
        

        public static void BookingMenu()
        {
            bool isRunningMenu = true;

            while (isRunningMenu)
            {

                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════╗");
                Console.WriteLine("║         Bokningsmenyn          ║");
                Console.WriteLine("╠════════════════════════════════╣");
                Console.WriteLine("║                                ║");
                Console.WriteLine("║   [1] Gör en ny bokning        ║");
                Console.WriteLine("║   [2] Ta bort bokning          ║");
                Console.WriteLine("║   [3] Ändra bokning            ║");
                Console.WriteLine("║   [4] Visa bokningar           ║");
                Console.WriteLine("║   [0] Återgå till huvudmenyn   ║");
                Console.WriteLine("║                                ║");
                Console.WriteLine("╚════════════════════════════════╝");

                // Använder metod ParseInt för att läsa in användarens menyval.
                int userChoice = Helper.ParseInt("\nAnge menyval: ", 0, 4);

                // Filtrerar alla rum i RoomRegistry och lägger endast ClassRoom/GroupRoom objekt i en lista.
                var classRooms = RoomRegistry.AllRooms.OfType<ClassRoom>().ToList();
                var groupRooms = RoomRegistry.AllRooms.OfType<GroupRoom>().ToList();
                switch (userChoice)
                {
                    case 1: // Om användaren väljer "Gör en ny bokning". Gjorts av Sara.
                        Helper.TypeOfRoomMenu();
                        // Använder metod ParseInt för att läsa in användarens val av typ av rum.
                        int newBookingChoice = Helper.ParseInt("\nAnge vad du vill boka: ", 0, 2);
                        switch (newBookingChoice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                Helper.ShowAvailableRooms("Klassrum", classRooms);
                                // Använder metod ParseInt för att läsa in användarens val av rum.
                                int classRoomChoice = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                // Skapar en bokning för det valda rummet med metoden NewBooking. -1 för att nå rätt index.
                                classRooms[classRoomChoice - 1].NewBooking();
                                Helper.BackToMenu("till menyn...");
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                Helper.ShowAvailableRooms("Grupprum", groupRooms);
                                int groupRoomChoice = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);
                                groupRooms[groupRoomChoice - 1].NewBooking();
                                Helper.BackToMenu("till menyn...");
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                isRunningMenu = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Helper.DisplayMessage(0, 2);
                                Helper.BackToMenu("till menyn...");
                                break;
                        }
                        break;

                    case 2: // Om användaren väljer "Ta bort bokning". Gjorts av Sara.
                        Helper.TypeOfRoomMenu();
                        // Använder metod ParseInt för att läsa in användarens val av typ av rum.
                        int removeBookingChoice = Helper.ParseInt("\nAnge vad du vill ta bort: ", 0, 2);
                        switch (removeBookingChoice)
                        {
                            case 1: // Om användaren väljer Klassrum.
                                Console.Clear();
                                Helper.ShowAvailableRooms("Klassrum", classRooms);
                                // Använder metod ParseInt för att läsa in användarens val av rum.
                                int classRoomChoiceCancel = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                // Avbryter en bokning för det valda rummet med metoden CancelBooking. -1 för att nå rätt index.
                                classRooms[classRoomChoiceCancel - 1].CancelBooking();
                                Helper.BackToMenu("till menyn...");
                                break;
                            case 2: // Om användaren väljer Grupprum.
                                Console.Clear();
                                Helper.ShowAvailableRooms("Grupprum", groupRooms);
                                int groupRoomChoiceCancel = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);
                                groupRooms[groupRoomChoiceCancel - 1].CancelBooking();
                                Helper.BackToMenu("till menyn...");
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn.
                                isRunningMenu = false; // While loopen avslutas.
                                break;
                            default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                Helper.DisplayMessage(0, 2);
                                Helper.BackToMenu("till menyn...");
                                break;
                        }
                        break;

                    case 3: // Om användaren väljer "Ändra bokning". Gjorts av Sara.
                        bool updateMenuActive = true; // Variabel som styr loop för menyn.
                        while (updateMenuActive)
                        {
                            Helper.TypeOfRoomMenu();
                            // Använder metod ParseInt för att läsa in användarens val av typ av rum.
                            int updateBookingChoice = Helper.ParseInt("\nAnge vad du vill uppdatera: ", 0, 2);
                            switch (updateBookingChoice)
                            {
                                case 1: // Om användaren väljer Klassrum.
                                    Helper.ShowAvailableRooms("Klassrum", classRooms);
                                    // Använder metod ParseInt för att läsa in användarens val av rum.
                                    int classRoomChoice = Helper.ParseInt("Välj rum: ", 1, classRooms.Count);
                                    // Uppdaterar en bokning för det valda rummet med metoden UpdateBooking. - 1 för att nå rätt index.
                                    classRooms[classRoomChoice - 1].UpdateBooking();
                                    Helper.BackToMenu("till menyn...");
                                    break;
                                case 2: // Om användaren väljer Grupprum.
                                    Helper.ShowAvailableRooms("Grupprum", groupRooms);
                                    int groupRoomChoice = Helper.ParseInt("Välj rum: ", 1, groupRooms.Count);
                                    groupRooms[groupRoomChoice - 1].UpdateBooking();
                                    Helper.BackToMenu("till menyn...");
                                    break;
                                case 0: // Om användaren vill återgå till huvudmenyn.
                                    updateMenuActive = false;
                                    isRunningMenu = false; // While looperna avslutas.
                                    break;
                                default: // Om användaren skriver in något annat än 1, 2 eller 0.
                                    Helper.DisplayMessage(0, 2);
                                    Helper.BackToMenu("till menyn...");
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
                        Helper.DisplayMessage(0,4);
                        Helper.BackToMenu("till menyn...");
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
                Console.WriteLine("\n╔════════════════════════════════╗");
                Console.WriteLine("║          Rumshantering         ║");
                Console.WriteLine("╠════════════════════════════════╣");
                Console.WriteLine("║                                ║");
                Console.WriteLine("║   [1] Sök efter rum            ║");
                Console.WriteLine("║   [2] Skapa ett nytt rum       ║");
                Console.WriteLine("║   [0] Återgå till huvudmenyn   ║");
                Console.WriteLine("║                                ║");
                Console.WriteLine("╚════════════════════════════════╝");

                int userChoice = Helper.ParseInt("\nAnge menyval: ", 0, 2);

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
                        RoomRegistry.NewRoom();
                        Console.Clear();
                        break;
                    case 0:
                        Console.Clear();
                        isRunningMenu = false;
                        break;
                    default:
                        Console.Clear();
                        Helper.DisplayMessage(0,2);
                        continue;
                }


            }
        }

        public static void AboutInfoscreen()
        {
            Console.Clear();
            Console.WriteLine($"Det här programmet skapades av:\n\n" +
                                $" - CodeCrafters-Teamet - \n\n   {devName1}\n\n   {devName2}\n\n   {devName3}\n\n   {devName4}\n\n\n" +
                                $"Tryck på [ENTER] för att återgå till menyn.");
            Console.ReadKey();
        }

        
    }
}

    

