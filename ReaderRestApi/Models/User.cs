using System;
using System.Collections.Generic;
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

    }
}
