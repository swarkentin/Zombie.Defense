using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Actors;
using Zombie.Defense.Model.Materials;

namespace Zombie.Defense.Model.ContextActions
{
    public static class ContextActionFactory
    {
        internal static IEnumerable<ContextActionAdapter> Create(Player player, Gameboard.GameBoard gameBoard)
        {
            var content = player.ContextContent;            

            if (content is StoneMaterial)
            {
                if (player.IsHolding(content))
                {
                    return new[] { new DropAction(content, player) };
                }
                else
                {
                    return new ContextActionAdapter[] { 
                        new PickupAction(content, player),
                        new UpgradeToTurretAction((StoneMaterial) content, gameBoard)
                    };
                }
            }
            return new ContextActionAdapter[0];
        }

        
    }
}
