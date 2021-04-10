using BoardGameFramework;

namespace ConnectFour
{
    public class ConnectFourPiece : Piece
    {
        public Square Location;

        public ConnectFourPiece(Player owner, char token, Square square) : base(owner, token)
        {
            Location = square;
        }
    }
}