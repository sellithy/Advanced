using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinMax
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Logic.Evaluate(board, Tile.O);
            Controller controller = new Controller();//"XO O   X "

            controller.AIGame();

            Console.Read();
        }
    }
}