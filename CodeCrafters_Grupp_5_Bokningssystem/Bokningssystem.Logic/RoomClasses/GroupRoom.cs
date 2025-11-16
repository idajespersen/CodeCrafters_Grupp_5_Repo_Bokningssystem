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
        private bool _hasSmartBoard;
        public bool HasSmartBoard => _hasSmartBoard;

        public GroupRoom(string roomId, string name, int roomCapacity, bool hasSmartboard)
                   : base(roomId, name, roomCapacity)
        {
            _hasSmartBoard = hasSmartboard;
        }

        public override int MaxBookingHours => 6;

    }
}
