using GameOfGoose.GameObjects;

namespace GameOfGoose.Interfaces
{
    public interface IPlayer
    {
        bool GameOver { get; set; }
        bool IsWaiting { get; set; }
        string Name { get; }
        int Position { get; set; }
        bool SkipNextTurn { get; set; }
        bool SkipThreeTurns { get; set; }
        bool IsMovingForward { get; set; }


        void DiceRollAndMove(GameBoard board, IDice dice);
        bool IsAnotherPlayerOnSquare(Player currentPlayer, List<Player> playerList);
        void StartWaiting();
        void StopWaiting();
        void TakeTurn();
        void MoveToSpace(int squareNumber);
        void Move(GameBoard board,int dicethrow);
    }
}