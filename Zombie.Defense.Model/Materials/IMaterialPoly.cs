using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Provider;

namespace Zombie.Defense.Model.Materials
{
    public interface IMaterialPoly : IMaterial
    {
        /// <summary>
        /// Array of groups of 3 points.
        /// </summary>
        int[] Points { get; }
    }
}
