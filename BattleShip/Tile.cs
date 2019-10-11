using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Tile
    {
        Coordinate location;
        Ship ship;
        bool isHit;

        public Tile(Coordinate location, Ship ship, bool isHit)
        {
            this.location = location;
            this.ship = ship;
            this.isHit = isHit;
        }
    }
}