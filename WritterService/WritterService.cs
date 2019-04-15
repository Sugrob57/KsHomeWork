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

            try
            {
                User _user = new User();
                _user.FirstName = CheckNullValue(firstName);
                _user.SecondName = CheckNullValue(secondName);
                _user.MiddleName = middleName;


                _user.Gender = CheckGenderValue(gender);
                _user.DateOfBirth = System.Convert.ToDateTime(CheckNullValue(dateOfBirth));
                if (_user.Save())
                {
                    Log.Information("User created. Id = {0}", _user.UserId);
                    return _user;
                }
            }
            catch (Exception e)
            {
                string txt = "Bad request. Error: " + e.Message;
                Log.Error(txt);
            }
            return null;

        }

        private static int CheckGenderValue(int value)
        {
            if ((value == 0) || (value == 1))
                return value;
            else
                throw new NullReferenceException();
        }

        private static string CheckNullValue(string value)
        {
            value.Trim();
            if (value == "")
                throw new NullReferenceException();
            else
                return value;
        }
    }


}
