using Newtonsoft.Json;

namespace CheckersBot.Models
{
    public class Cell
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
