using System.Collections.Generic;
using Newtonsoft.Json;

namespace CheckersBot.Models
{
    public class BoardModel
    {
        [JsonProperty("team")]
        public Team TeamToMoveNext { get; set; }
        [JsonProperty("field")]
        public List<List<CellState>> Rows { get; set; }
    }
}
