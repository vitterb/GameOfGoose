namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class InnSquare : SpecialSquare
        {
            public InnSquare(int number) : base(number) { }
        }
    }
}