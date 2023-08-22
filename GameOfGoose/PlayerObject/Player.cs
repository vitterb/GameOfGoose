using GameOfGoose.GameObjects;
using GameOfGoose.Interfaces;
using GameOfGoose.PlayerObject;

namespace GameOfGoose
{
    public class Player : IPlayer
    {
        public string Name { get; }
        public int Position { get; set; }
        public bool SkipNextTurn { get; private set; }
        public bool SkipThreeTurns { get; private set; }
        public bool GameOver { get; private set; }
        public bool IsWaiting { get; set; }
        private PlayerManager playerManager;

        public Player(string name, PlayerManager playerManager)
        {
            Name = name;
            Position = 0;
            this.playerManager = playerManager;

        }
        public bool IsAnotherPlayerOnSquare(Player currentPlayer, List<Player> playerList)
        {
            foreach (Player player in playerList)
            {
                if (player != currentPlayer && player.IsWaiting && player.Position == 31)
                {
                    return true;
                }
            }
            return false;
        }


        public void TakeTurn()
        {
            SkipNextTurn = false;
        }
        public void StartWaiting()
        {
            IsWaiting = true;
        }
        public void StopWaiting()
        {
            IsWaiting = false;
        }
        public void DiceRollAndMove(GameBoard board, IDice dice)
        {
            int roll1 = dice.Roll();
            int roll2 = dice.Roll();
            int smallerRoll = Math.Min(roll1, roll2);
            int largerRoll = Math.Max(roll1, roll2);
            int diceRoll = roll1 + roll2;

            if (Position == 0 && diceRoll == 9)
            {
                if ((smallerRoll == 3 && largerRoll == 6) || (smallerRoll == 4 && largerRoll == 5))
                {
                    Position = (smallerRoll == 3 && largerRoll == 6) ? 53 : 26;
                    return;
                }
            }
            else if (Position == 0)
            {
                Position += diceRoll;
            }
            else if (Position != 1)
            {
                Position += diceRoll;
            }
            if (Position >= board.Squares.Count)
            {
                int overshootAmount = Position - (board.Squares.Count - 1);
                Position = board.Squares.Count - 1 - overshootAmount;
            }
            if (board.Squares[Position] is ISquare.GooseSquare)
            {
                Position += diceRoll;
            }
            if (board.Squares[Position] is ISquare.BridgeSquare)
            {
                Position = 12;
            }
            if (board.Squares[Position] is ISquare.InnSquare)
            {
                SkipNextTurn = true;
            }
            if (board.Squares[Position] is ISquare.MazeSquare)
            {
                Position = 39;
            }
            if (board.Squares[Position] is ISquare.PrisonSquare)
            {
                SkipThreeTurns = true;
            }
            if (board.Squares[Position] is ISquare.DeathSquare)
            {
                Position = 1;
            }
            if (board.Squares[Position] is ISquare.EndSquare)
            {
                GameOver = true;
            }
            if (board.Squares[Position] is ISquare.WellSquare)
            {
                if (IsAnotherPlayerOnSquare(this, playerManager.GetPlayerList()))
                {
                    foreach (var player in playerManager.GetPlayerList())
                    {
                        if (player.IsWaiting && player.Position == Position)
                        {
                            player.StopWaiting();
                            this.StartWaiting();
                            break;
                        }
                    }
                }
                else this.StartWaiting();
            }
        }
    }
}