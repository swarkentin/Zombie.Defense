using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Provider;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Model.Materials
{
    /// <summary>
    /// Describes a material that can be used to construct walls, items, etc.
    /// It is a raw material.
    /// </summary>
    public interface IMaterial
    {
        float TileX { get; set; }
        float TileY { get; set; }

        /// <summary>
        /// Highlights the material.
        /// </summary>
        bool Highlight { get; set; }

        /// <summary>
        /// Updates the material to match game state.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="board"></param>
        void Update(GameTime gameTime, Gameboard.GameBoard board);

        /// <summary>
        /// Calculates a distance from two objects.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        float DistanceFrom(IMaterial other);
    }
}
