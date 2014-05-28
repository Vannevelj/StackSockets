using System;
using System.Linq;
using System.Xml.Linq;
using Library.Responses;
using Library.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTests
{
    // ReSharper disable PossibleNullReferenceException
    [TestClass]
    public class DataConverterTests
    {
        [TestMethod]
        public void DataConverterQuestionsActive_CanParse_ValidJsonResponse()
        {
            var doc = XDocument.Load("jsonresponses.xml");
            var json =
                doc.Element("data")
                    .Elements("response")
                    .FirstOrDefault(x => x.Element("query").Value == "questions-active")
                    .Element("string");

            var obj = JsonConvert.DeserializeObject<Response>(json.Value, new DataConverter<ActiveQuestionsData>());
            var response = obj.Data as ActiveQuestionsData;

            Assert.IsNotNull(response);
            Assert.AreEqual("155-questions-active", obj.Action);
            Assert.AreEqual("chinese.stackexchange.com", response.SiteBaseHostAddress);
            Assert.AreEqual(6977, response.PostId);
            Assert.AreEqual("My patience is running out", response.TitleEncodedFancy);
            Assert.AreEqual("How can I say my patience is wearing thin or my patience is running out?",
                response.BodySummary);
            CollectionAssert.AreEqual(new[] {"translation"}, response.Tags.ToArray());
            Assert.AreEqual(new DateTime(2014, 05, 28, 02, 17, 07), response.LastActivityDate);
            Assert.AreEqual("http://chinese.stackexchange.com/questions/6977/my-patience-is-running-out",
                response.QuestionUrl.ToString());
            Assert.AreEqual("http://chinese.stackexchange.com/users/2274/growler", response.OwnerUrl.ToString());
            Assert.AreEqual("Growler", response.OwnerDisplayName);
            Assert.AreEqual("chinese", response.ApiSiteParameter);
        }
    }

    // ReSharper restore PossibleNullReferenceException
}