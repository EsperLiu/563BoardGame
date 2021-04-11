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
                // It is possible at runtime that ActivePlayer changes within this loop
                // This happens when the current ActivePlayer restores the game to a previous position, 
                // in which he/she is no longer the ActivePlayer. 
                // In this case, we return null and let the Game logic handle it.
                if (Connect4Game.Instance.ActivePlayer != this)
                {
                    return null;
                }
                Console.WriteLine($"{this} to make a move. ");
                input = Utils.TakeStringInput(true);
                if (int.TryParse(input, out int targetColumn)) {
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