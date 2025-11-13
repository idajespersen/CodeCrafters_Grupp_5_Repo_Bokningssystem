using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.RoomClasses
{
    // Child class ClassRoom, inherits from Room and IBookable
    // Paramenters for constructor include projector availability
    public class ClassRoom : Room
    {
        bool hasProjector;
        public ClassRoom(string iId, string iName, int iCapacity, bool iIsAvailable, bool iHasProjector)
            : base(iId, iName, iCapacity, iIsAvailable)
        {
            hasProjector = iHasProjector;
        }
    }
}
