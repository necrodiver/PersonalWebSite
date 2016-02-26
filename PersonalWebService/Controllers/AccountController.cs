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
        PersonalWebService.BLL.Account_BLL accountBll = new PersonalWebService.BLL.Account_BLL();

        [HttpPost]
        [Route("Login")]
        [ModelValidationFilter]
        public async Task<string> Login([FromBody]UserLogin user)
        {
            YZMHelper yz = new YZMHelper();
            return await Task.Run(()=> {
                accountBll.VerifyUserInfo(user);
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
                LogRecordHelper.RecordLog(LogLevels.Error,ex);
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
