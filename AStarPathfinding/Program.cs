using Raylib_cs;

namespace AStarPathfinding
{
    internal class Program
    {
        // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
        [System.STAThread]
        public static void Main()
        {
            Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
            Raylib.InitWindow(1600, 900, "Pathfinding");

            GameManager manager = new();
            manager.Initialise();

            while (!Raylib.WindowShouldClose())
            {
                manager.Update();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.DrawText("Pathfinding", 12, 12, 20, Color.Black);

                manager.Draw();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}