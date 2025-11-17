using Bokningssystem.Logic;
using Bokningssystem.Logic.HelperMethods;
using Bokningssystem.Logic.RoomClasses;
using Grupp_5_Bokningssystem;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Serialization;



namespace Grupp_5_Bokningssystem
{
    internal class Program
    {
        private static string credits = FileHelper.ReadFromEmbeddedFile("credits.txt");

        // ----------------------------------------------------------------
        //                             Main
        // ----------------------------------------------------------------
        public static bool runProgram = true;
        static void Main(string[] args)
        {
            FileHelper.Initialize(Assembly.GetExecutingAssembly());
            FileHelper.AddFolderPath(typeof(ClassRoom), "Klassrum");
            FileHelper.AddFolderPath(typeof(GroupRoom), "Grupprum");

            // Hardcoded a few rooms to have something to work with
            /*
            RoomRegistry.RegisterRoom(new ClassRoom("AA9BF90B94804053B746950491256241", "Klassrum 101", 45, true, true));
            RoomRegistry.RegisterRoom(new ClassRoom("6FCCFE8B37DF473B9598D08461B7AFF7", "Klassrum 102", 20, true, true));
            RoomRegistry.RegisterRoom(new ClassRoom("32EE3ED541C44576BC175D13418BC557", "Klassrum 103", 30, true, true));
            RoomRegistry.RegisterRoom(new ClassRoom("D5B43EDB95EB40B799B7D6B857CA8740", "Klassrum 104", 15, false, false));
            RoomRegistry.RegisterRoom(new GroupRoom("E057DD50FC7F40539D01A9988185A923", "Grupprum 201", 6, false, false));
            RoomRegistry.RegisterRoom(new GroupRoom("21E8740CF52648B587EB9FEBCC8C3C59", "Grupprum 202", 8, true, true));
            RoomRegistry.RegisterRoom(new GroupRoom("247256E4889F4F0F8836141736C14163", "Grupprum 203", 4, false, false));
            */
            RoomRegistry.LoadRooms();

            while (runProgram == true)
            {
                MainMenu();
            }
        }

        // ----------------------------------------------------------------
        //                            Methods
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

                int userChoice = InputHelper.ParseInt("\nAnge menyval: ", 0, 3);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        MenuHelper.BookingMenu();
                        break;
                    case 2:
                        Console.Clear();
                        MenuHelper.RoomMenu();
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
                    MenuHelper.DisplayMessage(0, 3);
                    break;
                }
            }
        }  

        public static void AboutInfoscreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(credits);
            Console.WriteLine();
            MenuHelper.BackToMenu("tillbaka till menyn...");
        }
    }
}

    

