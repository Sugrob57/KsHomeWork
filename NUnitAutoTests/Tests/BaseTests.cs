using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NUnitAutoTests.Tests
{
    [SetUpFixture]
    public class BaseTests
    {
        // Ссылка к RestApi сервису чтения данных
        public static string RestApiUrl = @"http://localhost:49905"; //@"http://localhost:56006/";

        // Ссылка к SOAP WCF сервису
        public static string WcfSoapUrl = @"http://localhost:59888/WritterService";




        [OneTimeSetUp]
        public void Setup()
        {
            // TODO - запомнить ИД последней строчки в БД
        }

        [OneTimeTearDown]
        public void TermDown()
        {
            // TODO - стереть из БД все записи, которые сделали тесты 
            // т.е. созданные после записи с ИД, полученным выше
        }
    }
}
