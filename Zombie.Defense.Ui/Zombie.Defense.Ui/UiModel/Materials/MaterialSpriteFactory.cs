using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Zombie.Defense.Ui.UiModel;
using Zombie.Defense.Ui.UiModel.Models3d;
using Zombie.Defense.Ui.UiModel.ModelsPoly;

namespace Zombie.Defense.Ui.UiModel.Materials
{
    public class MaterialSpriteFactory
    {        
        private readonly ContentManager _contentManager;
        private readonly ModelBuilder _textureBuilder;                
        private readonly MaterialToScreenMap _materialToScreenMap;
        private readonly Dictionary<IMaterial, IUiModel> _created = new Dictionary<IMaterial, IUiModel>();
        private readonly GraphicsSettings _graphicsSettings;

        public MaterialSpriteFactory(
              MaterialToScreenMap materialToScreenMap
            , ContentManager contentManager
            , GraphicsSettings graphicsSettings)
        {
            _materialToScreenMap = materialToScreenMap;            
            _textureBuilder = new ModelBuilder(contentManager);
            _graphicsSettings = graphicsSettings;
        }

        //TODO... well; i did something wrong here code-wise.
        public IUiModel Build(IMaterial source)
        {
            var material2d = source as IMaterial2d;
            if (null != material2d)
            {
                return Build(material2d);
            }

            var material3d = source as IMaterial3d;
            if (null != material3d)
            {
                return Build(material3d);
            }

            var materialPoly = source as IMaterialPoly;
            if (null != materialPoly)
            {
                return Build(materialPoly);
            }
            throw new ArgumentException("source");
        }

        public IUiModel Build(IMaterial2d source)
        {
            var sprite = new MaterialUiSprite(source, _materialToScreenMap, _textureBuilder);
            _created.Add(source, sprite);

            return sprite;
        }

        public IUiModel Build(IMaterial3d source)
        {
            var model = new MaterialUiModel(source, _materialToScreenMap, _textureBuilder, _graphicsSettings);
            _created.Add(source, model);

            return model;
        }

        public IUiModel Build(IMaterialPoly source)
        {
            var model = new MaterialUiPoly(source, _materialToScreenMap, _graphicsSettings);
            _created.Add(source, model);

            return model;
        }

        internal IUiModel Existing(IMaterial material)
        {
            return _created[material];
        }        
    }
}
