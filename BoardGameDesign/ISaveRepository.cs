namespace BoardGameFramework
{
    public interface ISaveRepository
    {
        string[] FetchSaves();
        
        bool Save(string fileName);

        MoveHistory Load(string fileName);

    }
}