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
        public static readonly string RootFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            AppName);
        public static readonly string RoomsFolder = Path.Combine(RootFolder, "Rooms");
        public static readonly string BookingsFolder = Path.Combine(RootFolder, "Bookings");

        public static void CreateFoldersIfMissing()
        {
            Directory.CreateDirectory(RootFolder);
            Directory.CreateDirectory(RoomsFolder);
            Directory.CreateDirectory(BookingsFolder);
        }

        public static List<IBookable> LoadBookables()
        {
            List<IBookable> bookables = new List<IBookable>();



            return bookables;
        }

        public static void SaveRooms(List<IBookable> bookables)
        {
            
        }
    }
}
