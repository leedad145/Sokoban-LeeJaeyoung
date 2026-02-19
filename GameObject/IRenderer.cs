interface IRenderer
{
    void Clear();
    void PrintMessage(string msg);
    void Render();
}
class ConsoleRenderer : IRenderer
{
    private IEnumerable<GameObject> _gameObjects;
    public ConsoleRenderer(IEnumerable<GameObject> gameObjects)
    {
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Title = "My Sokoban";
        Console.CursorVisible = false;
        Console.Clear();  
        
        _gameObjects = gameObjects;
    }
    public void Clear()
    {
        Console.Clear();
    }

    public void PrintMessage(string msg)
    {
        Console.WriteLine(msg);
    }

    public void Render()
    {
        foreach(GameObject gameObject in _gameObjects)
        {
            Console.SetCursorPosition(gameObject.Pos.X, gameObject.Pos.Y);
            Console.Write((char)gameObject.Symbol);
        }
    }
}

