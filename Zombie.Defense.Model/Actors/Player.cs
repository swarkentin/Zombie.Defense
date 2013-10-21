using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;
using Microsoft.Xna.Framework;
using Zombie.Defense.Model.ContextActions;

namespace Zombie.Defense.Model.Actors
{
    public class Player : MaterialAdapter,  IActor
    {
        private Vector2 _maxSpeed = new Vector2(2.5f, 2.5f);
        private Vector2 _speed = new Vector2(0f, 0f);
        private IEnumerable<IMaterial> _lastNeighbors = new IMaterial[0];
        private IMaterial _contextContent;
        private IMaterial _holding;

        private const float _acceleration = 0.3f;
        private const float _accelerationCarrying = 0.15f;

        private readonly Interaction _interaction;

        public Provider.AssetKey3D AssetKey
        {
            get { return Provider.AssetKey3D.ZOMBIE; }
        }

        private float Acceleration { get { return _holding == null ? _acceleration : _accelerationCarrying; } }

        public Player(Interaction interaction)
        {
            _interaction = interaction;
        }

        public override void Update(GameTime gameTime, Gameboard.GameBoard board)
        {
            var movement = _interaction.Movement;
            var actions = _interaction.AvailableActions;

            HighlightCloseObjects(board);

            TryMove(movement, gameTime, board);
            DoActions(actions, gameTime, board);
        }

        private void TryMove(Movement movement, GameTime gameTime, Gameboard.GameBoard board)
        {
            _speed.X = movement.Direction.X == 0 ? 0f : _speed.X;
            _speed.Y = movement.Direction.Y == 0 ? 0f : _speed.Y;

            var proposed = new Vector2(Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * movement.Direction.X + _speed.X
                                     , Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * movement.Direction.Y + _speed.Y);

            _speed.X = Math.Abs(proposed.X) <= _maxSpeed.X ? proposed.X : _speed.X;
            _speed.Y = Math.Abs(proposed.Y) <= _maxSpeed.Y ? proposed.Y : _speed.Y;

            if (board.ValidatedMove(
                _speed
                , TileX
                , TileY))
            {
                TileX += _speed.X;
                TileY += _speed.Y;

                if (_holding != null)
                {
                    _holding.TileX += _speed.X;
                    _holding.TileY += _speed.Y;
                }
            }
        }

        private void DoActions(Actions actions, GameTime gameTime, Gameboard.GameBoard board)
        {
            if (!InAction)
            {
                InAction = true;
                _interaction.Execute(actions);
                InAction = false;
            }
        }

        private bool InAction { get; set; }

        private void HighlightCloseObjects(Gameboard.GameBoard board)
        {
            //Unhighlight last highlighted
            ClearNeighbors();

            //Highlight new neighbors
            FindNeighbors(board);
        }

        private void ClearNeighbors()
        {
            foreach (var neighbor in _lastNeighbors)
            {
                neighbor.Highlight = false;
            }
            _contextContent = null;
        }


        private void FindNeighbors(Gameboard.GameBoard board)
        {
            var neighbors = board.Neighbors(this).OrderBy(n => n.DistanceFrom(this));

            foreach (var neighbor in neighbors)
            {
                if (CanInteract(neighbor))
                {
                    neighbor.Highlight = true;
                    _contextContent = neighbor;
                    break;
                }
            }

            //Remember the new neighbors for next update.
            _lastNeighbors = neighbors.ToList();
        }

        public IMaterial ContextContent
        {
            get
            {
                return _contextContent;
            }
        }

        private bool CanInteract(IMaterial neighbor)
        {
            return neighbor is StoneMaterial;
        }



        #region Moving Stuff Around

        internal bool IsHolding(IMaterial content)
        {
            return _holding == content;
        }

        internal void Drop(IMaterial content)
        {
            content.TileX = (int) Math.Round(content.TileX);
            content.TileY = (int)Math.Round(content.TileY);
            _holding = null;
        }

        internal void Pickup(IMaterial content)
        {
            _holding = content;
        }

        #endregion
    }
}
