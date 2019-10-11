using System;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver driver = new Driver();
            driver.PlayHuntGame();
            Console.Read();
        }
    }
}