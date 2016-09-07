using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;
using PersonalWebService.Helper;
using PersonalWebService.DAL;

namespace PersonalWebService.BLL
{
    public class Article_BLL
    {
        private WordsFilterDt wordsFilter = new WordsFilterDt();
        public static List<ArticleSort> articleSortList;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[Article] WHERE {1}";
        private static readonly string sqlDeleteTemplate = @"DELETE [dbo].[SystemAdmin] where {0}";
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
                articleSortList = dal.GetDataList<ArticleSort>(null);
            }

            if (!articleSortList.Exists(artSort => artSort.ArticleSortId == article.ArticleSortId))
            {
                rsModel.isRight = false;
                rsModel.message = "新增文章类别不符，请重新设定类别";
                return rsModel;
            }

            try
            {
                if (dal.OpeData(article, OperatingModel.Add))
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

        /// <summary>
        /// 根据条件获取对应的文章列表
        /// </summary>
        /// <param name="articleCondition"></param>
        /// <returns></returns>
        public List<Article_Model> GetArticleList(ArticleCondition_Model articleCondition)
        {
            //sql先不写了，根据数据库来写
            //一堆判断
            string sql = "";
            List<DataField> datafiled = new List<DataField>();
            List<Article_Model> articleList = new List<Article_Model>();
            try
            {
                articleList = dal.GetDataList<Article_Model>(sql, datafiled);
                if (articleList == null || articleList.Count == 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                return null;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改文章内容
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public ReturnStatus_Model EditArticle(Article_Model article)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改文章";
            if (string.IsNullOrEmpty(article.ArticleId))
            {
                rsModel.message = "数据传入错误，请重新填写修改内容！";
                return rsModel;
            }
            string sql = string.Format(sqlSelectTemplate, "TOP (1) *", "ArticleId=@ArticleId");
            Article articleInfo = null;
            try
            {
                articleInfo = dal.GetDataSingle<Article>(sql, new DataField { Name = "@ArticleId", Value = article.ArticleId });
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "数据处理错误，请联系管理员";
                return rsModel;
            }

            if (articleInfo == null)
            {
                rsModel.message = "文章不存在，无法修改，请重试！";
                return rsModel;
            }
            articleInfo.ArticleName = article.ArticleName;
            articleInfo.ArticleContent = article.ArticleContent;
            articleInfo.ArticleSortId = article.ArticleSortId;
            articleInfo.EditTime = DateTime.Now;
            articleInfo.ArticleState = article.ArticleState;
            articleInfo.IsExpose = article.IsExpose;
            try
            {
                if (dal.OpeData(articleInfo, OperatingModel.Edit))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改成功！";
                    return rsModel;
                }
                rsModel.isRight = false;
                rsModel.message = "无法修改内容，请重新查看或联系管理员！";
                return rsModel;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.isRight = false;
                rsModel.message = "数据处理错误，请联系管理员";
                return rsModel;
            }
        }

        public ReturnStatus_Model DeleteArticle(string[] articleIdList)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "文章删除";
            if (articleIdList == null || articleIdList.Length <= 0)
            {
                rsModel.message = "需要操作删除的文章为空，请先选择需要删除的文章";
                return rsModel;
            }
            string ids = string.Empty;
            articleIdList.Select(l=> {
                ids += "'"+l+"'" + ",";
                return true;
            });
            ids= ids.Substring(0, ids.Length - 1);
            string sql =string.Format(sqlDeleteTemplate, "AdminId in");
            throw new NotImplementedException();
        }

        protected bool IsAdmin()
        {
            SystemAdmin adminInfo = SessionState.GetSession<SystemAdmin>("SystemAdmin");
            if(adminInfo != null&& !string.IsNullOrEmpty(adminInfo.AdminId))
            {
                return true;
            }
            UserInfo userInfo
            if()
        }

    }
}
