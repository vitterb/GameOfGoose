namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class WellSquare : SpecialSquare
        {
            public WellSquare(int number) : base(number) { }
        }
    }
}