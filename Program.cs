namespace Sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 콘솔 초기화
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Title = "My Sokoban";
            Console.CursorVisible = false;
            Console.Clear();

            // 변수 초기화
            char cPlayer = 'P';
            char cWall = 'W';
            char cBox = 'B';
            char cGoal = 'G';

            int playerPosX = 10;
            int playerPosY = 5;

            int wallPosX = 4;
            int wallPosY = 2;

            int boxPosX = 8;
            int boxPosY = 6;

            int goalPosX = 10;
            int goalPosY = 10;
            // 맵 사이즈
            int minPosX = 0;
            int minPosY = 0;
            int maxPosX = 20;
            int maxPosY = 10;

            // 벽 플레이어 충돌 감지 확인
            bool isSamePlayerXAndWallX = playerPosX == wallPosX;
            bool isSamePlayerYAndWallY = playerPosY == wallPosY;
            bool isCollidedPlayerWithWall = isSamePlayerXAndWallX && isSamePlayerYAndWallY;

            // 박스 플레이어 충돌 감지 확인
            bool isSamePlayerXAndBoxX = playerPosX == boxPosX;
            bool isSamePlayerYAndBoxY = playerPosY == boxPosY;
            bool isCollidedPlayerWithBox = isSamePlayerXAndBoxX && isSamePlayerYAndBoxY;

            // 박스 벽 충돌 감지 확인
            bool isSameWallXAndBoxX = wallPosX == boxPosX;
            bool isSameWallYAndBoxY = wallPosY == boxPosY;
            bool isCollidedWallWithBox = isSameWallXAndBoxX && isSameWallYAndBoxY;

            // 골 박스 충돌 감지 확인
            bool isSameGoalXAndBoxX = goalPosX == boxPosX;
            bool isSameGoalYAndBoxY = goalPosY == boxPosY;
            bool isCollidedGoalWithBox = isSameGoalXAndBoxX && isSameGoalYAndBoxY;


            ConsoleKeyInfo cKey;

            // 벽 매핑
            // int[,] wallMapping = new int[maxPosX - minPosX, maxPosY - minPosY];



            while (true) // 무한 루프
            {
                // Player를 좌표에 그림
                Console.SetCursorPosition(playerPosX, playerPosY);
                Console.Write(cPlayer);
                // 벽 그림
                Console.SetCursorPosition(wallPosX, wallPosY);
                Console.Write(cWall);
                // 박스 그림
                Console.SetCursorPosition(boxPosX, boxPosY);
                Console.Write(cBox);
                // 골 그림
                Console.SetCursorPosition(goalPosX, goalPosX);
                Console.Write(cGoal);

                // 키를 입력받아 Player의 좌표를 변경함
                cKey = Console.ReadKey();
                Console.Clear();
                switch (cKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        playerPosX = Math.Max(minPosX, playerPosX - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        playerPosX = Math.Min(maxPosX, playerPosX + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        playerPosY = Math.Max(minPosY, playerPosY - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        playerPosY = Math.Min(maxPosY, playerPosY + 1);
                        break;
                }
                // 벽 플레이어 충돌 감지 확인
                isSamePlayerXAndWallX = playerPosX == wallPosX;
                isSamePlayerYAndWallY = playerPosY == wallPosY;
                isCollidedPlayerWithWall = isSamePlayerXAndWallX && isSamePlayerYAndWallY;

                if (isCollidedPlayerWithWall)
                {
                    Console.SetCursorPosition(20, 20);
                    Console.Write("벽 플레이어 충돌");

                    switch (cKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            playerPosX++;
                            break;
                        case ConsoleKey.RightArrow:
                            playerPosX--;
                            break;
                        case ConsoleKey.UpArrow:
                            playerPosY++;
                            break;
                        case ConsoleKey.DownArrow:
                            playerPosY--;
                            break;
                    }
                }
                // 박스 플레이어 충돌 감지 확인
                isSamePlayerXAndBoxX = playerPosX == boxPosX;
                isSamePlayerYAndBoxY = playerPosY == boxPosY;
                isCollidedPlayerWithBox = isSamePlayerXAndBoxX && isSamePlayerYAndBoxY;
                
                if (isCollidedPlayerWithBox)
                {
                    Console.SetCursorPosition(20, 21);
                    Console.Write("박스 플레이어 충돌");

                    switch (cKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            boxPosX = Math.Max(minPosX, boxPosX - 1);
                            break;
                        case ConsoleKey.RightArrow:
                            boxPosX = Math.Min(maxPosX, boxPosX + 1);
                            break;
                        case ConsoleKey.UpArrow:
                            boxPosY = Math.Max(minPosY, boxPosY - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            boxPosY = Math.Min(maxPosY, boxPosY + 1);
                            break;
                    }
                }
                // 박스 벽 충돌 감지 확인
                isSameWallXAndBoxX = wallPosX == boxPosX;
                isSameWallYAndBoxY = wallPosY == boxPosY;
                isCollidedWallWithBox = isSameWallXAndBoxX && isSameWallYAndBoxY;

                if (isCollidedWallWithBox)
                {
                    Console.SetCursorPosition(20, 22);
                    Console.Write("박스 벽 충돌");

                    switch (cKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            boxPosX++;
                            playerPosX++;
                            break;
                        case ConsoleKey.RightArrow:
                            boxPosX--;
                            playerPosX--;
                            break;
                        case ConsoleKey.UpArrow:
                            boxPosY++;
                            playerPosY++;
                            break;
                        case ConsoleKey.DownArrow:
                            boxPosY--;
                            playerPosY--;
                            break;
                    }
                }
                // 골 박스 충돌 감지 확인
                isSameGoalXAndBoxX = goalPosX == boxPosX;
                isSameGoalYAndBoxY = goalPosY == boxPosY;
                isCollidedGoalWithBox = isSameGoalXAndBoxX && isSameGoalYAndBoxY;
                if (isCollidedGoalWithBox)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.Write("박스 골 충돌");
                    Console.SetCursorPosition(0, 2);
                    Console.Write("박스 골 충돌");
                    Console.SetCursorPosition(0, 3);
                    Console.Write("박스 골 충돌");
                    Console.SetCursorPosition(0, 4);
                    Console.Write("박스 골 충돌"); 
                    Console.SetCursorPosition(0, 5);
                    Console.Write("박스 골 충돌"); 
                    Console.SetCursorPosition(0, 6);
                    Console.Write("박스 골 충돌"); 
                    Console.SetCursorPosition(0, 7);
                    Console.Write("박스 골 충돌");
                }
            }
        }
    }
}