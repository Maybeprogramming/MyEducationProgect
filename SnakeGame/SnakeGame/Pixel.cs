﻿namespace SnakeGame
{
    internal readonly struct Pixel
    {
        private const char PixelChar = '#';
        private const char PixelEmpty = ' ';

        public Pixel(int x, int y, ConsoleColor color, int pixelSize = 3) : this()
        {
            X = x;
            Y = y;
            Color = color;
            PixelSize = pixelSize;
        }

        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }
        public int PixelSize { get; }

        public void Draw()
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(PixelChar);
                }
            }
        }
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(PixelEmpty);
        }
    }
}
