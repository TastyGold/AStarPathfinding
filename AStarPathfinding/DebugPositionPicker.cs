using AStarPathfinding;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

internal class DebugPositionPicker
{
    private VecInt2 start = new(-1, -1);
    private VecInt2 end = new(-1, -1);
    private readonly Color startColor = new(50, 200, 50);
    private readonly Color endColor = new(220, 60, 60);
    private readonly Color mousePositionColor = Color.Blue;

    private VecInt2 lastMousePosition = new(-1, -1);

    public void Update(int px, int py, int scale)
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        VecInt2 boardPos = new((int)((mousePos.X - px) / scale), (int)((mousePos.Y - py) / scale));
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            start = boardPos;
        }
        if (Raylib.IsMouseButtonPressed(MouseButton.Right))
        {
            end = boardPos;
        }

        lastMousePosition = boardPos;
    }

    public VecInt2 GetStart() => start;
    public VecInt2 GetEnd() => end;

    public void Draw(int px, int py, int scale)
    {
        // draw start and end
        Raylib.DrawRectangle(px + start.x * scale, py + start.y * scale, scale, scale, startColor);
        Raylib.DrawRectangle(px + end.x * scale, py + end.y * scale, scale, scale, endColor);

        // draw mouse position
        Raylib.DrawRectangleLines(px + lastMousePosition.x * scale, py + lastMousePosition.y * scale, scale, scale, mousePositionColor);
    }
}
