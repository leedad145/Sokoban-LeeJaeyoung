class GameManager
{
    IInputHandler inputHandler;
    IRenderer renderer;
    Map map;
    public GameManager()
    {
        inputHandler = new ConsoleInputHandler();   
        map = new Map();
        renderer = new ConsoleRenderer(map.Objects);

    }
    void ProcessInput() => inputHandler.ProcessInput();
    public void Run()
    {
        foreach(string[] stage in map.Stages)
        {
            map.StageBindding(stage);
            Update();
            while (!map.StageClear())
            {
                ProcessInput();
                Direction direction = inputHandler.GetDirection();
                map.Update(direction);
            }
        }
        EndGame();
        ////////////////////////////////////////////////////////
        void Update()
        {
            map.Update();
            Render();
            ////////////////////////////////////////////////////
            void Render()
            {
                renderer.Clear();
                renderer.Render();
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