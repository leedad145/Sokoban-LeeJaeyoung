public class Box : GameObject
{
    public override Symbols Symbol => _isBoxInGoal ? Symbols.GoalInBox : Symbols.Box;
    public bool _isBoxInGoal = false;
    public Box(Position pos = default, Symbols symbol = Symbols.Box, bool isMoving = true, bool canPushOut = false) : base(pos, symbol, isMoving, canPushOut)
    {
        
    }
    public void In(IEnumerable<GameObject> goals)
    {
        if(goals.ExistsAt(Pos))
        {
            _isBoxInGoal = true;
        }
        else
        {
            _isBoxInGoal = false;
        } 
    }
}