namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class PrisonSquare : SpecialSquare
        {
            public PrisonSquare(int number) : base(number) { }

        }
    }
}