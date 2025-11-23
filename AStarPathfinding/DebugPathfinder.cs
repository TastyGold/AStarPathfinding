using AStarPathfinding;
using Raylib_cs;

internal class DebugPathfinder
{
    private Pathfinder pathfinder = new();

    public void Update(Board board, VecInt2 start, VecInt2 end)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.P))
        {
            pathfinder.Start(start, end);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket) || Raylib.IsKeyDown(KeyboardKey.RightBracket))
        {
            Console.WriteLine("step");

            pathfinder.Step(board);

            // print out closed and open sets of pathfinder and all their positions
            Console.WriteLine("Open set count: " + pathfinder.GetOpenSet().Count);
            Console.WriteLine("Closed set count: " + pathfinder.GetClosedSet().Count);
        }
    }

    public void Draw(int px, int py, int scale)
    {
        foreach (var pos in pathfinder.GetClosedSet())
        {
            Raylib.DrawRectangle(px + pos.x * scale, py + pos.y * scale, scale, scale, Color.Blue);
        }

        foreach (var pathNode in pathfinder.GetOpenSet())
        {
            VecInt2 pos = pathNode.position;
            Raylib.DrawRectangle(px + pos.x * scale, py + pos.y * scale, scale, scale, Color.Orange);
            Raylib.DrawText($"{pathNode.gCost}", px + pos.x * scale + 1, py + pos.y * scale, scale / 3, Color.Black);
            Raylib.DrawText($"{pathNode.hCost}", px + pos.x * scale + 1 + scale / 2, py + pos.y * scale, scale / 3, Color.Black);
            Raylib.DrawText($"{pathNode.FCost}", px + pos.x * scale + 1 + (int)(scale * 0.2f), py + pos.y * scale + scale / 2, scale / 2, Color.Black);
        }
    }
}