using BattleshipStateTracker.Core.Enums;

namespace BattleshipStateTracker.Core.Entities.Ships
{
    public class BattleshipGeneral : Ship
    {
        public BattleshipGeneral()
        {
            Name = "Battleship General";
            Width = 4;
            OccupationType = OccupationType.BattleshipGeneral;
        }
    }
}
