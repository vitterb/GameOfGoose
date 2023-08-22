namespace GameOfGoose.GameObjects
{
    interface ISquare
    {
        int Number { get; set; }

        void HandlePlayer(IPlayer player);
    }
}