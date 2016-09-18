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
    public class AccountAdminController : AdminBaseController
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
        [AuthorityAdmin]
        [Route("GetList")]
        public async Task<List<AdminInfo_Model>> GetList([FromBody]AdminInfoCondition condition)
        {
            return await Task.Run(() =>
            {
                return accountAdminBll.GetAdminInfoList(condition);
            });
        }

        [HttpPost]
        [BasicAuthentication]
        [AuthorityAdmin]
        [Route("Edit")]
        public async Task<ReturnStatus_Model>Edit([FromBody]EditAdmin editAdmin)
        {
            return await Task.Run(()=> {
                return accountAdminBll.EditAdminInfo(editAdmin);
            });
        }

        [HttpPost]
        [BasicAuthentication]
        [AuthorityAdmin]
        [Route("Delete")]
        public async Task<ReturnStatus_Model> DeleteList([FromBody]string[] adminIds) {
            return await Task.Run(()=> {
                return accountAdminBll.DeleteList(adminIds);
            });
        }
    }
}
