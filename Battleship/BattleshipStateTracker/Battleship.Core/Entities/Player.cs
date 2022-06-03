using BattleshipStateTracker.Core.Entities.Boards;
using BattleshipStateTracker.Core.Entities.Ships;
using BattleshipStateTracker.Core.Enums;
using BattleshipStateTracker.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker.Core.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }
        public FiringBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            Ships = new List<Ship>()
            {
                new Battleship(),
                new BattleshipGeneral()
            };
            GameBoard = new GameBoard();
            FiringBoard = new FiringBoard();
        }

        public void OutputBoards()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Own Board:                          Firing Board:");
            for(int row = 1; row <= 10; row++)
            {
                for(int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(GameBoard.Panels.At(row, ownColumn).Status + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(FiringBoard.Panels.At(row, firingColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void PlaceShips()
        {            
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    var startcolumn = rand.Next(1,11);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2;

                    List<int> panelNumbers = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    if(endrow > 10 || endcolumn > 10)
                    {
                        isOpen = true;
                        continue;
                    }

                    var affectedPanels = GameBoard.Panels.Range(startrow, startcolumn, endrow, endcolumn);
                    if(affectedPanels.Any(x=>x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach(var panel in affectedPanels)
                    {
                        panel.OccupationType = ship.OccupationType;
                    }
                    isOpen = false;
                }
            }
        }        

        public ShotResult ProcessShot(Coordinates coords)
        {
            var panel = GameBoard.Panels.At(coords.Row, coords.Column);
            if(!panel.IsOccupied)
            {
                Console.WriteLine(Name + " Radio: \"Miss!\"");
                return ShotResult.Miss;
            }
            var ship = Ships.First(x => x.OccupationType == panel.OccupationType);
            ship.Hits++;
            Console.WriteLine(Name + " Radio: \"Hit!\"");
            if (ship.IsSunk)
            {
                Console.WriteLine(Name + " Radio: \"You sunk my " + ship.Name + "!\"");
            }
            return ShotResult.Hit;
        }

        public void ProcessShotResult(Coordinates coords, ShotResult result)
        {
            var panel = FiringBoard.Panels.At(coords.Row, coords.Column);
            switch(result)
            {
                case ShotResult.Hit:
                    panel.OccupationType = OccupationType.Hit;
                    break;

                default:
                    panel.OccupationType = OccupationType.Miss;
                    break;
            }
        }
    }
}
