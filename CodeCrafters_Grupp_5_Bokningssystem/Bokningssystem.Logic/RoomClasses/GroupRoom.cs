using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic.RoomClasses
{
    // ----------------------------------------------------------------
    // 3. Child Classes
    // ----------------------------------------------------------------

    // Child class GroupRoom, inherits from Room and IBookable
    // Paramenters for constructor include smartboard availability
    public class GroupRoom : Room
    {
        bool hasSmartBoard;
        public GroupRoom(string iId, string iName, int iCapacity, bool iIsAvailable, bool iHasSmartboard)
                   : base(iId, iName, iCapacity, iIsAvailable)
        {
            hasSmartBoard = iHasSmartboard;
        }

        public override int MaxBookingHours => 6;

    }
}
