using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Tests.WritterTests
{
    [TestFixture]
    class AddUserTests
    {
        [OneTimeSetUp]
        public void ClassSetUp()
        {
            //
        }

        [SetUp]
        public void AfterTest()
        {
            //
        }


        [Test]
        public void AddTest()
        {
            Assert.Pass();
        }

        [Test]
        public void AddBadUserTest()
        {
            Assert.Pass();
        }
    }
}
