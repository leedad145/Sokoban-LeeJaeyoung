namespace Sokoban
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            색깔 조정
            제목 수정 
            커서 숨김
            */

            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Title = "My Sokoban";
            Console.CursorVisible = false;
            Console.Clear();

            char cPlayer = 'P';
            int playerPosX = 5;
            int playerPosY = 10;
            ConsoleKeyInfo cKey;

            while(true) //무한 루프
            {
                //Player를 그림
                Console.Clear();
                Console.SetCursorPosition(playerPosX, playerPosY);
                Console.Write(cPlayer);

                //키를 입력받아 Player의 좌표를 변경함
                cKey = Console.ReadKey();
                switch (cKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        playerPosX--;
                        break;
                    case ConsoleKey.UpArrow:
                        playerPosY--;
                        break;
                    case ConsoleKey.RightArrow:
                        playerPosX++;
                        break;
                    case ConsoleKey.DownArrow:
                        playerPosY++;
                        break;
                }
            }
        }
    }
}