using GameOfGoose.Factory;
using GameOfGoose.GameObjects;
using GameOfGoose.Interfaces;


namespace GameOfGoose
{
    public class Player: IPlayer
    {
        public string Name { get; }
        public int Position { get; set; }
        public bool GameOver { get; set; }
        public bool IsWaiting { get; set; }
        public bool IsMovingForward { get ; set ; }
        public int SkipCounter { get; set; }
        
        public Player(string name)
        {
            Name = name;
            Position = 0;
            SkipCounter = 0;
        }
        


        public void TakeTurn()
        {
            SkipNextTurn = false;
        }
       
        public void DiceRollAndMove(GameBoard board, IDice dice) { }

        public bool IsAnotherPlayerOnSquare(Player currentPlayer, List<Player> playerList)
        {
            throw new NotImplementedException();
        }

        public void StartWaiting()
        {
           IsWaiting = true;
        }

        public void StopWaiting()
        {
            IsWaiting= false;
        }

        public void MoveToSpace(int squareNumber)
        {
            Position = squareNumber;
        }


        public void Move(GameBoard board, int diceThrow)
        {
            
            Position += diceThrow;

            if (Position < 64)
            {
                while (SquareTypes.Goose == board.GetSquare(Position).Type && IsMovingForward)
                {
                    Position += diceThrow;
                    if (Position >= board.Squares.Count)
                    {
                        IsMovingForward = false;
                        int overshootAmount = Position - (board.Squares.Count - 1);
                        Position = board.Squares.Count - 1 - overshootAmount;
                    }
                }
            }
            if (Position >= board.Squares.Count)
            {
                IsMovingForward = false;
                int overshootAmount = Position-(board.Squares.Count-1);
                Position = board.Squares.Count-1 - overshootAmount;
            }
            if (Position < 64)
            {
                
                while (SquareTypes.Goose == board.GetSquare(Position).Type && !IsMovingForward)
                {
                    Position -= diceThrow;
                }
                IsMovingForward = true;
            }
        }
    }
}