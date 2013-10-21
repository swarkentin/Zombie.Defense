using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Defense.Model.ContextActions
{
    public abstract class ContextActionAdapter
    {
        public abstract void Execute();

        public abstract bool CanExecute { get; }

        public abstract Keys Key { get; }

        public abstract string Text { get; }
               
    }
}
