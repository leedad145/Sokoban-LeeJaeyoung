class GameManager
{
    private GameObject _player = new GameObject();
    private List<GameObject> _boxes = new List<GameObject>();
    private List<GameObject> _walls = new List<GameObject>();
    private List<GameObject> _goals = new List<GameObject>();
    private List<GameObject> _goalInBoxes = new List<GameObject>();
    private int _goalPoint;
    public GameManager()
    {
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Title = "My Sokoban";
        Console.CursorVisible = false;
        Console.Clear();
    }
    public void DoGame()
    {
        foreach(string[] stage in Stage._stage)
        {
            StageBindding(stage);
            Update();
            while (IsRun())
            {
                ConsoleKeyInfo keyInfoy = Console.ReadKey();
                Update(keyInfoy);
            }
        }
        EndGame();
        ////////////////////////////////////////////////////////
        void StageBindding(string[] stage)
        {
            _player = new GameObject();
            _boxes = new List<GameObject>();
            _walls = new List<GameObject>();
            _goals = new List<GameObject>();
            _goalInBoxes = new List<GameObject>();
            for (int i = 0; i < stage.Length; i++)
            {
                for(int j = 0; j < stage[i].Length; j++)
                {
                    switch (stage[i][j])
                    {
                        case 'P':
                            _player = new GameObject(new Position(j, i), Symbols.Player);
                            break;
                        case 'B':
                            _boxes.Add(new GameObject(new Position(j, i), Symbols.Box));
                            break;
                        case 'W':
                            _walls.Add(new GameObject(new Position(j, i), Symbols.Wall));
                            break;
                        case 'G':
                            _goals.Add(new GameObject(new Position(j, i), Symbols.Goal));
                            break;
                    }
                }
            }
            if(_goals.Count != _boxes.Count)
                throw new Exception("박스와 목표지점의 수가 다릅니다.");

            _goalPoint = _goals.Count;
        }
        bool IsRun()
        {
            if(_goalPoint == _goalInBoxes.Count)
                return false;
            return true;
        }
        void Update(ConsoleKeyInfo keyInfo = default)
        {
            Direction dir = DirectionExtensions.FromKey(keyInfo.Key);
            if(_player.TryMove(dir, CanMovePlayer))
                Draw();
            ////////////////////////////////////////////////////
            void Draw()
            {
                Console.Clear();
                Console.SetCursorPosition(0,0);
                Console.Write($"현재 점수: {_goalInBoxes.Count}/{_goalPoint}");
                DrawObjects(_boxes);
                DrawObjects(_walls);
                DrawObjects(_goals);
                DrawObjects(_goalInBoxes);
                DrawObject(_player);
                /////////////////////////////////////////////////
                void DrawObjects(List<GameObject> objects)
                {
                    foreach(GameObject obj in objects)
                        DrawObject(obj);
                }

                void DrawObject(GameObject obj)
                {
                    Console.SetCursorPosition(obj.Pos.X, obj.Pos.Y + 1); // 점수표시를 위해 한 칸 내림
                    Console.Write((char)obj.Symbol);
                }
            }
            bool CanMovePlayer(Position nextPos)
            {
                if(_walls.ExistsAt(nextPos))                            // 다음위치에 벽이 있다면 움직이지마
                    return false;

                GameObject? targetBox = _boxes.GetObj(nextPos);
                if(targetBox != null)                                   // 다음 위치가 박스라면
                {
                    if(targetBox.TryMove(dir, CanMoveBox))              // 박스 움직여보고 움직이면 같이 움직여
                    {
                        CheckGoalIn(targetBox);                         //박스가 골에 들어갔는지 확인해
                        return true;
                    }
                    return false;
                }
                return true;
                ////////////////////////////////////////////////////////
                bool CanMoveBox(Position nextPos)
                {
                    if(_walls.ExistsAt(nextPos) || _boxes.ExistsAt(nextPos)) // 벽이랑 박스가 아니면 움직여
                        return false;
                    
                    return true;
                }
                void CheckGoalIn(GameObject box)
                {
                    GameObject? goal = _goals.GetObj(box.Pos); // 위치가 일치한가
                    if(goal != null)
                    {
                        _goalInBoxes.Add(new GameObject(box.Pos, Symbols.GoalInBox));
                        _boxes.Remove(box);
                        _goals.Remove(goal);
                    }
                }
            }
        }
        void EndGame()
        {
            Console.Clear();
            Console.SetCursorPosition(10, 2);
            Console.Write("게임이 끝났습니다.");
        }
    }
}