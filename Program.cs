using Rogue;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Linq;


namespace Roguelike
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.SetCursorPosition(50, 5);
            //Console.Write("ENTER YOUR USERNAME: ");
            //string username = Console.ReadLine();
            //Console.WriteLine();

            //Console.SetCursorPosition(54, 6);
            //Console.WriteLine("Have a good game!");

            //Thread.Sleep(2000);
            //Console.Clear();
            //Console.OutputEncoding = System.Text.Encoding.Unicode;

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.Write("                                                                        _____                                    \n");
            //Thread.Sleep(200);
            //Console.Write("                                                                   ,___'     `--.__                              \n");
            //Thread.Sleep(200);
            //Console.Write("                                                               ,-'                ; `.                           \n");
            //Thread.Sleep(200);
            //Console.Write("       _    _      _ _                                        ,'                  `--.`--.                       \n");
            //Thread.Sleep(200);
            //Console.Write("      | |  | |    | | |                                      ,'                       `._ `-.                    \n");
            //Thread.Sleep(200);
            //Console.Write("      | |  | | ___| | | ___ ___  _ __ ___   ___              |                     ;     `__ ;                   \n");
            //Thread.Sleep(200);
            //Console.Write("      | |/\\| |/ _ \\ | |/ __/ _ \\| '_ ` _ \\ / _ \\           ,-'-_       _,-~~-.      ,--      `.                  \n");
            //Thread.Sleep(200);
            //Console.Write("      \\  /\\  /  __/ | | (_| (_) | | | | | |  __/            ||   `-,;    ,'~`.__    ||||    | |                  \n");
            //Thread.Sleep(200);
            //Console.Write("      |\\/  \\/ \\___|_|_|\\___\\___/|_| |_| |_|\\___|           ||    |,'  ,||      `,  |;;     ` . |                 \n");
            //Thread.Sleep(200);
            //Console.Write("      | |    | |                                           `:   ,'    `||     __/  `.;       | |                 \n");
            //Thread.Sleep(200);
            //Console.Write("      | |    | |                                            ;---.   `.   `---'~~    ||      | |                  \n");
            //Thread.Sleep(200);
            //Console.Write("      |_|____| |                                            `,' `.   `.            |||     |'                    \n");
            //Thread.Sleep(200);
            //Console.Write("      |_   _|| |                                            ,',^\\ `\\  `._    __    `||     |'                    \n");
            //Thread.Sleep(200);
            //Console.Write("        | | _| |                                             `-' `--'    ~`--'~~`--.  ~    |'                    \n");
            //Thread.Sleep(200);
            //Console.Write("        | |/ _ \\                                           /|`-|_ | |. /. /   ; ~~`-.     |                      \n");
            //Thread.Sleep(200);
            //Console.Write("        | | (_) |                                         ; |  | `,|`-|__|---;      `----'                       \n");
            //Thread.Sleep(200);
            //Console.Write("        \\_/\\___/                                          ``-`-;__|:  |  |__;                                    \n");
            //Thread.Sleep(200);
            //Console.Write("            ||                                                     ` `-'                                         \n");
            //Thread.Sleep(200);
            //Console.Write("            ||                                                                                                   \n");
            //Thread.Sleep(200);
            //Console.Write("       _____||             _    _            _     _ _        _____          _                                   \n");
            //Thread.Sleep(200);
            //Console.Write("      |_   _| |           | |  | |          | |   | ( )      |  ___|        | |                                  \n");
            //Thread.Sleep(200);
            //Console.Write("        | | | |__   ___   | |  | | ___  _ __| | __| |/ ___   | |__ _ __   __| |                                  \n");
            //Thread.Sleep(200);
            //Console.Write("        | | | '_ \\ / _ \\  | |/\\| |/ _ \\| '__| |/ _` | / __|  |  __| '_ \\ / _` |                                  \n");
            //Thread.Sleep(200);
            //Console.Write("        | | | | | |  __/  \\  /\\  / (_) | |  | | (_| | \\__ \\  | |__| | | | (_| |                                  \n");
            //Thread.Sleep(200);
            //Console.Write("        \\_/ |_| |_|\\___|   \\/  \\/ \\___/|_|  |_|\\__,_| |___/  \\____/_| |_|\\__,_|                                  \n");


            //Console.SetCursorPosition(50, 28);
            //Console.WriteLine("ENTER ANY KEY TO CONTINUE");
            //Console.ReadKey();

            //Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            int width = 70;
            int height = 40;
            MapGenerator generator = new MapGenerator(width, height);
            char[,] map = generator.Generate();
            int visible = generator.visible_map();
            List<Trader> traders = new List<Trader>();


            Player Player = new Player("Chetkypots", 100, 0, 50, '@');
            string json = JsonConvert.SerializeObject(Player);
            File.WriteAllText("Player.json", json);

            List<Enemy> enemies = new List<Enemy>()
            {
                new Enemy("Goblin", 150, 60,2),
                new Enemy("Orc", 100, 100,3),
                new Enemy("Troll", 100, 120,4)
            };
            string json1 = JsonConvert.SerializeObject(enemies);
            File.WriteAllText("Game.json", json);
            Item sword = new Item() { Name = "Sword", Damage = 50, Price = 200 };
            Item axe = new Item() { Name = "Axe", Damage = 70, Price = 150 };
            Item potion = new Item() { Name = "Mana Potion", Damage = 40, Price = 50 };
            List<Item> items = new List<Item>() { sword, axe, potion };
            int CountLines = 0;

            Player.Gold = 100;
            Player.Mana = 50;

            Random random = new Random();

            // Найти случайную точку на карте, которая является комнатой (символ " ")
            int rnd_x, rnd_y;
            do
            {
                rnd_x = random.Next(width);
                rnd_y = random.Next(height);
            } while (map[rnd_x, rnd_y] != ' ');

            // Установить символ игрока на найденную позицию
            Player.X = rnd_x;
            Player.Y = rnd_y;
            map[rnd_x, rnd_y] = '@';

            // Вывод карты на экран 
            generator.Draw_Map(Player.X, Player.Y);

            //Определение случайных координат для Enemies

            int rnd_x_Enemy, rnd_y_Enemy;
            foreach (Enemy enemy in enemies)
            {
                do
                {
                    // Установление координат для Enemies
                    rnd_x_Enemy = random.Next(width);
                    rnd_y_Enemy = random.Next(height);
                } while (map[rnd_x_Enemy, rnd_y_Enemy] != ' ');

                enemy.X = rnd_x_Enemy;
                enemy.Y = rnd_y_Enemy;
            }
            //Область видимости для врагов
            double distance;
            int distanceToPlayer;



            // Вывод информации о персонаже
            Player.PrintCharacterInfo();


            //Перемещение игрока по карте 
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (Player.Health <= 0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(20, 10);
                    Console.WriteLine("YOU LOSE!");
                    break;
                }
                //Отрисовка карты в области видимости игрока
                generator.Map_Visible(Player.X, Player.Y);
                Console.SetCursorPosition(Player.X, Player.Y);
                Console.Write("@");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                //Открыть инвентарь
                if (keyInfo.Key == ConsoleKey.I)
                {
                    while (true)
                    {
                        Console.Clear();
                        Player.DisplayInventory(CountLines);
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        Console.Clear();
                        if (key.Key == ConsoleKey.I)
                        {
                            break;
                        }

                    }
                }

                else if (keyInfo.Key == ConsoleKey.UpArrow && (map[Player.X, Player.Y - 1] == ' ' || map[Player.X, Player.Y - 1] == '$' || map[Player.X, Player.Y - 1] == 'G' || map[Player.X, Player.Y - 1] == 'T' || map[Player.X, Player.Y - 1] == 'O' || map[Player.X, Player.Y - 1] == 'U' || map[Player.X, Player.Y - 1] == 'S' || map[Player.X, Player.Y - 1] == 'B'))
                {
                    if (map[Player.X, Player.Y - 1] == 'B')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.Y--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Console.Write("@");
                        int luck = random.Next(1, 4);
                        if (luck == 1)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET AN AXE!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 36; i++)
                                Console.Write(" ");

                        }
                        if (luck == 2)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A MANA POTION!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 43; i++)
                                Console.Write(" ");
                        }
                        if (luck == 3)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A SWORD!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 37; i++)
                                Console.Write(" ");

                        }
                    }


                    else if (map[Player.X, Player.Y - 1] == 'S')
                    {

                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        Player.Y--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@");
                        Player.Reputation += 5;
                        traders = generator.list_traders();
                        foreach (Trader trader in traders)
                        {
                            trader.Menu_Trade(CountLines, items, Player);
                        }
                    }
                    else if (map[Player.X, Player.Y - 1] == 'U')
                    {
                        Console.Clear();
                        Console.SetCursorPosition(20, 10);
                        Console.WriteLine("YOU WIN!");
                        break;
                    }

                    else if (map[Player.X, Player.Y - 1] == '$')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.Y--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Player.Health += 20;
                        Console.Write("@");
                    }

                    else
                    {

                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        Player.Y--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@");

                        // Перемещение всех врагов
                        foreach (Enemy enemy in enemies)
                        {
                            map[enemy.X, enemy.Y] = ' ';
                            enemy.Move(width, height, map);
                            map[enemy.X, enemy.Y] = enemy.name[0];
                        }

                        // Отображение только врагов, находящихся в радиусе видимости игрока
                        foreach (Enemy enemy in enemies)
                        {

                            distance = Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));
                            if (distance <= 5)
                            {
                                // Вычисление расстояния до игрока
                                distanceToPlayer = (int)Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));

                                // Если игрок в поле зрения врага, то враг начинает преследовать игрока
                                if (distanceToPlayer <= enemy.SightRange)
                                {
                                    // Стираем старый символ врага
                                    Console.SetCursorPosition(enemy.X, enemy.Y);
                                    Console.Write(" ");
                                    map[enemy.X, enemy.Y] = ' ';
                                    // Вычисление направления движения врага
                                    int dx = Math.Sign(Player.X - enemy.X);
                                    int dy = Math.Sign(Player.Y - enemy.Y);

                                    // Проверка возможности перемещения в заданном направлении
                                    if (map[enemy.X + dx, enemy.Y] == ' ')
                                    {
                                        enemy.X += dx;
                                    }
                                    else if (map[enemy.X, enemy.Y + dy] == ' ')
                                    {
                                        enemy.Y += dy;
                                    }
                                    map[enemy.X, enemy.Y] = enemy.name[0];
                                }


                                Console.SetCursorPosition(enemy.X, enemy.Y);
                                if (enemy.name == "Goblin")
                                    Console.Write("G");
                                else if (enemy.name == "Orc")
                                    Console.Write("O");
                                else if (enemy.name == "Troll")
                                    Console.Write("T");

                                if (distanceToPlayer == 0)
                                {
                                    Console.Clear();
                                    Draw_Battle draw_battle1 = new Draw_Battle();
                                    Thread.Sleep(100);
                                    Console.Clear();
                                    enemies.Remove(enemy);
                                    map[enemy.X, enemy.Y] = ' ';
                                    Player.Health -= enemy.damage;

                                    enemy.health -= Player.Sum_Damage();
                                    break;
                                }
                            }
                        }


                    }
                }



                else if (keyInfo.Key == ConsoleKey.DownArrow && (map[Player.X, Player.Y + 1] == ' ' || map[Player.X, Player.Y + 1] == 'O' || map[Player.X, Player.Y + 1] == 'G' || map[Player.X, Player.Y + 1] == 'T' || map[Player.X, Player.Y + 1] == '$' || map[Player.X, Player.Y + 1] == 'U' || map[Player.X, Player.Y + 1] == 'S' || map[Player.X, Player.Y + 1] == 'B'))
                {
                    if (map[Player.X, Player.Y + 1] == 'B')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.Y++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Console.Write("@");
                        int luck = random.Next(1, 4);
                        if (luck == 1)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET AN AXE!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 36; i++)
                                Console.Write(" ");

                        }
                        if (luck == 2)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A MANA POTION!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 43; i++)
                                Console.Write(" ");
                        }
                        if (luck == 3)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A SWORD!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 37; i++)
                                Console.Write(" ");

                        }


                    }
                    else if (map[Player.X, Player.Y + 1] == 'S')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        Player.Y++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@");
                        Player.Reputation += 5;
                        foreach (Trader trader in traders)
                        {
                            trader.Menu_Trade(CountLines, items, Player);
                        }
                    }
                    else if (map[Player.X, Player.Y + 1] == 'U')
                    {
                        Console.Clear();
                        Console.SetCursorPosition(20, 10);
                        Console.WriteLine("YOU WIN!");
                        break;
                    }
                    else if (map[Player.X, Player.Y + 1] == '$')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.Y++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Player.Health += 20;
                        Console.Write("@");
                    }

                    else
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" "); // Восстановить символ комнаты на старой позиции игрока 
                        Player.Y++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@"); // Установить символ игрока на новой позиции 

                        foreach (Enemy enemy in enemies)
                        {
                            map[enemy.X, enemy.Y] = ' ';
                            enemy.Move(width, height, map);
                            map[enemy.X, enemy.Y] = enemy.name[0];
                        }

                        // Отображение только врагов, находящихся в радиусе видимости игрока
                        foreach (Enemy enemy in enemies)
                        {

                            distance = Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));
                            if (distance <= 5)
                            {
                                // Вычисление расстояния до игрока
                                distanceToPlayer = (int)Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));

                                // Если игрок в поле зрения врага, то враг начинает преследовать игрока
                                if (distanceToPlayer <= enemy.SightRange)
                                {
                                    // Стираем старый символ врага
                                    Console.SetCursorPosition(enemy.X, enemy.Y);
                                    Console.Write(" ");
                                    map[enemy.X, enemy.Y] = ' ';

                                    // Вычисление направления движения врага
                                    int dx = Math.Sign(Player.X - enemy.X);
                                    int dy = Math.Sign(Player.Y - enemy.Y);

                                    // Проверка возможности перемещения в заданном направлении
                                    if (map[enemy.X + dx, enemy.Y] == ' ')
                                    {
                                        enemy.X += dx;
                                    }
                                    else if (map[enemy.X, enemy.Y + dy] == ' ')
                                    {
                                        enemy.Y += dy;
                                    }
                                    map[enemy.X, enemy.Y] = enemy.name[0];
                                }

                                Console.SetCursorPosition(enemy.X, enemy.Y);
                                if (enemy.name == "Goblin")
                                    Console.Write("G");
                                else if (enemy.name == "Orc")
                                    Console.Write("O");
                                else if (enemy.name == "Troll")
                                    Console.Write("T");
                                if (distanceToPlayer == 0)
                                {
                                    Console.Clear();
                                    Draw_Battle draw_battle2 = new Draw_Battle();
                                    Thread.Sleep(100);
                                    enemies.Remove(enemy);
                                    map[enemy.X, enemy.Y] = ' ';
                                    Player.Health -= enemy.damage;

                                    enemy.health -= Player.Sum_Damage();
                                    Console.Clear();
                                    break;
                                }
                            }

                        }
                        Thread.Sleep(10);
                        Player.PrintCharacterInfo();


                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow && (map[Player.X - 1, Player.Y] == ' ' || map[Player.X - 1, Player.Y] == 'G' || map[Player.X - 1, Player.Y] == 'O' || map[Player.X - 1, Player.Y] == '$' || map[Player.X - 1, Player.Y] == 'T' || map[Player.X - 1, Player.Y] == 'U' || map[Player.X - 1, Player.Y] == 'S' || map[Player.X - 1, Player.Y] == 'B'))
                {
                    if (map[Player.X - 1, Player.Y] == 'B')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.X--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Console.Write("@");
                        int luck = random.Next(1, 4);
                        if (luck == 1)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET AN AXE!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 36; i++)
                                Console.Write(" ");

                        }
                        if (luck == 2)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A MANA POTION!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 43; i++)
                                Console.Write(" ");
                        }
                        if (luck == 3)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A SWORD!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 37; i++)
                                Console.Write(" ");

                        }


                    }
                    else if (map[Player.X - 1, Player.Y] == 'S')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        Player.X--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@");
                        Player.Reputation += 5;
                        foreach (Trader trader in traders)
                        {
                            trader.Menu_Trade(CountLines, items, Player);
                        }
                    }
                    else if (map[Player.X - 1, Player.Y] == '$')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.X--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Player.Health += 20;
                        Console.Write("@");
                    }
                    else if (map[Player.X - 1, Player.Y] == 'U')
                    {
                        Console.Clear();
                        Console.SetCursorPosition(20, 10);
                        Console.WriteLine("YOU WIN!");
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" "); // Восстановить символ комнаты на старой позиции игрока 
                        Player.X--;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@"); // Установить символ игрока на новой позиции 

                        foreach (Enemy enemy in enemies)
                        {
                            map[enemy.X, enemy.Y] = ' ';
                            enemy.Move(width, height, map);
                            map[enemy.X, enemy.Y] = enemy.name[0];
                        }

                        // Отображение только врагов, находящихся в радиусе видимости игрока
                        foreach (Enemy enemy in enemies)
                        {

                            distance = Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));
                            if (distance <= 5)
                            {
                                // Вычисление расстояния до игрока
                                distanceToPlayer = (int)Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));

                                // Если игрок в поле зрения врага, то враг начинает преследовать игрока
                                if (distanceToPlayer <= enemy.SightRange)
                                {
                                    // Стираем старый символ врага
                                    Console.SetCursorPosition(enemy.X, enemy.Y);
                                    Console.Write(" ");
                                    map[enemy.X, enemy.Y] = ' ';

                                    // Вычисление направления движения врага
                                    int dx = Math.Sign(Player.X - enemy.X);
                                    int dy = Math.Sign(Player.Y - enemy.Y);

                                    // Проверка возможности перемещения в заданном направлении
                                    if (map[enemy.X + dx, enemy.Y] == ' ')
                                    {
                                        enemy.X += dx;
                                    }
                                    else if (map[enemy.X, enemy.Y + dy] == ' ')
                                    {
                                        enemy.Y += dy;
                                    }
                                    map[enemy.X, enemy.Y] = enemy.name[0];
                                }

                                Console.SetCursorPosition(enemy.X, enemy.Y);
                                if (enemy.name == "Goblin")
                                    Console.Write("G");
                                else if (enemy.name == "Orc")
                                    Console.Write("O");
                                else if (enemy.name == "Troll")
                                    Console.Write("T");
                                if (distanceToPlayer == 0)
                                {
                                    Console.Clear();
                                    Draw_Battle draw_battle3 = new Draw_Battle();
                                    Thread.Sleep(100);
                                    enemies.Remove(enemy);
                                    map[enemy.X, enemy.Y] = ' ';
                                    Player.Health -= enemy.damage;
                                    enemy.health -= Player.Sum_Damage();
                                    Console.Clear();
                                    break;
                                }
                            }

                        }

                        Thread.Sleep(10);
                        Player.PrintCharacterInfo();

                    }
                }

                else if (keyInfo.Key == ConsoleKey.RightArrow && (map[Player.X + 1, Player.Y] == ' ' || map[Player.X + 1, Player.Y] == '$' || map[Player.X + 1, Player.Y] == 'G' || map[Player.X + 1, Player.Y] == 'T' || map[Player.X + 1, Player.Y] == 'O' || map[Player.X + 1, Player.Y] == 'U' || map[Player.X + 1, Player.Y] == 'S' || map[Player.X + 1, Player.Y] == 'B'))
                {
                    if (map[Player.X + 1, Player.Y] == 'B')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.X++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Console.Write("@");
                        int luck = random.Next(1, 4);
                        if (luck == 1)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET AN AXE!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 36; i++)
                                Console.Write(" ");

                        }
                        if (luck == 2)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A MANA POTION!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 43; i++)
                                Console.Write(" ");
                        }
                        if (luck == 3)
                        {
                            Console.SetCursorPosition(80, 30);
                            Console.WriteLine("OUR CONGRATULATIONS! YOU GET A SWORD!");
                            Thread.Sleep(2000);
                            Console.SetCursorPosition(80, 30);
                            for (int i = 0; i < 37; i++)
                                Console.Write(" ");

                        }


                    }
                    else if (map[Player.X + 1, Player.Y] == 'S')
                    {

                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        Player.X++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@");
                        Player.Reputation += 5;
                        foreach (Trader trader in traders)
                        {
                            trader.Menu_Trade(CountLines, items, Player);
                        }
                    }
                    else if (map[Player.X + 1, Player.Y] == '$')
                    {
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" ");
                        map[Player.X, Player.Y] = ' ';
                        Player.X++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        map[Player.X, Player.Y] = ' ';
                        Player.Health += 20;
                        Console.Write("@");
                    }
                    else if (map[Player.X + 1, Player.Y] == 'U')
                    {
                        Console.Clear();
                        Console.SetCursorPosition(20, 10);
                        Console.WriteLine("YOU WIN!");
                        break;
                    }
                    else
                    {

                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write(" "); // Восстановить символ комнаты на старой позиции игрока 
                        Player.X++;
                        Console.SetCursorPosition(Player.X, Player.Y);
                        Console.Write("@"); // Установить символ игрока на новой позиции 

                        foreach (Enemy enemy in enemies)
                        {
                            map[enemy.X, enemy.Y] = ' ';
                            enemy.Move(width, height, map);
                            map[enemy.X, enemy.Y] = enemy.name[0];
                        }

                        // Отображение только врагов, находящихся в радиусе видимости игрока
                        foreach (Enemy enemy in enemies)
                        {

                            distance = Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));
                            if (distance <= 5)
                            {
                                // Вычисление расстояния до игрока
                                distanceToPlayer = (int)Math.Sqrt(Math.Pow(Player.X - enemy.X, 2) + Math.Pow(Player.Y - enemy.Y, 2));

                                // Если игрок в поле зрения врага, то враг начинает преследовать игрока
                                if (distanceToPlayer <= enemy.SightRange)
                                {
                                    // Стираем старый символ врага
                                    Console.SetCursorPosition(enemy.X, enemy.Y);
                                    Console.Write(" ");
                                    map[enemy.X, enemy.Y] = ' ';

                                    // Вычисление направления движения врага
                                    int dx = Math.Sign(Player.X - enemy.X);
                                    int dy = Math.Sign(Player.Y - enemy.Y);

                                    // Проверка возможности перемещения в заданном направлении
                                    if (map[enemy.X + dx, enemy.Y] == ' ')
                                    {
                                        enemy.X += dx;
                                    }
                                    else if (map[enemy.X, enemy.Y + dy] == ' ')
                                    {
                                        enemy.Y += dy;
                                    }
                                    map[enemy.X, enemy.Y] = enemy.name[0];
                                }

                                Console.SetCursorPosition(enemy.X, enemy.Y);
                                if (enemy.name == "Goblin")
                                    Console.Write("G");
                                else if (enemy.name == "Orc")
                                    Console.Write("O");
                                else if (enemy.name == "Troll")
                                    Console.Write("T");
                                if (distanceToPlayer == 0)
                                {
                                    Console.Clear();
                                    Draw_Battle draw_battle4 = new Draw_Battle();
                                    Thread.Sleep(100);
                                    enemies.Remove(enemy);
                                    map[enemy.X, enemy.Y] = ' ';
                                    Player.Health -= enemy.damage;
                                    enemy.health -= Player.Sum_Damage();
                                    Console.Clear();
                                    break;
                                }
                            }
                        }

                        Thread.Sleep(10);
                        Player.PrintCharacterInfo();

                    }
                }
            }
        }
    }
}
