using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    enum ShipType { Empty, Carrier, Battleship, Cruiser, Submarine, Destroyer }

    class Ship
    {
        public static readonly Dictionary<ShipType, int> Lengths = new Dictionary<ShipType, int>
        {
            { ShipType.Carrier, 5 },
            { ShipType.Battleship, 4 },
            { ShipType.Submarine, 3 },
            { ShipType.Cruiser, 3 },
            { ShipType.Destroyer, 2 }
        };

        public bool Horizontal { get; }
        public Coordinate Coordinate { get; }
        public ShipType Type { get; }

        public Ship(bool horizontal, int x, int y, ShipType type)
        {
            this.Horizontal = horizontal;
            Coordinate = new Coordinate(x, y);
            this.Type = type;
        }

        public List<Coordinate> GetCoordinates()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            for (int i = 0; i < Lengths[Type]; i++)
            {
                int tempx = Coordinate.X + (Horizontal ? i : 0);
                int tempy = Coordinate.Y + (Horizontal ? 0 : i);
                coordinates.Add(new Coordinate(tempx, tempy));
            }

            return coordinates;
        }

        public bool IsCrossing(Ship ship)
        {
            return GetCoordinates().Intersect(ship.GetCoordinates()).Any();
        }
    }
}