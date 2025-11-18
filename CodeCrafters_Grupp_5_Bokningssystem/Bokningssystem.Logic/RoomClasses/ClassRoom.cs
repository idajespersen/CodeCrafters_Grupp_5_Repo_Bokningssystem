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
            if (Bookings.Count == 0) // Om rummet inte har några bokningar alls.
            {
                Console.WriteLine($"{Name} inte bokad\n");
            }

            foreach (var booking in Bookings) // Loopar igenom varje bokning som finns i rummet.
            {
                Console.WriteLine($"[{booking.RoomName}] Bokad av: {booking.BookerName} Projektor: {(HasProjector ? "Ja" : "Nej")} ({booking.StartTime}) - ({booking.EndTime})\n");
            }
        }

        }
}
