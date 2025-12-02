namespace Sokoban
{
    internal class Program
    {
        enum MapSize : int // Object의 움직임을 제한하는 범위
        {
            MinPosX = 0,
            MinPosY = 0,
            MaxPosX = 20,
            MaxPosY = 20
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

            (char drawChar, (int x, int y) pos)[] ObjArr = new (char, (int, int))[] //맵은 제미나이가 만들어 줬습니다.
            {
                // 플레이어 (P)
                ('P', (10, 10)),

                // 상단 벽 (y = 4, x = 4 to 14)
                ('W', (4, 4)), ('W', (5, 4)), ('W', (6, 4)), ('W', (7, 4)), ('W', (8, 4)), ('W', (9, 4)), ('W', (10, 4)), ('W', (11, 4)), ('W', (12, 4)), ('W', (13, 4)), ('W', (14, 4)),

                // 하단 벽 (y = 14, x = 4 to 14)
                ('W', (4, 14)), ('W', (5, 14)), ('W', (6, 14)), ('W', (7, 14)), ('W', (8, 14)), ('W', (9, 14)), ('W', (10, 14)), ('W', (11, 14)), ('W', (12, 14)), ('W', (13, 14)), ('W', (14, 14)),

                // 좌측 벽 (x = 4, y = 5 to 13)
                ('W', (4, 5)), ('W', (4, 6)), ('W', (4, 7)), ('W', (4, 8)), ('W', (4, 9)), ('W', (4, 10)), ('W', (4, 11)), ('W', (4, 12)), ('W', (4, 13)),

                // 우측 벽 (x = 14, y = 5 to 13)
                ('W', (14, 5)), ('W', (14, 6)), ('W', (14, 7)), ('W', (14, 8)), ('W', (14, 9)), ('W', (14, 10)), ('W', (14, 11)), ('W', (14, 12)), ('W', (14, 13)),

                // 내부 벽 (W) - 미로처럼 배치
                ('W', (6, 6)), ('W', (7, 6)), ('W', (8, 6)),
                ('W', (8, 7)),
                ('W', (6, 8)),
                ('W', (12, 12)), ('W', (12, 11)), ('W', (12, 10)),

                // 상자 (B)
                ('B', (8, 10)),
                ('B', (12, 6)),
                ('B', (7, 11)),
                ('B', (11, 8)),

                // 목표 지점 (G)
                ('G', (5, 13)),
                ('G', (13, 5)),
                ('G', (9, 9)),
                ('G', (11, 6)),

                // 새로운 기능
            };

            bool isPlayerMove = true; // 플레이어가 움직일지
            bool isBoxMove = true; // 박스가 움직일지

            ConsoleKeyInfo cKey;

            int goalPoint = 0; // goal 갯수
            foreach ((char drawChar, (int x, int y) pos) obj in ObjArr)
            {
                if (obj.drawChar == 'G')
                    goalPoint++;
            }
            int goalCount = 0; // goal에 도달한 box의 갯수

            while (true) // 무한 루프
            {
                // 좌표에 오브젝트 그림
                foreach ((char drawChar, (int x, int y) pos) obj in ObjArr)
                {
                    PrintObject(obj);
                }
                //플레이어 가장 위에 그림
                PrintObject(ObjArr[0]);

                // 키를 입력
                cKey = Console.ReadKey();

                Console.Clear();

                // 플레이어 이동위치 지정 playerNewPosX, playerNewPosY
                (int x, int y) playerNewPos = MoveObjectPos(cKey, ObjArr[0]);

                for (int i = 1; i < ObjArr.Length; i++)
                {
                    if (ObjArr[i].pos == playerNewPos)// obj와 플레이어 충돌
                    {
                        switch (ObjArr[i].drawChar) //충돌한 obj에 따른 case
                        {
                            case 'P':
                                continue;
                            case 'W':
                                isPlayerMove = false;
                                continue;
                            case 'B':
                                (int x, int y) boxNewPos = MoveObjectPos(cKey, ObjArr[i]);
                                for (int j = 1; j < ObjArr.Length; j++)
                                {
                                    if (ObjArr[j].pos == boxNewPos) // 박스가 다른 오브젝트와 충돌하면
                                    {
                                        if (ObjArr[j].drawChar == 'G')// 박스 골 충돌
                                        {
                                            goalCount++;
                                            Console.Write(goalCount);
                                            ObjArr[i] = ('O', ObjArr[j].pos);
                                            ObjArr[j] = ('O', ObjArr[j].pos);
                                        }
                                        else
                                        {
                                            isPlayerMove = false;
                                            isBoxMove = false;
                                        }
                                        continue;
                                    }
                                }
                                if (isBoxMove)
                                {
                                    ObjArr[i] = (ObjArr[i].drawChar, boxNewPos);
                                    ObjArr[0] = (ObjArr[0].drawChar, playerNewPos);
                                }
                                continue;
                            case 'G':
                                continue;
                            case 'O':
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
                    Console.Write("모든 골 넣음");

                    Console.SetCursorPosition(20, 20);
                    Console.Write("종료하려면 아무키나 누르세요");
                    Console.ReadKey();
                    break;
                }

            }
        }

        // 오브젝트 프린트
        static void PrintObject((char drawChar, (int x, int y) pos) obj)
        {
            Console.SetCursorPosition(obj.pos.x, obj.pos.y);
            Console.Write(obj.drawChar);
        }
        // 키입력 방향 이동
        static (int x, int y) MoveObjectPos(ConsoleKeyInfo key, (char drawChar, (int x, int y) pos) obj)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    obj.pos.x = Math.Max((int)MapSize.MinPosX, obj.pos.x - 1);
                    break;
                case ConsoleKey.RightArrow:
                    obj.pos.x = Math.Min((int)MapSize.MaxPosX, obj.pos.x + 1);
                    break;
                case ConsoleKey.UpArrow:
                    obj.pos.y = Math.Max((int)MapSize.MinPosY, obj.pos.y - 1);
                    break;
                case ConsoleKey.DownArrow:
                    obj.pos.y = Math.Min((int)MapSize.MaxPosY, obj.pos.y + 1);
                    break;
                default:
                    Console.SetCursorPosition(10, 20);
                    Console.WriteLine($"[Error]MoveObject(): 잘못된 방향 입력 [{key.Key}]");
                    break;
            }
            return obj.pos;
        }
    }
}