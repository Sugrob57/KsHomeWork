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
        // 2 метода, которые будем запрашивать у службы
        // Сложение
        [OperationContract]
        double GetSum(double i, double j);

        // Умножение
        [OperationContract]
        double GetMult(double i, double j);

        [OperationContract]
        User Add(string firstName, string secondName, int gender, string dateOfBirth, string middleName = null);
    }
}
