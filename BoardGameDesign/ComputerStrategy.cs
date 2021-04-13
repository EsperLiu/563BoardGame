namespace BoardGameFramework
{
    public interface IComputerStrategy
    {
        Move GenerateMove(Board board, Player player);
    }
}