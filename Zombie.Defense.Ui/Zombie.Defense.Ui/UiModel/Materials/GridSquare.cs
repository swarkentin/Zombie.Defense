using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.Gameboard;

namespace Zombie.Defense.Ui.UiModel.Materials
{
    public class GridSquare : MaterialAdapter, IMaterialPoly
    {
        public void Update(GameTime gameTime, GameBoard board)
        {
            //Do Nothing
        }

        int[] IMaterialPoly.Points
        {
            get
            {
                return new[] {
                    0,0,0,
                    0,1,0,
                    1,1,0,
                    1,0,0
                };
            }
        }      
    }
}
