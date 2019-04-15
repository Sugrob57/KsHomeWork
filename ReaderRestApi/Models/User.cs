using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ReaderRestApi.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int UserId { get; set; }

        public User(string firstName, string secondName, DateTime dateOfBirth, int gender, int userId, string middleName = null)
        {
            FirstName = firstName;
            SecondName = secondName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            UserId = userId;
            MiddleName = middleName;
        }

        public User() {}

        // преобразование списка объектов в класс User
        public static bool Parse(List<object> obj_user, out User _user)
        {
            _user = new User();

            if (obj_user.Count != 6)
            {
                return false;
            }
            else
            {
                // ------Parse data---------
                _user.FirstName = obj_user[0].ToString();
                _user.SecondName = obj_user[1].ToString();
                _user.MiddleName = obj_user[5]?.ToString();

                // get userId
                try
                {
                    _user.UserId = System.Convert.ToInt32(obj_user[4]);
                }
                catch
                {
                    Log.Error("Incorrect userId value \"{0}\"", obj_user[4].ToString());
                    return false;
                }

                // get dateTime for DateOfBirth
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    _user.DateOfBirth = DateTime.ParseExact(obj_user[2].ToString(), "yyyy-MM-dd", provider);
                }
                catch
                {
                    Log.Error("Incorrect datetime value \"{0}\" for record #{1}", obj_user[2].ToString(), _user.UserId);
                    return false;
                }

                // get Gender 
                try
                {
                    _user.Gender = System.Convert.ToInt32(obj_user[3]);
                    if ((_user.Gender != 0) && (_user.Gender != 1))
                    {
                        Log.Error("Incorrect gender number \"{0}\". \n Must be \"1\" for male OR \"0\" for female ", obj_user[3].ToString());
                        return false;
                    };
                }
                catch
                {
                    Log.Error("Incorrect gender value \"{0}\"", obj_user[3].ToString());
                    return false;
                };
                // ----end parse data
                return true;
            }
        }

    }
}
