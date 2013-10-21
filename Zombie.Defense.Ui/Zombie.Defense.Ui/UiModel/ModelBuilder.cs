using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zombie.Defense.Provider;
using Zombie.Defense.Model.Materials;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;

namespace Zombie.Defense.Ui.UiModel
{
    public class ModelBuilder
    {
        private readonly ContentManager _contentManager;

        #region Enum -> Content Map

        private readonly Dictionary<AssetKey2D, String> _assetContentMap2d = new Dictionary<AssetKey2D, string>()
        {
             {AssetKey2D.MATERIAL_STONE, "Sprites/StoneBlock"}
            ,{AssetKey2D.ZOMBIE, "Sprites/Zombie"}
            ,{AssetKey2D.GRID_SQUARE, "Sprites/GridSquare"}
        };

        private readonly Dictionary<AssetKey3D, String> _assetContentMap3d = new Dictionary<AssetKey3D, string>()
        {
              {AssetKey3D.ZOMBIE, "Models/Crate"}
             ,{AssetKey3D.PLAYER, "Models/Crate"}
             ,{AssetKey3D.TURRET, "Models/turret"}
        };

        #endregion

        public ModelBuilder(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Texture2D Build(IMaterial2d material)
        {
            return _contentManager.Load<Texture2D>(_assetContentMap2d[material.AssetKey]);
        }

        public XnaModel Build(IMaterial3d material)
        {
            return _contentManager.Load<XnaModel>(_assetContentMap3d[material.AssetKey]);
        }
    }
}
