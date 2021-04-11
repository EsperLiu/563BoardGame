using System;
using BoardGameFramework;

namespace ConnectFour
{
    public class HumanC4Player : ConnectFourPlayer
    {
        public HumanC4Player(string name) : base(name)
        {

        }
        public HumanC4Player(string name, string color) : base(name, color)
        {
        }

        public override Move MakeMove(Board board)
        {
            var oc = (board as ConnectFourBoard).OpenColumns;
            string input = "";
            bool legalMove = false;
            while (!legalMove)
            {
                Console.WriteLine($"{this} to make a move. Legal moves are: Columns " 
                                  + string.Join(", ", oc.ToArray()));
                input = Utils.TakeStringInput(true);
                if (input.StartsWith("$"))
                {
                    CommandHandler.HandleCommand(input);
                } else if (int.TryParse(input, out int targetColumn)) {
                    foreach (int i in oc)
                    {
                        if (i == targetColumn)
                        {
                            legalMove = true;
                            break;
                        }
                    }
                }
            }
            return new ConnectFourMove(input, Id);
        }
    }
}