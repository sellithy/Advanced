using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    enum Tile { X = -1, EMPTY = 0, O = 1}

    class Board
    {
        readonly private Tile[] board = new Tile[9];

        public Tile this[int index]
        {
            get
            {
                return board[index];
            }
        }

        public Board()
        {
            for (int i = 0; i < board.Length; i++)
                board[i] = Tile.EMPTY;
        }

        public Board(string line)
        {
            if (line.Length != 9)
                throw new ArgumentException("The input string is not the correct length");

            for (int i = 0; i < board.Length; i++)
                switch (line[i])
                {
                    case 'X':
                        board[i] = Tile.X;
                        break;

                    case 'O':
                        board[i] = Tile.O;
                        break;

                    default:
                        board[i] = Tile.EMPTY;
                        break;
                }
        }

        public Board(Board b)
        {
            board = (Tile[]) b.board.Clone();
        }

        public void Change(Tile player, int square)
        {
            board[square] = player;
        }

        public void SafeChange(Tile player, int square)
        {
            if (player == Tile.EMPTY)
                throw new ArgumentException("Player can't be EMPTY");

            if (board[square] != Tile.EMPTY)
                throw new ArgumentException("This square is not empty");

            board[square] = player;
        }

        public List<int> EmptyTiles()
        {
            List<int> openSquares = new List<int>();

            for (int i = 0; i < board.Length; i++)
                if (board[i] == Tile.EMPTY)
                    openSquares.Add(i);

            return openSquares;
        }

        public bool Filled()
        {
            foreach (Tile t in board)
                if (t == Tile.EMPTY) return false;

            return true;
        }

        public override string ToString()
        {
            string lines = "";

            for (int i = 0; i < board.Length; i++)
            {
                switch (board[i])
                {
                    case Tile.X:
                        lines += "X ";
                        break;

                    case Tile.O:
                        lines += "O ";
                        break;

                    default:
                        lines += "  ";
                        break;
                }

                if (i % 3 == 2) lines += "\n";
            }

            return lines;
        }
    }
}