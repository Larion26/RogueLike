using Roguelike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Rogue
{
    abstract class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public Character(string name, int health, int damage)
        {
            this.Name = name;
            this.Health = health;
            this.Damage = damage;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }

        public virtual void Attack(Character target)
        {
            int damageDealt = Damage;
            Console.WriteLine($"{Name} attacks {target.Name} for {damageDealt} damage.");
            target.TakeDamage(damageDealt);
        }

        public void TakeDamage(int damageTaken)
        {
            Health -= damageTaken;
            Console.WriteLine($"{Name} takes {damageTaken} damage. {Name}'s health is now {Health}.");
        }
    }

    public class Item
    {
        public string Name { get; set; }
    }

    public class Weapon : Item
    {
        public int Damage { get; set; }
    }


    class Player : Character
    {
        public Player(string name, int health, int damage, int mana, char Char) : base(name, health, damage)
        {
            Inventory = new List<Item>();
            this.Mana = mana;
            this.Name = name;
            this.Damage = +damage;
            this.Char = Char;
        }

        public override void Attack(Character target)
        {
            int damageDealt = Damage * 2;
            Console.WriteLine($"{Name} uses a special attack on {target.Name} for {damageDealt} damage.");
            target.TakeDamage(damageDealt);
        }
        public int X;
        public int Y;
        public char Char;
        public List<Item> Inventory { get; set; }



        public int Gold { get; set; }
        public int Mana { get; set; }
        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
        }

        public void RemoveFromInventory(Item item)
        {
            Inventory.Remove(item);
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Inventory:");
            foreach (Item item in Inventory)
            {
                if (item is Weapon)
                {
                    Weapon weapon = (Weapon)item;
                    Console.WriteLine("{0} - Damage: {1}", weapon.Name, weapon.Damage);
                }
                else
                {
                    Console.WriteLine(item.Name);
                }
            }
            Console.WriteLine("Gold: {0}", Gold);
            Console.WriteLine("Mana: {0}", Mana);
        }



        public int MoveUp()
        {
            Y--;
            return Y;
        }

        public int MoveDown()
        {
            Y++;
            return Y;
        }

        public int MoveLeft()
        {
            X--;
            return X;
        }

        public int MoveRight()
        {
            X++;
            return X;
        }
    }

    class Enemy : Character
    {
        public string name;
        public int damage;
        public int X { get; set; }
        public int Y { get; set; }
        private Random rng = new Random();
        public Enemy(string name, int health, int damage) : base(name, health, damage) { }

        //public Enemy(string name, int x, int y, int damage)
        //{
        //    this.name = name;
        //    this.X = x;
        //    this.Y = y;
        //    this.damage = damage;
        //}

        public void Move(int width,int height, bool[,] map)
        {
            int dx = rng.Next(-1, 2);
            int dy = rng.Next(-1, 2);

            int newX = X + dx;
            int newY = Y + dy;
            if (newX >= 0 && newX <width && newY >= 0 && newY < height && map[newX, newY])
            {
                X = newX;
                Y = newY;
            }
        }
    }
}
