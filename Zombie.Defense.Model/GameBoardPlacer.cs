using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Gameboard;
using Zombie.Defense.Model.Materials;

namespace Zombie.Defense.Model
{
    /// <summary>
    /// Used to place set pieces on the game board.
    /// </summary>
    class GameBoardPlacer
    {
        internal void Add(
              GameBoard target
            , IMaterial material
            , int xPosition
            , int yPosition)
        {
            material.TileX = xPosition;
            material.TileY = yPosition;
            target.Materials.Add(material);
        }
    }
}
