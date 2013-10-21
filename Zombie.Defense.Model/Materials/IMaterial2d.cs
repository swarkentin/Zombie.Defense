using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Provider;

namespace Zombie.Defense.Model.Materials
{
    public interface IMaterial2d : IMaterial
    {
        AssetKey2D AssetKey { get; }
    }
}
