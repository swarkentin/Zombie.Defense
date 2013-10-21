using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Zombie.Defense.Model.Materials;

namespace Zombie.Defense.Model.ContextActions
{
    class UpgradeToTurretAction : ContextActionAdapter
    {
        private readonly StoneMaterial _content;
        private readonly Gameboard.GameBoard _gameBoard;        

        public UpgradeToTurretAction(StoneMaterial content, Gameboard.GameBoard gameBoard)
        {
            _content = content;
            _gameBoard = gameBoard;
        }

        public override void Execute()
        {
            _gameBoard.UpgardeToTurret(_content);
        }

        public override bool CanExecute
        {
            get
            {
                return _content != null;
            }
        }

        public override Keys Key
        {
            get
            {
                return Keys.D1;
            }
        }

        public override string Text
        {
            get
            {
                return CanExecute ?
                    string.Format("Upgrade To Turret", _content.ToString()) :
                    string.Empty;
            }
        }
    }
}
