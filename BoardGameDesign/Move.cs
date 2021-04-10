namespace BoardGameFramework
{
    public class Move
    {
        public string Command { get; set; }

        public Move(string command)
        {
            Command = command;
        }
    }
}