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
    protected Symbols _symbol;
    public virtual Symbols Symbol => _symbol;
    protected Position _pos;
    public Position Pos
    {
        get{return _pos;}
        set{_pos = value;}
    }
    public bool IsMoving;
    public bool CanPushOut;
    public GameObject(Position pos = default, Symbols symbol = Symbols.None, bool isMoving = false, bool canPushOut = false)
    {
        _symbol = symbol;
        _pos = pos;
        IsMoving = isMoving;
        CanPushOut = canPushOut;
    }
}
public static class GameObjectExtensions
{
    public static bool ExistsAt<T>(this IEnumerable<T> objects, Position targetPos) where T : GameObject
    {
        return objects.Any(obj => obj.Pos == targetPos);
    }
    public static T? GetObj<T>(this IEnumerable<T> objects, Position targetPos) where T : GameObject
    {
        return objects.FirstOrDefault(obj => obj.Pos == targetPos);
    }
}
public static class GameObjectFactory
    {
        public static GameObject CreatePlayer(Position pos)
        {
            return new(pos, Symbols.Player, true, true);
        }
        public static GameObject CreateWall(Position pos)
        {
            return new(pos, Symbols.Wall);
        }
        public static Box CreateBox(Position pos)
        {
            return new(pos, Symbols.Box);
        }
        public static GameObject CreateGoal(Position pos)
        {
            return new(pos, Symbols.Goal);
        }
    }