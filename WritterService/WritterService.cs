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

        public User Add(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            ILogger _logger = new Logger();
            _logger.Info("1234567890------------------------");


            User _user = new User();
            _user.FirstName = firstName;
            _user.SecondName = secondName;
            _user.MiddleName = middleName;
            _user.Gender = gender;
            _user.DateOfBirth = System.Convert.ToDateTime(dateOfBirth);
            _user.Save();

            return _user;
        }
    }
}
