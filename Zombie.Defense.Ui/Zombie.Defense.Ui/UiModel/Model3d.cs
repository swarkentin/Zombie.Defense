using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using Zombie.Defense.Model.Materials;

namespace Zombie.Defense.Ui.UiModel
{
    public abstract class Model3d : IUiModel
    {
        private readonly XnaModel _model;
        private readonly IMaterial3d _material;
        private float _scale = 1.0f;
        private readonly GraphicsSettings _settings;
        private Matrix _world;

        protected Model3d(XnaModel model, IMaterial3d material, GraphicsSettings settings)
        {
            _model = model;
            _material = material;
            _settings = settings;        
        }

        public void Update(GameTime gameTime)
        {            
            _world = Matrix.Identity * Matrix.CreateScale(_scale);
            _world *= Matrix.CreateFromAxisAngle(Vector3.Up, Rotation);
            _world *= Matrix.CreateTranslation(Position);
        }

        public Rectangle Size { get; private set; }
        public abstract Vector3 Position { get; }
        public abstract float Rotation { get; }

        protected XnaModel Model { get { return _model; } }

        protected void ApplyScale(int tileSize)
        {
            var radius = 0f;
            foreach (var m in Model.Meshes)
            {
                radius = Math.Max(m.BoundingSphere.Radius, radius);
            }
            if (radius == 0f)
            {
                _scale = 0f;
                return;
            }

            _scale = (float)tileSize / (radius);
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            //Copy any parent transforms
            var transforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(transforms);

            //Draw the Model, as Model CannotUnloadAppDomainException have multiple meshes; so loop.
            foreach (var mesh in Model.Meshes)
            {
                //Set Mesh orientation, along iwth camera and projection
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    if(_material.Highlight)
                        effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0f);
                    if (_material is Zombie.Defense.Model.Actors.Zombie)
                    {
                        effect.AmbientLightColor = new Vector3(1.0f, 0.1f, 0.1f);
                    }
                    if (_material is Zombie.Defense.Model.Actors.Player)
                    {
                        effect.AmbientLightColor = new Vector3(0.1f, 0.1f, 1.0f);
                    }
                    effect.World = transforms[mesh.ParentBone.Index] * _world;
                    effect.View = _settings.Camera.viewMatrix;
                    effect.Projection = _settings.Camera.projectionMatrix;
                }
                mesh.Draw();
            }            
        }     
    }
}
