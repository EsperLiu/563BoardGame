namespace BoardGameFramework
{
    public abstract class ComputerStrategy
    {
        protected string Name { get; set; }

        public abstract Move GenerateMove(Board board, Player player);
    }
}