public enum Symbols
{
    Player = 'P',
    Box = 'B',
    Wall = '#',
    Goal = '0',
    GoalInBox = '@',
    None = ' ',
}

public class GameObject
{
    private Symbols _symbol;
    public Symbols Symbol
    {
        get{return _symbol;}
    }
    private Position _pos;
    public Position Pos
    {
        get{return _pos;}
    }
    public GameObject(Position pos = default, Symbols symbol = Symbols.None)
    {
        _symbol = symbol;
        _pos = pos;
    }
    public bool TryMove(Direction dir, Func<Position, bool> CanMove)
    {
        Position nextPos = dir.ToOffset(_pos);
        if (CanMove(nextPos)) // 다음 위치로 움직일 수 있나?
        {
            _pos = nextPos; // 움직여라
            return true;
        }
        
        return false;
    }
}
public static class Extensions
{
    public static bool ExistsAt(this IEnumerable<GameObject> objects, Position targetPos)
    {
        return objects.Any(obj => obj.Pos == targetPos);
    }
    public static GameObject? GetObj(this IEnumerable<GameObject> objects, Position targetPos)
    {
        return objects.FirstOrDefault(obj => obj.Pos == targetPos);
    }
}