using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Serilog;
using NUnitAutoTests.Helpers;
using NUnitAutoTests.Helpers.RestApi;

namespace NUnitAutoTests.Tests.ReaderTests
{
    [TestFixture]
    public class GetUserTests
    {
        string RequestUrl = BaseTests.RestApiUrl + @"/api/User/";
        GetRequest Request { get; set; }
        RestResponse Response { get; set; }
        string UserId { get; set; }


        [OneTimeSetUp]
        public void ClassSetUp()
        {
            UserId = RestApiHelper.GetFirstUserId(RequestUrl);

            string url = RequestUrl + UserId;
            Request = new GetRequest(url);
        }

        [SetUp]
        public void AfterTest()
        {
            Response = Request.Run();
        }


        [Test]
        public void MethodIsWorkedTest()
        {
            // Проверка , что метод работает и возвращает http код 200
            Assert.IsTrue(Response.IsSuccess);
        }


        [Test]
        public void InResponseHasDataTest()
        {
            // Проверка,что в ответе есть какие-либо данные
            bool _position = Response.Content.Contains('{');

            Assert.IsTrue(_position);
        }


        [TestCase("userId", true)]
        [TestCase("firstName", true)]
        [TestCase("secondName", true)]
        [TestCase("dateOfBirth", true)]
        [TestCase("gender", true)]
        public void InResponsePresentUserFieldsTest(string field, bool expectResult)
        {
            // Проверка,что в ответе есть поля, описывающие объект типа User
            string req = field;
            string fieldValue = String.Empty;
            bool isFound = JsonParser.ExecuteValue(Response.Content, req, out fieldValue);

            Assert.AreEqual(isFound, expectResult);
        }


        [Test]
        public void UserNotFoundResponseTest()
        {
            UserId = RestApiHelper.GetLastUserId(RequestUrl);
            int IntId = System.Convert.ToInt32(UserId) + 1;
            string url = RequestUrl + IntId.ToString();
            Request = new GetRequest(url);
            Response = Request.Run();

            Assert.AreEqual(Response.HttpCode, "NotFound");
        }


        [Test]
        public void BadRequestTest()
        {
            string url = RequestUrl + "string";
            GetRequest request = new GetRequest(url);
            request.Run();

            Assert.IsFalse(request.Response.IsSuccess);
        }
    }
}
