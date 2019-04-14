using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAutoTests.Helpers
{
    class JsonParser
    {
        private static JObject ParseString2Json(string str)
        {
            JObject jobject = new JObject();
            jobject = JObject.Parse(str);

            return jobject;
        }

        public static bool ExecuteValue(string response, string query, out string value)
        {
            try
            {
                JObject jobject = new JObject();
                string str = "{ 'request': " + response + " }";
                jobject = ParseString2Json(str);
                //request.[1].title
                query = "request." + query;
                value = jobject.SelectToken(query).ToString();
                return true;
            }
            catch
            {
                value = String.Empty;
                return false;
            }
            
        }

        public static bool ExecuteList(string response, string query, out List<string> list)
        {
            List<string> values = new List<string>();
            try
            {
                JObject jobject = new JObject();

                string str = "{ 'request': " + response + " }";
                jobject = ParseString2Json(str);

                query = "request." + query;
                IEnumerable<JToken> jValues = jobject.SelectTokens(query);

                foreach (JToken item in jValues)
                    values.Add(item.ToString());

                list = values;
                return true;
            }
            catch
            {
                list = values;
                return false;
            }
            
        }
    }
}
