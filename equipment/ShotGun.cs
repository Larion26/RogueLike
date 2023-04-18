using RogueLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
     class ShotGun : Weapon
    {
        public ShotGun(string nameweapon, int range, int damage) 
        {
            this.NameWeapon = nameweapon;
            this.Range = range;
            this.Damage = damage;
        }
    }
}
