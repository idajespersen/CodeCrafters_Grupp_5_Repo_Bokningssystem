using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic
{
    // ----------------------------------------------------------------
    // 1. Interface - IBookable
    // ----------------------------------------------------------------
    public interface IBookable
    {
        void NewBooking();
        void CancelBooking();
        void UpdateBooking();
        void ListBookings();
        void ListBookingsByYear();
    }
}
