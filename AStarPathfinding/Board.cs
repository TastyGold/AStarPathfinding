internal class Board(int width, int height)
{
    private readonly Tile[,] board = new Tile[width, height];
    public readonly int width = width;
    public readonly int height = height;

    public Tile GetTile(int x, int y)
    {
        return board[x, y];
    }

    public void SetTile(int x, int y, Tile value)
    {
        board[x, y] = value;
    }

    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
