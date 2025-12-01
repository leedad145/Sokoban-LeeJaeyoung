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
                ('P', (10, 5)),
                ('W', (4, 2)), ('W', (5, 2)), ('W', (6, 2)) ,
                ('B', (8, 6)) ,('B', (6, 4)) ,('B', (8, 8)) ,
                ('G', (10, 10))
            };

            bool isPlayerMove = true;
            bool isBoxMove = true;
            ConsoleKeyInfo cKey;

            while (true) // 무한 루프
            {
                // 좌표에 오브젝트 그림
                foreach((char drawChar, (int x, int y) pos)obj in ObjArr)
                {
                    Console.SetCursorPosition(obj.pos.x, obj.pos.y);
                    Console.Write(obj.drawChar);
                }
                
                // 키를 입력
                cKey = Console.ReadKey();

                Console.Clear();

                // 플레이어 이동위치 지정 playerNewPosX, playerNewPosY
                (int x, int y) playerNewPos = MoveObjectPos(cKey, ObjArr[0].pos);
                
                for(int i = 1; i < ObjArr.Length - 1; i++)
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
                                for(int j = 1; j < ObjArr.Length - 1; j++)
                                {
                                    if (boxNewPos == ObjArr[j].pos) // 박스가 다른 오브젝트와 충돌하면
                                    {
                                        isBoxMove = false;
                                        Console.SetCursorPosition(10, 20);
                                        Console.Write($"박스 오브젝트 충돌{ObjArr[j]}");
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
                if(isPlayerMove)
                    ObjArr[0].pos = playerNewPos;

                isBoxMove = true;
                isPlayerMove = true;
                /*
                // 벽과 플레이어 충돌
                if (ObjArrArr[(int)Obj.Wall].Contains(ObjArrArr[(int)Obj.Player][0].pos))
                {
                    Console.SetCursorPosition(10, 20);
                    Console.Write("플레이어 벽 충돌");
                }
                // 박스와 플레이어 충돌
                else if (boxPos == playerNewPos)
                {
                    // 박스 이동위치 지정 boxNewPosX, boxNewPosY
                    (int x, int y) boxNewPos = MoveObjectPos(cKey, ObjArrArr[(int)Obj.Box][0].pos);

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
                        ObjArrArr[(int)Obj.Box][0].pos = boxNewPos;
                        ObjArrArr[(int)Obj.Player][0].pos = playerNewPos;

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
                    ObjArrArr[(int)Obj.Player][0].pos = playerNewPos;
                }
                */
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