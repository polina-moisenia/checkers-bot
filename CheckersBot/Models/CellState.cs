using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CheckersBot.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CellState
    {
        [EnumMember(Value = ".")]
        Empty,
        [EnumMember(Value = "w")]
        WhitePiece,
        [EnumMember(Value = "W")]
        WhiteKing,
        [EnumMember(Value = "b")]
        BlackWhite,
        [EnumMember(Value = "B")]
        BlackKing
    }
}
