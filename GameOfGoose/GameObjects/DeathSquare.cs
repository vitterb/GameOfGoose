namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class DeathSquare : SpecialSquare
        {
            public DeathSquare(int number) : base(number) { }
        }
    }
}