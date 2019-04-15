using NUnit.Framework;
using NUnitAutoTests.Helpers;
using NUnitAutoTests.Helpers.RestApi;
using NUnitAutoTests.Helpers.SoapApi.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Tests.IntegrationTests
{
    [TestFixture]
    public class CreateAndReadTests
    // Тесты интгерации между двумя сервисами (создать User'a в одном сервисе, и получить его во втором)
    {
        string RequestUrl = BaseTests.RestApiUrl + @"/api/User/"; // Ссылка к методу сервера


        [TestCase(true, "Artem", "Sidoruk", 1, "2012-01-01", "Sanich")]
        public void CompareCreatedIdAndLastIdTest(bool expected, string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            // Проверить, что идентификатор из ответа метода создания 
            //и идентификатор последней записи из метода чтения совпадают
            
            
            // Создаем нового User'a
            AddRequest req = new AddRequest(RequestUrl, firstName, secondName, gender, dateOfBirth, middleName);
            req.Run();
            // Идентификатор созданного пользователя
            int createdUserId = req.Response.Response.UserId;

            // Идентификатор последнего пользователя БД
            int lastUserId = System.Convert.ToInt32(RestApiHelper.GetLastUserId(RequestUrl));

            // Идентификаторы созданного и последнего юзера должны мовпадать
            Assert.AreEqual(createdUserId, lastUserId);
        }


        [TestCase("Artem", "Sidoruk", 1, "01.01.2012 0:00:00", "Sanich")]
        [TestCase("Artem", "Sidoruk", 1, "01.01.2012 0:00:00", "")]
        public void CompareDataInRequestAndResponseTest(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            // Проверить, что данные в запросе и в ответе совпадают
            AddRequest req = new AddRequest(RequestUrl, firstName, secondName, gender, dateOfBirth, middleName);
            req.Run();
            int createdUserId = req.Response.Response.UserId; // Id созаднной записи

            string url = RequestUrl + createdUserId;
            GetRequest request = new GetRequest(url);
            request.Run(); // Запрос информации по полученному ранее Id

            // Извлекаем значения
            string gettedFirstName = string.Empty;
            JsonParser.ExecuteValue(request.Response.Content, "firstName", out gettedFirstName);

            string gettedSecondName = string.Empty;
            JsonParser.ExecuteValue(request.Response.Content, "secondName", out gettedSecondName);

            string gettedMiddleName = string.Empty;
            JsonParser.ExecuteValue(request.Response.Content, "middleName", out gettedMiddleName);

            string gettedGender = string.Empty;
            JsonParser.ExecuteValue(request.Response.Content, "gender", out gettedGender);

            string gettedDoB = string.Empty;
            JsonParser.ExecuteValue(request.Response.Content, "dateOfBirth", out gettedDoB);


            // Сравниваем значения из запроса на создание и из запроса на чтение
            Assert.AreEqual(firstName, gettedFirstName);
            Assert.AreEqual(secondName, gettedSecondName);
            Assert.AreEqual(middleName, gettedMiddleName);
            Assert.AreEqual(gender.ToString(), gettedGender);
            Assert.AreEqual(dateOfBirth, gettedDoB);
        }


        [TestCase("", "", 1, "01.01.2012 0:00:00", "Sanich")]
        public void BadRequestNotAddRecordTest(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            // Проверить, что некооректный запрос не создает запись в БД

            // Сначала получим ID последней имеющейся записи
            int lastUserId = System.Convert.ToInt32(RestApiHelper.GetLastUserId(RequestUrl));

            // теперь отправим некорректный запрос на создание
            try
            {
                AddRequest req = new AddRequest(RequestUrl, firstName, secondName, gender, dateOfBirth, middleName);
                req.Run();
            }
            catch { }
            
            // Снова получим ID последней имеющейся записи
            int newLastUserId = System.Convert.ToInt32(RestApiHelper.GetLastUserId(RequestUrl));

            //ID записи в БД не должен был измениться
            Assert.AreEqual(lastUserId, newLastUserId);
        }
    }
}


