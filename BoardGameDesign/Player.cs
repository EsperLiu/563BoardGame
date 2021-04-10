namespace BoardGameFramework
{
    public abstract class Player
    {
        public string Name;
        public int Id;

        protected Player(string name)
        {
            Name = name;
        }

        public abstract Move MakeMove(Board board);
    }
}