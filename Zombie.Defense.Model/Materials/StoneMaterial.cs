using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Provider;

namespace Zombie.Defense.Model.Materials
{
    internal class StoneMaterial : MaterialAdapter, IMaterial3d
    {
        public AssetKey3D AssetKey { get { return AssetKey3D.ZOMBIE; } }

    }
}
