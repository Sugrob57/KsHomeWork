using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WritterWcfService;

namespace NUnitAutoTests.Helpers.SoapApi.Base
{
    public class AddRequest : SoapRequest
    {
        string FirstName { get; set; }
        string SecondName { get; set; }
        int Gender { get; set; }
        string DateOfBirth { get; set; }
        string MiddleName { get; set; }
        public AddResponse Response { get; set; }

        public AddRequest(string url, string firstName, string secondName, int gender, string dateOfBirth, string middleName = null) : base(url)
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }

        public AddResponse Run()
        {
            LocalClent = new WritterServiceClient();
            var task = base.LocalClent.AddAsync(FirstName, SecondName, Gender, DateOfBirth, MiddleName);

            //Task.WaitAll(task);
            Task.WaitAny(task);

            AddResponse _resp = new AddResponse();
            _resp.Response = new User();
            _resp.Response.FirstName = task.Result.FirstName;
            _resp.Response.SecondName = task.Result.SecondName;
            _resp.Response.MiddleName = task.Result.MiddleName;
            _resp.Response.Gender = task.Result.Gender;
            _resp.Response.DateOfBirth = task.Result.DateOfBirth;
            _resp.Response.UserId = task.Result.UserId;
            _resp.Content = task.Result.ToString();
            
            _resp.IsSuccess = true;

            Response = _resp;
            return _resp;
        }
    }
}
