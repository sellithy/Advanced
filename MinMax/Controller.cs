using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Controller
    {
        private readonly Random random = new Random();
        private readonly Board board;
        Tile turn;

        public Controller(string line)
        {
            board = new Board(line);
            int xCounter = 0;
            int oCounter = 0;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Tile.X) xCounter++;
                if (board[i] == Tile.O) oCounter++;
            }

            if (xCounter == oCounter) turn = Tile.X;
            else turn = Tile.O;
        }

        public Controller()
        {
            board = new Board();
            turn = Tile.X;
        }

        public void RandomGame()
        {
            while (!Done())
            {
                PlayerMove();
                if (!Done())
                    RandomMove();

                Console.WriteLine(board);
            }

            Console.WriteLine("Winner is " + Logic.Winner.ToString());
        }

        public void HuntGame()
        {
            while (!Done())
            {
                PlayerMove();
                if (!Done())
                    WinningOrRandomMove();

                Console.WriteLine(board);
            }

            Console.WriteLine("Winner is " + Logic.Winner.ToString());
        }

        public void AIGame()
        {
            while (!Done())
            {
                BestMove();
                Console.WriteLine(board);
                if (!Done())
                    PlayerMove();

                Console.WriteLine(board);
            }

            Console.WriteLine("Winner is " + Logic.Winner.ToString());
        }

        private void OneMove(int square)
        {
            board.SafeChange(turn, square);
            turn = (Tile)((int)turn * -1);
        }

        public void PlayerMove()
        {
            int choice = int.Parse(Console.ReadLine());
            OneMove(choice);
        }

        public void RandomMove()
        { 
            List<int> openSquares = board.EmptyTiles();
            int square = openSquares[random.Next(openSquares.Count)];
            OneMove(square);
        }

        private void WinningOrRandomMove()
        {
            int winning = Logic.WinningMove(board, turn);

            OneMove(winning);
        }

        private void BestMove()
        {
            int best = Logic.BestMove(board, turn);

            if (best != -1)
                OneMove(best);
            else
                RandomMove();
        }

        private bool Done()
        {
            return Logic.Done(board);
        }
    }
}