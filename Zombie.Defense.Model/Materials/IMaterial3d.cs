using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Provider;

namespace Zombie.Defense.Model.Materials
{
    public interface IMaterial3d : IMaterial
    {
        AssetKey3D AssetKey { get; }        
    }
}
