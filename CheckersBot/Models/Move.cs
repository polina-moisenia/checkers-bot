using Newtonsoft.Json;

namespace CheckersBot.Models
{
    public class Move
    {
        [JsonProperty("from")]
        public Cell StartingPoint { get; set; }
        [JsonProperty("to")]
        public Cell EndingPoint { get; set; }
    }
}
