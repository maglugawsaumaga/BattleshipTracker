using BattleshipStateTracker.Core.Enums;
using System;

namespace BattleshipStateTracker.Core.Entities.Ships
{
    public abstract class Ship
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public OccupationType OccupationType { get; set; }
        internal bool Equals(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
        
        public bool IsSunk
        {
            get
            {
                return Hits >= Width;
            }
        }


    }
}
