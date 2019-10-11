using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Logic
    {
        Random random = new Random();
        Coordinate lastTarget;
        Stack<Coordinate> targets;
        Board board;

        public Logic(Board board)
        {
            targets = new Stack<Coordinate>();
            this.board = board;
        }

        public Coordinate HuntHit()
        {
            if (targets.Count == 0)
            {
                Coordinate testCor = new Coordinate(random.Next(Board.Size), random.Next(Board.Size));
                while (!board.AvaliableHit(testCor))
                    testCor = new Coordinate(random.Next(Board.Size), random.Next(Board.Size));
                lastTarget = testCor;
                return testCor;
            }

            lastTarget = targets.Pop();
            return lastTarget;
        }

        internal void Report(bool hit)
        {
            if (hit)
            {
                if (lastTarget.Y + 1 < Board.Size)
                    targets.Push(new Coordinate(lastTarget.X, lastTarget.Y + 1));

                if (lastTarget.Y - 1 >= 0)
                    targets.Push(new Coordinate(lastTarget.X, lastTarget.Y - 1));

                if (lastTarget.X + 1 < Board.Size)
                    targets.Push(new Coordinate(lastTarget.X + 1, lastTarget.Y));

                if (lastTarget.X - 1 >= 0)
                    targets.Push(new Coordinate(lastTarget.X - 1, lastTarget.Y));
            }
        }

        public Coordinate RandomHit()
        {
            Coordinate testCor = new Coordinate(random.Next(Board.Size), random.Next(Board.Size));
            while (!board.AvaliableHit(testCor))
                testCor = new Coordinate(random.Next(Board.Size), random.Next(Board.Size));

            return testCor;
        }
    }
}