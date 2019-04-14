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
    class GetUserListTests
    {
        GetRequest Request { get; set; }
        RestResponse Response { get; set; }

        [OneTimeSetUp]
        public void ClassSetUp()
        {
            Log.Information("2. setup class fixture");
            string url = @"http://localhost:49905/api/User";
            Request = new GetRequest(url);
        }

        [SetUp]
        public void AfterTest()
        {
            Log.Information("3. setup for every test in class");
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
            // Проверка,что в ответе есть поля, описывающие объекты типа User
            string req = "[0]." + field;
            string fieldValue = String.Empty;

            bool isFound = JsonParser.ExecuteValue(Response.Content, req, out fieldValue);

            Assert.AreEqual(isFound, expectResult);
        }
    }
}
