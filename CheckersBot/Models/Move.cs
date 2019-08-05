using Newtonsoft.Json;

namespace CheckersBot.Models
{
    public class Move
    {
        [JsonProperty("from")]
        public int StartingPoint { get; set; }
        [JsonProperty("to")]
        public int EndingPoint { get; set; }
    }
}
