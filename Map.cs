class Map
{
    public string[][] Stages => _stages;
    private Position _minSize;
    private Position _maxSize;
    public IEnumerable<GameObject> Objects = new List<GameObject>();
    private GameObject _player = new GameObject();
    private List<GameObject> _boxes = new List<GameObject>();
    private List<GameObject> _walls = new List<GameObject>();
    private List<GameObject> _goals = new List<GameObject>();
    private int _canPushBox = 1; // 밀수있는 박스
    private int _curPushBox = 0; // 밀고있는 박스
    IRenderer Renderer = null;
    public void StageBindding(string[] stage)
    {
        _player = new GameObject();
        _boxes = new List<GameObject>();
        _walls = new List<GameObject>();
        _goals = new List<GameObject>();
        for (int i = 0; i < stage.Length; i++)
        {
            for(int j = 0; j < stage[i].Length; j++)
            {
                switch (stage[i][j])
                {
                    case 'P':
                        _player = GameObjectFactory.CreatePlayer(Position.At(j, i));
                        break;
                    case 'B':
                        _boxes.Add(GameObjectFactory.CreateBox(Position.At(j, i)));
                        break;
                    case 'W':
                        _walls.Add(GameObjectFactory.CreateWall(Position.At(j, i)));
                        break;
                    case 'G':
                        _goals.Add(GameObjectFactory.CreateGoal(Position.At(j, i)));
                        break;
                }
            }
        }
        if(_goals.Count != _boxes.Count)
            Renderer.PrintMessage("골과 박스의 갯수가 다릅니다.");

        Objects = _goals.Concat(_walls).Concat(_boxes).Append(_player);
        Renderer = new ConsoleRenderer(Objects);
    }
    public bool StageClear() => _boxes.Cast<Box>().All(box => box._isBoxInGoal);
    public void Update(Direction dir = Direction.None)
    {
        if(TryMove(_player, dir)){}
        
        ////////////////////////////////////////////////////
        bool TryMove(GameObject gameObject, Direction dir)
        {
            if(!gameObject.IsMoving)
                return false;
            if(_curPushBox > _canPushBox)
            {
                _curPushBox = 0;
                return false;
            }

            Position nextPos = gameObject.Pos + dir.ToOffset();
            if(!IsOutOfRange(nextPos))
                return false;
            if(_walls.ExistsAt(nextPos))
                return false;

            GameObject? target = _boxes.GetObj(nextPos);
            if(target != null)                                   // 다음 위치에 박스가 있다면
            {
                if (TryMove(target, dir))
                {
                    (target as Box)!.In(_goals);
                    gameObject.Pos = nextPos;
                    return true;
                }
                return false;
            }
            gameObject.Pos = nextPos;
            return true;
        }
    }
    public bool IsOutOfRange(Position pos)
    {
        bool isOutOfRangeX = pos.X < _minSize.X || pos.X > _maxSize.X;
        bool isOutOfRangeY = pos.Y < _minSize.Y || pos.Y > _maxSize.Y;

        return isOutOfRangeX || isOutOfRangeY;
    }
    ///////////////////////////////////
    private static string[][] _stages =
    [
        [   "WWWWWWWW",
            "W  P   W",
            "W  B   W",
            "W  G   W",
            "WWWWWWWW"],
        [   "WWWWWWWWWWWWWWWWWWW",
            "W                 W",
            "W      G       B  W",
            "W                 W",
            "W         WWWWWWWWW",
            "W                 W",
            "WWWWWWWW          W",
            "W     GW          W",
            "W      W          W",
            "W B               W",
            "W           P     W",
            "WWWWWWWWWWWWWWWWWWW"],
        [   "WWWWWWWWWWWWWWWWWWWWWWW",
            "W                    GW",
            "W                     W",
            "W                     W",
            "W       B    B        W",
            "W                     W",
            "W                     W",
            "W     G   W           W",
            "W         W           W",
            "W         W           W",
            "W         W P         W",
            "W         W           W",
            "W         W     GB    W",
            "W         W           W",
            "WWWWWWWWWWWWWWWWWWWWWWW"]
    ];
}
