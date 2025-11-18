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


            RoomRegistry.LoadRooms();

            while (runProgram == true)
            {
                MainMenu();
            }
        }

        // ----------------------------------------------------------------
        //                            Menus
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
            MenuHelper.GoBack("tillbaka till menyn...");
        }
    }
}

    

