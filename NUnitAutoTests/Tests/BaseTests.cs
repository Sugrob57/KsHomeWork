using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using NUnit.Framework;

namespace NUnitAutoTests.Tests
{
    [SetUpFixture]
    public class BaseTests
    {
        public static string RestApiUrl = @"http://localhost:49905";

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.WriteTo.File(@"C:\writter-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File(@"C:\tmp\ks\AutoTests-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("1. Setup base class ");
        }

        //[OneTimeTearDown]
        public void TermDown()
        {
            Log.Information("4. tearDown base class");
            //Assert.Pass();
        }
    }
}
