using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic
{
    // Interface som definierar funktionalitet som alla bokningsbara objekt måste implementera.
    public interface IBookable
    {
        void NewBooking();
        void CancelBooking();
        void UpdateBooking();
        void ListBookings();
        void ListBookingsByYear();
        void SearchRoom();
        void NewRoom();
    }
}
