using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace WritterService
{
    public class Logger : ILogger
    {
        public Logger()
        {
            //Log.Logger = new LoggerConfiguration().CreateLogger();

            Log.Logger = new LoggerConfiguration()
                //.ReadFrom.AppSettings()
                .MinimumLevel.Debug()
                .WriteTo.File(@"C:\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }


        public void Debug(string text)
        {
            Log.Debug(text);
        }

        public void Error(string text, Exception e)
        {
            Log.Error(text + ": " + e.Message);
        }

        public void Info(string text)
        {
            Log.Information(text);
        }
    }
}
