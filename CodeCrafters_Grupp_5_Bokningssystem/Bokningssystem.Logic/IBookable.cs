using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic
{
    // ----------------------------------------------------------------
    //                Interface - IBookable. Made by Ida.
    // ----------------------------------------------------------------
    public interface IBookable
    {
        string Name
        {
            get;
        }

        void NewBooking();
        void CancelBooking();
        void UpdateBooking();
        void ListBookings();
        void ListBookingsByYear();
    }
}
