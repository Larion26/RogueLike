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
        public int X;
        public int Y;
        public int Price;
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
        public void PrintCharacterInfo()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int windowWidth = 20;
            int windowHeight = 10;
            // Вычисление координат окошка
            int inputWindowX = windowWidth + 60;
            int inputWindowY = 10;

            // Верхняя граница окошка
            Console.SetCursorPosition(inputWindowX, inputWindowY);
            Console.Write("╔" + new string('═', windowWidth - 2) + "╗");

            // Боковые границы окошка
            for (int y = inputWindowY + 1; y < inputWindowY + windowHeight - 1; y++)
            {
                Console.SetCursorPosition(inputWindowX, y);
                Console.Write("║");
                Console.SetCursorPosition(inputWindowX + windowWidth - 1, y);
                Console.Write("║");
            }

            // Нижняя граница окошка
            Console.SetCursorPosition(inputWindowX, inputWindowY + windowHeight - 1);
            Console.Write("╚" + new string('═', windowWidth - 2) + "╝");

            // Заголовок окошка
            Console.SetCursorPosition(inputWindowX + 1, inputWindowY);
            Console.Write("Character Info [I]");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 2);
            Console.Write($"Name: {Name}");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 3);
            Console.Write($"Health: {Health}");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 4);
            Console.Write($"Mana: {Mana}");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 5);
            Console.Write($"Money: {Money}");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 6);
            Console.Write($"Reputation: {Reputation}");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 7);
            Console.Write($"Position:({X}, {Y})");


        }
        public int Money = 300;
        public int Reputation;
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
        public int Sum_Damage()
        {
            int sum_damage = 0;
            foreach (Item item in Inventory)
            {
                sum_damage += item.Damage;
            }
            return sum_damage;
        }

        public void RemoveFromInventory(Item item)
        {
            Inventory.Remove(item);
        }

        public void DisplayInventory(int CountLines)
        {
            int windowWidth = 27 + CountLines;
            int windowHeight = 8;
            // Вычисление координат окошка
            int inputWindowX = windowWidth + 20;
            int inputWindowY = 10;

            // Верхняя граница окошка
            Console.SetCursorPosition(inputWindowX, inputWindowY);
            Console.Write("╔" + new string('═', windowWidth - 2) + "╗");

            // Боковые границы окошка
            for (int y = inputWindowY + 1; y < inputWindowY + windowHeight - 1; y++)
            {
                Console.SetCursorPosition(inputWindowX, y);
                Console.Write("║");
                Console.SetCursorPosition(inputWindowX + windowWidth - 1, y);
                Console.Write("║");
            }

            // Нижняя граница окошка
            Console.SetCursorPosition(inputWindowX, inputWindowY + windowHeight - 1);
            Console.Write("╚" + new string('═', windowWidth - 2) + "╝");

            int Count = 2;
            Console.SetCursorPosition(inputWindowX + 9, inputWindowY);
            Console.WriteLine("Inventory:");
            Console.SetCursorPosition(inputWindowX + 2, inputWindowY + 2);
            foreach (Item item in Inventory)
            {
                Console.SetCursorPosition(inputWindowX + 2, inputWindowY + Count);
                Console.WriteLine("{0} - Damage: {1}", item.Name, item.Damage);
                Count++;

            }
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
        public int SightRange;
        private Random rng = new Random();
        public string name;
        public int damage;
        public int health;
        public int X { get; set; }
        public int Y { get; set; }
        public Enemy(string name, int health, int damage, int sightrange) : base(name, health, damage)
        {
            this.name = name;
            this.damage = damage;
            this.health = health;
            this.SightRange = sightrange;
        }

        public Enemy(int x, int y, string name) : base("", 0, 0)
        {
            this.X = x;
            this.Y = y;
            this.name = name;
        }

        public void Move(int width, int height, char[,] map)
        {
            int dx = rng.Next(-1, 2);
            int dy = rng.Next(-1, 2);

            int newX = X + dx;
            int newY = Y + dy;
            if (newX >= 0 && newX < width && newY >= 0 && newY < height && map[newX, newY] == ' ')
            {
                X = newX;
                Y = newY;
            }
        }
    }

    public class Draw_Battle
    {

        public Draw_Battle()
        {

            Console.OutputEncoding = System.Text.Encoding.Unicode;

            int consoleWidth = Console.WindowWidth;
            int mid = consoleWidth / 2;

            Console.WindowHeight = 50;


            int leftX = consoleWidth / 3, rightX = consoleWidth - consoleWidth / 3;
            int leftY = consoleWidth / 3, rightY = consoleWidth - consoleWidth / 3;
            int leftZ = consoleWidth / 3, rightZ = consoleWidth - consoleWidth / 3;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 10);
            Console.Write(new string('_', Console.WindowWidth));
            Console.SetCursorPosition(0, 25);
            Console.Write(new string('_', Console.WindowWidth));

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.SetCursorPosition(10, 13);
            Console.Write("|___|");
            Console.SetCursorPosition(10, 12);
            Console.Write(" ___|");
            Console.SetCursorPosition(15, 11);
            Console.Write("___");
            Console.SetCursorPosition(15, 12);
            Console.Write("___|");

            Console.SetCursorPosition(45, 13);
            Console.Write("|___|");
            Console.SetCursorPosition(45, 12);
            Console.Write(" ___|");
            Console.SetCursorPosition(50, 11);
            Console.Write("___");
            Console.SetCursorPosition(50, 12);
            Console.Write("___|");
            Console.SetCursorPosition(40, 12);
            Console.Write(" |___|");
            Console.SetCursorPosition(40, 11);
            Console.Write("  ___");

            Console.SetCursorPosition(80, 13);
            Console.Write("|___|");
            Console.SetCursorPosition(80, 12);
            Console.Write(" ___|");
            Console.SetCursorPosition(85, 11);
            Console.Write("___");
            Console.SetCursorPosition(85, 12);
            Console.Write("___|");
            Console.SetCursorPosition(75, 12);
            Console.Write(" |___|");
            Console.SetCursorPosition(75, 11);
            Console.Write("  ___");

            Console.SetCursorPosition(60, 13);
            Console.Write("|___|");
            Console.SetCursorPosition(60, 12);
            Console.Write(" ___|");
            Console.SetCursorPosition(65, 11);
            Console.Write("___");
            Console.SetCursorPosition(65, 12);
            Console.Write("___|");

            Console.SetCursorPosition(25, 13);
            Console.Write("|___|");
            Console.SetCursorPosition(25, 12);
            Console.Write(" ___|");
            Console.SetCursorPosition(30, 11);
            Console.Write("___");
            Console.SetCursorPosition(30, 12);
            Console.Write("___|");

            Console.SetCursorPosition(110, 13);
            Console.Write("|___|");
            Console.SetCursorPosition(110, 12);
            Console.Write(" ___|");
            Console.SetCursorPosition(115, 11);
            Console.Write("___");
            Console.SetCursorPosition(115, 12);
            Console.Write("___|");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(rightX + 20, 11);
            Console.Write("|||||");
            Console.SetCursorPosition(rightX + 21, 12);
            Console.Write("|||");
            Console.SetCursorPosition(rightX + 22, 13);
            Console.Write("|");


            Console.SetCursorPosition(2, 11);
            Console.Write("|||||");
            Console.SetCursorPosition(3, 12);
            Console.Write("|||");
            Console.SetCursorPosition(4, 13);
            Console.Write("|");

            Console.SetCursorPosition(rightX + 15, 11);
            Console.Write("||");
            Console.SetCursorPosition(rightX + 15, 12);
            Console.Write(" |");

            Console.SetCursorPosition(rightX - 30, 11);
            Console.Write("|||||");
            Console.SetCursorPosition(rightX - 30, 12);
            Console.Write("|||");
            Console.SetCursorPosition(rightX - 30, 13);
            Console.Write("|");

            Console.SetCursorPosition(rightX - 40, 11);
            Console.Write("||");
            Console.SetCursorPosition(rightX - 40, 12);
            Console.Write(" |");






            while (leftX + 10 < mid && rightX > mid)
            {
                for (int i = 14; i <= 21; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 18);
                Console.Write(new string('_', Console.WindowWidth));

                Console.SetCursorPosition(0, 19);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 20);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 21);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 22);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 23);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 24);
                Console.Write(new string('|', Console.WindowWidth));


                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(leftX, 15);
                Console.Write("  _\\|/^\n");
                Console.SetCursorPosition(leftX, 16);
                if (leftX % 2 == 0)
                {
                    Console.Write("  (_oo/");
                }
                else
                {
                    Console.Write("  (_oo");
                }
                //Console.Write("/|)\n");
                Console.SetCursorPosition(leftX, 17);
                if (leftX % 2 == 0)
                {
                    Console.Write("/-|--/");
                }
                else
                {
                    Console.Write("==-|--|");
                }
                Console.SetCursorPosition(leftX, 18);
                if (leftX % 2 == 0)
                {
                    Console.Write(" \\ |  ");
                }
                else
                {
                    Console.Write("|  |  \\ ");
                }

                Console.SetCursorPosition(leftX, 19);
                if (leftX % 2 == 0)
                {
                    Console.Write("   /--i ");
                }
                else
                {
                    Console.Write("   \\  ");
                }
                Console.SetCursorPosition(leftX, 20);
                if (leftX % 2 == 0)
                {
                    Console.Write("  /   L  ");
                }
                else
                {
                    Console.Write("   \\  ");
                }
                //Console.Write("/|)\n");
                Console.SetCursorPosition(leftX, 21);
                if (leftX % 2 == 0)
                {
                    Console.Write("  L    ");
                }
                else
                {
                    Console.Write("   //_  ");
                }
                //Console.Write("/)\n");

                Console.SetCursorPosition(rightX, 14);
                Console.Write("  /_\\");
                Console.SetCursorPosition(rightX, 15);
                Console.Write(" (0_т)");
                Console.SetCursorPosition(rightX, 16);
                Console.Write("/--{}");
                Console.SetCursorPosition(rightX, 17);
                Console.Write("( ( {}  ");
                Console.SetCursorPosition(rightX, 18);
                Console.Write("( (  {}   ");
                Console.SetCursorPosition(rightX, 19);
                if (rightX % 3 == 0)
                {
                    Console.Write("     /-\\  ");
                }
                else
                {
                    Console.Write("     |-|  ");
                }
                //Console.Write("     /-\\  ");
                Console.SetCursorPosition(rightX, 20);
                if (rightX % 3 == 0)
                {
                    Console.Write("    /   \\  ");
                }
                else
                {
                    Console.Write("     | |  ");
                }
                Console.SetCursorPosition(rightX, 21);
                if (rightX % 3 == 0)
                {
                    Console.Write("   _)   \\  ");
                }
                else
                {
                    Console.Write("    _) |_ ");
                }




                leftX++;
                rightX = rightX - 2;
                System.Threading.Thread.Sleep(150);
            }

            for (int i = 14; i <= 21; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 18);
                Console.Write(new string('_', Console.WindowWidth));
                Console.SetCursorPosition(0, 19);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 20);
                Console.Write(new string('|', Console.WindowWidth));
                Console.SetCursorPosition(0, 21);
                Console.Write(new string('|', Console.WindowWidth));
            }


            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(leftX, 15);
            Console.Write("  _\\|/^\n");
            Console.SetCursorPosition(leftX, 16);
            Console.Write("  (_oo ");
            Console.SetCursorPosition(leftX, 17);
            Console.Write("  |-|--| ");
            Console.SetCursorPosition(leftX, 18);
            Console.Write("  \\ |  \\  ");
            Console.SetCursorPosition(leftX, 19);
            Console.Write("   | |    ");
            Console.SetCursorPosition(leftX, 20);
            Console.Write("   | |    ");
            Console.SetCursorPosition(leftX, 21);
            Console.Write("   |_|_   ");

            Console.SetCursorPosition(rightX, 14);
            Console.Write("      /_\\");
            Console.SetCursorPosition(rightX, 15);
            Console.Write("     (0_т)");
            Console.SetCursorPosition(rightX, 16);
            Console.Write("    /-{}-\\");
            Console.SetCursorPosition(rightX, 17);
            Console.Write("   (  {}  )");
            Console.SetCursorPosition(rightX, 18);
            Console.Write("   J  {}  J ");
            Console.SetCursorPosition(rightX, 19);
            Console.Write("     /-\\    ");
            Console.SetCursorPosition(rightX, 20);
            Console.Write("     | |    ");
            Console.SetCursorPosition(rightX, 21);
            Console.Write("    _) (_   ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(rightX - 12, 2);
            Console.Write("###### #  ####  #    # #####\n");
            Thread.Sleep(200);
            Console.SetCursorPosition(rightX - 12, 3);
            Console.Write("#      # #    # #    #   #       \n");
            Thread.Sleep(200);
            Console.SetCursorPosition(rightX - 12, 4);
            Console.Write("#####  # #      ######   #       \n");
            Thread.Sleep(200);
            Console.SetCursorPosition(rightX - 12, 5);
            Console.Write("#      # #  ### #    #   #      \n");
            Thread.Sleep(200);
            Console.SetCursorPosition(rightX - 12, 6);
            Console.Write("#      # #    # #    #   #       \n");
            Thread.Sleep(200);
            Console.SetCursorPosition(rightX - 12, 7);
            Console.Write("#      #  ####  #    #   #       \n");
            Thread.Sleep(200);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(rightX - 30, 27);
            Console.Write("HERO");




            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(rightX + 25, 27);
            Console.Write("ENEMY");
            Console.SetCursorPosition(rightX + 25, 29);
            Console.SetCursorPosition(rightX, 32);
            Console.Write("ENTER 'ENTER' TO CONTINUE");

            Console.ReadLine();



        }

    }


}



