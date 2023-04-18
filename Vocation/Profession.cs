using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    public class Profession
    {
        protected string NameProfession = "";
        protected int Strength;
        protected int Medicine;
        protected int Agility;

        public void PrintProfession()
        {
            Console.WriteLine(this.NameProfession);
            Console.WriteLine(this.Strength);
            Console.WriteLine(this.Medicine);
            Console.WriteLine(this.Agility);
        }
    }
}
