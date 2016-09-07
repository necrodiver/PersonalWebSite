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
    [RoutePrefix("api/Article")]
    [ModelValidationFilter]
    [BasicAuthentication]
    public class ArticleController : ApiController
    {
        BLL.Article_BLL articleBll = new BLL.Article_BLL();
        [HttpPost]
        [Route("Add")]
        public async Task<ReturnStatus_Model> Add([FromBody]Article_Model article)
        {
            return await Task.Run(() =>
            {
                return articleBll.AddArticle(article);
            });
        }

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
        public async Task<ReturnStatus_Model> Edit([FromBody]Article_Model article)
        {
            return await Task.Run(() => { return articleBll.EditArticle(article); });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ReturnStatus_Model> Delete([FromBody]string[] articleIdList)
        {
            return await Task.Run(()=> { return articleBll.DeleteArticle(articleIdList); });
        }
    }
}
