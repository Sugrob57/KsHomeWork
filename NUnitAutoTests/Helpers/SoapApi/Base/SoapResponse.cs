using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.SoapApi.Base
{
    public class SoapResponse // Базовый класс SOAP-ответа
    {
        public bool IsSuccess { get; set; }
        public string Content { get; set; }
    }
}
