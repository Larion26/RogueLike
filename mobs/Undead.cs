using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
     class Undead:Person
    {
        public Undead(string name, int hp, int heal, int armor)
        {
            this.Name = name;
            this.HP = hp;
            this.Heal = heal;
            this.Armor = armor;
            
        }
    }
}
