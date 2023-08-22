namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class EndSquare : SpecialSquare
        {
            public EndSquare(int number) : base(number) { }
        }
    }
}