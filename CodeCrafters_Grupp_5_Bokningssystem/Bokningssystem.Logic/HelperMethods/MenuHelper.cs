using Bokningssystem.Logic.RoomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.HelperMethods
{
    public static class MenuHelper
    {
        // ----------------------------------------------------------------
        //                       MENYER OCH VISNING
        // ----------------------------------------------------------------
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

                int userChoice = InputHelper.ParseInt("\nAnge menyval: ", 0, 4);

                // Filtrerar alla rum i RoomRegistry och lägger endast ClassRoom/GroupRoom objekt i en lista.
                var classRooms = RoomRegistry.AllRooms.OfType<ClassRoom>().ToList();
                var groupRooms = RoomRegistry.AllRooms.OfType<GroupRoom>().ToList();
                switch (userChoice)
                {
                    // -------------------------------
                    //  Ny bokning. Gjorts av Sara.
                    // -------------------------------
                    case 1:
                        TypeOfRoomMenu();
                        int newBookingChoice = InputHelper.ParseInt("\nAnge vad du vill boka: ", 0, 2);
                        switch (newBookingChoice)
                        {
                            case 1: // Om användaren väljer klassrum.
                                ShowRoomsType("klassrum", classRooms);
                                if (classRooms.Count > 0)
                                {
                                    int classRoomChoice = InputHelper.ParseInt("\nVälj rum: ", 1, classRooms.Count);
                                    // Skapar en bokning för det rummet som användaren valt. -1 för att nå rätt index.
                                    classRooms[classRoomChoice - 1].NewBooking();
                                    BackToMenu("tillbaka till menyn...");
                                }
                                break;
                            case 2: // Om användaren väljer grupprum.
                                ShowRoomsType("grupprum", groupRooms);
                                if (groupRooms.Count > 0)
                                {
                                    int groupRoomChoice = InputHelper.ParseInt("\nVälj rum: ", 1, groupRooms.Count);
                                    groupRooms[groupRoomChoice - 1].NewBooking();
                                    BackToMenu("tillbaka till menyn...");
                                }
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn
                                isRunningMenu = false;
                                break;
                            default:
                                DisplayMessage(0, 2);
                                BackToMenu("tillbaka till menyn...");
                                break;
                        }
                        break;
                    // ---------------------------------
                    //  Ta bort bokning. Gjorts av Sara.
                    // ---------------------------------
                    case 2:
                        TypeOfRoomMenu();

                        int removeBookingChoice = InputHelper.ParseInt("\nAnge vad du vill ta bort: ", 0, 2);
                        switch (removeBookingChoice)
                        {
                            case 1: // Om användaren väljer klassrum.
                                ShowRoomsType("klassrum", classRooms);
                                if (classRooms.Count > 0)
                                {
                                    int classRoomChoiceCancel = InputHelper.ParseInt("\nVälj rum: ", 1, classRooms.Count);
                                    // Tar bort en bokning för det rummet som användaren valt. -1 för att nå rätt index.
                                    classRooms[classRoomChoiceCancel - 1].CancelBooking();
                                    BackToMenu("tillbaka till menyn...");
                                }
                                break;
                            case 2: // Om användaren väljer grupprum.
                                ShowRoomsType("grupprum", groupRooms);
                                if (groupRooms.Count > 0)
                                {
                                    int groupRoomChoiceCancel = InputHelper.ParseInt("\nVälj rum: ", 1, groupRooms.Count);
                                    groupRooms[groupRoomChoiceCancel - 1].CancelBooking();
                                    BackToMenu("tillbaka till menyn...");
                                }
                                break;
                            case 0: // Om användaren vill återgå till huvudmenyn.
                                isRunningMenu = false;
                                break;
                            default:
                                DisplayMessage(0, 2);
                                BackToMenu("tillbaka till menyn...");
                                break;
                        }
                        break;
                    // -------------------------------
                    //  Ändra bokning. Gjorts av Sara.
                    // -------------------------------
                    case 3:
                        while (true)
                        {
                            TypeOfRoomMenu();
                            int updateBookingChoice = InputHelper.ParseInt("\nAnge vad du vill uppdatera: ", 0, 2);
                            switch (updateBookingChoice)
                            {
                                case 1: // Om användaren väljer klassrum.
                                    ShowRoomsType("klassrum", classRooms);
                                    if (classRooms.Count > 0)
                                    {
                                        int classRoomChoice = InputHelper.ParseInt("\nVälj rum: ", 1, classRooms.Count);
                                        // Uppdaterar bokning för det rummet som användaren valt. -1 för att nå rätt index.
                                        classRooms[classRoomChoice - 1].UpdateBooking();
                                        BackToMenu("tillbaka till menyn...");
                                    }
                                    break;
                                case 2: // Om användaren väljer grupprum.
                                    ShowRoomsType("grupprum", groupRooms);
                                    if (classRooms.Count > 0)
                                    {
                                        int groupRoomChoice = InputHelper.ParseInt("\nVälj rum: ", 1, groupRooms.Count);
                                        groupRooms[groupRoomChoice - 1].UpdateBooking();
                                        BackToMenu("tillbaka till menyn...");
                                    }
                                    break;
                                case 0: // Om användaren vill återgå till huvudmenyn.
                                    isRunningMenu = false;
                                    break;
                                default:
                                    DisplayMessage(0, 2);
                                    BackToMenu("tillbaka till menyn...");
                                    break;
                            }
                            break;
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
                        isRunningMenu = false;
                        break;
                    default:
                        DisplayMessage(0, 4);
                        BackToMenu("tillbaka till menyn...");
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

                int userChoice = InputHelper.ParseInt("\nAnge menyval: ", 0, 2);

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
                        RoomRegistry.NewRoom();
                        break;
                    case 0:
                        isRunningMenu = false;
                        break;
                    default:
                        DisplayMessage(0, 2);
                        continue;
                }


            }
        }
        // ----------------------------------------------------------------
        //    Metod för att visa alla klassrum/grupprum. Gjord av Sara.
        // ----------------------------------------------------------------
        public static void ShowRoomsType<T>(string type, List<T> typeOfRoom) where T : Room
        {
            Console.Clear();
            // Visar varje rum med namn, kapacitet & status.
            // Beroende på vilken rumslista man skickar in (grupprum/klassrum).
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║                      Tillgängliga {type}                       ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════════════╝\n");
            if (typeOfRoom.Count > 0)
            {
                for (int i = 0; i < typeOfRoom.Count; i++)
                {
                    var room = typeOfRoom[i];
                    // Variabel som sparar ett boknings objekt ifall det är någon bokning som pågår just nu.
                    var currentBooking = room.Bookings
                    .FirstOrDefault(b => DateTime.Now >= b.StartTime && DateTime.Now < b.EndTime);
                    Console.Write($"[{i + 1}] {room.Name} Kapacitet: {room.RoomCapacity} ");
                    if (room is ClassRoom classRoom)
                    { Console.Write($" Projektor: {(classRoom.HasProjector ? "Ja " : "Nej")} "); }
                    if (room is GroupRoom groupRoom)
                    { Console.Write($" Smartboard: {(groupRoom.HasSmartBoard ? "Ja " : "Nej")} "); }
                    // Visar status i olika färger beroende på om rummet är bokat just nu eller inte.
                    Console.ForegroundColor = room.IsCurrentlyAvailable ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(room.IsCurrentlyAvailable ? "Tillgängligt just nu."
                        : currentBooking != null ? $"Upptaget just nu. Kan bokas efter kl {currentBooking.EndTime:HH:mm}."
                        : "Upptaget just nu.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine($"Det finns inga {type}");
                BackToMenu("tillbaka till menyn...");
            }
        }
        public static void ShowBookingsForRoom(Room rooms)
        {
            for (int i = 0; i < rooms.Bookings.Count; i++)
            {
                var booking = rooms.Bookings[i];
                Console.WriteLine($"[{i + 1}] {booking.BookerName} " +
                    $"- {booking.StartTime:dd MMMM yyyy} " +
                    $"{booking.StartTime:HH\\:mm} - {booking.EndTime:HH\\:mm}");
            }
        }
        public static void TypeOfRoomMenu()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║        Välj typ av rum         ║");
            Console.WriteLine("╠════════════════════════════════╣");
            Console.WriteLine("║                                ║");
            Console.WriteLine("║   [1] Klassrum                 ║");
            Console.WriteLine("║   [2] Grupprum                 ║");
            Console.WriteLine("║   [0] Återgå till huvudmenyn   ║");
            Console.WriteLine("║                                ║");
            Console.WriteLine("╚════════════════════════════════╝");
        }
        public static void NewBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║        Skapa ny bokning        ║");
            Console.WriteLine("╚════════════════════════════════╝");
        }
        public static void CancelBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║         Ta bort bokning        ║");
            Console.WriteLine("╚════════════════════════════════╝\n");
        }
        public static void UpdateBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║        Uppdatera bokning       ║");
            Console.WriteLine("╚════════════════════════════════╝\n");
        }
        public static void UpdateBookingMenuChoices()
        {
            Console.Clear();
            Console.WriteLine("\n╔═════════════════════════════════╗");
            Console.WriteLine("║        Uppdatera bokning        ║");
            Console.WriteLine("╠═════════════════════════════════╣");
            Console.WriteLine("║  [1] Ändra namn på bokning      ║");
            Console.WriteLine("║  [2] Ändra datum/tid på bokning ║");
            Console.WriteLine("║  [0] Återgå till menyn          ║");
            Console.WriteLine("╚═════════════════════════════════╝\n");
        }
        public static void UpdateBookingMenuChoicesDateAndTime()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║        Uppdatera bokning       ║");
            Console.WriteLine("╠════════════════════════════════╣");
            Console.WriteLine("║   [1] Ändra datum på bokning   ║");
            Console.WriteLine("║   [2] Ändra tid på bokning     ║");
            Console.WriteLine("║   [0] Återgå till menyn        ║");
            Console.WriteLine("╚════════════════════════════════╝\n");
        }
        public static void NewRoomMenu()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║         Skapa nytt rum         ║");
            Console.WriteLine("╚════════════════════════════════╝");
        }
        public static void BackToMenu(string message)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"\nTryck [ENTER] för att gå {message}");
            Console.ReadKey();
            Console.Clear();
        }
        public static void DisplayMessage(int min, int max)
        {
            Console.Clear();
            Console.WriteLine($"\nDu måste skriva in en siffra mellan {min}-{max}! Vänligen försök igen.");

        }
    }

}
