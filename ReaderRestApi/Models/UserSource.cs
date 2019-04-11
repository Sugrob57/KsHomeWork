using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReaderRestApi.Models
{
    public class UserSource
    {
        private static List<User> _users = null;

        public static List<User> All
        {
            get
            {
                if (_users == null)
                {
                    _users = new List<User>();   
                    // todo get users from db
                }
                return _users;
            }
        }
    }
}
