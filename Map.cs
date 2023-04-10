using System;
using System.Collections.Generic;

class MapGenerator
{
    //private int width, height;
    public int width;
    public int height;
    private bool[,] map;
    private Random rng = new Random();
    public bool IsWithinBounds(int x, int y)
    {
        return 0 <= x && x < width && 0 <= y && y < height;
    }

    public MapGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.map = new bool[width, height];
    }

    public bool[,] Generate()
    {
        // Генерирую комнаты
        List<Room> rooms = new List<Room>();
        for (int i = 0; i < 10; i++)
        {
            Room room = new Room(rng.Next(5, 15), rng.Next(5, 15), rng.Next(width - 15), rng.Next(height - 15));
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
                    map[ax, ay] = true;
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
                        map[x, y] = true;
                    }
                }
            }
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
