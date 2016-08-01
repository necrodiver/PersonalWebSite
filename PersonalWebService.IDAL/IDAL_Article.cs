using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.IDAL
{
    public interface IDAL_Article
    {
        /// <summary>
        /// 修改文章信息
        /// </summary>
        /// <param name="article">文章内容</param>
        /// <param name="operate">操作方式（增删改查）</param>
        /// <returns></returns>
        bool ArticleOpe(Article_Model article, OperatingModel operate);
        /// <summary>
        /// 获取文章集合
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="article">文章条件内容</param>
        /// <returns></returns>
        List<T> GetArticleList<T>(T article);
        /// <summary>
        /// 获取文章集合
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        List<T> getArticleList<T>(string sql, object param);
        /// <summary>
        /// 文章数量
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        int GetArticleCount(string sql, object param);
        /// <summary>
        /// 获取单个文章内容
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        T GetArticle<T>(string sql, object param);

    }
}
