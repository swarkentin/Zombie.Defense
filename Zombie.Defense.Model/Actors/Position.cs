﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombie.Defense.Model.Actors
{
    public class Position
    {
        private readonly int _x;
        private readonly int _y;

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
    }
}
