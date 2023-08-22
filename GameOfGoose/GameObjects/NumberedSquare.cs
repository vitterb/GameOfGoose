namespace GameOfGoose.GameObjects
{
    public class NumberedSquare : ISquare
    {
        public NumberedSquare(int number) : base(number) { }

        public int Number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void HandlePlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }

}