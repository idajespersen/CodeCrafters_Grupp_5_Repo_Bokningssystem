using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Rooms
{
    // Child class GroupRoom, inherits from Room
    // Paramenters for constructor include smartboard availability
    public class GroupRoom : Room
    {
        public GroupRoom(string name, int capacity, bool hasSmartboard)
            : base(name, capacity)
        {
            HasSmartBoard = hasSmartboard;
        }

        public bool HasSmartBoard { get; }
    }
}
