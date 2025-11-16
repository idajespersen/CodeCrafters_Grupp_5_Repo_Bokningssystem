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
        private bool _hasProjector;

        public bool HasProjector => _hasProjector;

        public ClassRoom(string roomId, string name, int roomCapacity, bool hasProjector)
            : base(roomId, name, roomCapacity)
        {
            _hasProjector = hasProjector;
        }
    }
}
