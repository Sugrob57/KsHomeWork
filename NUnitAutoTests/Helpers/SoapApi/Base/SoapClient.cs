using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.IO;
using WritterWcfService;
using System.Threading.Tasks;
using NUnitAutoTests.Helpers.RestApi;

namespace NUnitAutoTests.Helpers.SoapApi.Base
{
    public class SoapClient
    {
        public static string SoapUrl = @"http://localhost:59888/WritterService";

        public async static void TestRequest()
        {
            
            

            //SenSoap6();
        }

        private static void SenSoap5()
        {
            
        }


        private static void SenSoap6()
        {
            var proxy = new WritterServiceClient();

            var t1 = proxy.AddAsync("Artem", "Sidoruk", 1, "1990-08-09", "");
            var t2 = proxy.AddAsync("Artem", "Sidoruk", 1, "1990-08-09", "");
            //they're runnning asynchronously now!

            //let's wait for the results:
            Task.WaitAll(t1, t2);
                var result1 = t1.Result;
                var result2 = t2.Result;
               // Console.WriteLine(result1);
               // Console.WriteLine(result2);

        }

        private static string GetBody(string firstName, string secondName, int gender, string dateOfBirth, string middleName)
        {
            string _body = String.Format(
                "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\"> "+
                    "<soapenv:Header/>"+
    "<soapenv:Body>"+
        "<tem:Add>"+
           " <tem:firstName>{0}</tem:firstName>"+
               " <tem:secondName>{1}</tem:secondName>"+
                  "  <tem:gender>{2}</tem:gender>"+
                       "<tem:dateOfBirth>{3}</tem:dateOfBirth>"+
                           "<tem:middleName>{4}</tem:middleName>"+
                          "</tem:Add>"+
                        "   </soapenv:Body>"+
                        " </soapenv:Envelope>"
                          ,firstName, secondName,gender, dateOfBirth, middleName);


            return _body;
        }

    }
}

