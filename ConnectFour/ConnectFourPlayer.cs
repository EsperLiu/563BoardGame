using System;
using BoardGameFramework;

namespace ConnectFour
{
    public abstract class ConnectFourPlayer : Player
    {
        public char Token { get; set; }

        public string _color;
        public string Color
        {
            get => _color;
            set
            {
                if (value == "Yellow")
                {
                    Token = '○';
                    _color = value;
                    Id = 1;
                }
                else if (value == "Red")
                {
                    Token = '●';
                    _color = value;
                    Id = 2;
                }
            }
        }

        protected ConnectFourPlayer(string name) : base(name)
        {

        }

        protected ConnectFourPlayer(string name, string color) : base(name)
        {
            Color = color;
        }

        public override string ToString()
        {
            return $"{Name}({Token})";
        }
    }
}