using AStarPathfinding;
using Raylib_cs;

internal class BoardManager(int width, int height)
{
    private readonly Board board = new(width, height);
    private readonly Random random = new();

    public void SetRandomTiles(int minId, int maxId)
    {
        for (int y = 0; y < board.height; y++)
        {
            for (int x = 0; x < board.width; x++)
            {
                board.SetTile(x, y, new Tile(random.Next(minId, maxId + 1)));
            }
        }
    }

    public Board GetBoard() => board;

    public void SetRandomTilePresence(double population)
    {
        for (int y = 0; y < board.height; y++)
        {
            for (int x = 0; x < board.width; x++)
            {
                board.SetTile(x, y, random.NextDouble() < population ? Tile.present : Tile.empty);
            }
        }
    }

    public void SetSmoothRandomTilePresence(double populationA, double populationB, int left, int right)
    {
        for (int y = 0; y < board.height; y++)
        {
            for (int x = 0; x < board.width; x++)
            {
                double t = (double)(x - left) / (right - left);
                t = Math.Clamp(t, 0.0, 1.0);
                double population = populationA * (1.0 - t) + populationB * t;
                board.SetTile(x, y, random.NextDouble() < population ? Tile.present : Tile.empty);
            }
        }
    }

    public void GenerateCavesTerrain(double initialPopulation, int automataSteps)
    {
        SetRandomTilePresence(initialPopulation);
        TerrainGenerator.RunCaveAutomataSteps(board, automataSteps);
    }

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.G))
        {
            TerrainGenerator.RunCaveAutomataStep(board);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.H) || Raylib.IsKeyPressed(KeyboardKey.K))
        {
            TerrainGenerator.RunCaveAutomataSteps(board, 5);
        }
    }

    public void DrawBoard(int px, int py, int scale)
    {
        BoardRenderer.DrawBoard(board, px, py, scale);
    }
}