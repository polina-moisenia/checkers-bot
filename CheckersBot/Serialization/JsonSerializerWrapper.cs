using System.IO;
using Newtonsoft.Json;

namespace CheckersBot.Serialization
{
    public class JsonSerializerWrapper : ISerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonSerializerWrapper()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, _serializerSettings);
        }

        public T Deserialize<T>(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                return serializer.Deserialize<T>(reader);
            }
        }

        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, _serializerSettings);
        }
    }
}
