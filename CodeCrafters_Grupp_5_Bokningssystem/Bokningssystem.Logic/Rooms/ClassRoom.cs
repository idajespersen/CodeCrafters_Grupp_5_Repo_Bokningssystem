using Bokningssystem.Logic.BookingClass;
using Bokningssystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.Rooms
{
    // Child class ClassRoom, inherits from Room and IBookable
    // Paramenters for constructor include projector availability
    public class ClassRoom : Room
    {
        bool hasProjector;
        public ClassRoom(string iName, int iCapacity, bool iIsAvailable, bool iHasProjector)
            : base(iName, iCapacity, iIsAvailable)
        {
            hasProjector = iHasProjector;
        }
    }
}
