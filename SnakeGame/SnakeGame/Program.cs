using System.Diagnostics;
using static System.Console;
#pragma warning disable CA1416 // Проверка совместимости платформы

namespace SnakeGame
{
    internal class Program
    {
        private const int MapWidth = 30;
        private const int MapHeiht = 20;

        private const int ScreenWidth = MapWidth * 3;
        private const int ScreenHeight = MapHeiht * 3;

        private const ConsoleColor BorderColor = ConsoleColor.White;
        private const ConsoleColor HeadColor = ConsoleColor.Blue;
        private const ConsoleColor BodyColor = ConsoleColor.DarkBlue;

        private const int FrameMs = 250;


        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;

            DrawBorder();

            Direction currentMovement = Direction.Right;

            Snake snake = new Snake(10,5, HeadColor, BodyColor);

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();

                while (sw.ElapsedMilliseconds <= FrameMs)
                {
                    currentMovement = ReadMovement(currentMovement);
                }

                snake.Move(currentMovement);
            }

            ReadKey();
        }

        static Direction ReadMovement(Direction currentDirection)
        {
            if(KeyAvailable == false)
            {
                return currentDirection;
            }

            ConsoleKey key = ReadKey(true).Key;

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };

            return currentDirection;
        }

        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, MapHeiht - 1, BorderColor).Draw();
            }

            for (int i = 0; i < MapHeiht; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth - 1, i, BorderColor).Draw();
            }
        }
    }

}
#pragma warning restore CA1416 // Проверка совместимости платформы