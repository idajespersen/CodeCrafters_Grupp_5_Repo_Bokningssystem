using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.BookingClass
{
    // Klass som hanterar bokningar. Gjord av Sara.
    public class Booking
    {
        public string BookerName { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public Booking(string bookerName, string roomName, DateTime startTime, DateTime endTime)
        {
            BookerName = bookerName;
            RoomName = roomName;
            StartTime = startTime;
            EndTime = endTime;
            // Kontrollerar att sluttiden är efter starttiden annars kastas ett undantag.
            if (endTime <= startTime)
                throw new ArgumentException("Sluttid måste vara efter starttid.");
        }

        //Metod i Booking klassen för att kolla om bokningar överlappar.
        public bool BookingsOverlap(Booking otherBooking)
        {
            // Om den andra bokningen är null så kan den inte överlappa och den returnerar då false.
            if (otherBooking == null) return false;

            // Kollar om en boknings start och sluttid överlappar med en annan bokning.
            bool noOverlap = StartTime >= otherBooking.EndTime || EndTime <= otherBooking.StartTime;

            // Om noOverlap är true så överlappar de inte och returnerar false.
            // Om noOverlap är false så överlappar de inte och returnerar true.
            return !noOverlap;
        }
    }
}
