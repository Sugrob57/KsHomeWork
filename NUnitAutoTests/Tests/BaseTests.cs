using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NUnitAutoTests.Tests
{
    [SetUpFixture]
    public class BaseTests
    {
        public static string RestApiUrl = @"http://localhost:56006/";
        public static string WcfSoapUrl = @"http://localhost:59888/WritterService";
        //@"http://localhost:49905";

        [OneTimeSetUp]
        public void Setup()
        {
            //
        }

        //[OneTimeTearDown]
        public void TermDown()
        {
            //Assert.Pass();
        }
    }
}
