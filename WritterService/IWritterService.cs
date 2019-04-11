using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WritterService
{
    [ServiceContract]
    public interface IWritterService
    {
        /// <summary>
        /// Метод создания пользователя в БД
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="secondName"></param>
        /// <param name="gender"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="middleName"></param>
        /// <returns>Созданный пользователь</returns>
        [OperationContract]
        User Add(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null);
    }
}
