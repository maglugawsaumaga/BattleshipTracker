using BattleshipStateTracker.Core.Enums;

namespace BattleshipStateTracker.Core.Entities.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Width = 4;
            OccupationType = OccupationType.Battleship;
        }
    }
}
