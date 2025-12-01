namespace Sokoban
{
    internal class Program
    {
        enum MapSize : int // Object의 움직임을 제한하는 범위
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
            (int x, int y) playerPos = (10, 5);
            (int x, int y)[] wallPos = { (4, 2), (5, 2), (6, 2) };
            (int x, int y)boxPos = (8, 6);
            (int x, int y)goalPos = (10, 10);

            ConsoleKeyInfo cKey;

            while (true) // 무한 루프
            {
                // 좌표에 오브젝트 그림
                Console.SetCursorPosition(playerPos.x, playerPos.y);
                Console.Write(cPlayer);
                foreach((int x, int y)pos in wallPos)
                {
                    Console.SetCursorPosition(pos.x, pos.y);
                    Console.Write(cWall);
                }
                Console.SetCursorPosition(boxPos.x, boxPos.y);
                Console.Write(cBox);
                Console.SetCursorPosition(goalPos.x, goalPos.y);
                Console.Write(cGoal);

                // 키를 입력
                cKey = Console.ReadKey();

                Console.Clear();

                // 플레이어 이동위치 지정 playerNewPosX, playerNewPosY
                (int x, int y) playerNewPos = MoveObjectPos(cKey, playerPos);
                
                // 벽과 플레이어 충돌
                if (wallPos.Contains(playerNewPos))
                {
                    Console.SetCursorPosition(10, 20);
                    Console.Write("플레이어 벽 충돌");
                }
                // 박스와 플레이어 충돌
                else if (boxPos == playerNewPos)
                {
                    // 박스 이동위치 지정 boxNewPosX, boxNewPosY
                    (int x, int y) boxNewPos = MoveObjectPos(cKey, boxPos);

                    // 박스가 벽과 충돌
                    if (wallPos.Contains(boxNewPos))
                    {
                        Console.SetCursorPosition(10, 20);
                        Console.Write("박스 벽 충돌");
                    }
                    // 박스가 테두리에 도달
                    // 범위 제한이 있기 때문에 boxNewPos와 playerNewPos가 같을 때 테두리에 위치함
                    else if (boxNewPos == playerNewPos)
                    {
                        Console.SetCursorPosition(10, 20);
                        Console.Write("박스 테두리 충돌");
                    }
                    else // 박스와 함께 이동
                    {
                        boxPos = boxNewPos;
                        playerPos = playerNewPos;

                        // 박스 골 충돌
                        if (goalPos == boxPos)
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
                    playerPos = playerNewPos;
                }
            }
        }

        // 키입력 방향 이동
        static (int x, int y) MoveObjectPos(ConsoleKeyInfo key, (int x, int y) pos)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    pos.x = Math.Max((int)MapSize.MinPosX, pos.x - 1);
                    break;
                case ConsoleKey.RightArrow:
                    pos.x = Math.Min((int)MapSize.MaxPosX, pos.x + 1);
                    break;
                case ConsoleKey.UpArrow:
                    pos.y = Math.Max((int)MapSize.MinPosY, pos.y - 1);
                    break;
                case ConsoleKey.DownArrow:
                    pos.y = Math.Min((int)MapSize.MaxPosY, pos.y + 1);
                    break;
                default:
                    Console.SetCursorPosition(10, 20);
                    Console.WriteLine($"[Error]MoveObject(): 잘못된 방향 입력 [{key.Key}]");
                    break;
            }
            return pos;
        }
    }
}