using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zombie.Defense.Model.Weapons;
using Zombie.Defense.Model.Projectiles;

namespace Zombie.Defense.Model.Materials
{
    public class MaterialFactory
    {
        public static IMaterial3d Stone
        {
            get
            {
                return new StoneMaterial();
            }            
        }

        public static IMaterial3d Turret
        {
            get
            {
                return new Turret();
            }
        }

        public static IProjectile Bullet
        {
            get
            {
                return new Bullet();
            }
        }
    }
}
