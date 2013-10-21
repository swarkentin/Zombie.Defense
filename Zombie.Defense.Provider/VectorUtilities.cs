using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Provider
{
    public static class VectorUtilities
    {
        /// <summary>
        /// Just like Vector2.Normalize, except makes all vectors '1'.
        /// 
        /// Plus, it will not impact existing vector.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static Vector2 NormalizeOne(this Vector2 source)
        {
            var dest = new Vector2(
                  source.X > 0f ? 1 : source.X < 0f ? -1 : 0f
                , source.Y > 0f ? 1 : source.Y < 0f ? -1 : 0f);
            return dest;
        }
    }
}
