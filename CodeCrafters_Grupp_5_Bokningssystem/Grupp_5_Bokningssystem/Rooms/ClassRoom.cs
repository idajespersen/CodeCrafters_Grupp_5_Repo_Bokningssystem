using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp_5_Bokningssystem.Rooms
{
    // Child class ClassRoom, inherits from Room
    // Paramenters for constructor include projector availability
    public class ClassRoom : Room
    {
        public ClassRoom(string name, int capacity, bool hasProjector)
            : base(name, capacity)
        {
            HasProjector = hasProjector;
        }

        public bool HasProjector { get; }
    }
}
