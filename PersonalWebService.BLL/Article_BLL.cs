using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;
using PersonalWebService.Helper;
using PersonalWebService.DAL;
using System.Text.RegularExpressions;

namespace PersonalWebService.BLL
{
    public class Article_BLL
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public static readonly int PageNum = 12;
        private WordsFilterDt wordsFilter = new WordsFilterDt();
        public static List<ArticleSort> articleSortList;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[Article] WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[Article] where {0}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[Article] SET {0} WHERE {1}";
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
                rsModel.message = "新增文章的类别无效，请重新设定类别";
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
                    rsModel.message = "新增文章失败，请整理后再次发布";
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
            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!string.IsNullOrEmpty(articleCondition.NickName))
            {
                sbsql.Append("U.NickName=@NickName AND");
                param.Add(new DataField { Name = "@NickName", Value = articleCondition.NickName });
            }
            if (!string.IsNullOrEmpty(articleCondition.ArticleName))
            {
                sbsql.Append("A.ArticleName=@ArticleName AND");
                param.Add(new DataField { Name = "@ArticleName", Value = articleCondition.ArticleName });
            }
            if (articleCondition.ArticleSortId != null)
            {
                sbsql.Append("A.ArticleSortId=@ArticleSortId AND");
                param.Add(new DataField { Name = "@ArticleSortId", Value = articleCondition.ArticleSortId });
            }
            if (articleCondition.FirstTime != null)
            {
                sbsql.Append("A.EditTime>=@FirstTime AND");
                param.Add(new DataField { Name = "@FirstTime", Value = articleCondition.FirstTime });
            }
            if (articleCondition.LastTime != null)
            {
                sbsql.Append("A.EditTime<=@LastTime AND");
                param.Add(new DataField { Name = "@LastTime", Value = articleCondition.LastTime });
            }

            string sqlIndex = string.Empty;
            if (articleCondition.PageIndex != null)
            {
                int pageIndex = Convert.ToInt32(articleCondition.PageIndex);
                int firstIndex = (pageIndex - 1) * PageNum + 1;
                int lastIndex = pageIndex * PageNum;
                sqlIndex = " NUM>=@firstIndex AND NUM<=@lastIndex ";
                param.Add(new DataField { Name = "@firstIndex", Value = firstIndex });
                param.Add(new DataField { Name = "@lastIndex", Value = lastIndex });
            }
            else
            {
                sbsql.Append(" NUM<=@PageNum ");
                param.Add(new DataField { Name = "@PageNum", Value = PageNum });
            }
            string sqlSelect = @"SELECT * FROM
                                (
	                                SELECT ROW_NUMBER() OVER(ORDER BY Hits DESC)NUM,* FROM
	                                (	
	                                	SELECT P.* FROM [dbo].[Article] A
	                                	LEFT JOIN [dbo].[UserInfo] U ON A.UserId=U.UserId
	                                	WHERE {0}
	                                )UA
                                ) UI
                                WHERE {1}";
            //硬条件只针对普通用户，管理员不在此范围内
            sbsql.Append("A.IsFreeze=1 AND A.ArticleState=1");
            sqlSelect = string.Format(sqlSelect, sbsql.ToString(),sqlIndex);
            List<Article> articleList = new List<Article>();
            try
            {
                articleList = dal.GetDataList<Article>(sqlSelect, param);
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
            List<Article_Model> articleModelList = new List<Article_Model>();
            articleList.Select(al =>
            {
                articleModelList.Add(
                    new Article_Model
                    {
                        ArticleId = al.ArticleId,
                        ArticleName = al.ArticleName,
                        ArticleContent = al.ArticleContent,
                        ArticleSortId = al.ArticleSortId,
                        ArticleState = al.ArticleState,
                        IsExpose = al.IsExpose
                    });
                return true;
            });
            return articleModelList;
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
            StringBuilder sbSql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!article.ArticleName.Equals(articleInfo.ArticleName))
            {
                sbSql.Append("[ArticleName]=@ArticleName,");
                param.Add(new DataField { Name = "@ArticleName", Value = article.ArticleName });
            }
            if (!article.ArticleContent.Equals(articleInfo.ArticleContent))
            {
                sbSql.Append("[ArticleContent]=@ArticleContent,");
                param.Add(new DataField { Name = "@ArticleContent", Value = article.ArticleContent });
            }
            if (article.ArticleSortId != articleInfo.ArticleSortId)
            {
                sbSql.Append("[ArticleSortId]=@ArticleSortId,");
                param.Add(new DataField { Name = "@ArticleSortId", Value = article.ArticleSortId });
            }
            if (article.ArticleState != articleInfo.ArticleState)
            {
                sbSql.Append("[ArticleState]=@ArticleState,");
                param.Add(new DataField { Name = "@ArticleState", Value = article.ArticleState });
            }
            if (article.IsExpose != articleInfo.IsExpose)
            {
                sbSql.Append("[IsExpose]=@IsExpose");
                param.Add(new DataField { Name = "@IsExpose", Value = article.IsExpose });
            }
            sbSql.Append("[EditTime]=GETDATE()");
            param.Add(new DataField { Name = "@ArticleId", Value = article.ArticleId });
            string sqlEdit = string.Format(sqlUpdateTemplate, sbSql.ToString(), "[ArticleId]=@ArticleId");
            try
            {
                if (dal.OpeData(sqlEdit, param))
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

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="articleIdList"></param>
        /// <returns></returns>
        public ReturnStatus_Model DeleteArticle(string[] articleIdList)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "文章删除";
            if (articleIdList.Length > 100)
            {
                rsModel.message = "你的一次删除操作太多，不予执行！";
                return rsModel;
            }
            if (articleIdList == null || articleIdList.Length <= 0)
            {
                rsModel.message = "需要操作删除的文章为空，请先选择需要删除的文章";
                return rsModel;
            }
            UserInfo userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
            {
                rsModel.message = "你未登录账号或账号已过期，请重新登录！";
                return rsModel;
            }

            string ids = string.Empty;
            if (Utility_Helper.IsClassIds(articleIdList))
            {
                rsModel.message = "你所需要操作的内容不合法！账号将被记录，请规范操作！";
                StringBuilder articleids = new StringBuilder();
                articleIdList.Select(l => { articleids.Append(l); return true; });
                LogRecord_Helper.RecordLog(LogLevels.Warn, "错误删除文章操作，怀疑为sql注入,用户Id为" + userInfo.UserId + "，输入信息为" + articleids.ToString());
                return rsModel;
            }

            articleIdList.Select(l =>
            {
                ids += "'" + l + "'" + ",";
                return true;
            });

            ids = ids.Substring(0, ids.Length - 1);
            string sql = string.Format(sqlDeleteTemplate, "ArticleId in(" + ids + ")", "[UserId]=@UserId");

            try
            {
                if (dal.OpeData(sql, new DataField { Name = "@UserId", Value = userInfo.UserId }))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改成功！";
                    return rsModel;
                }
                else
                {
                    rsModel.isRight = false;
                    rsModel.message = "你删除的内容有误，请重试或联系管理员！";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.isRight = false;
                rsModel.message = "系统出现一个问题，请联系管理员或重试！";
                return rsModel;
            }
        }
    }
}
