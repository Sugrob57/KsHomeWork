using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;


namespace WritterService
{
    public class User
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int UserId { get; set; }

        public bool Save()
        {
            try
            {
                DBProvider _provider = new DBProvider(Program.WorkPath);
                UserId = _provider.AddUser(FirstName, SecondName, Gender, DateOfBirth.ToShortDateString(), MiddleName);
            }
            catch (Exception e)
            {
                Log.Error("Error in process saving user in DB: {0}", e.Message);

                return false;
            }
            
            return true;
        }
    }
}
