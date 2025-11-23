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

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.G))
        {
            TerrainGenerator.RunCaveAutomataStep(board);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.H) || Raylib.IsKeyPressed(KeyboardKey.K))
        {
            for (int i = 0; i < 5; i++) TerrainGenerator.RunCaveAutomataStep(board);
        }
    }

    public void DrawBoard(int px, int py, int scale)
    {
        BoardRenderer.DrawBoard(board, px, py, scale);
    }
}
