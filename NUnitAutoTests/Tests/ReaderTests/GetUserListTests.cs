using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnitAutoTests.Helpers;
using NUnitAutoTests.Helpers.RestApi;

namespace NUnitAutoTests.Tests.ReaderTests
{
    [TestFixture]
    class GetUserListTests
    // Тесты метода "GET /api/User"
    {
        string RequestUrl = BaseTests.RestApiUrl + @"/api/User/"; // Ссылка к методу сервера
        GetRequest Request { get; set; }
        RestResponse Response { get; set; }

        [OneTimeSetUp]
        public void ClassSetUp()
        {
            // При запуске модуля формируем ссылку и создаем объекты
            Request = new GetRequest(RequestUrl);
        }

        [SetUp]
        public void BeforeEveryTest()
        {
            // Перед запуском наждого теста прогоняем запрос и передаем в тест его результат
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
