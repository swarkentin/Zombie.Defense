using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zombie.Defense.Ui.UiModel
{
    public interface IUiModel
    {
         void Update(GameTime gameTime);

         void Draw(SpriteBatch spriteBatch);
    }
}
