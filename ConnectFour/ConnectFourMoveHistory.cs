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
    }
}