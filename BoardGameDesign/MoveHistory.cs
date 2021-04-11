using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace BoardGameFramework
{
    public abstract class MoveHistory
    {
        public List<Move> MoveList { get; set; }

        protected MoveHistory()
        {
            MoveList = new List<Move>();
        }

        public abstract Board ReconstructPosition(int moveNumber);

        public void AppendMove(Move move)
        {
            MoveList.Add(move);
        }

        public override string ToString()
        {
            string s = "";
            int moveCount = 0;
            foreach (var move in MoveList)
            {
                moveCount++;
                s += $"#{moveCount}: Column {move.Command} \n";
            }
            return s;
        }
    }
}