using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Board
    {
        private readonly static Random random = new Random();
        public const int Size = 10;
        readonly List<Ship> Fleet;
        readonly char[,] ships;
        readonly bool[,] hits;

        private Board(List<Ship> fleet)
        {
            this.Fleet = fleet;
            this.hits = new bool[Size, Size];
            ships = new char[Size, Size];

            foreach (Ship s in fleet)
                foreach (Coordinate c in s.GetCoordinates())
                    ships[c.X, c.Y] = s.Type.ToString()[0];
        }

        public bool Done()
        {
            foreach (Ship s in Fleet)
                foreach (Coordinate c in s.GetCoordinates())
                    if (!hits[c.X, c.Y])
                        return false;

            return true;
        }

        public bool Hit(Coordinate cor)
        {
            return Hit(cor.X, cor.Y);
        }

        public bool Hit(int x, int y)
        {
            if (hits[x, y])
                throw new ArgumentException("Already hit");

            hits[x, y] = true;

            return ships[x,y] != '\0';
        }

        public bool SunkAShip(Ship s)
        {
            foreach (Coordinate c in s.GetCoordinates())
                if (!hits[c.X, c.Y])
                    return false;

            return true;
        }

        public bool AvaliableHit(Coordinate cor)
        { 
            return !hits[cor.X, cor.Y];
        }

        public string PrintHits()
        {
            string full = "────────────────────────────────────────\n";

            for (int j = 0; j < ships.GetLength(0); j++)
            {
                for (int i = 0; i < ships.GetLength(1); i++)
                {
                    if (hits[i, j])
                    {
                        if (ships[i, j] == '\0')
                            full += " . |";
                        else
                            full += " x |";
                    }
                    else
                        full += "   |";
                }
                full += "\n────────────────────────────────────────\n";
            }
            
            return full;
        }

        public override string ToString()
        {
            string full = "────────────────────────────────────────\n";

            for (int j = 0; j < ships.GetLength(0); j++) 
            {
                for (int i = 0; i < ships.GetLength(1); i++)
                {
                    if (ships[i, j] == '\0')
                        if(!hits[i,j])
                            full += "   |";
                        else
                            full += " . |";
                    else 
                        if (!hits[i, j])
                            full += " " + ships[i, j] + " |";
                        else
                            full += "-" + ships[i, j] + "-|";
                }
                full += "\n────────────────────────────────────────\n";
            }

            return full;
        }

        public static Board RandomBoard()
        {
            Board.Builder builder = new Board.Builder();
            foreach (ShipType t in Enum.GetValues(typeof(ShipType)))
            {
                if (t != ShipType.Empty)
                {
                    Ship testShip = RandomShip(t);
                    while (!builder.IsValid(testShip))
                        testShip = RandomShip(t);
                    builder.AddShip(testShip);
                }
            }
            Board board = builder.GetBoard();
            return board;
        }

        private static Ship RandomShip(ShipType t)
        {
            bool hor = random.Next(2) == 0 ? false : true;
            int y = random.Next(Board.Size - (hor ? Ship.Lengths[t] : 0));
            int x = random.Next(Board.Size - (hor ? 0 : Ship.Lengths[t]));
            return new Ship(hor, x, y, t);
        }

        public class Builder
        {
            readonly List<Ship> fleet;
            public enum ErrorType {Used , Occupied, Bounds, Empty };
            ErrorType errorType;

            public static readonly Dictionary<ErrorType, string> Messages = new Dictionary<ErrorType, string>
            {
                { ErrorType.Used, "Already placed this ship"},
                { ErrorType.Occupied, "A ship is already placed here"},
                { ErrorType.Bounds, "Cannot place ship outside the board"},
                { ErrorType.Empty, "Cannot Place An Empty Ship"},
            };

            public Builder()
            {
                fleet = new List<Ship>();
            }

            public void AddShip(Ship ship)
            {
                if (IsValid(ship))
                    fleet.Add(ship);
                else
                    throw new ArgumentException(Messages[errorType]);
            }

            public bool IsValid(Ship ship)
            {
                if (fleet.Any(x => x.Type == ship.Type))
                {
                    errorType = ErrorType.Used;
                    return false;
                }

                foreach (Ship s in fleet)
                    if (s.IsCrossing(ship))
                    {
                        errorType = ErrorType.Occupied;
                        return false;
                    }

                bool outOfBounds = (ship.Horizontal && ship.Coordinate.X + Ship.Lengths[ship.Type] > Board.Size ||
                        !ship.Horizontal && ship.Coordinate.Y + Ship.Lengths[ship.Type] > Board.Size);

                if(outOfBounds)
                {
                    errorType = ErrorType.Bounds;
                    return false;
                }

                if (ship.Type == ShipType.Empty)
                {
                    errorType = ErrorType.Empty;
                    return false;
                }

                return true;
            }

            public override string ToString()
            {
                return new Board(fleet).ToString();
            }

            public Board GetBoard()
            {
                if (fleet.Count != 5)
                    throw new Exception("Not all ships have been added");
                return new Board(fleet);
            }


        }
    }
}