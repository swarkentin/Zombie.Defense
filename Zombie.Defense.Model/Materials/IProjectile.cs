using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Model.Materials
{
    public interface IProjectile : IMaterial3d
    {
        Vector2 Directon { get; set; }        
    }
}
