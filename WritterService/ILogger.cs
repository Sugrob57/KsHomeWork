using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritterService
{
    public interface ILogger
    {
        void Debug(string text);

        void Info(string text);

        void Error(string text, Exception e);
    }
}
