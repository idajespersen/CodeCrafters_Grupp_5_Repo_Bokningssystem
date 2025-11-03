using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp_5_Bokningssystem.Rooms;

namespace Grupp_5_Bokningssystem
{
    public static class FileHelper
    {
        public static readonly string AppName = "Bokningssystemet";
        public static readonly string RootPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            AppName);
        public static readonly string ClassRoomPath = Path.Combine(RootPath, "ClassRooms");
        public static readonly string GroupRoomPath = Path.Combine(RootPath, "GroupRooms");

        private static void CreateRoomsFolders()
        {
            Directory.CreateDirectory(ClassRoomPath);
            Directory.CreateDirectory(GroupRoomPath);
        }

        public static List<Room> LoadRooms()
        {
            CreateRoomsFolders();

            List<Room> rooms = new List<Room>();



            return rooms;
        }

        public static void SaveRooms(List<Room> rooms)
        {
            CreateRoomsFolders();
        }
    }
}
