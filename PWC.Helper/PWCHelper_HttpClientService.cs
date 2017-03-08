using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PWC.Helper
{
    public class PWCHelper_HttpClientService
    {
        private static string baseUrl = "http://localhost:4525/Service/api/";
        public async Task<string> TestDM2()
        {
            string url = baseUrl + "Account/GetTestValues?DM=1000";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string resultStr = await response.Content.ReadAsStringAsync();
                    return resultStr;
                }
            }
            return "null";
        }
    }
}
