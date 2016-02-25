using Microsoft.AspNet.Identity;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersonalWebService.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        [ModelValidationFilter]
        public async Task<string> Login([FromBody]UserLogin user)
        {
            return await Task.Run(()=> {
                return string.Empty;
            });
            //return await GetValueAsync(user.UserName, user.PassWord);
        }

        private Task<string> GetValueAsync(string userName, string passWord)
        {
            return Task.Run(() =>
            {
                return DateTime.Now.ToString() + "  UserName:" + userName + "  PWD:" + passWord;
            });
        }
        public string GetTestValues(int DM)
        {
            try
            {
                int a = 100 / DM;
            }
            catch (Exception ex)
            {
                LogRecordHelper.RecordLog(ex.Message);
            }
           
            return DateTime.Now.ToString() + ":" + DM;
        }

        [HttpPost]
        public string TestValues1([FromBody]string SS)
        {
            return DateTime.Now.ToString() + ":" + SS;
        }
    }
}
