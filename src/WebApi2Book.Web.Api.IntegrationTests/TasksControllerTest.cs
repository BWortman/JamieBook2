// TasksControllerTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.IntegrationTests
{
    [TestFixture]
    public class TasksControllerTest
    {
        [SetUp]
        public void Setup()
        {
            _webClientHelper = new WebClientHelper();
        }

        private const string UriRoot = "http://localhost:52975/api/v1/";

        //private static IisExpressHelper _iisExpressHelper;

        private WebClientHelper _webClientHelper;

        //[TestFixtureSetUp]
        //public static void TestFixtureSetUp()
        //{
        //    // TODO: Fix the helper...
        //    _iisExpressHelper = new IisExpressHelper();
        //    _iisExpressHelper.StartServer();
        //}

        //[TestFixtureTearDown]
        //public static void TestFixtureTearDown()
        //{
        //    _iisExpressHelper.StopServer();
        //}

        [Test]
        public void AddTask()
        {
            const string data = "{\"Subject\":\"Fix something important\"}";

            var client = _webClientHelper.CreateWebClient();
            const string address = UriRoot + "tasks";

            var responseString = client.UploadString(address, HttpMethod.Post.Method, data);

            var jsonResponse = JObject.Parse(responseString);
            Assert.IsNotNull(jsonResponse.ToObject<TaskCreatedActionResult>());
        }

        [Test]
        public void AddTask_denied()
        {
            const string data = "{\"Subject\":\"Fix something important\"}";

            var client = _webClientHelper.CreateWebClient(username: "jdoe");
            const string address = UriRoot + "tasks";

            try
            {
                client.UploadString(address, HttpMethod.Post.Method, data);
                Assert.Fail();
            }
            catch (WebException e)
            {
                var statusCode = ((HttpWebResponse) (e.Response)).StatusCode;
                Assert.AreEqual(HttpStatusCode.Unauthorized, statusCode);
            }
        }

        [Test]
        public void GetTasks()
        {
            var client = _webClientHelper.CreateWebClient();
            const string address = UriRoot + "tasks";

            var responseString = client.DownloadString(address);

            var jsonResponse = JObject.Parse(responseString);
            Assert.IsNotNull(jsonResponse.ToObject<PagedDataInquiryResponse<Task>>());
        }
    }
}