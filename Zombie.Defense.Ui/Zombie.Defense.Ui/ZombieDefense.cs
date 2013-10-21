using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zombie.Defense.Ui.Renderer;
using Zombie.Defense.Model;

namespace Zombie.Defense.Ui
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ZombieDefense : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        
        private SpriteManager _spriteManager;
        private GameState _gameState;
        private Camera _camera;

        public ZombieDefense()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,                
                PreferredBackBufferHeight = 600
            };
            _camera = new Camera();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _gameState = new GameState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var aspectRatio = (float)_graphics.GraphicsDevice.Viewport.Width /
                              (float)_graphics.GraphicsDevice.Viewport.Height;
            
            _spriteManager = new SpriteManager(
                new SpriteBatch(GraphicsDevice)
                , _gameState
                , this.Content
                , GraphicsDevice.DisplayMode
                , new GraphicsSettings(
                       _camera                         
                    , _graphics.GraphicsDevice.Viewport
                    , _graphics.GraphicsDevice)
               );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _camera.Update();
            _spriteManager.Update(gameTime);            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {            
            GraphicsDevice.Clear(Color.CornflowerBlue);            

            _spriteManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
