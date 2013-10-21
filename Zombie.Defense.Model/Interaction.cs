using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Zombie.Defense.Model.ContextActions;
using Zombie.Defense.Model.Actors;
using Zombie.Defense.Model.Gameboard;

namespace Zombie.Defense.Model
{
    public class Interaction
    {
        private KeyboardState _previousMoveState;
        private KeyboardState _previousActionState;
        private readonly GameBoard _activeBoard;

        public Interaction(GameBoard activeBoard)
        {                        
            _activeBoard = activeBoard;              
        }
        
        public Movement Movement
        {
            get
            {
                var state = Keyboard.GetState();                

                var movement = Movement.FromKeyboard(state);
                _previousMoveState = state;
                return movement;
            }
        }

        public Actions AvailableActions
        {
            get
            {
                var actions = new Actions();
                actions.AddRange(ContextActionFactory.Create(Player, _activeBoard));         
       
                return actions;
            }
        }

        public void Execute(Actions actions)
        {
            var state = Keyboard.GetState();

            actions.Execute(_previousActionState, state);
            _previousActionState = state;
        }

        public Player Player { get; set; }
    }
}
