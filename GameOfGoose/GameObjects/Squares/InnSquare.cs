﻿using GameOfGoose.Factory;
using GameOfGoose.Interfaces;

namespace GameOfGoose.GameObjects.Squares
{
    public class InnSquare : ISquare
    {
        public int Number { get; set; }
        public SquareTypes Type { get; set; }

        public InnSquare(int number, SquareTypes type)
        {
            Number = number;
            Type = type;
        }

        public void Action(IPlayer player)
        {
            player.SkipCounter = 1;
        }
    }
}