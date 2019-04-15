using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.RestApi
{
    public class RestResponse // Базовый класс для ответа Rest запроса
    {
        public string Content { get; set; }
        public string HttpCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
