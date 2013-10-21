using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Model
{
    public class Movement
    {
        public Vector2 Direction { get; private set; }
        internal static Movement FromKeyboard(KeyboardState state)
        {
            var result = new Movement();

            result.Direction = DirectionFromState(state);
            return result;
        }

        private static Vector2 DirectionFromState(KeyboardState state)
        {
            var vec = new Vector2(0,0);

            if (state.IsKeyDown(Keys.Left) == true)
            {
                vec.X = -1;
            }
            else if (state.IsKeyDown(Keys.Right) == true)
            {
                vec.X = 1;
            }

            if (state.IsKeyDown(Keys.Up) == true)
            {
                vec.Y = -1;
            }
            else if (state.IsKeyDown(Keys.Down) == true)
            {
                vec.Y = 1;
            }
            
            return vec;
        }
    }
}
