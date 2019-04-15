using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.RestApi
{
    public class GetRequest : RestRequest // GET REST запрос
    {
        public GetRequest(string url) : base(url)
        {

        }

        public RestResponse Run()
        {
            var response = LocalClent.GetAsync(base.Url).Result;
            base.Response = new RestResponse();
            Response.Content = response.Content.ReadAsStringAsync().Result;
            Response.HttpCode = response.StatusCode.ToString();
            Response.IsSuccess = response.IsSuccessStatusCode;

            return Response;
        }
    }
}
