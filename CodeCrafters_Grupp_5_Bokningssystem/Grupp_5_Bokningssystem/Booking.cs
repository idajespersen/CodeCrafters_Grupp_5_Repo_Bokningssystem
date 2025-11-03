using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Grupp_5_Bokningssystem
{
    public class Booking
    {
        public Booking(IBookable bookable, string madeBy, DateTime startDate, TimeSpan duration)
        {
            Bookable = bookable;
            MadeBy = madeBy;
            StartDate = startDate;
            Duration = duration;
        }

        public IBookable Bookable
        {
            get;
        }

        public string MadeBy
        {
            get;
        }

        public DateTime StartDate
        {
            get;
        }

        public DateTime EndDate
        {
            get { return StartDate + Duration; }
        }

        public TimeSpan Duration
        {
            get;
        }
    }
}
