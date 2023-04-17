using Rogue;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(50, 5);
            Console.Write("ENTER YOUR USERNAME: ");
            string username = Console.ReadLine();
            Console.WriteLine();
            //Console.SetCursorPosition(43, 5);
            //Console.WriteLine($"WELCOME TO THE END OF THE WORLD, DEAR {username}");
            //Console.WriteLine();
            Console.SetCursorPosition(54, 6);
            Console.WriteLine("Have a good game!");

            Thread.Sleep(2000);
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("                                                                        _____                                    \n");
            Thread.Sleep(200);
            Console.Write("                                                                   ,___'     `--.__                              \n");
            Thread.Sleep(200);
            Console.Write("                                                               ,-'                ; `.                           \n");
            Thread.Sleep(200);
            Console.Write("       _    _      _ _                                        ,'                  `--.`--.                       \n");
            Thread.Sleep(200);
            Console.Write("      | |  | |    | | |                                      ,'                       `._ `-.                    \n");
            Thread.Sleep(200);
            Console.Write("      | |  | | ___| | | ___ ___  _ __ ___   ___              |                     ;     `__ ;                   \n");
            Thread.Sleep(200);
            Console.Write("      | |/\\| |/ _ \\ | |/ __/ _ \\| '_ ` _ \\ / _ \\           ,-'-_       _,-~~-.      ,--      `.                  \n");
            Thread.Sleep(200);
            Console.Write("      \\  /\\  /  __/ | | (_| (_) | | | | | |  __/            ||   `-,;    ,'~`.__    ||||    | |                  \n");
            Thread.Sleep(200);
            Console.Write("      |\\/  \\/ \\___|_|_|\\___\\___/|_| |_| |_|\\___|           ||    |,'  ,||      `,  |;;     ` . |                 \n");
            Thread.Sleep(200);
            Console.Write("      | |    | |                                           `:   ,'    `||     __/  `.;       | |                 \n");
            Thread.Sleep(200);
            Console.Write("      | |    | |                                            ;---.   `.   `---'~~    ||      | |                  \n");
            Thread.Sleep(200);
            Console.Write("      |_|____| |                                            `,' `.   `.            |||     |'                    \n");
            Thread.Sleep(200);
            Console.Write("      |_   _|| |                                            ,',^\\ `\\  `._    __    `||     |'                    \n");
            Thread.Sleep(200);
            Console.Write("        | | _| |                                             `-' `--'    ~`--'~~`--.  ~    |'                    \n");
            Thread.Sleep(200);
            Console.Write("        | |/ _ \\                                           /|`-|_ | |. /. /   ; ~~`-.     |                      \n");
            Thread.Sleep(200);
            Console.Write("        | | (_) |                                         ; |  | `,|`-|__|---;      `----'                       \n");
            Thread.Sleep(200);
            Console.Write("        \\_/\\___/                                          ``-`-;__|:  |  |__;                                    \n");
            Thread.Sleep(200);
            Console.Write("            ||                                                     ` `-'                                         \n");
            Thread.Sleep(200);
            Console.Write("            ||                                                                                                   \n");
            Thread.Sleep(200);
            Console.Write("       _____||             _    _            _     _ _        _____          _                                   \n");
            Thread.Sleep(200);
            Console.Write("      |_   _| |           | |  | |          | |   | ( )      |  ___|        | |                                  \n");
            Thread.Sleep(200);
            Console.Write("        | | | |__   ___   | |  | | ___  _ __| | __| |/ ___   | |__ _ __   __| |                                  \n");
            Thread.Sleep(200);
            Console.Write("        | | | '_ \\ / _ \\  | |/\\| |/ _ \\| '__| |/ _` | / __|  |  __| '_ \\ / _` |                                  \n");
            Thread.Sleep(200);
            Console.Write("        | | | | | |  __/  \\  /\\  / (_) | |  | | (_| | \\__ \\  | |__| | | | (_| |                                  \n");
            Thread.Sleep(200);
            Console.Write("        \\_/ |_| |_|\\___|   \\/  \\/ \\___/|_|  |_|\\__,_| |___/  \\____/_| |_|\\__,_|                                  \n");

            Console.SetCursorPosition(50, 28);
            Console.WriteLine("ENTER ANY KEY TO CONTINUE");
            Console.ReadKey();

            Console.Clear();


            int width = 70;
            int height = 40;

            //Генерирую карту двумерным массивом символов
            MapGenerator generator = new MapGenerator(width, height);
            char[,] map = generator.Generate();
            //Формирую объект класса Player
            Player Player = new Player("Chetkypots", 100, 0, 50, '@');
            //Задаю массив врагов
            List<Enemy> enemies = new List<Enemy>()
            {
                new Enemy("Goblin", 50, 5),
                new Enemy("Orc", 75, 8),
                new Enemy("Troll", 100, 12)
            };
            Weapon sword = new Weapon() { Name = "Sword", Damage = 10 };
            Weapon axe = new Weapon() { Name = "Axe", Damage = 15 };
            Item potion = new Item() { Name = "Mana Potion" };

            //Заполняю инвентарь 
            Player.AddToInventory(sword);
            Player.AddToInventory(axe);
            Player.AddToInventory(potion);

            Player.Gold = 100;
            Player.Mana = 50;

            //Player.DisplayInventory();
            Random random = new Random();

            // Нахожу случайную точку на карте, которая является комнатой (символ "#")
            int rnd_x, rnd_y;
            do
            {
                rnd_x = random.Next(width);
                rnd_y = random.Next(height);
            } while (map[rnd_x, rnd_y] != '#');

            // Устанавливаю символ игрока на найденную позицию
            Player.X = rnd_x;
            Player.Y = rnd_y;
            map[rnd_x, rnd_y] = '@';

            // Перемещение игрока по карте 

            // Вывод карты на экран 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[x, y] == '#')
                    { Console.Write("#"); }
                    else
                    { Console.Write(" "); }
                }
                Console.WriteLine();
            }

            // Перемещение игрока по карте 
            while (true)
            {
                // Установка курсора в позицию игрока и вывод символа игрока 
                Console.SetCursorPosition(Player.X, Player.Y);
                Console.Write("@");

                // Вывод текущей позиции игрока 
                //Console.WriteLine($"Character position: ({Player.X}, {Player.Y})");

                // Запрос направления движения от пользователя 
                //Console.Write("Enter a direction (using arrow keys): ");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Перемещение игрока в зависимости от выбранного направления 
                if (keyInfo.Key == ConsoleKey.UpArrow && map[Player.X, Player.Y - 1] == '#')
                {
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("#"); // Восстананавливаю символ комнаты на старой позиции игрока 
                    Player.Y--;
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("@"); // Устанавливаю символ игрока на новой позиции 
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && map[Player.X, Player.Y + 1] == '#')
                {
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("#"); // Восстанавливаю символ комнаты на старой позиции игрока 
                    Player.Y++;
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("@"); // Устанавливаю символ игрока на новой позиции 
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow && map[Player.X - 1, Player.Y] == '#')
                {
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("#"); // Восстанавливаю символ комнаты на старой позиции игрока 
                    Player.X--;
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("@"); // Устанавливаю символ игрока на новой позиции 
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && map[Player.X + 1, Player.Y] == '#')
                {
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("#"); // Восстанавливаю символ комнаты на старой позиции игрока 
                    Player.X++;
                    Console.SetCursorPosition(Player.X, Player.Y);
                    Console.Write("@"); // Устанавливаю символ игрока на новой позиции 
                }

                // Проверяю на выход за границы карты 
                if (!generator.IsWithinBounds(Player.X, Player.Y))
                {
                    Console.WriteLine("Out of bounds! Try again!");
                    break;
                }

                // Очистка строки с запросом направления движения 
                //Console.SetCursorPosition(0, height + 2);
                //Console.Write(new string(' ', Console.WindowWidth));
                //Console.SetCursorPosition(0, height + 2);
            }
        }
    }
}
