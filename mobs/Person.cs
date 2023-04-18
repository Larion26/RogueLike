using Roguelike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueLike
{
    public class Person: Profession
    {
        protected string Name = " ";
        protected int HP;
        protected int Heal;
        protected int Armor;
        public void PrintRase()
        {
            Console.WriteLine(this.Name);
            Console.WriteLine(this.HP);
            Console.WriteLine(this.Heal);
            Console.WriteLine(this.Armor);
            
        }
    }
}
