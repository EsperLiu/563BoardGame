using System;
using System.Runtime.InteropServices;

namespace BoardGameDesign
{
    public abstract class Player
    {
        public string Name;
        public char Stone;

        protected Player(string name, char stone)
        {
            Name = name;
            Stone = stone;
        }

        public abstract string MakeMove();
    }

    public abstract class ConnectFourPlayer : Player
    {
        protected ConnectFourPlayer(string name, char stone):base(name, stone)
        {
        }
    }

    public class HumanC4Player: ConnectFourPlayer
    {

        public HumanC4Player(string name, char stone) : base(name, stone)
        { }

        
        public override string ToString()
        {
            return $"{Name}({Stone})";
        }

        public override string MakeMove()
        {
            Console.WriteLine($"{this} to make a move: ");
            string m = Console.ReadLine();
            return m;
        }
    }

    public class ComputerC4Player : ConnectFourPlayer
    {
        private C4ComputerStrategy Strategy { get; }

        public ComputerC4Player(string name, char stone, C4ComputerStrategy strategy) : base(name, stone)
        {
            Strategy = strategy;
        }

        public override string ToString()
        {
            return $"{Name}({Stone})";
        }

        public override string MakeMove()
        {
            Random rand = new Random();
            int m = rand.Next(0, 6);
            Console.WriteLine($"{Name} made a move: [Column {m}] ");
            return m.ToString();
        }
    }
}