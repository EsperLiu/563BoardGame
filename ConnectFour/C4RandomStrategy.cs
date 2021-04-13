using BoardGameFramework;
using System;

namespace ConnectFour
{
    public class C4RandomStrategy : IComputerStrategy
    {
        public C4RandomStrategy()
        {
        }

        public Move GenerateMove(Board board, Player player)
        {
            {
                var possibleColumns = (board as ConnectFourBoard).OpenColumns.ToArray();
                var rand = new Random();
                int chosen = rand.Next(0, possibleColumns.Length);
                string command = possibleColumns[chosen].ToString();
                ConnectFourMove move = new ConnectFourMove(command, player.Id);
                return move;
            }
        }
    }
}