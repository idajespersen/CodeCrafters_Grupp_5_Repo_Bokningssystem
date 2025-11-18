using Bokningssystem.Logic.BookingClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.RoomClasses
{
    // ----------------------------------------------------------------
    //               Child class GroupRoom. Made by Ida.
    // ----------------------------------------------------------------
    // Child class GroupRoom, inherits from Room and IBookable
    // Paramenters for constructor include smartboard availability
    public class GroupRoom : Room
    {
        private bool _hasSmartBoard;
        public bool HasSmartBoard => _hasSmartBoard;

        public GroupRoom(string roomId, string name, int roomCapacity, bool hasSmartboard)
                   : base(roomId, name, roomCapacity)
        {
            _hasSmartBoard = hasSmartboard;
        }

        public override int MaxBookingHours => 6;

        // -------------------------------
        //  ListBookings gjord av Daniel
        // -------------------------------
        public override void ListBookings()
        {
            if (Bookings.Count == 0) // Om rummet inte har några bokningar alls.
            {
                Console.WriteLine($"[{Name}] inte bokad\n");
            }

            foreach (var booking in Bookings) // Loopar igenom varje bokning som finns i rummet.
            {
                Console.WriteLine($"[{booking.RoomName}] Bokad av: {booking.BookerName} Smartboard: {(HasSmartBoard ? "Ja" : "Nej")} ({booking.StartTime}) - ({booking.EndTime})\n");
            }

        }
    }
}
