namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class BridgeSquare : SpecialSquare
        {
            public BridgeSquare(int number) : base(number) { }
        }
    }
}