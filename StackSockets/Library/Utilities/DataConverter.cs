using System;
using Library.Responses;
using Newtonsoft.Json;

namespace Library.Utilities
{
    internal sealed class DataConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Data);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var value = reader.Value as string;

            if (typeof (T) == typeof (QuestionActivityData))
            {
                return GetSpecificQuestionActivity(value);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        private object GetSpecificQuestionActivity(string json)
        {
            // return correct deserialization based on the data
            // try-catch parsing or a Contains() check?

            // return-type to indicate what Activity is being used?

            // Set events in each RequestParameters object
            // Use general FireEvent(responseObject) method (internal)
            // Only open events to user
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}