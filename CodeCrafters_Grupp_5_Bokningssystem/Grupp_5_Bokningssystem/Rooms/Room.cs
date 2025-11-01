using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Rooms
{
    public class Room : IBookable
    {
        // Put variables here

        // Parent class accepts name, capacity and availability as parameters
        // Methods for IBookable interface implemented with NotImplementedException
        public Room(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        // Property for room name
        public string Name { get; }
        // Variable for room capacity
        public int Capacity { get; }
        // Variable for room availability
        public bool IsAvailable(DateTime start, DateTime end)
        {
            if (start > end)
                throw new Exception("Start date must occurr after End date");

            return true;
        }

        public void NewBooking()
        {
            throw new NotImplementedException();
        }

        public void CancelBooking()
        {
            throw new NotImplementedException();
        }

        public void UpdateBooking()
        {
            throw new NotImplementedException();
        }

        public void ListBookings()
        {
            throw new NotImplementedException();
        }

        public void ListBookingsByYear()
        {
            throw new NotImplementedException();
        }

        public void SearchRoom()
        {
            throw new NotImplementedException();
        }

        public void NewRoom()
        {
            throw new NotImplementedException();
        }
    }
}
