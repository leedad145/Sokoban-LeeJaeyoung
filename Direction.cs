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
    public static Direction FromKey(ConsoleKey key) => key switch
    {
        ConsoleKey.UpArrow => Direction.Up,
        ConsoleKey.DownArrow => Direction.Down,
        ConsoleKey.LeftArrow => Direction.Left,
        ConsoleKey.RightArrow => Direction.Right,
        _ => Direction.None
    };
    public static Position ToOffset(this Direction direction, Position position) => direction switch
    {
        Direction.Up => position.At(0, -1),
        Direction.Down => position.At(0, 1),
        Direction.Left => position.At(-1, 0),
        Direction.Right => position.At(1, 0),
        _ => position.At(0, 0),
    };
}