using Rogue;
using Roguelike;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using static MapGenerator;

public class MapGenerator
{
    // Создание списка объектов Gold
    List<Gold> golds = new List<Gold>();
    // Создание списка объектов Box
    List<Box> boxes = new List<Box>();
    // Создание списка объектов Trader
    List<Trader> traders = new List<Trader>();
    // Создание списка объектов Stairs
    List<Stairs> stairs = new List<Stairs>();
    //вернуть списки с сундуками для дальнейших проверок
    public List<Box> list_boxes()
    {
        return boxes;
    }
    //вернуть списки с золотом для дальнейших проверок
    public List<Gold> list_golds()
    {
        return golds;
    }
    //вернуть список торговцев для дальнейших проверок
    public List<Trader> list_traders()
    {
        return traders;
    }
    //вернуть список выходов для дальнейших проверок
    public List<Stairs> list_stairs()
    {
        return stairs;
    }




    private int width = 70;
    private int height = 40;
    private int visible = 5;
    private char[,] map;
    private Random rng = new Random();
    public bool IsWithinBounds(int x, int y)
    {
        return 0 <= x && x < width && 0 <= y && y < height;
    }
    public int visible_map()
    {
        return visible;
    }

    public MapGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.map = new char[width, height];
    }

    public void Map_Visible(int PlayerX, int PlayerY)
    {
        for (int y = PlayerY - visible; y <= PlayerY + visible; y++)
        {
            for (int x = PlayerX - visible; x <= PlayerX + visible; x++)
            {
                //Проверка, что координаты находятся в пределах карты
                if (IsWithinBounds(x, y))
                {
                    // Проверка, что клетка находится в радиусе обзора игрока
                    if ((Math.Pow(PlayerX - x, 2) + Math.Pow(PlayerY - y, 2)) <= Math.Pow(visible, 2))
                    {

                        Console.SetCursorPosition(x, y);
                        Console.Write(map[x, y]);
                    }
                }
            }
        }
    }

    public void Draw_Map(int PlayerX, int PlayerY)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (PlayerX == x && PlayerY == y)
                {
                    //Console.Write(" ");
                    map[x, y] = ' ';
                }
                else if (map[x, y] == ' ')
                {
                    //Console.Write(" ");
                    map[x, y] = ' ';
                }
                else if (map[x, y] == '$')
                {
                    //Console.Write("$");
                    map[x, y] = '$';
                }
                else if (map[x, y] == 'U')
                {
                    //Console.Write("U");
                    map[x, y] = 'U';
                }
                else if (map[x, y] == 'S')
                {
                    //Console.Write("S");
                    map[x, y] = 'S';
                }
                else if (map[x, y] == 'B')
                {
                    //Console.Write("B");
                    map[x, y] = 'B';
                }
                else
                {
                    //Console.Write('#');
                    map[x, y] = '#';
                }
            }
            Console.WriteLine();
        }
    }



    public char[,] Generate()
    {
        // Генерирую комнаты
        List<Room> rooms = new List<Room>();
        for (int i = 0; i < 10; i++)
        {

            Room room = new Room(rng.Next(5, 15), rng.Next(5, 15), rng.Next(1, width - 15), rng.Next(1, height - 15));

            bool intersects = false;
            foreach (Room r in rooms)
            {
                if (r.Intersects(room))
                {
                    intersects = true;
                    break;
                }
            }

            if (!intersects)
            {
                rooms.Add(room);
            }
        }

        //соединяю коридорами
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            Room a = rooms[i];
            Room b = rooms[i + 1];
            int ax = a.X + rng.Next(a.Width);
            int ay = a.Y + rng.Next(a.Height);
            int bx = b.X + rng.Next(b.Width);
            int by = b.Y + rng.Next(b.Height);


            while (ax != bx || ay != by)
            {
                if (ax < bx)
                {
                    ax++;
                }
                else if (ax > bx)
                {
                    ax--;
                }
                else if (ay < by)
                {
                    ay++;
                }
                else if (ay > by)
                {
                    ay--;
                }

                if (ax >= 0 && ax < width && ay >= 0 && ay < height)
                {
                    map[ax, ay] = ' ';
                }
            }
        }

        // Заполняю комнаты и коридоры 
        foreach (Room room in rooms)
        {

            for (int x = room.X; x < room.X + room.Width; x++)
            {
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        map[x, y] = ' ';
                    }
                }
            }
        }

        //золото
        int goldCount = rng.Next(5, 11); // Количество золота, которое нужно сгенерировать

        for (int i = 0; i < goldCount; i++)
        {
            int x_gold, y_gold;

            // Генерация случайных координат до тех пор, пока не будет найдена пустая клетка
            do
            {
                x_gold = rng.Next(1, width - 1);
                y_gold = rng.Next(1, height - 1);
            }
            while (map[x_gold, y_gold] != ' ');

            // Создание нового объекта Gold и добавление его в список
            Gold gold = new Gold(x_gold, y_gold);
            golds.Add(gold);
            map[x_gold, y_gold] = '$';
        }

        int x_trader, y_trader;
        // Генерация случайных координат до тех пор, пока не будет найдена пустая клетка
        do
        {
            x_trader = rng.Next(1, width - 1);
            y_trader = rng.Next(1, height - 1);
        }
        while (map[x_trader, y_trader] != ' ');

        // Создание нового объекта Trader и добавление его в список
        string name_trader = "SIDOROVICH";
        Trader trader = new Trader(x_trader, y_trader, name_trader);
        traders.Add(trader);
        map[x_trader, y_trader] = 'S';


        int x_stairs, y_stairs;
        // Генерация случайных координат до тех пор, пока не будет найдена пустая клетка
        do
        {
            x_stairs = rng.Next(1, width - 1);
            y_stairs = rng.Next(1, height - 1);
        }
        while (map[x_stairs, y_stairs] != ' ');

        // Создание нового объекта Stairs и добавление его в список

        Stairs stair = new Stairs(x_stairs, y_stairs);
        stairs.Add(stair);
        map[x_stairs, y_stairs] = 'U';


        //сундуки
        int box_count = 3; // Количество сундуков, которое нужно сгенерировать

        for (int i = 0; i < box_count; i++)
        {
            int x_box, y_box;

            // Генерация случайных координат до тех пор, пока не будет найдена пустая клетка
            do
            {
                x_box = rng.Next(1, width - 1);
                y_box = rng.Next(1, height - 1);
            }
            while (map[x_box, y_box] != ' ');

            // Создание нового объекта Box и добавление его в список
            Box box = new Box(x_box, y_box);
            boxes.Add(box);
            map[x_box, y_box] = 'B';
        }


        return map;

    }


    private class Room
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Room(int width, int height, int x, int y)
        {
            this.Width = width;
            this.Height = height;
            this.X = x;
            this.Y = y;
        }

        public bool Intersects(Room other)
        {
            return !(X + Width <= other.X || other.X + other.Width <= X || Y + Height <= other.Y || other.Y + other.Height <= Y);
        }
    }
}

public class Stairs
{
    private int X;
    private int Y;
    public Stairs(int x_stairs, int y_stairs)
    {
        X = x_stairs;
        Y = y_stairs;
    }
    public int X_coord()
    {
        return X;
    }
    public int Y_coord()
    {
        return Y;
    }
}


public class Gold
{
    private int X;
    private int Y;
    public Gold(int x_gold, int y_gold)
    {
        X = x_gold;
        Y = y_gold;
    }
    public int X_coord()
    {
        return X;
    }
    public int Y_coord()
    {
        return Y;
    }
}
public class Box
{
    private int X;
    private int Y;
    public Box(int x_gold, int y_gold)
    {
        X = x_gold;
        Y = y_gold;
    }

}

public class Trader
{
    private int X;
    private int Y;
    private string name;
    public Trader()
    {

    }
    public Trader(int x_trader, int y_trader, string name_trader)
    {
        X = x_trader;
        Y = y_trader;
    }
    internal void Menu_Trade(int Count, List<Item> items, Player Player)
    {
        int window_w = 30;
        int window_h = 15;

        // Вычисление координат окошка
        int WindowX = window_w + 45;
        int WindowY = 20;

        // Верхняя граница окошка
        Console.SetCursorPosition(WindowX, WindowY);
        Console.Write("╔" + new string('═', window_w - 2) + "╗");

        // Боковые границы окошка
        for (int y = WindowY + 1; y < WindowY + window_h - 1; y++)
        {
            Console.SetCursorPosition(WindowX, y);
            Console.Write("║");
            Console.SetCursorPosition(WindowX + window_w - 1, y);
            Console.Write("║");
        }

        // Нижняя граница окошка
        Console.SetCursorPosition(WindowX, WindowY + window_h - 1);
        Console.Write("╚" + new string('═', window_w - 2) + "╝");

        // Заголовок окошка
        Console.SetCursorPosition(WindowX + 7, WindowY + 1);
        Console.Write("MERCHANT'S SHOP");

        foreach (Item item in items)
        {
            Console.SetCursorPosition(WindowX + 2, WindowY + 2 + Count);
            Console.WriteLine("{0} - PRICE = {1}", item.Name, item.Price);
            Count++;
        }
        Console.SetCursorPosition(WindowX + 2, WindowY + 2 + Count);
        Console.WriteLine("ENTER THE NUMBER OF ITEM");
        Console.SetCursorPosition(WindowX + 8, WindowY + Count + 3);
        Console.WriteLine("FROM 0 TO 2");
        Console.SetCursorPosition(WindowX + 3, WindowY + Count + 4);
        Console.WriteLine("TO CLOSE IS ON ESCAPE");
        int k1 = 0, k2 = 0, k3 = 0;
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                for (int length = 75; length < 120; length++)
                {
                    for (int width = 20; width < 50; width++)
                    {
                        Console.SetCursorPosition(length, width);
                        Console.WriteLine(" ");
                    }
                }
                break;
            }
            if (keyInfo.Key == ConsoleKey.D0)
            {
                if (Player.Money >= items[0].Price)
                {
                    Player.Money -= items[0].Price;
                    Player.AddToInventory(items[0]);
                    Console.SetCursorPosition(WindowX + 3, WindowY + Count + 15);
                    Console.WriteLine($"YOU SUCCESSFULLY BOUGHT {items[0].Name}");
                    Count++;

                }
                else
                {
                    if (k1 == 0)
                    {
                        Console.SetCursorPosition(WindowX + 3, WindowY + Count + 15);
                        Console.WriteLine($"YOU HAVE NOT ENOUGH MONEY ON {items[0].Name}!");
                        k1++;
                        Count++;
                    }
                }
                continue;
            }
            else if (keyInfo.Key == ConsoleKey.D1)
            {
                if (Player.Money >= items[1].Price)
                {
                    Player.Money -= items[1].Price;
                    Player.AddToInventory(items[1]);
                    Console.SetCursorPosition(WindowX + 3, WindowY + Count + 15);
                    Console.WriteLine($"YOU SUCCESSFULLY BOUGHT {items[1].Name}");
                    Count++;

                }
                else
                {
                    if (k2 == 0)
                    {
                        Console.SetCursorPosition(WindowX + 3, WindowY + Count + 15);
                        Console.WriteLine($"YOU HAVE NOT ENOUGH MONEY ON {items[1].Name}!");
                        Count++;
                        k2++;
                    }
                }
                continue;
            }
            else if (keyInfo.Key == ConsoleKey.D2)
            {
                if (Player.Money >= items[2].Price)
                {
                    Player.Money -= items[2].Price;
                    Player.AddToInventory(items[2]);
                    Console.SetCursorPosition(WindowX + 3, WindowY + Count + 15);
                    Console.WriteLine($"YOU SUCCESSFULLY BOUGHT {items[2].Name}");
                    Count++;
                }
                else
                {
                    if (k3 == 0)
                    {
                        Console.SetCursorPosition(WindowX + 3, WindowY + Count + 15);
                        Console.WriteLine($"YOU HAVE NOT ENOUGH MONEY ON {items[2].Name}!");
                        Count++;
                        k3++;
                    }
                }
                continue;
            }
        }
    }
}




