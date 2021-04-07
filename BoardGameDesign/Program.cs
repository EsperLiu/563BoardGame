using System;

namespace BoardGameDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**********************************************");
            Console.WriteLine("*                                            *");
            Console.WriteLine("*          Connect-Four Game by Tao          *");
            Console.WriteLine("*    Developed with IFN563 Game Framework    *");
            Console.WriteLine("*                                            *");
            Console.WriteLine("**********************************************");
            Console.WriteLine();
            Game game = new Connect4Game("Connect-4");
            game.Configure();
            while (true)
            {
                game.Play();
            }
        }
    }
}
