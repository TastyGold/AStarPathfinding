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

            TraversalRules.Initialise();

            // print out all of TraversalRules.cardinal to console
            foreach (var rule in TraversalRules.cardinal)
            {
                Console.WriteLine($"DV: ({rule.dv.x}, {rule.dv.y}), Cost: {rule.cost}, Requirements: {rule.requirements.Count}");
                foreach (var req in rule.requirements)
                {
                    Console.WriteLine($"\tRelative Position: ({req.relativePosition.x}, {req.relativePosition.y}), Must Be Empty: {req.mustBeEmpty}");
                }
            }

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