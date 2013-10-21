using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Zombie.Defense.Model.Actors;

namespace Zombie.Defense.Model
{
    class GameBoardController
    {
        private Gameboard.GameBoard _activeBoard;
        private EnemyManager _enemyManager;
        private Player _player;
        private Interaction _interaction;

        internal void Attach(Gameboard.GameBoard activeBoard, EnemyManager enemyManager)
        {
            _activeBoard = activeBoard;
            _enemyManager = enemyManager;
            _interaction = new Interaction(_activeBoard);
            _interaction.Player = AddPlayer();
            FillBoard();            
        }

        public Interaction Interaction { get { return _interaction; } }

        private Player AddPlayer()
        {
            _player = new Player(_interaction)
            {
                TileX = 0
                ,
                TileY = _activeBoard.SizeY - 3
            };
            _activeBoard.Materials.Add(_player);
            return _player;
        }

        private void FillBoard()
        {
            FillMaterials();
        }

        private void FillMaterials()
        {
            var rows = 2;
            var gameBoardPlacer = new GameBoardPlacer();

            for (int xPosition = 0; xPosition < _activeBoard.SizeX; xPosition++)
            {
                for (int yPosition = _activeBoard.SizeY - 1; yPosition > (_activeBoard.SizeY - rows - 1); yPosition--)
                {
                    gameBoardPlacer.Add(
                          _activeBoard
                        , MaterialFactory.Stone
                        , xPosition
                        , yPosition);

                }
            }
        }

        internal GameStateChanges Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var changes = _enemyManager.Update(gameTime, _activeBoard);
            changes = changes.Merge(_activeBoard.Update(gameTime));

            return changes;
        }

        public Player Player
        {
            get
            {
                return _player;
            }
        }
    }
}
