using Raylib_cs;

internal static class BoardRenderer
{
    public static readonly Color gridColor = new(200, 200, 200, 200);

    public static void DrawBoard(Board board, int px, int py, int scale)
    {
        DrawBoardOutline(board, px, py, scale, gridColor);
        DrawBoardTiles(board, px, py, scale);
    }

    public static void DrawBoardOutline(Board board, int px, int py, int scale, Color col)
    {
        // senselessly, should be -1 +1 either side but drawrectanglelines was weird
        Raylib.DrawRectangleLines(px, -1 + py, board.width * scale + 1, board.height * scale + 1, col);
    }

    public static void DrawBoardTiles(Board board, int px, int py, int scale)
    {
        for (int y = 0; y < board.height; y++)
        {
            for (int x = 0; x < board.width; x++)
            {
                Tile tile = board.GetTile(x, y);
                if (!tile.IsEmpty)
                {
                    DrawTile(tile, px + x * scale, py + y * scale, scale);
                }
            }
        }
    }

    public static void DrawTile(Tile tile, int px, int py, int scale)
    {
        Raylib.DrawRectangle(px, py, scale, scale, TileRenderer.GetTileColor(tile));
    }
}