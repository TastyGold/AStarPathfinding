using Raylib_cs;

internal static class TileRenderer
{
    public static Color GetTileColor(Tile tile)
    {
        return tile.id switch
        {
            1 => Color.White,
            _ => Color.Black,
        };
    }
}
