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

            (char drawChar, (int x, int y) pos)[] ObjArr = new (char, (int, int))[]
            {
                // P: 플레이어 (Player)
                ('P', (5, 5)), 

                // W: 벽 (Wall) - 외곽과 중앙 장애물
                ('W', (0, 0)), ('W', (1, 0)), ('W', (2, 0)), ('W', (3, 0)), ('W', (4, 0)), ('W', (5, 0)), ('W', (6, 0)), ('W', (7, 0)), ('W', (8, 0)), ('W', (9, 0)),
                ('W', (0, 1)), ('W', (9, 1)),
                ('W', (0, 2)), ('W', (3, 2)), ('W', (9, 2)),
                ('W', (0, 3)), ('W', (3, 3)), ('W', (9, 3)),
                ('W', (0, 4)), ('W', (9, 4)),
                ('W', (0, 5)), ('W', (9, 5)),
                ('W', (0, 6)), ('W', (9, 6)),
                ('W', (0, 7)), ('W', (9, 7)),
                ('W', (0, 8)), ('W', (9, 8)),
                ('W', (0, 9)), ('W', (1, 9)), ('W', (2, 9)), ('W', (3, 9)), ('W', (4, 9)), ('W', (5, 9)), ('W', (6, 9)), ('W', (7, 9)), ('W', (8, 9)), ('W', (9, 9)),
    
                // B: 박스 (Box)
                ('B', (2, 2)),
                ('B', (7, 7)), 

                // G: 골 (Goal)
                ('G', (1, 7)),
                ('G', (8, 2))
            };

            bool isPlayerMove = true;
            bool isBoxMove = true;
            ConsoleKeyInfo cKey;
            int goalPoint = 2;
            int goalCount = 0;
            while (true) // 무한 루프
            {
                // 좌표에 오브젝트 그림
                foreach ((char drawChar, (int x, int y) pos) obj in ObjArr)
                {
                    Console.SetCursorPosition(obj.pos.x, obj.pos.y);
                    Console.Write(obj.drawChar);
                }

                // 키를 입력
                cKey = Console.ReadKey();

                Console.Clear();

                // 플레이어 이동위치 지정 playerNewPosX, playerNewPosY
                (int x, int y) playerNewPos = MoveObjectPos(cKey, ObjArr[0].pos);

                for (int i = 1; i < ObjArr.Length - 1; i++)
                {
                    if (ObjArr[i].pos == playerNewPos)// obj와 플레이어 충돌
                    {
                        isPlayerMove = false;
                        switch (ObjArr[i].drawChar)
                        {
                            case 'P':
                                continue;
                            case 'W':
                                Console.SetCursorPosition(10, 20);
                                Console.Write("플레이어 벽 충돌");
                                continue;
                            case 'B':
                                (int x, int y) boxNewPos = MoveObjectPos(cKey, ObjArr[i].pos);
                                for (int j = 1; j < ObjArr.Length - 1; j++)
                                {
                                    if (boxNewPos == ObjArr[j].pos) // 박스가 다른 오브젝트와 충돌하면
                                    {
                                        if (ObjArr[j].drawChar == 'G')// 박스 골 충돌
                                        {
                                            goalCount++;
                                            Console.SetCursorPosition(5, 5);
                                            Console.Write("박스 골 충돌");
                                        }
                                        else
                                        {
                                            isBoxMove = false;
                                            Console.SetCursorPosition(10, 20);
                                            Console.Write($"박스 오브젝트 충돌{ObjArr[j]}");
                                        }
                                    }
                                    else if (boxNewPos == playerNewPos) // 박스가 테두리와 충돌하면
                                    {
                                        isBoxMove = false;
                                        Console.SetCursorPosition(10, 21);
                                        Console.Write("박스 테두리 도달");
                                    }
                                }
                                if (isBoxMove)
                                {
                                    ObjArr[i].pos = boxNewPos;
                                    ObjArr[0].pos = playerNewPos;
                                }
                                continue;
                            case 'G':
                                continue;
                        }
                    }
                }
                if (isPlayerMove)
                    ObjArr[0].pos = playerNewPos;

                isBoxMove = true;
                isPlayerMove = true;
                // 모든 골안에 박스가 들어있는지 확인
                if (goalPoint == goalCount) 
                {
                    Console.SetCursorPosition(5, 5);
                    Console.Write("박스 골 충돌");

                    Console.SetCursorPosition(20, 20);
                    Console.Write("종료하려면 아무키나 누르세요");
                    Console.ReadKey();
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