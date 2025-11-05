using Bokningssystem.Logic.Helpers;
using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Rooms
{
    // Klass som håller reda på alla rum som skapats. Gjord av Sara.
    public static class RoomRegistry
    {
        public static List<Room> AllRooms { get; private set; } = new();

        public static void RegisterRoom(Room room)
        {
            AllRooms.Add(room);
        }
    }
}
