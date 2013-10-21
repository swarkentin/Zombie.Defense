using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;

namespace Zombie.Defense.Model.Weapons
{
    public class Turret : MaterialAdapter, IMaterial3d
    {
        private Random _rand = new Random(DateTime.Now.Second);

        private const float TimeToBuild = 2f; //5 seconds to build
        private const float TimeBetweenShots = 1f;
        
        private float TimeUntilNextShot { get; set; }
        private float TimeSpentBuilding { get; set; }
        public bool IsComplete { get; private set; }
        

        public Provider.AssetKey3D AssetKey
        {
            get { return Provider.AssetKey3D.TURRET; }
        }

        public override void Update(GameTime gameTime, Gameboard.GameBoard board)
        {
            if (!IsComplete)
            {
                TimeSpentBuilding += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimeSpentBuilding >= TimeToBuild)
                {
                    System.Console.WriteLine("Turret Complete!");
                    IsComplete = true;
                }
                else
                {
                    return;
                }
            }
            TryFire(gameTime, board);
        }

        private void TryFire(GameTime gameTime, Gameboard.GameBoard board)
        {
            TimeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimeUntilNextShot >= TimeBetweenShots)
            {
                if (Fire(board))
                    TimeUntilNextShot = 0f;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns>True if a target was found and the turret can fire, false otherwise.</returns>
        private bool Fire(Gameboard.GameBoard board)
        {
            var bullet = MaterialFactory.Bullet;
            bullet.TileX = TileX;
            bullet.TileY = TileY;

            var direction = FindTarget(board);
            if (Vector2.Zero.Equals(direction))
                return false;

            bullet.Directon = direction;
            board.FireProjectile(bullet);            

            System.Console.WriteLine("Fire!!");
            return true;
        }

        private Vector2 FindTarget(Gameboard.GameBoard board)
        {
            var targets = board.Materials.Where(tar => tar is Actors.Zombie).OrderBy(n => n.DistanceFrom(this));
            if (!targets.Any())
                return Vector2.Zero;
            
            var target = targets.First();

            if (null == target)
                return Vector2.Zero;
            return Vector2.Normalize(
                new Vector2(
                     target.TileX - TileX
                    , target.TileY - TileY));
        }  
    }
}
