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
        private static DBProvider _provider { get; set; }

        public UserSource()
        {
            _provider = new DBProvider();
        }   

        public static bool GetAll(out List<User> users)
        {
            users = new List<User>();
            
            try
            {
                if (_provider == null)
                    _provider = new DBProvider();

                User user = null;
                int m = 0, n = 0;
                foreach (List<object> userObj in _provider.GetUsers())
                {
                    user = null;
                    if (User.Parse(userObj, out user))
                    {
                        users.Add(user);
                        m++;
                    }
                    n++;
                }
                Log.Debug("Read {0} user records from {1}", m, n);
                return true;
            }
            catch (NullReferenceException e)
            {
                Log.Error("Get all users error: {0}", e.Message);
                return false;
            }
        }

        public static User GetUserById(int userId)
        {
            try
            {
                User _user = _provider?.GetUser(userId);
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
