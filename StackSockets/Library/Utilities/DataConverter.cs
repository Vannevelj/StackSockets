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
            if (IsType(json, "post-edit"))
            {
                return JsonConvert.DeserializeObject<PostEditedData>(json);
            }

            if (IsType(json, "comment-add"))
            {
                return JsonConvert.DeserializeObject<CommentAddedData>(json);
            }

            if (IsType(json, "score"))
            {
                return JsonConvert.DeserializeObject<ScoreChangedData>(json);
            }

            if (IsType(json, "answer-add"))
            {
                return JsonConvert.DeserializeObject<AnswerAddedData>(json);
            }

            if (IsType(json, "accept"))
            {
                return JsonConvert.DeserializeObject<AnswerAcceptedData>(json);
            }

            if (IsType(json, "unaccept"))
            {
                return JsonConvert.DeserializeObject<AnswerUnAcceptedData>(json);
            }

            throw new JsonException("The passed JSON couldn't be recognized");
        }

        private bool IsType(string data, string type)
        {
            var value =
                string.Format("{0}{1}{2}",
                    "\"a\":\"",
                    type,
                    "\"");
            return data.Contains(value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}