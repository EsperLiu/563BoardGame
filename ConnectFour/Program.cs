using BoardGameFramework;
using System;

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
            Console.WriteLine(">> Welcome! Remember, you can ask for help at any time with \"/help\" or simply \"/\". ");
            try
            {
                Game game = Connect4Game.Instance;
                game.Configure();
                while (true) game.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
                Console.WriteLine(">> Oops, something went wrong! It seems you just found a bug...");
                Console.WriteLine(">> Press [Enter] to restart the game.");
                Console.ReadLine();
                Main(args);
            }
        }
    }
}