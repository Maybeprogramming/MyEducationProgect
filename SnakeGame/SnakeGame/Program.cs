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
        private const ConsoleColor FoodColor = ConsoleColor.Red;

        private const int FrameMs = 250;

        private static readonly Random random = new Random();


        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;

            StartGame();

            ReadKey();
        }


        static void StartGame()
        {
            Clear();

            int score = 0;

            DrawBorder();

            Direction currentMovement = Direction.Right;

            Snake snake = new Snake(10, 5, HeadColor, BodyColor);

            Pixel food = GenFood(snake);
            food.Draw();

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();

                Direction oldMovement = currentMovement;

                while (sw.ElapsedMilliseconds <= FrameMs)
                {
                    if (currentMovement == oldMovement)
                        currentMovement = ReadMovement(currentMovement);
                }

                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMovement, true);

                    food = GenFood(snake);
                    food.Draw();
                    score++;
                }
                else
                {
                    snake.Move(currentMovement);

                }

                if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeiht
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;
            }

            snake.Clear();

            SetCursorPosition(ScreenWidth / 3, ScreenHeight / 2);
            Write($"Вы проиграли!!! Вы скушали {score} яблок");
        }

        static Pixel GenFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(random.Next(1, MapWidth - 2), random.Next(1, MapHeiht - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
                    || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));

            return food;
        }


        static Direction ReadMovement(Direction currentDirection)
        {
            if (KeyAvailable == false)
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