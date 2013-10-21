using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Defense.Model.ContextActions
{
    class PickupAction : ContextActionAdapter
    {
        private Materials.IMaterial _content;
        private Actors.Player _player;

        public PickupAction(Materials.IMaterial content, Actors.Player player)
        {
            _content = content;
            _player = player;
        }

        public override void Execute()
        {
            _player.Pickup(_content);
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
                return Keys.Space;
            }
        }

        public override string Text
        {
            get
            {
                return CanExecute ?
                    string.Format("Pickup {0}", _content.ToString()) :
                    string.Empty;
            }
        }
    }
}
