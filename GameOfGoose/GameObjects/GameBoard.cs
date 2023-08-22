using System.Text;
using static GameOfGoose.GameObjects.ISquare;

namespace GameOfGoose.GameObjects
{
    public class GameBoard
    {
        public List<ISquare> Squares { get; } = new List<ISquare>();

        public GameBoard() 
        {
            InitializeNumberedSquares();
            InitializeSpecialSquares();
            InitializaGooseSquars();
        }

        private void InitializeNumberedSquares()
        {
            for (int i = 0; i < 64; i++)
            {
                Squares.Add(new NumberedSquare(i + 1));
            }
        }
        private void InitializeSpecialSquares()
        {
            ReplaceWithSpecialSquare(new BridgeSquare(6));
            ReplaceWithSpecialSquare(new InnSquare(19));
            ReplaceWithSpecialSquare(new WellSquare(31));
            ReplaceWithSpecialSquare(new MazeSquare(42));
            ReplaceWithSpecialSquare(new PrisonSquare(52));
            ReplaceWithSpecialSquare(new DeathSquare(58));
            ReplaceWithSpecialSquare(new EndSquare(63));
        }
        private void ReplaceWithSpecialSquare(SpecialSquare specialSquare)
        {
            int index = specialSquare.Number;
            if (index < Squares.Count)
            {
                Squares[index] = specialSquare;
            }
        }
        private void InitializaGooseSquars()
        {
            var gooseSquareNumbers = new List<int>() { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

            foreach (int gooseSquareNumber in gooseSquareNumbers) 
            {
                int index = gooseSquareNumber;
                if (index < Squares.Count)
                {
                    Squares[index] = new GooseSquare(gooseSquareNumber);
                }
            }
        }
    }
}