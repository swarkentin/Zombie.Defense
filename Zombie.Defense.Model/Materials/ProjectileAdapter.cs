using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombie.Defense.Model.Materials
{
    public abstract class ProjectileAdapter : MaterialAdapter, IProjectile
    {
        public Microsoft.Xna.Framework.Vector2 Directon { get; set; }        

        public abstract Provider.AssetKey3D AssetKey { get; }       
    }
}
