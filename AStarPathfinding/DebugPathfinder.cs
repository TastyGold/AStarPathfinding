using AStarPathfinding;
using Raylib_cs;
using static System.Formats.Asn1.AsnWriter;

internal class DebugPathfinder
{
    private Pathfinder pathfinder = new();
    private PathNode? finishedPath;

    public void Update(Board board, VecInt2 start, VecInt2 end)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.P))
        {
            pathfinder.Start(start, end);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket) || Raylib.IsKeyDown(KeyboardKey.RightBracket))
        {
            pathfinder.Step(board);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.Backslash))
        {
            while (pathfinder.GetOpenSet().Count > 0 && !pathfinder.ReachedTarget)
            {
                pathfinder.Step(board);
            }
        }

        if (Raylib.IsKeyDown(KeyboardKey.O))
        {
            finishedPath = pathfinder.FindPath(board, start, end);
        }
    }

    public void Draw(int px, int py, int scale)
    {
        DrawClosedSet(px, py, scale);

        DrawOpenSet(px, py, scale, drawCosts:true);

        DrawPathFromEndNode(finishedPath, px, py, scale);
    }

    public void DrawClosedSet(int px, int py, int scale)
    {
        foreach (var pos in pathfinder.GetClosedSet())
        {
            Raylib.DrawRectangle(px + pos.x * scale, py + pos.y * scale, scale, scale, Color.Blue);
        }
    }

    public void DrawOpenSet(int px, int py, int scale, bool drawCosts)
    {
        foreach (var pathNode in pathfinder.GetOpenSet())
        {
            VecInt2 pos = pathNode.position;
            Raylib.DrawRectangle(px + pos.x * scale, py + pos.y * scale, scale, scale, Color.Orange);

            if (drawCosts)
            {
                Raylib.DrawText($"{pathNode.gCost}", px + pos.x * scale + 1, py + pos.y * scale, scale / 3, Color.Black);
                Raylib.DrawText($"{pathNode.hCost}", px + pos.x * scale + 1 + scale / 2, py + pos.y * scale, scale / 3, Color.Black);
                Raylib.DrawText($"{pathNode.FCost}", px + pos.x * scale + 1 + (int)(scale * 0.2f), py + pos.y * scale + scale / 2, scale / 2, Color.Black);
            }
        }
    }

    public static void DrawPathFromEndNode(PathNode? node, int px, int py, int scale)
    {
        if (node is null) return;

        int totalCost = node.gCost;

        PathNode? current = node;
        while (current is not null)
        {
            VecInt2 pos = current.position;
            Raylib.DrawRectangle(px + pos.x * scale, py + pos.y * scale, scale, scale, Color.Red);
            current = current.parent;
        }

        Raylib.DrawText($"Total Cost: {totalCost}", 200, 5, 20, Color.White);
    }
}