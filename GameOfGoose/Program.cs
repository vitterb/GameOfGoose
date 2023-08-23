using GameOfGoose.Interfaces;
using GameOfGoose.Logger;

namespace GameOfGoose
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            Processor processor = new Processor(logger);

            Console.Write("How many players 2 - 4 ?");
            int NumberOfPlayers = Int32.Parse(Console.ReadLine());

            processor.InitializeGame(NumberOfPlayers);
        }
    }
}