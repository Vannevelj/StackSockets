using System;
using Newtonsoft.Json;

namespace Library.Utilities
{
    internal class EpochTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            long timestamp;
            long.TryParse(reader.Value.ToString(), out timestamp);
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}