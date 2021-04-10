using System;
using BoardGameFramework;

namespace ConnectFour
{
    public class ComputerC4Player : ConnectFourPlayer
    {
        public ComputerC4Player(string name) : base(name)
        {

        }
        public ComputerC4Player(string name, string color, C4RandomStrategy strategy) : base(name, color)
        {
            Strategy = strategy;
        }

        public ComputerStrategy Strategy { get; set; }

        public override Move MakeMove(Board board)
        {
            ConnectFourMove move = Strategy.GenerateMove(board, this) as ConnectFourMove;
            Console.WriteLine($"{Name} made a move: [Column {move.TargetColumn}] ");
            return move;
        }
    }
}