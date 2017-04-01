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
    [RoutePrefix("api/ArticleOperate")]
    [ModelValidationWebApiFilter]
    [BasicAuthentication]
    public class ArticleOperateController : AdminBaseController
    {
        private static Article_BLL articleBll = new Article_BLL();

        [HttpPost]
        [Route("GetList")]
        public async Task<List<Article_Model>> GetArticleList([FromBody]ArticleCondition_Model articleCondition)
        {
            return await Task.Run(() =>
            {
                return articleBll.GetArticleList(articleCondition);
            });
        }
        [HttpPost]
        [Route("Edit")]
        public async Task<ReturnStatus_Model> EditPicture([FromBody]Article_Model article)
        {
            return await Task.Run(() =>
            {
                return articleBll.EditArticle(article);
            });
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<ReturnStatus_Model> DeletePictureList(string[] articleIds)
        {
            return await Task.Run(() =>
            {
                return articleBll.DeleteArticle(articleIds);
            });
        }
    }
}
