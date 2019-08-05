using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CheckersBot.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Team
    {
        [EnumMember(Value = "b")]
        Black,
        [EnumMember(Value = "w")]
        White
    }
}
