using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.RestApi
{
    public class RestResponse
    {
        public string Content { get; set; }
        public string HttpCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
