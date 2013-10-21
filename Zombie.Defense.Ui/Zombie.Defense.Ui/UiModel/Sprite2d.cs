using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Zombie.Defense.Ui.UiModel
{
    public abstract class Sprite2d : IUiModel
    {
        private readonly Texture2D _texture;
        private float _scale = 1.0f;        

        protected Sprite2d(Texture2D texture2D)
        {
            _texture = texture2D;
        }

        public abstract void Update(GameTime gameTime);

        public Rectangle Size { get; private set; }
        public abstract Vector2 Position { get; }

        protected Texture2D Texture { get { return _texture; } }

        protected void ApplyScale(int tileSize)
        {
            Size = new Rectangle(
                  0
                , 0
                , tileSize
                , tileSize);
        }

        public float Scale
        {
            get { return _scale; }
            protected set
            {
                _scale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(_texture.Width * Scale), (int)(_texture.Height * Scale));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                  _texture
                , Position
                , Size
                , Color.White
                , 0.0f
                , Vector2.Zero
                , Scale
                , SpriteEffects.None
                , 0);
        }     
    }
}
