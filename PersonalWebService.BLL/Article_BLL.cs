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
        private DAL.IDAL_Article articleDal = new DAL.Article_DAL();
        private WordsFilterDt wordsFilter = new WordsFilterDt();
        public static List<ArticleSort_Model> articleSortList;
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
            if (!wordsFilter.DetectionWords(article.ArticleName))
            {
                rsModel.isRight = false;
                rsModel.message = "新增文章标题存在脏词，请检查后再次进行提交";
                return rsModel;
            }

            if (!wordsFilter.DetectionWords(article.ArticleContent))
            {
                rsModel.isRight = false;
                rsModel.message = "新增文章内容存在脏词，请检查后再次进行提交";
                return rsModel;
            }

            //这里直接获取内容
            if (articleSortList == null)
            {
                DAL.IDAL_ArticleSort articleSort = new DAL.ArticleSort_DAL();
                articleSortList = articleSort.GetArticleSortAllList<ArticleSort_Model>();
            }

            if (!articleSortList.Exists(artSort => artSort.ArticleSortId == article.ArticleSortId))
            {
                rsModel.isRight = false;
                rsModel.message = "新增文章类别不符，请重新设定类别";
                return rsModel;
            }

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

        public Article_Model GetArticle(ArticleCondition_Model articleCondition)
        {
            //sql先不写了，根据数据库来写
            //一堆判断
            string sql = "";
            List<DataField> datafiled = new List<DataField>();
            Article_Model articleModel = new Article_Model();
            try
            {
                articleModel=articleDal.GetArticle<Article_Model>(sql, datafiled);
                if (articleModel == null) {

                }
            }
            catch (Exception)
            {

                throw;
            }

            throw new NotImplementedException();
        }
    }
}
