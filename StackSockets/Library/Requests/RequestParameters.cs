using Library.Responses;
using Library.Utilities;
using Newtonsoft.Json;

namespace Library.Requests
{
    public abstract class RequestParameters
    {
        internal abstract string GetRequestValue();

        internal abstract JsonConverter ResponseDataType { get; }
    }

    public sealed class ActiveQuestionsRequestParameters : RequestParameters
    {
        public string SiteId { get; set; }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-active";
        }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<ActiveQuestionsData>(); }
        }
    }

    public sealed class NewestQuestionsByTagRequestParameters : RequestParameters
    {
        public string SiteId { get; set; }
        public string Tag { get; set; }

        internal override string GetRequestValue()
        {
            return SiteId + "-questions-newest-tag-" + Tag.ToLower();
        }

        internal override JsonConverter ResponseDataType
        {
            get { return new DataConverter<NewestQuestionsByTagData>(); }
        }
    }
}