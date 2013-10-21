using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Ui.UiModel.Models3d
{
    public class MaterialUiModel : Model3d
    {
        private readonly IMaterial _material;
        private readonly MaterialToScreenMap _screenMap;
        private GraphicsSettings _settings;

        internal MaterialUiModel(
             IMaterial3d material
            , MaterialToScreenMap screenMap
            , ModelBuilder builder
            , GraphicsSettings graphicsSettings) :
            base(builder.Build(material), material, graphicsSettings)
        {
            _material = material;
            _screenMap = screenMap;
            _settings = graphicsSettings;
            ApplyScale(_screenMap.TileSize);
        }

        public override float Rotation
        {
            get
            {
                return 0f;
            }
        }

        public override Vector3 Position
        {
            get
            {             
                var pos = _screenMap.ToCoordinates(_material.TileX, _material.TileY);
                
                //return worldPosition;                
                return new Vector3(
                   pos.X
                , -pos.Y
                , 0f);
            }
        }
    }
}
