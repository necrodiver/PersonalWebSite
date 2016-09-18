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
    [RoutePrefix("api/UserOperate")]
    [ModelValidationFilter]
    public class UserInfoOperateController : AdminBaseController
    {
        BLL.Account_BLL accountBll = new BLL.Account_BLL();
        [HttpPost]
        [BasicAuthentication]
        [Route("Edit")]
        public async Task<ReturnStatus_Model> EditUserInfo([FromBody]AdminEditUserInfo editUserInfo)
        {
            return await Task.Run(() =>
            {
                return accountBll.AdminEditUserInfo(editUserInfo);
            });
        }
    }
}
