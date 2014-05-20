using System;
using System.Collections.Generic;

namespace Library.Responses
{
    public class Response
    {
        public string Action { get; set; }

        public string SiteBaseHostAddress { get; set; }

        public string Id { get; set; }

        public string TitleEncodedFancy { get; set; }

        public string BodySummary { get; set; }

        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        ///     Last moment of activity expressed in GMT.
        /// </summary>
        /// <returns></returns>
        public DateTime LastActivityDate { get; set; }

        public Uri QuestionUrl { get; set; }

        public Uri OwnerUrl { get; set; }

        public string OwnerDisplayName { get; set; }

        public string ApiSiteParameter { get; set; }
    }
}