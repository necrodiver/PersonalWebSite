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
        private static string baseUrl = "http://localhost:10841/Service/api/";
        public async Task<string> TestDM(int num)
        {
            string url = "Account/GetTestValues";
            var dmStr = num;
            //var handler=new HttpClientHandler() { AutomaticDecompression=System.Net.DecompressionMethods}
            using (var httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ToString();
                }
                return "null";
                //httpClient.PostAsJsonAsync("api/userinfo", userInfo).Result;
                //response.IsSuccessStatusCode()
                //response.Content.ToString();
            }
        }
    }
}
