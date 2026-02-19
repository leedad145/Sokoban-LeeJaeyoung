public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None,
}
public static class DirectionExtensions
{
    public static Position ToOffset(this Direction direction) => direction switch
    {
        Direction.Up => Position.At(0, -1),
        Direction.Down => Position.At(0, 1),
        Direction.Left => Position.At(-1, 0),
        Direction.Right => Position.At(1, 0),
        _ => Position.At(0, 0),
    };
}