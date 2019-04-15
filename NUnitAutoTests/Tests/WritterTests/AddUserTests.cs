using NUnit.Framework;
using NUnitAutoTests.Helpers.SoapApi;
using NUnitAutoTests.Helpers.SoapApi.Base;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Tests.WritterTests
{
    [TestFixture]
    class AddUserTests
    {
        string RequestUrl = BaseTests.WcfSoapUrl;


        [OneTimeSetUp]
        public void ClassSetUp()
        {
            //
        }

        [SetUp]
        public void BeforeEveryTest()
        {
            //
        }

        // Positive cases
        [TestCase(true, "Artem", "Sidoruk", 1, "2012-01-01", "Sanich")] // Создать пользователя мужского рода
        [TestCase(true, "Artem", "Sidoruk", 1, "2012-01-01")] // Создать пользователя без отчества
        [TestCase(true, "Name", "secName", 0, "2012-01-01","middleName")] // Cоздать пользователя женского рода
        // Negative cases
        [TestCase(false, "Name", "secName", 3, "2012-01-01", "middleName")] // Неверно указан пол 
        [TestCase(false, "Name", "secName", 0, "2012-13-13", "middleName")]  // неверная дата рождения
        [TestCase(false, "Name", "", 0, "2012-12-12", "middleName")]         // пустое обязательное поле SecondName
        [TestCase(false, "", "SecName", 0, "2012-12-12", "middleName")]      // пустое обязательное поле FirstName
        [TestCase(false, "Name", "SecName", 0, "", "middleName")]      // пустое обязательное поле DateOfBirth
        // ------ Date formats
        [TestCase(false, "Name", "SecName", 0, "12.31.2012", "middleName")] // Неверный формат даты
        [TestCase(true, "Name", "secName", 0, "31.12.2012", "middleName")] // Дополнительный верный формат даты
        [TestCase(true, "Name", "secName", 0, "31.12.2012 08:12:54", "middleName")]
        [TestCase(false, "Name", "SecName", 0, "23 января 2013 года", "middleName")] // неверный формат поля DateOfBirth
        public void AddUserTest(bool expected, string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            AddRequest req = new AddRequest(RequestUrl, firstName, secondName, gender, dateOfBirth, middleName);

            bool result = false;
            try
            {
                req.Run();
                result = req.Response.IsSuccess;
            }
            catch { }

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void AddBadUserTest()
        {
            Assert.Pass();
        }
    }
}
