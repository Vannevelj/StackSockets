using System;
using System.Linq;
using System.Xml.Linq;
using Library.Requests;
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
        private string GetResponseFromXml(string request)
        {
            var doc = XDocument.Load("jsonresponses.xml");
            var json =
                doc.Element("data")
                    .Elements("response")
                    .FirstOrDefault(x => x.Element("query").Value == request)
                    .Element("string");

            return json.Value;
        }

        [TestMethod]
        public void DataConverter_CanParse_QuestionsActive()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("questions-active"),
                new DataConverter<ActiveQuestionsData>());
            var response = obj.Data as ActiveQuestionsData;

            Assert.IsNotNull(response);
            Assert.AreEqual("155-questions-active", obj.Action);
            Assert.AreEqual(Activity.ActiveQuestions, response.Activity);
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

        // Tests the body with a snipped version: the actual response contains many HTML tags, newlines and escape characters
        [TestMethod]
        public void DataConverter_CanParse_QuestionsNewestTag()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("questions-newest-tag"),
                new DataConverter<NewestQuestionsByTagData>());
            var response = obj.Data as NewestQuestionsByTagData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-questions-newest-tag-java", obj.Action);
            Assert.AreEqual(Activity.NewestQuestionsByTag, response.Activity);
            Assert.AreEqual(23943937, response.PostId);
            Assert.AreEqual("snipped body", response.Body);
            CollectionAssert.AreEqual(new[] {"java", "json", "gson"}, response.Tags.ToArray());
            Assert.AreEqual(1, response.SiteId);
            Assert.IsFalse(response.Fetch);
        }

        [TestMethod]
        public void DataConverter_CanParse_Question_PostEdit()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("question-post-edit"),
                new DataConverter<PostEditedData>());
            var response = obj.Data as PostEditedData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-question-23943956", obj.Action);
            Assert.AreEqual(23944022, response.PostId);
            Assert.AreEqual(215377, response.AccountId);
        }

        [TestMethod]
        public void DataConverter_CanParse_Question_CommentAdd()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("question-comment-add"),
                new DataConverter<CommentAddedData>());
            var response = obj.Data as CommentAddedData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-question-23943956", obj.Action);
            Assert.AreEqual(23944022, response.PostId);
            Assert.AreEqual(36882693, response.CommentId);
            Assert.AreEqual(215389, response.AccountId);
        }

        [TestMethod]
        public void DataConverter_CanParse_Question_ScoreChange()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("question-score-change"),
                new DataConverter<ScoreChangedData>());
            var response = obj.Data as ScoreChangedData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-question-23943956", obj.Action);
            Assert.AreEqual(23944023, response.PostId);
            Assert.AreEqual(1, response.Score);
        }

        [TestMethod]
        public void DataConverter_CanParse_Question_AnswerAdd()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("question-answer-add"),
                new DataConverter<AnswerAddedData>());
            var response = obj.Data as AnswerAddedData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-question-23945234", obj.Action);
            Assert.AreEqual(23945234, response.PostId);
            Assert.AreEqual(23945257, response.AnswerId);
            Assert.AreEqual(1888343, response.AccountId);
        }

        [Ignore]
        [TestMethod]
        public void DataConverter_CanParse_Question_Accept()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("question-accept"),
                new DataConverter<AnswerAcceptedData>());
            var response = obj.Data as AnswerAcceptedData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-question-0", obj.Action);
            Assert.AreEqual(0, response.PostId);
            Assert.AreEqual(0, response.AnswerId);
            Assert.AreEqual(0, response.AccountId);
        }

        [Ignore]
        [TestMethod]
        public void DataConverter_CanParse_Question_UnAccept()
        {
            var obj = JsonConvert.DeserializeObject<Response>(GetResponseFromXml("question-unaccept"),
                new DataConverter<AnswerUnAcceptedData>());
            var response = obj.Data as AnswerUnAcceptedData;

            Assert.IsNotNull(response);
            Assert.AreEqual("1-question-0", obj.Action);
            Assert.AreEqual(0, response.PostId);
            Assert.AreEqual(0, response.AnswerId);
            Assert.AreEqual(0, response.AccountId);
        }
    }

    // ReSharper restore PossibleNullReferenceException
}