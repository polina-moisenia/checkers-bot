namespace CheckersBot.Helpers
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
    }
}
