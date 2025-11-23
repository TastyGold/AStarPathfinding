internal struct Tile(int id)
{
    public int id = id;
    public const int emptyId = 0;
    public readonly bool IsEmpty => id == emptyId;
    public static Tile empty = new(emptyId);
    public static Tile present = new(1);
}
