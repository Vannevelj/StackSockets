using System;
using Library.Responses;
using Newtonsoft.Json;

namespace Library.Utilities
{
    internal sealed class DataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Data);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var value = reader.Value as string;
            return JsonConvert.DeserializeObject<Data>(value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}