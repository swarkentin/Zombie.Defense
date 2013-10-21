using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Zombie.Defense.Provider;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.Actors;


namespace Zombie.Defense.Model.Gameboard
{
    public class GameBoard
    {        
        private List<IMaterial> _materials = new List<IMaterial>();
        private List<IMaterial> _changesAdded = new List<IMaterial>();
        private List<IMaterial> _changesRemoved = new List<IMaterial>();

        private readonly int _sizeX;
        private readonly int _sizeY;        

        public GameBoard(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
        }

        public int SizeX { get { return _sizeX; } }
        public int SizeY { get { return _sizeY; } }

        public List<IMaterial> Materials { get { return _materials; } }

        public IEnumerable<IMaterial> GetMaterials(int tileX, int tileY)
        {
            return Materials.Where(m => (int)Math.Round(m.TileX) == tileX && (int)Math.Round(m.TileY) == tileY);
        }

        //Examines a direction, and a material at a given (x,y) and returns a direction that is valid
        //to travel.
        public bool ValidatedMove(Vector2 speed, float TileX, float TileY)
        {
            var targetX = speed.X + TileX;
            var targetY = speed.Y + TileY;

            if ((targetX) < 0 || (targetX) >= _sizeX)
                return false;

            if ((targetY) < 0 || (targetY) >= _sizeY)
                return false;

            return true;
        }

        public IEnumerable<IMaterial> Neighbors(IMaterial source)
        {
            var xRange = new[] { source.TileX - 1, source.TileX + 1 };
            var yRange = new[] { source.TileY - 1, source.TileY + 1 };

            return Materials.Where(
                m => xRange[0] <= m.TileX && xRange[1] >= m.TileX &&
                    yRange[0] <= m.TileY && yRange[1] >= m.TileY &&
                    m != source);
        }

        public Player Player
        {
            get
            {                
                return (Player) Materials.First(material => material is Player);
            }
        }

        internal void FireProjectile(IProjectile bullet)
        {
            _changesAdded.Add(bullet);
            Materials.Add(bullet);
        }

        internal void UpgardeToTurret(StoneMaterial source)
        {
            var turret = MaterialFactory.Turret;
            turret.TileX = source.TileX;
            turret.TileY = source.TileY;

            if (Destroy(source))
                Add(turret);               
        }

        private void Add(IMaterial3d source)
        {
            _changesAdded.Add(source);
            Materials.Add(source);
        }

        public bool Destroy(IMaterial source)
        {
            _changesRemoved.Add(source);
            return Materials.Remove(source);
        }

        internal GameStateChanges Update(GameTime gameTime)
        {
            foreach (var mat in Materials.ToList())
            {
                mat.Update(gameTime, this);
            }

           var changes = new GameStateChanges(_changesAdded.ToList(), _changesRemoved.ToList());
           _changesAdded.Clear();
           _changesRemoved.Clear();
                       
           return changes;
        }
        
    }
}
