public struct Position : IEquatable<Position>
{
    private readonly int _x;
    private readonly int _y;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }
    public Position(int x = 0, int y = 0)
    {
        _x = x;
        _y = y;
    }
    public Position At(int x, int y)
    {
        return new Position(X + x, Y + y);
    }

    public bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }
    
    public static bool operator ==(Position a, Position b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Position a, Position b)
    {
        return !a.Equals(b);
    }

    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }
    ////// 노란 줄 없에기
    public override bool Equals(object? obj)
    {
        return obj is Position && Equals((Position)obj);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}