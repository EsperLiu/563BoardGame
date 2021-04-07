using System;
using System.Dynamic;

namespace BoardGameDesign
{
    public abstract class ComputerStrategy
    {
        protected string Mode { get; set; }

        protected abstract string GenerateMove();
    }

    public class C4ComputerStrategy : ComputerStrategy
    {

        public C4ComputerStrategy(string mode)
        {
            Mode = mode;
        }

        protected override string GenerateMove()
        {
            if (Mode == "RANDOM")
            {
                Random rand = new Random();
                return rand.Next(0, 6).ToString();
            }

            return "0";
        }
    }


}