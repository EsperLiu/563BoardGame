using System;

namespace ConnectFour
{
    public static class CommandHandler
    {
        public static void HandleCommand(string input)
        {
            if (input.ToUpper() == "$HELP")
            {
                Help();
            }
        }
        public static void Help()
        {
            Console.WriteLine("**************************");
            Console.WriteLine("This is the help document.");
            Console.WriteLine("**************************");
        }
    }
}