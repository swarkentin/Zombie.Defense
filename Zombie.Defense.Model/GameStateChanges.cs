using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Materials;

namespace Zombie.Defense.Model
{
    public class GameStateChanges
    {
        private readonly IEnumerable<IMaterial> _materialsAdded;
        private readonly IEnumerable<IMaterial> _materialsRemoved;

        internal static GameStateChanges NoChanges()
        {
            return new GameStateChanges(new IMaterial[0], new IMaterial[0]);
        }

        internal GameStateChanges(
             IEnumerable<IMaterial> materialsAdded
            , IEnumerable<IMaterial> materialsRemoved)
        {
            _materialsAdded = materialsAdded;
            _materialsRemoved = materialsRemoved;
        }

        public IEnumerable<IMaterial> MaterialsAdded { get { return _materialsAdded; } }
        public IEnumerable<IMaterial> MaterialsRemoved { get { return _materialsRemoved; } }

        internal GameStateChanges Merge(GameStateChanges gameStateChanges)
        {
            return new GameStateChanges(
                MaterialsAdded.Union(gameStateChanges.MaterialsAdded)
              , MaterialsRemoved.Union(gameStateChanges.MaterialsRemoved)
            );
        }
    }
}
