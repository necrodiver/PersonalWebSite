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
            YZMHelper yz = new YZMHelper();
            return await Task.Run(() =>
            {
                return articleBll.AddArticle(article);
            });
        }

        [HttpPost]
        [Route("GetList")]
        public async Task<List<Article_Model>>GetArticleList([FromBody]ArticleCondition_Model articleCondition)
        {
            YZMHelper yz = new YZMHelper();
            return await Task.Run(()=> {
                return articleBll.GetArticleList(articleCondition);
            });
        }
    }
}
