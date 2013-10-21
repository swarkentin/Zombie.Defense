using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Gameboard;
using Zombie.Defense.Model.Materials;
using Zombie.Defense.Model.Actors;

namespace Zombie.Defense.Model
{
    public class GameState
    {
        private readonly GameBoardBuilder _boardBuilder = new GameBoardBuilder();
        private readonly GameBoardController _boardController = new GameBoardController();
        private readonly EnemyManager _enemyManager;

        private readonly GameBoard _activeBoard;        

        private readonly GameStats _stats = new GameStats();

        public GameState()
        {
            _activeBoard = _boardBuilder.Create();
            _enemyManager = new EnemyManager(100, _stats);                            
            _boardController.Attach(_activeBoard, _enemyManager);
        }

        public GameBoard Board
        {
            get
            {
                return _activeBoard;
            }
        }

        public Interaction Interaction
        {
            get
            {
                return _boardController.Interaction;
            }
        }

        public GameStats Statistics
        { get { return _stats; } }

        public Player Player
        {
            get
            {
                return _boardController.Player;
            }
        }

        public GameStateChanges Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            return _boardController.Update(gameTime);            
        }
    }
}
