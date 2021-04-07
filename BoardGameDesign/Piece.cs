namespace BoardGameDesign
{
    public abstract class Piece
    {
        public char Token;
        public Player Owner;

        protected Piece(Player owner, char token)
        {
            Owner = owner;
            Token = token;
        }
    }

    public class ConnectFourPiece:Piece
    {
        public Square Location;
        public ConnectFourPiece(Player owner, char token, Square square) : base(owner, token)
        {
            Location = square;
        }
    }
}