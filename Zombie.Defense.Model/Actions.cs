using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.ContextActions;

namespace Zombie.Defense.Model
{
    public class Actions : IEnumerable<ContextActionAdapter>
    {
        private readonly List<ContextActionAdapter> _actions = new List<ContextActionAdapter>();
        public bool ContextAction { get; set; }        

        public void Execute(KeyboardState previousState, KeyboardState state)
        {
            foreach (var action in this)
            {
                if (action.CanExecute &&
                    previousState.IsKeyDown(action.Key) && state.IsKeyUp(action.Key))
                {
                    action.Execute();
                }
            }            
        }        

        public void Add(ContextActionAdapter action){
            _actions.Add(action);
        }    

        public void AddRange(IEnumerable<ContextActionAdapter> actions){
            _actions.AddRange(actions);
        }
          
        public IEnumerator<ContextActionAdapter>  GetEnumerator()
        {
 	        return _actions.GetEnumerator();
        }

        System.Collections.IEnumerator  System.Collections.IEnumerable.GetEnumerator()
        {
 	        return GetEnumerator();
        }
    }
}
