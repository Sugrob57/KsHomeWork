using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritterService
{
    // Реализация методов, которые описаны в интерфейсе
    public class WritterService : IWritterService
    {
        public double GetSum(double i, double j)
        {
            return i + j;
        }

        public double GetMult(double i, double j)
        {
            return i * j;
        }
    }
}
