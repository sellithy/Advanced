using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Logic
    {
        public static Tile Winner;

        private static Random random = new Random();

        public static int BestMove(Board board, Tile player)
        {
            int best = 0;
            int move = -1;
            foreach (int i in board.EmptyTiles())
            {
                Board b = new Board(board);
                b.SafeChange(player, i);


                int temp = Evaluate(b, (Tile)((int)player*-1));
                if ((int)player == -1 && temp < best)
                {
                    best = temp;
                    move = i;
                }

                if ((int)player == 1 && temp > best)
                {
                    best = temp;
                    move = i;
                }
            }

            return move;
        }

        public static int Evaluate(Board board, Tile player)
        {
            if (Done(board)) return (int)Winner * 1000;

            List<int> values = new List<int>();

            foreach (int i in board.EmptyTiles())
            {
                Board b = new Board(board);
                b.SafeChange(player, i);


                values.Add(Evaluate(b, (Tile)((int)player * -1)));
            }

            return (int)player == -1 ? values.Min() : values.Max();
        }

        public static int WinningMove(Board board, Tile player)
        {
            int square = -1;

            foreach (int i in board.EmptyTiles())
            {
                Board b = new Board(board);
                b.SafeChange(player, i);

                if (Logic.Done(b))
                {
                    square = i;
                }
            }
            return square;
        }

        public static bool Done(Board board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[3 * i] != Tile.EMPTY && board[3 * i] == board[3 * i + 1] && board[3 * i + 1] == board[3 * i + 2])
                {
                    Winner = board[3 * i];
                    return true;
                }

                if (board[i] != Tile.EMPTY && board[i] == board[i + 3] && board[i + 3] == board[i + 6])
                {
                    Winner = board[i];
                    return true;
                }
            }

            if (board[0] != Tile.EMPTY && board[0] == board[4] && board[4] == board[8])
            {
                Winner = board[0];
                return true;
            }

            if (board[2] != Tile.EMPTY && board[2] == board[4] && board[4] == board[6])
            {
                Winner = board[2];
                return true;
            }

            if (board.Filled())
            {
                Winner = Tile.EMPTY;
                return true;
            }

            return false;
        }
    }
}