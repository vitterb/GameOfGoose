using GameOfGoose.Factory;
using GameOfGoose.Interfaces;
using GameOfGoose.GameObjects;

namespace GameOfGoose.GameObjects.Squares
{
    public class GooseSquare : ISquare
    {
        public int Number { get; set; }
        public SquareTypes Type { get; set; }
        

        public GooseSquare(int number, SquareTypes type) 
        {
            this.Number = number;
            this.Type = type;

        }
        public void Action(IPlayer player)
        {
            
        }
    }
}