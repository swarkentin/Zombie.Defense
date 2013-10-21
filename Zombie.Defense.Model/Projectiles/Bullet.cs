using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Model.Projectiles
{
    public class Bullet : ProjectileAdapter
    {        
        private Vector2 _maxSpeed = new Vector2(1.0f, 1.0f);
        private Vector2 _speed = new Vector2(0f, 0f);
        private const float _acceleration = 0.5f;  

        public override Provider.AssetKey3D AssetKey
        {
            get { return Provider.AssetKey3D.ZOMBIE; }
        }

        public override void Update(GameTime gameTime, Gameboard.GameBoard board)
        {            
            _speed.X = Directon.X == 0 ? 0f : _speed.X;
            _speed.Y = Directon.Y == 0 ? 0f : _speed.Y;

            var proposed = new Vector2(_acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * Directon.X + _speed.X
                                     , _acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * Directon.Y + _speed.Y);

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
            else
            {
                board.Destroy(this);
            }

        }
    }
}
