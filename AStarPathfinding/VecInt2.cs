namespace AStarPathfinding;

internal struct VecInt2(int x, int y)
{
    public int x = x;
    public int y = y;

    public readonly VecInt2 Rotate(int clockwiseRotations)
    {
        clockwiseRotations = Mod(clockwiseRotations, 4);

        int newX = clockwiseRotations switch
        {
            0 => x,
            1 => y,
            2 => -x,
            3 => -y,
            _ => throw new NotImplementedException(),
        };

        int newY = clockwiseRotations switch
        {
            0 => y,
            1 => -x,
            2 => -y,
            3 => x,
            _ => throw new NotImplementedException(),
        };

        return new VecInt2(newX, newY);
    }

    public static VecInt2 operator +(VecInt2 a, VecInt2 b)
    {
        return new VecInt2(a.x + b.x, a.y + b.y);
    }
    public static VecInt2 operator -(VecInt2 a, VecInt2 b)
    {
        return new VecInt2(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(VecInt2 a, VecInt2 b)
    {
        return !(a != b);
    }
    public static bool operator !=(VecInt2 a, VecInt2 b)
    {
        return a.x != b.x || a.y != b.y;
    }

    public static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is VecInt2 @int &&
               x == @int.x &&
               y == @int.y;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override readonly string? ToString()
    {
        return $"<{x}, {y}>";
    }
}
