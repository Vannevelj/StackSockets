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

            if (IsPostEdit(json))
            {
                return JsonConvert.DeserializeObject<PostEditedData>(json);
            }

            if (IsCommentAdd(json))
            {
                return JsonConvert.DeserializeObject<CommentAddedData>(json);
            }

            if (IsScore(json))
            {
                return JsonConvert.DeserializeObject<ScoreChangedData>(json);
            }

            if (IsAnswerAdd(json))
            {
                return JsonConvert.DeserializeObject<AnswerAddedData>(json);
            }

            throw new JsonException("The passed JSON couldn't be recognized");

            // return-type to indicate what Activity is being used?

            // Set events in each RequestParameters object
            // Use general FireEvent(responseObject) method (internal)
            // Only open events to user
        }

        private bool IsPostEdit(string data)
        {
            return IsType(data, "post-edit");
        }

        private bool IsCommentAdd(string data)
        {
            return IsType(data, "comment-add");
        }

        private bool IsScore(string data)
        {
            return IsType(data, "score");
        }

        private bool IsAnswerAdd(string data)
        {
            return IsType(data, "answer-add");
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