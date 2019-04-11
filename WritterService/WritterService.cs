using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace WritterService
{
    // Реализация методов, которые описаны в интерфейсе
    public class WritterService : IWritterService
    {
        public User Add(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null)
        {
            string log_info = String.Format("Get ADD request. {0} {1} {2} {3} {4}", firstName, secondName, middleName, gender, dateOfBirth);
            Log.Information(log_info);
            Console.WriteLine(log_info);


            User _user = new User();
            _user.FirstName = firstName;
            _user.SecondName = secondName;
            _user.MiddleName = middleName;
            _user.Gender = gender;
            _user.DateOfBirth = System.Convert.ToDateTime(dateOfBirth);
            _user.Save();

            Log.Information("User created. Id = {0}", _user.UserId);
            return _user;
        }
    }
}
