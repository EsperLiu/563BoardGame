using System;
using System.Runtime.InteropServices.ComTypes;
using BoardGameFramework;

namespace ConnectFour
{
    public class ConnectFourMove : Move
    {
        public int ColorId { get; set; }
        public int TargetColumn { get; set; }

        public ConnectFourMove(string command, int colorId) : base(command)
        {
            ColorId = colorId;
            int.TryParse(Command, out int result);
            TargetColumn = result;
        }

    }
}