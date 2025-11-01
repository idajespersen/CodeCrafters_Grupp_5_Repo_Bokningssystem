using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem
{
    public interface IBookable
    {
        void NewBooking();

        void CancelBooking();

        void UpdateBooking();

        void ListBookings();

        void ListBookingsByYear();

        bool IsAvailable(DateTime start, DateTime end);

        // these two make the IBookable more of a IBookableRoom interface
        //void SearchRoom();

        //void NewRoom();
    }
}
