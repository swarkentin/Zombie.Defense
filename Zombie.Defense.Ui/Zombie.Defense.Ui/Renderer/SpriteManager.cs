using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Zombie.Defense.Ui.UiModel;
using Zombie.Defense.Model;

namespace Zombie.Defense.Ui.Renderer
{
    public class SpriteManager
    {
        private readonly Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private readonly Microsoft.Xna.Framework.Content.ContentManager _contentManager;
        private readonly GameStatePainter _gamePainter;
        private readonly GraphicsSettings _graphicsSettings;        
        private readonly HashSet<Sprite2d> _sprites = new HashSet<Sprite2d>();        

        public SpriteManager(
              Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
            , GameState gameState
            , Microsoft.Xna.Framework.Content.ContentManager contentManager
            , DisplayMode display
            , GraphicsSettings graphicsSettings)
        {            
            _spriteBatch = spriteBatch;
            _gamePainter = new GameStatePainter(gameState, contentManager, display, graphicsSettings);
            _contentManager = contentManager;
            _graphicsSettings = graphicsSettings;
        }        

        internal void Update(GameTime gameTime)
        {
            _gamePainter.Update(gameTime);            
        }

        internal void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();
            
            _gamePainter.Draw(_spriteBatch);

            _spriteBatch.End();
        }
       
    }
}
