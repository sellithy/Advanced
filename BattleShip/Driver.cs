using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Driver
    {
        Board humanBoard, computerBoard;
        Logic humanLogic;

        public Driver()
        {
            Console.SetWindowSize(90, 26);
            humanBoard = Board.RandomBoard();
            computerBoard = Board.RandomBoard();
            humanLogic = new Logic(humanBoard);
        }

        public void PlayHuntGame()
        {
            while (!Done())
            {
                PrintBoards();

                PlayerInput();

                if (!Done())
                    HuntHit();
            }
            Console.WriteLine("WON");
        }

        public void PlayRandomGame()
        {
            while (!Done())
            {
                PrintBoards();

                PlayerInput();

                if(!Done())
                    RandomHit();
            }
            Console.WriteLine("WON");
        }

        private void HuntHit()
        {
            Coordinate testCor = humanLogic.HuntHit();
            bool hit = humanBoard.Hit(testCor);
            humanLogic.Report(hit);
            ShipType sunkShip = humanBoard.SunkShip();
            Console.WriteLine($"Computer targets {testCor}, {(hit ? "and hits" : "but misses")}");
            if (sunkShip != ShipType.Empty)
                Console.WriteLine($"sunk the {sunkShip}");
        }

        private void RandomHit()
        {
            Coordinate testCor = humanLogic.RandomHit();
            bool hit = humanBoard.Hit(testCor);
            ShipType sunkShip = humanBoard.SunkShip();
            Console.WriteLine($"Computer targets {testCor}, {(hit ? "and hits" : "but misses")}");
            if (sunkShip != ShipType.Empty)
                Console.WriteLine($"sunk the {sunkShip}");
        }

        private void PlayerInput()
        {
            Console.Write("Where Do you want to hit? ");
            string line = Console.ReadLine();
            Console.Clear();
            string[] tokens = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            bool hit = computerBoard.Hit(int.Parse(tokens[0]), int.Parse(tokens[1]));
            ShipType sunkShip = computerBoard.SunkShip();
            Console.WriteLine($"targeted ({tokens[0]},{tokens[1]}), {(hit ? "and hit" : "but missed")}");
            if(sunkShip != ShipType.Empty)
                Console.WriteLine($"sunk the {sunkShip}");
        }

        private bool Done()
        {
            return computerBoard.Done() || humanBoard.Done();
        }

        private void PrintBoards()
        {
            Console.WriteLine("───────────── Your Board ───────────────       ─────────── Guessing Board ─────────────");
            string[] human = humanBoard.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] computer = computerBoard.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < human.Length; i++)
                Console.WriteLine(human[i] + "  | |  " + computer[i]);
        }
    }
}