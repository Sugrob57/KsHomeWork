using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NUnitAutoTests.Helpers.RestApi
{
    public class RestRequest // Базовый класс для Rest запроса
    {
        public string Url { get; set; }
        public RestResponse Response { get; set; }
        public static HttpClient SharedClient { get; set; }
        public HttpClient LocalClent { get; set; }

        public RestRequest(string url)
        {
            Url = url;
            try
            {
                if (SharedClient != null)
                    LocalClent = SharedClient;
                else
                    LocalClent = new HttpClient();
            }
            catch (ObjectDisposedException)
            {
                LocalClent = new HttpClient();
            }
            catch (NullReferenceException)
            {
                LocalClent = new HttpClient();
            }
            SharedClient = LocalClent;
        }
    }
}
