using Bokningssystem.Logic.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.RoomClasses
{
    // Klass som håller reda på alla rum som skapats. Gjord av Sara.
    public static class RoomRegistry
    {
        private static bool _hasBeenLoaded = false;
        // Lista med rum. Private set så att endast klassen kan ändra själva listan.
        public static List<IBookable> AllRooms { get; private set; } = new();

        public static void SaveRoom(IBookable bookable)
        {
            FileHelper.TrySave(bookable);
        }

        public static void LoadRooms()
        {
            if (_hasBeenLoaded)
                throw new Exception("Rooms has already been loaded.");

            // Loads all class rooms
            if(FileHelper.TryLoad<ClassRoom>(out var classRooms))
            {
                foreach(IBookable bookable in classRooms)
                {
                    AllRooms.Add(bookable);
                }
            }

            // Loads all group rooms
            if (FileHelper.TryLoad<GroupRoom>(out var groupRooms))
            {
                foreach (IBookable bookable in groupRooms)
                {
                    AllRooms.Add(bookable);
                }
            }

            _hasBeenLoaded = true;
        }

        // Metod för att registrera ett nytt rum i rumslistan.
        public static void RegisterRoom(IBookable room)
        {
            AllRooms.Add(room);
            SaveRoom(room);
        }
    
        // - Ability to make new rooms with error handling for duplicate names
        public static void NewRoom()
        {
            Console.Clear();
            bool isRunningMenu = true;
            while (isRunningMenu)
            {
                Helper.TypeOfRoomMenu();

                var newRoomType = "Room";
                int typeChoice = Helper.ParseInt("Ange vad för rum du vill skapa: ", 0,2 );
               
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
                        //Helper.DisplayMessage();
                        NewRoom();
                        //Tove: Added return to not make a recursive loop
                        return;
                        //continue;
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
                        if (string.Equals(room.Name, newRoomName, StringComparison.OrdinalIgnoreCase))
                        {
                            //Tove: Commented out code, and added return to prevent crash
                            //because of possible change in AllRooms.Count.
                            //bool nameAlreadyExists = true;
                            //if (nameAlreadyExists)
                            //{
                            Console.WriteLine($"\nFel: Rummet '{newRoomName}' finns redan!\n\nTryck [ENTER] för att återvända.");
                            Console.ReadKey();
                            NewRoom();
                            //continue;
                            
                            return;
                            //}
                        }
                    }
                }
                // String was null/empty and error message is shown
                else
                {
                    Console.WriteLine("Rummet måste ha ett namn.\n\nTryck [ENTER] för att återvända.");
                    Console.ReadKey();
                    NewRoom();
                    //Tove: Added return to not make a recursive loop
                    return;
                    //continue;
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
                    //Tove: Added return to not make a recursive loop
                    return;
                    //continue;
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
                            Helper.DisplayMessage(0,2);
                            Helper.BackToMenu("vidare...");
                            NewRoom();
                            //Tove: Added return to not make a recursive loop
                            return;
                            //continue;
                    }
                }
                else
                {
                    Helper.DisplayMessage(0,2);
                    Helper.BackToMenu("vidare...");
                    NewRoom();

                    //Tove: Added return to not make a recursive loop
                    return;
                    //continue;
                }
                // Add room to the list once user didn't trigger errors and assign ID
                string newRoomId = Guid.NewGuid().ToString("N");

                if (newRoomType == "ClassRoom")
                {
                    //bool newRoomIsAvailable = true;
                    ClassRoom newRoom = new ClassRoom(newRoomId, newRoomName, newRoomCapacity, newRoomEquipment); //newRoomIsAvailable);
                    RoomRegistry.RegisterRoom(newRoom);
                }
                if (newRoomType == "GroupRoom")
                {
                    //bool newRoomIsAvailable = true;
                    GroupRoom newRoom = new GroupRoom(newRoomId, newRoomName, newRoomCapacity, newRoomEquipment); //newRoomIsAvailable);
                    RoomRegistry.RegisterRoom(newRoom);
                }

                Console.WriteLine($"\nSammanfattning:\n\n" +
                                    $"Rumstyp: {newRoomType}\n" +
                                    $"Namn: {newRoomName}\n" +
                                    $"Kapacitet: {newRoomCapacity}\n" +
                                    $"Har utrustning: {newRoomEquipment}\n\n" +
                                    $"Rummet har lagts till.\n\n" +
                                    $"Tryck [ENTER] för att återgå till menyn.");

                //Tove: Added Console.ReadLine() to make above text visible to the user.
                Console.ReadLine();
            }
        }

    }

}
