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

        public void AppendMove(Move move)
        {
            MoveList.Add(move);
        }

        public override string ToString()
        {
            string s = "Move History: \n";
            int moveCount = 0;
            foreach (var move in MoveList)
            {
                moveCount++;
                s += $"Move #{moveCount}: C{move.Command} \n";
            }
            return s;
        }

        public string[] ToArray()
        {
            List<string> a = new List<string>();
            foreach (var move in MoveList)
            {
                a.Add(move.Command);
            }

            return a.ToArray();
        }

        public abstract Board ReconstructBoard(int n); // Re-do all moves up to the nth move, then return new board state.
    }
}