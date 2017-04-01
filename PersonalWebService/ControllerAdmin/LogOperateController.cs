using PersonalWebService.BLL;
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
    [RoutePrefix("api/Log")]
    [ModelValidationWebApiFilter]
    [BasicAuthentication]
    public class LogOperateController : AdminBaseController
    {
        private static Log_BLL logBll = new Log_BLL();
        [HttpPost]
        [Route("GetList")]
        public async Task<List<LogInfo>>GetList([FromBody]LogCondition logCondition)
        {
            return await Task.Run(()=> {
                return logBll.GetList(logCondition);
            });
        }

        public async Task<ReturnStatus_Model> Delete(int[] logIds)
        {
            return await Task.Run(()=> {
                return logBll.DeleteList(logIds);
            });
        }
    }
}
