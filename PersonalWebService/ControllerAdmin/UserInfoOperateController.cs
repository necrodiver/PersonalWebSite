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
    [ModelValidationWebApiFilter]
    [BasicAuthentication]
    public class UserInfoOperateController : AdminBaseController
    {
        BLL.Account_BLL accountBll = new BLL.Account_BLL();
        [HttpPost]
        [Route("Edit")]
        public async Task<ReturnStatus_Model> EditUserInfo([FromBody]AdminEditUserInfo editUserInfo)
        {
            return await Task.Run(() =>
            {
                return accountBll.AdminEditUserInfo(editUserInfo);
            });
        }
        [HttpPost]        [Route("GetList")]
        public async Task<List<UserInfo_Model>> GetUserInfo([FromBody]UserInfoCondition userInfoCondition)
        {
            return await Task.Run(()=> {
                return accountBll.GetUserInfoList(userInfoCondition);
            });
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<ReturnStatus_Model> DeleteList(string[] userInfoIdList)
        {
            return await Task.Run(()=> {
                return accountBll.DeleteUserinfoList(userInfoIdList);
            });
        }
    }
}
