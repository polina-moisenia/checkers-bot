namespace CheckersBot.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
    }
}
