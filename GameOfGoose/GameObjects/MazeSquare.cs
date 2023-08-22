namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class MazeSquare : SpecialSquare
        {
            public MazeSquare(int number) : base(number) { }
        }
    }
}