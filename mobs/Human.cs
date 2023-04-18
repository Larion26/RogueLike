using RogueLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class Human : Person
    {
        public Human(string name,int hp, int heal, int armor){
            this.Name = name;
            this.HP = hp;
            this.Heal = heal;
            this.Armor = armor;
            
        }
       
    }
}
