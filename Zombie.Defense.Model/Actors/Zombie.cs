using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.Projectiles;

namespace Zombie.Defense.Model.Actors
{
    public class Zombie : MaterialAdapter,  IActor
    {
        private Vector2 _direction = new Vector2(0f, 0f);
        private Vector2 _maxSpeed = new Vector2(0.1f, 0.1f);
        private Vector2 _speed = new Vector2(0f, 0f);
        private const float _acceleration = 0.1f;    

        public Provider.AssetKey3D AssetKey
        {
            get { return Provider.AssetKey3D.ZOMBIE; }
        }

        public override void Update(GameTime gameTime, Gameboard.GameBoard board)
        {
            var movement = ChooseDirection(board);

            _speed.X = movement.X == 0 ? 0f : _speed.X;
            _speed.Y = movement.Y == 0 ? 0f : _speed.Y;

            var proposed = new Vector2(_acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * movement.X + _speed.X
                                     , _acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * movement.Y + _speed.Y);

            _speed.X = Math.Abs(proposed.X) <= _maxSpeed.X ? proposed.X : _speed.X;
            _speed.Y = Math.Abs(proposed.Y) <= _maxSpeed.Y ? proposed.Y : _speed.Y;

            if (board.ValidatedMove
                (
                 _speed
                , TileX
                , TileY
                ))
            {
                TileX += _speed.X;
                TileY += _speed.Y;
            }
            
        }        

        private Vector2 ChooseDirection(Gameboard.GameBoard board)
        {
            var target = board.Player;
            var direction = new Vector2(
                    target.TileX < TileX ? -1 : target.TileX > TileX ? 1 : 0
                ,   target.TileY < TileY ? -1 : target.TileY > TileY ? 1 : 0);

            //direction.X = board.GetMaterials(TileX + direction.X, TileY).Any(m => m is StoneMaterial) ? 0 : direction.X;
            //direction.Y = board.GetMaterials(TileX, TileY + direction.Y).Any(m => m is StoneMaterial) ? 0 : direction.Y;
            
            return direction;
        }        
    }
}
