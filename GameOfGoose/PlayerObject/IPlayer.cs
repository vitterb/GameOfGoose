using GameOfGoose.GameObjects;
using GameOfGoose.Interfaces;

namespace GameOfGoose
{
    public interface IPlayer
    {
        bool GameOver { get; }
        bool IsWaiting { get; set; }
        string Name { get; }
        int Position { get; set; }
        bool SkipNextTurn { get; }
        bool SkipThreeTurns { get; }

        void DiceRollAndMove(GameBoard board, IDice dice);
        bool IsAnotherPlayerOnSquare(Player currentPlayer, List<Player> playerList);
        void StartWaiting();
        void StopWaiting();
        void TakeTurn();
    }
}