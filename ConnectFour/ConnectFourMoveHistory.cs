using System;
using System.Collections.Generic;
using BoardGameFramework;

namespace ConnectFour
{
    public class ConnectFourMoveHistory : MoveHistory
    {
        public ConnectFourMoveHistory() : base()
        {

        }

        public override Board ReconstructBoard(int moveNumber)
        {
            ConnectFourBoard ConstructedBoard = new ConnectFourBoard(7,6);
            int moveCount = 0;
            int playerId = 1;
            foreach (ConnectFourMove move in MoveList.GetRange(0, moveNumber))
            {
                moveCount++;
                playerId = (moveCount % 2 == 1) ? 1 : 2;
                ConstructedBoard.ExecuteMove(move, Utils.GetPlayerById(Connect4Game.Instance.Players, playerId));
            }

            return ConstructedBoard;
        }

        public void AppendMove(string command)
        {
            int colorId = 1;
            if (MoveList.Count % 2 == 0)
            {
                colorId = 1;
            }
            else
            {
                colorId = 2;
            }
            ConnectFourMove move = new ConnectFourMove(command, colorId);
            MoveList.Add(move);
        }
    }
}