using BattleshipStateTracker.Core.Enums;

namespace BattleshipStateTracker.Core.Entities.Boards
{
    public class Panel
    {
        public OccupationType OccupationType { get; set; }
        public Coordinates Coordinates { get; set; }

        public Panel(int row, int column)
        {
            Coordinates = new Coordinates(row, column);
            OccupationType = OccupationType.Empty;
        }

        public string Status
        {
            get
            {
                return ((char)OccupationType).ToString();
            }
        }

        public bool IsOccupied
        {
            get
            {
                return OccupationType == OccupationType.Battleship;
            }
        }

        public bool IsRandomAvailable
        {
            get
            {
                return (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
                    || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
            }
        }
    }
}
