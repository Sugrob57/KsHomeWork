using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers.RestApi
{
    public class RestApiHelper // дополнительные методы для получения данных
    {
        public static string GetFirstUserId(string url)
        {
            GetRequest request = new GetRequest(url);
            request.Run();

            string _userId = String.Empty;
            JsonParser.ExecuteValue(request.Response.Content, "[0].userId", out _userId);

            return _userId;
        }


        public static string GetLastUserId(string url)
        {
            GetRequest request = new GetRequest(url);
            request.Run();

            List<string> users = new List<string>();
            JsonParser.ExecuteList(request.Response.Content, ".userId", out users);

            if (users.Count > 0)
                return users[users.Count - 1];
            else
                return "-1";
        }
    }
}
