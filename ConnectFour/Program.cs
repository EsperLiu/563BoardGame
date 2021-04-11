using System;
using BoardGameFramework;

namespace ConnectFour
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("**********************************************");
            Console.WriteLine("*                                            *");
            Console.WriteLine("*          Connect-Four Game by Tao          *");
            Console.WriteLine("*    Developed with IFN563 Game Framework    *");
            Console.WriteLine("*                                            *");
            Console.WriteLine("**********************************************");
            Console.WriteLine();
            Game game = new Connect4Game();
            game.Configure();
            while (true) game.Play();
        }
    }
}