using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombie.Defense.Model.Gameboard
{
    public class GameBoardBuilder
    {
        internal GameBoard Create()
        {
            return new GameBoard(100, 120);
        }


    }
}
