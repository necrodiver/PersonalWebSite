using PersonalWebService.Controllers;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersonalWebService.ControllerAdmin
{
    [RoutePrefix("api/AccountSystem")]
    [ModelValidationFilter]
    [AuthorityAdmin]
    public class AccountAdminController : ApiController
    {
        private static BLL.AccountAdmin_BLL accountAdminBll = new BLL.AccountAdmin_BLL();
        private static BLL.Account_BLL accountBll = new BLL.Account_BLL();
        [HttpPost]
        [Route("Login")]
        public async Task<ReturnStatus_Model> Login([FromBody]AdminLogin user)
        {
            YZMHelper yz = new YZMHelper();
            return await Task.Run(() =>
            {
                return accountAdminBll.VerifyAdmin(user);
            });
        }

        [HttpPost]
        [BasicAuthentication]
        [Route("GetUserList")]
        public async Task<List<UserInfo_Model>> GetList([FromBody]UserInfoCondition condition)
        {
            return await Task.Run(() =>
            {
                return accountBll.GetUserInfoList(condition);
            });
        }
    }
}
