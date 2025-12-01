namespace Sokoban
{
    internal class Program
    {
        enum MapSize : int
        {
            MinPosX = 0,
            MinPosY = 0,
            MaxPosX = 20,
            MaxPosY = 10
        }

        static void Main(string[] args)
        {
            // 콘솔 초기화
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Title = "My Sokoban";
            Console.CursorVisible = false;
            Console.Clear();

            // 그릴 문자 지정
            const char cPlayer = 'P';
            const char cWall = 'W';
            const char cBox = 'B';
            const char cGoal = 'G';

            // 좌표 변수 초기화
            int playerPosX = 10;
            int playerPosY = 5;

            int wallPosX = 4;
            int wallPosY = 2;

            int boxPosX = 8;
            int boxPosY = 6;

            int goalPosX = 10;
            int goalPosY = 10;

            ConsoleKeyInfo cKey;

            while (true) // 무한 루프
            {
                // 좌표에 오브젝트 그림
                Console.SetCursorPosition(playerPosX, playerPosY);
                Console.Write(cPlayer);
                Console.SetCursorPosition(wallPosX, wallPosY);
                Console.Write(cWall);
                Console.SetCursorPosition(boxPosX, boxPosY);
                Console.Write(cBox);
                Console.SetCursorPosition(goalPosX, goalPosX);
                Console.Write(cGoal);

                // 키를 입력
                cKey = Console.ReadKey();

                Console.Clear();

                // 플레이어 이동위치 지정 playerNewPosX, playerNewPosY
                (int playerNewPosX, int playerNewPosY) = MoveObject(cKey, playerPosX, playerPosY);

                // 벽과 플레이어 충돌
                if (CollidedObjects(wallPosX, wallPosY, playerNewPosX, playerNewPosY))
                {
                    //무반응
                    Console.SetCursorPosition(10, 20);
                    Console.Write("플레이어 벽 충돌");
                }
                // 박스와 플레이어 충돌
                else if (CollidedObjects(boxPosX, boxPosY, playerNewPosX, playerNewPosY))
                {
                    // 박스 이동위치 지정 boxNewPosX, boxNewPosY
                    (int boxNewPosX, int boxNewPosY) = MoveObject(cKey, boxPosX, boxPosY);

                    // 박스가 벽과 충돌
                    if (CollidedObjects(wallPosX, wallPosY, boxNewPosX, boxNewPosY))
                    {
                        //무반응
                        Console.SetCursorPosition(10, 20);
                        Console.Write("박스 벽 충돌");
                    }
                    else // 박스와 함께 이동
                    {
                        (boxPosX, boxPosY) = (boxNewPosX, boxNewPosY);
                        (playerPosX, playerPosY) = (playerNewPosX, playerNewPosY);

                        // 박스 골 충돌
                        if (CollidedObjects(goalPosX, goalPosY, boxPosX, boxPosY))
                        {
                            Console.SetCursorPosition(5, 5);
                            Console.Write("박스 골 충돌");

                            Console.SetCursorPosition(20, 20);
                            Console.Write("종료하려면 아무키나 누르세요");
                            Console.ReadKey();
                            break;
                        }
                    }
                }
                else // 이동
                {
                    (playerPosX, playerPosY) = (playerNewPosX, playerNewPosY);
                }
            }
        }

        // Obj1과 Obj2의 충돌 판단
        static bool CollidedObjects(int obj1X, int obj1Y, int obj2X, int obj2Y)
        {
            bool isSameObj1XAndObj2X = obj1X == obj2X;
            bool isSameObj1YAndObj2Y = obj1Y == obj2Y;
            return isSameObj1XAndObj2X && isSameObj1YAndObj2Y;
        }

        // 키입력 방향 이동
        static (int x, int y) MoveObject(ConsoleKeyInfo key, int x, int y)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    x = Math.Max((int)MapSize.MinPosX, x - 1);
                    break;
                case ConsoleKey.RightArrow:
                    x = Math.Min((int)MapSize.MaxPosX, x + 1);
                    break;
                case ConsoleKey.UpArrow:
                    y = Math.Max((int)MapSize.MinPosY, y - 1);
                    break;
                case ConsoleKey.DownArrow:
                    y = Math.Min((int)MapSize.MaxPosY, y + 1);
                    break;
                default:
                    Console.SetCursorPosition(10, 20);
                    Console.WriteLine($"[Error]MoveObject(): 잘못된 방향 입력 [{key.Key}]");
                    break;
            }
            return (x, y);
        }
    }
}