using BattleshipStateTracker.Core.Entities;
using BattleshipStateTracker.Core.Entities.Boards;

namespace BattleshipStateTracker.Core
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        
        public void PlayRound(int row, int column)
        {          
            var coordinates = new Coordinates(row, column);
            
            var result = Player2.ProcessShot(coordinates);
            Player1.ProcessShotResult(coordinates, result);
        }        
    }
}
