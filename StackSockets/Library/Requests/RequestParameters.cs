using Library.Responses;
using Library.Utilities;
using Newtonsoft.Json;

namespace Library.Requests
{
    public abstract class RequestParameters
    {
        public abstract string GetRequestValue();

        public abstract JsonConverter ResponseDataType { get; }
    }

    public sealed class ActiveQuestionsRequestParameters : RequestParameters
    {
        public string SiteId { get; set; }

        public override string GetRequestValue()
        {
            return SiteId + "-questions-active";
        }

        public override JsonConverter ResponseDataType
        {
            get { return new DataConverter<ActiveQuestionsData>(); }
        }
    }

    public sealed class NewestQuestionsByTagRequestParameters : RequestParameters
    {
        public string SiteId { get; set; }
        public string Tag { get; set; }

        public override string GetRequestValue()
        {
            return SiteId + "-questions-newest-tag-" + Tag.ToLower();
        }

        public override JsonConverter ResponseDataType
        {
            get { return new DataConverter<NewestQuestionsByTagData>(); }
        }
    }
}