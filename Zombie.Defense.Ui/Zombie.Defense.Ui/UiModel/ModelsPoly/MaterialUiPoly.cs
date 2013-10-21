using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zombie.Defense.Ui.UiModel.ModelsPoly
{
    public class MaterialUiPoly : IUiModel
    {
        private readonly IMaterialPoly _source;
        private readonly MaterialToScreenMap _screenMap;
        private readonly GraphicsSettings _graphicsSettings;
        private readonly BasicEffect _basicEffect;
        private float _scale = 50.0f;
        private VertexPositionColor[] _pointList;
        private short[] _lineListIndicies;

        private Matrix _world;


        public MaterialUiPoly(IMaterialPoly source, MaterialToScreenMap screenMap, GraphicsSettings graphicsSettings)
        {
            _source = source;
            _screenMap = screenMap;
            _graphicsSettings = graphicsSettings;
            _basicEffect = new BasicEffect(graphicsSettings.Device);

            CreatePolyModel();
        }

        private void CreatePolyModel()
        {
            var points = _source.Points;
            var numPoints = points.Length;

            if (numPoints % 3 != 0)
                throw new ArgumentException("Material Poly must have %3 points");

            var numPointIndicies = numPoints / 3;
            //Extract Points
            _pointList = new VertexPositionColor[numPointIndicies];

            int sp = 0;
            for (var p = 0; p < numPointIndicies; p++)
            {
                _pointList[p] = new VertexPositionColor(
                      new Vector3(points[sp], points[sp + 1], points[sp + 2])
                    , Color.White);
                sp += 3;
            }

            //Determine Lines
            _lineListIndicies = new short[(numPointIndicies * 2) - 2];

            // Populate the array with references to indices in the vertex buffer
            for (int i = 0; i < numPointIndicies - 1; i++)
            {
                _lineListIndicies[i * 2] = (short)(i);
                _lineListIndicies[(i * 2) + 1] = (short)(i + 1);
            }
        }

        public Vector3 Position
        {
            get
            {                
                var pos = _screenMap.ToCoordinates(_source.TileX, _source.TileY);
             
                //return worldPosition;                
                return new Vector3(
                   pos.X
                , -pos.Y
                , 0f);
            }
        }

        void IUiModel.Update(GameTime gameTime)
        {
            _world = Matrix.Identity * Matrix.CreateScale(_scale);
            _world *= Matrix.CreateTranslation(Position);

            _basicEffect.World = _world;
            _basicEffect.View = _graphicsSettings.Camera.viewMatrix;
            _basicEffect.Projection = _graphicsSettings.Camera.projectionMatrix;
        }

        void IUiModel.Draw(SpriteBatch spriteBatch)
        {
            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();                
                _graphicsSettings.Device.DrawUserIndexedPrimitives<VertexPositionColor>(
                     PrimitiveType.LineList
                    , _pointList
                    , 0
                    , _pointList.Length
                    , _lineListIndicies
                    , 0
                    , _pointList.Length - 1);

            }
        }
    }
}
