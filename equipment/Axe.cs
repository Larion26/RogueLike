using RogueLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RogueLike
{
     class Axe:Weapon
    {
        public Axe(string nameweapon, int range, int damage)
        {
            this.NameWeapon = nameweapon;
            this.Range = range;
            this.Damage = damage;
        }
    }
}
