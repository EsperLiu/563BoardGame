namespace BoardGameFramework
{
    public abstract class Piece
    {
        public Player Owner;
        public char Token;
        public string Color;

        protected Piece(Player owner, char token)
        {
            Owner = owner;
            Token = token;
        }
    }
}