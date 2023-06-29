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

        private const ConsoleColor borderColor = ConsoleColor.White;


        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;

            DrawBorder();

            ReadKey();
        }

        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, borderColor).Draw();
                new Pixel(i, MapHeiht - 1, borderColor).Draw();
            }

            for (int i = 0; i < MapHeiht; i++)
            {
                new Pixel(0, i, borderColor).Draw();
                new Pixel(MapWidth - 1, i, borderColor).Draw();
            }
        }
    }

}
#pragma warning restore CA1416 // Проверка совместимости платформы