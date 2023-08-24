using GameOfGoose.GameObjects;
using GameOfGoose.Interfaces;

namespace GameOfGoose
{
    public class Processor
    {
        public int Turn { get; set; }
        public GameBoard Board { get; set; }
        public List<IPlayer> players = new List<IPlayer>();
        public ILogger Logger { get; set; }
        public IDice Dice { get; set; }

        public Processor(ILogger logger)
        {
            Board = new GameBoard();
            Logger = logger;
            Dice = new Dice();
            Turn = 1;
        }

        public void InitializeGame(int numberOfPlayers)
        {
            if (numberOfPlayers < 2)
            {
                Logger.Info("Less than 2 players is not Allowed!");
            }
            else if (numberOfPlayers > 4)
            {
                Logger.Info("More than 4 Players is not Allowed!");
            }
            else
            {
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    players.Add(new Player($"player{i}"));
                }
                NextTurn();
            }
        }

        public void TurnLogic(int Roll1, int Roll2, int turn, IPlayer player)
        {
            int smallerRoll = Math.Min(Roll1, Roll2);
            int largerRoll = Math.Max(Roll1, Roll2);
            int result = Roll1+Roll2;

            if ( player.SkipCounter != 0)
            {
                result = 0;
                player.SkipCounter -= 1;
            }
            if (result == 9 && turn == 1)
            {
                if ((smallerRoll == 3 && largerRoll == 6) || (smallerRoll == 4 && largerRoll == 5))
                {
                    player.MoveToSpace((smallerRoll == 3 && largerRoll == 6) ? 53 : 26);
                }
            }
            else
            {
                player.Move(Board, result);
            }

           
            ISquare square = Board.GetSquare(player.Position);

            if (square.Type == Factory.SquareTypes.Goose)
            {
                player.Move(Board, result);
            }
            square.Action(player);
        }

        private void NextTurn()
        {
            Logger.Info($"Turn {Turn}");
            string output = "P1 \t P2 \t P3 \t P4 \n";
            foreach (IPlayer p in players)
            {
                TurnLogic(Dice.Roll(), Dice.Roll(), Turn, p);
                output += p.Position + "\t";


                if (p.GameOver)
                {
                    Logger.Info(output);
                    EndOfGame();
                }
            }
            output += "\n";
            Logger.Info(output);
            Turn++;

            Logger.Info("Press ENTER to play the nex turn!");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
                NextTurn();
        }
        private void EndOfGame()
        {
            Logger.Info("There is a winner");
            System.Environment.Exit(0);
        }
    }
}