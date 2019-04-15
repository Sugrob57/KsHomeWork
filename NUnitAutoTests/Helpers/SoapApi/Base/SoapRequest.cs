//using SoapServiceReference;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnitAutoTests.Helpers.SoapApi.Base;
using WritterWcfService;

namespace NUnitAutoTests.Helpers.SoapApi
{
    public class SoapRequest
    {
        public string Url { get; set; }
        public static WritterServiceClient SharedClient { get; set; }
        public WritterServiceClient LocalClent { get; set; }

        public SoapRequest(string url)
        {
            Url = url;
            try
            {
                if (SharedClient != null)
                    LocalClent = SharedClient;
                else
                    LocalClent = new WritterServiceClient();
            }
            catch (ObjectDisposedException)
            {
                LocalClent = new WritterServiceClient();
            }
            catch (NullReferenceException)
            {
                LocalClent = new WritterServiceClient();
            }
            SharedClient = LocalClent;
        }

    }
}
