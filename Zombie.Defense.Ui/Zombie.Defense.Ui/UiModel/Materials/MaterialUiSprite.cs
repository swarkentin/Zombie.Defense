using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Zombie.Defense.Provider;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Ui.UiModel.Materials
{
    public class MaterialUiSprite : Sprite2d
    {
        private readonly IMaterial _material;
        private readonly MaterialToScreenMap _screenMap;       

        internal MaterialUiSprite(IMaterial2d material, MaterialToScreenMap screenMap, ModelBuilder builder)
            : base(builder.Build(material))
        {
            _material = material;
            _screenMap = screenMap;
            ApplyScale(_screenMap.TileSize);
        }        

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Nothing
        }

        public override Vector2 Position
        {
            get
            {
                var pos = _screenMap.ToCoordinates(_material.TileX, _material.TileY);
                return pos;
            }
        }
    }
}
