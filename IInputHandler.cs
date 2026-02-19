interface IInputHandler
{
    void ProcessInput();
    Direction GetDirection();
}

class ConsoleInputHandler : IInputHandler
{
    ConsoleKeyInfo _keyInfo;
    public void ProcessInput()
    {
        _keyInfo = Console.ReadKey();
    }

    public Direction GetDirection() =>_keyInfo.Key switch
    {
        ConsoleKey.UpArrow => Direction.Up,
        ConsoleKey.DownArrow => Direction.Down,
        ConsoleKey.LeftArrow => Direction.Left,
        ConsoleKey.RightArrow => Direction.Right,
        _ => Direction.None
    };
}