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
            int playerPosX = 13;
            int playerPosY = 5;
            int maxPosX = 25;
            int maxPosY = 10;
            ConsoleKeyInfo cKey;

            while(true) // 무한 루프
            {
                //범위 지정 [0 ~ maxPos]
                playerPosX = playerPosX < 0 ? 0 : playerPosX;
                playerPosY = playerPosY < 0 ? 0 : playerPosY;
                playerPosX = playerPosX > maxPosX ? maxPosX : playerPosX;
                playerPosY = playerPosY > maxPosY ? maxPosY : playerPosY;
          
                // Player를 좌표에 그림
                Console.Clear();
                Console.SetCursorPosition(playerPosX, playerPosY);
                Console.Write(cPlayer);

                // 키를 입력받아 Player의 좌표를 변경함
                cKey = Console.ReadKey();
                switch (cKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        playerPosX--;
                        break;
                    case ConsoleKey.RightArrow:
                        playerPosX++;
                        break;
                    case ConsoleKey.UpArrow:
                        playerPosY--;
                        break;
                    case ConsoleKey.DownArrow:
                        playerPosY++;
                        break;
                }
            }
        }
    }
}