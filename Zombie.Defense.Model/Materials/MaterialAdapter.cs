using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Provider;
using Zombie.Defense.Model.Gameboard;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Model.Materials
{
    public abstract class MaterialAdapter : IMaterial
    {
        public float TileX { get; set; }
        public float TileY { get; set; }

        /// <summary>
        /// By default, highlighting does nothing.
        /// </summary>
        public bool Highlight { get; set; }

        /// <summary>
        /// By default, there is nothing to update.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="board"></param>
        public virtual void Update(GameTime gameTime, GameBoard board) { }

        /// <summary>
        /// Distance between this and another material... not accounting for offeset yet...
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public float DistanceFrom(IMaterial other)
        {
            var xDif = (float) Math.Abs(TileX - other.TileX);
            var yDif = (float) Math.Abs(TileY - other.TileY);
            return (float) Math.Sqrt(Math.Pow(xDif, 2) + Math.Pow(yDif, 2));
        }
    }
}
