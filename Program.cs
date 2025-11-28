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


            Console.Write("입력시 종료: ");
            Console.Read();
        }
    }
}