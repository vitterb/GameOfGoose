﻿using GameOfGoose.Factory;
using GameOfGoose.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfGoose.GameObjects.Squares
{
    public class BridgeSquare : ISquare
    {
        public int Number { get; set; }
        public SquareTypes Type { get; set; }

        public BridgeSquare(int number, SquareTypes type)
        {
            Number = number;
            Type = type;
        }

        public void Action(IPlayer player)
        {
            player.MoveToSpace(12);
        }
    }
}