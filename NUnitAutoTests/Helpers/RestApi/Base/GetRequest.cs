using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.RestApi
{
    public class GetRequest : RestRequest
    {
        public GetRequest(string url) : base(url)
        {

        }

        public RestResponse Run()
        {
            //using (base.LocalClent)
            //{
                //httpClient.BaseAddress = new Uri(base.ConectionString);
                //var converter = new IsoDateTimeConverter();
                //converter.DateTimeStyles = DateTimeStyles.AdjustToUniversal;
                //var json = JsonConvert.SerializeObject(body, converter);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
            

            var response = LocalClent.GetAsync(base.Url).Result;
            base.Response = new RestResponse();
            Response.Content = response.Content.ReadAsStringAsync().Result;
            Response.HttpCode = response.StatusCode.ToString();
            Response.IsSuccess = response.IsSuccessStatusCode;

            return Response;
            //}
        }
    }
}
