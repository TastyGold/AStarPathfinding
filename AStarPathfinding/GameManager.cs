using AStarPathfinding;
using Raylib_cs;

internal class GameManager
{
    private const int tileMultiplier = 1;

    private static readonly int boardWidth = 77 * tileMultiplier;
    private static readonly int boardHeight = 42 * tileMultiplier;
    private double population = 0.483f;

    private readonly int px = 30;
    private readonly int py = 30;
    private readonly int scale = 20 / tileMultiplier;

    private readonly BoardManager board = new(boardWidth, boardHeight);

    private readonly DebugPositionPicker debugPositionPicker = new();

    private readonly DebugPathfinder pathfinder = new();

    public void Initialise()
    {
        board.GenerateCavesTerrain(population, automataSteps:5);
    }

    public void Update()
    {
        // change population with up/down keys
        if (Raylib.IsKeyDown(KeyboardKey.Up)) population += Raylib.GetFrameTime() / 10.0;
        if (Raylib.IsKeyDown(KeyboardKey.Down)) population -= Raylib.GetFrameTime() / 10.0;

        // generate new terrain with J/K/L keys
        if (Raylib.IsKeyPressed(KeyboardKey.J) || Raylib.IsKeyPressed(KeyboardKey.K))
        {
            board.SetRandomTilePresence(population);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.L))
        {
            board.SetSmoothRandomTilePresence(0.35, 0.65, 0, boardWidth);
        }

        // update board and debug position picker
        board.Update();
        debugPositionPicker.Update(px, py, scale);
        pathfinder.Update(board.GetBoard(), debugPositionPicker.GetStart(), debugPositionPicker.GetEnd());

        // modify tiles with N/M keys
        VecInt2 mousePosition = debugPositionPicker.GetMousePosition();
        if (Raylib.IsKeyDown(KeyboardKey.N))
        {
            board.GetBoard().SetTile(mousePosition.x, mousePosition.y, Tile.empty);
        }
        if (Raylib.IsKeyDown(KeyboardKey.M))
        {
            board.GetBoard().SetTile(mousePosition.x, mousePosition.y, Tile.present);
        }
    }

    public void Draw()
    {
        board.DrawBoard(px, py, scale);
        Raylib.DrawText($"Population = {population}", 10, 10, 10, Color.White);
        pathfinder.Draw(px, py, scale);
        debugPositionPicker.Draw(px, py, scale);
        Raylib.DrawFPS(10, 875);
    }
}
