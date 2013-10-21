using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zombie.Defense.Ui
{
    public class GraphicsSettings
    {           
       public GraphicsSettings(
              Camera camera            
            , Viewport viewport
            , GraphicsDevice device)
        {
            Camera = camera;            
            Viewport = viewport;
            Device = device;
        }               

        public Camera Camera { get; private set; }        

        public Viewport Viewport { get; private set; }

        public GraphicsDevice Device { get; private set; }
    }
}
