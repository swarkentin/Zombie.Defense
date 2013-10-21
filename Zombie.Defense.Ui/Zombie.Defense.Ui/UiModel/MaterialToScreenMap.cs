using Microsoft.Xna.Framework;
using System;

namespace Zombie.Defense.Ui.UiModel
{
    public class MaterialToScreenMap
    {
        private readonly int _boardSizeX;
        private readonly int _boardSizeY;
        private readonly Rectangle _boardBoundary;
        private readonly int _tileSize;        
        
        public MaterialToScreenMap(int boardSizeX, int boardSizeY, Rectangle GameBoardZone)
        {
            _boardSizeX = boardSizeX;
            _boardSizeY = boardSizeY;
            _boardBoundary = GameBoardZone;

            _tileSize = _boardBoundary.Width / _boardSizeX;
        }       

        internal Vector2 ToCoordinates(float x, float y)
        {            
            return new Vector2(x*_tileSize + _boardBoundary.X, y*_tileSize + _boardBoundary.Y);
        }

        public int TileSize { get { return _tileSize; } }        
    }
}
