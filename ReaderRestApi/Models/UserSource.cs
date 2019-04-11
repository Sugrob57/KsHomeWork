using ReaderRestApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace ReaderRestApi.Models
{
    public class UserSource
    {
        public UserSource()
        {
            _provider = new DBProvider();
        }
        private static DBProvider _provider { get; set; }

        public static List<User> All
        {
            get
            {
                List<User> _users = _provider.GetUsers();   
                return _users;
            }
        }
        public static List<User> GetAll()
        {
            List<User> _users = new List<User>();
            _users = _provider?.GetUsers();
            return _users;
        }

        public static User GetUserById(int userId)
        {
            try
            {
                User _user = _provider.GetUser(userId);
                return _user;
            }
            catch (Exception e)
            {
                Log.Error("Get user error: {0}", e.Message);
                return null;
            }
            
            
        }
    }
}
