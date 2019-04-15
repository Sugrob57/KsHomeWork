using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.SoapApi.Base
{
    public class SoapResponse
    {
        public string HttpCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Content { get; set; }
    }
}
