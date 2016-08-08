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
    [RoutePrefix("api/Account")]
    [ModelValidationFilter]
    [AuthorityAdmin]
    public class AccountAdminController : AccountController
    {
        BLL.Account_BLL accountBll = new BLL.Account_BLL();

        [HttpPost]
        [BasicAuthentication]
        [Route("Get")]
        public async Task<List<UserInfo_Model>> GetList([FromBody]UserInfoCondition condition)
        {
            return await Task.Run(() =>
            {
                return accountBll.GetUserInfoList(condition);
            });
        }
    }
}
