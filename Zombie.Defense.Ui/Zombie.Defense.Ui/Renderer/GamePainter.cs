using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Ui.UiModel;
using Microsoft.Xna.Framework.Graphics;
using Zombie.Defense.Ui.UiModel.Materials;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework.Content;

namespace Zombie.Defense.Ui.Renderer
{
    class GameStatePainter
    {
        /// <summary>
        /// 500x500 gameboard
        /// </summary>        
        private readonly Rectangle GameBoardZone = new Rectangle(150, 100, 450, 500);

        private readonly Model.GameState _gameState;
        private readonly Microsoft.Xna.Framework.Content.ContentManager _contentManager;
        private readonly DisplayMode _displayMode;
        private readonly MaterialSpriteFactory _spriteFactory;
        private readonly HashSet<IUiModel> _models = new HashSet<IUiModel>();
        private readonly HashSet<IUiModel> _gridModels = new HashSet<IUiModel>();
        private readonly SpriteFont _spriteFont;
        private readonly GraphicsSettings _graphicsSettings;

        private bool _showGrid = false;

        public GameStatePainter(
              Model.GameState gameState
            , ContentManager contentManager
            , DisplayMode displayMode
            , GraphicsSettings graphicsSettings)
        {
            _gameState = gameState;
            _contentManager = contentManager;
            _displayMode = displayMode;
            _graphicsSettings = graphicsSettings;

            _spriteFactory = new MaterialSpriteFactory(
                  new MaterialToScreenMap(gameState.Board.SizeX, gameState.Board.SizeY, GameBoardZone)
                , contentManager
                , graphicsSettings);

            _spriteFont = contentManager.Load<SpriteFont>("SpriteFont1");
            Initialize();
        }

        private void Initialize()
        {
            foreach (IMaterial mat in _gameState.Board.Materials)
            {
                _models.Add(_spriteFactory.Build(mat));
            }

            if (_showGrid)
            {
                for (int x = 0; x < _gameState.Board.SizeX; x++)
                {
                    for (int y = 0; y < _gameState.Board.SizeY; y++)
                    {
                        _gridModels.Add(_spriteFactory.Build(new GridSquare() { TileX = x, TileY = y }));
                    }
                }
            }
        }

        internal void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var changeSet = _gameState.Update(gameTime);
            foreach (var added in changeSet.MaterialsAdded)
            {
                _models.Add(_spriteFactory.Build(added));
            }
            foreach (var removed in changeSet.MaterialsRemoved)
            {
                _models.Remove(_spriteFactory.Existing(removed));
            }

            foreach (var model in _gridModels)
            {
                model.Update(gameTime);
            }
            foreach (var sprite in _models)
            {
                sprite.Update(gameTime);
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {

            if (_showGrid)
            {
                foreach (var model in _gridModels)
                {
                    model.Draw(spriteBatch);
                }
            }

            DisplayActions(spriteBatch);

            foreach (var model in _models)
            {
                model.Draw(spriteBatch);
            }
        }

        private void DisplayActions(SpriteBatch spriteBatch)
        {
            var actions = _gameState.Interaction.AvailableActions;

            int strRow = 10;
            foreach (var action in actions)
            {
                spriteBatch.DrawString(
                  _spriteFont
                , string.Format("{0}: [{1}]", action.Text, action.Key)
                , new Vector2(10, strRow)
                , Color.White);

                strRow += 20;
            }

            spriteBatch.DrawString(
                  _spriteFont
                , string.Format("Zombies Killed, '{0}'", _gameState.Statistics.KillCount)
                , new Vector2(600, 10)
                , Color.White);
        }
    }
}
