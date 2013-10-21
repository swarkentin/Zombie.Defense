using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.Projectiles;

namespace Zombie.Defense.Model
{
    public class EnemyManager
    {
        private const int MaxSpawn = 50;
        private int _toSpawn = 1;
        private int _spawnTime;
        private float _newSpawnTimeS = 0.05f;        
        private double _lastSpawnTime = 0;
        private Random _rand = new Random(DateTime.Now.Millisecond);
        private int _killed = 0;
        
        private List<IActor> _enemies = new List<IActor>();
        private int p;
        private GameStats _stats;        

        public EnemyManager(int spawnTimeMs, GameStats stats)
        {
            _spawnTime = spawnTimeMs;
            _stats = stats;
        }
        

        internal GameStateChanges Update(
              GameTime gameTime
            , Gameboard.GameBoard board)
        {
            var spawnResult = Spawn(gameTime, board);

            return spawnResult.Merge(new GameStateChanges(new IMaterial [0],
                CheckHits(board)));
        }

        private IEnumerable<IMaterial> CheckHits(Gameboard.GameBoard board)
        {
            var killed = new List<IMaterial>();
            foreach (var enemy in _enemies.ToList())
            {
                var projectilesInZone = CheckHit(enemy, board);
                if (projectilesInZone.Any())
                {
                    board.Destroy(enemy);
                    board.Destroy(projectilesInZone.First()); //It consumes the bullet
                    _enemies.Remove(enemy);
                    killed.Add(enemy);
                    _stats.KillCount++;
                    System.Console.WriteLine("Zombie Killed!");
                }
            }
            return killed;
        }

        private IEnumerable<IProjectile> CheckHit(IActor enemy, Gameboard.GameBoard board)
        {
            return board.GetMaterials((int)Math.Round(enemy.TileX), (int)Math.Round(enemy.TileY)).OfType<IProjectile>();
        }

        private GameStateChanges Spawn(
              GameTime gameTime
            , Gameboard.GameBoard board)
        {
            if (gameTime.TotalGameTime.Milliseconds < _spawnTime || _enemies.Count >= MaxSpawn)
                return GameStateChanges.NoChanges();

            var newEnemies = new List<IActor>();

            if (gameTime.TotalGameTime.TotalSeconds - _lastSpawnTime > _newSpawnTimeS)
            {
                _toSpawn++;
            }

            if (_toSpawn > 0)
            {
                for (int i = 0; i < _toSpawn; i++)
                {
                    newEnemies.Add(new Actors.Zombie()
                    {
                        TileX = SpawnColumn(board),
                        TileY = 0
                    });
                }
                _enemies.AddRange(newEnemies);
                board.Materials.AddRange(newEnemies);
                _lastSpawnTime = gameTime.TotalGameTime.TotalSeconds;
                _toSpawn = 0;
            }

            return new GameStateChanges(newEnemies, new IMaterial[0]);
        }

        private int SpawnColumn(Gameboard.GameBoard board)
        {
            return _rand.Next(0, board.SizeX);
        }        
    }
}
