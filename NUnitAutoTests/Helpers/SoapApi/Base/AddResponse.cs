using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.SoapApi.Base
{
    public class AddResponse : SoapResponse
    {
        public User Response { get; set; }
    }
}
