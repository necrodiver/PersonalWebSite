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

namespace PersonalWebService.Controllers
{
    [RoutePrefix("api/Comment")]
    [ModelValidationFilter]
    [BasicAuthentication]
    public class CommentController : ApiController
    {
        private static UserComment_BLL userCommentBll = new UserComment_BLL();
        [HttpPost]
        [Route("Add")]
        public async Task<ReturnStatus_Model> Add([FromBody]UserComment_Model userComment)
        {
            return await Task.Run(() =>
            {
                return userCommentBll.AddComment(userComment);
            });
        }
        [HttpPost]
        [Route("Delete")]
        //这个方法先留着，不公开，没必要公开给用户自己删除自己的评论 
        public async Task<ReturnStatus_Model>DeleteSingle(string userCommendId)
        {
            return await Task.Run(()=> {
                return userCommentBll.DeleteSingle(userCommendId);
            });
        }

        [HttpPost]
        [Route("GetList")]
        public async Task<List<UserComment_Model>>GetList([FromBody]UserCommentCondition_Model condition)
        {
            return await Task.Run(()=> {
                return userCommentBll.GetList(condition);
            });
        }
    }
}
