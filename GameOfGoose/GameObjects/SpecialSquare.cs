namespace GameOfGoose.GameObjects
{
    public abstract partial class ISquares
    {
        public class SpecialSquare : ISquare
        {
            public SpecialSquare(int number) { }

            public int Number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public void HandlePlayer(IPlayer player)
            {
                throw new NotImplementedException();
            }
        }
    }
}