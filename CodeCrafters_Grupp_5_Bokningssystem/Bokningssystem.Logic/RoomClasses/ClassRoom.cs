using Bokningssystem.Logic.BookingClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.RoomClasses
{
    // ----------------------------------------------------------------
    //               Child class ClassRoom. Made by Ida.
    // ----------------------------------------------------------------
    // Child class ClassRoom, inherits from Room and IBookable
    // Paramenters for constructor include projector availability
    public class ClassRoom : Room
    {
        private bool _hasProjector;

        public bool HasProjector => _hasProjector;

        public ClassRoom(string roomId, string name, int roomCapacity, bool hasProjector)
            : base(roomId, name, roomCapacity)
        {
            _hasProjector = hasProjector;
        }

        // -------------------------------
        //  ListBookings gjord av Daniel
        // -------------------------------
        public override void ListBookings()
        {
            if (Bookings.Count == 0)
            {
                Console.WriteLine($"{Name} inte bokad");
            }

            foreach (var booking in Bookings)
            {
                Console.WriteLine($"[{booking.RoomName}] {booking.BookerName} Projektor: {(HasProjector ? "Ja" : "Nej")} ({booking.StartTime}) - ({booking.EndTime})\n");
            }
        }

        }
}
