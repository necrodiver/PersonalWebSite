using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;
using PersonalWebService.Helper;

namespace PersonalWebService.BLL
{
    public class Article_BLL
    {
        private IDAL.IDAL_Article articleDal = new DAL.Article_DAL();

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article">文章数据</param>
        /// <returns></returns>
        public ReturnStatus_Model AddArticle(Article_Model article)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "新增文章";
            //首先各种验证
            try
            {
                if (articleDal.ArticleOpe(article, OperatingModel.Add))
                {
                    rsModel.message = "新增文章成功";
                }
                else
                {
                    rsModel.message = "新增文章失败，请稍后再试一次";
                }
            }
            catch (Exception ex)
            {
                rsModel.message = "服务器错误,请稍后重试,如果不行请联系管理员进行处理（请先保存好您的文章）";
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return rsModel;
        }
    }
}
