using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.DAL
{
    public interface IDAL_ArticleSort
    {
        /// <summary>
        /// 获取文章类别内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="article"></param>
        /// <returns></returns>
        List<T> GetArticleSortList<T>(T articleSort);
        /// <summary>
        /// 修改文章类别
        /// </summary>
        /// <param name="article">类别信息</param>
        /// <param name="operate">操作方式（增删改查）</param>
        /// <returns></returns>
        bool ArticleSortOpe(ArticleSort_Model articleSort, OperatingModel operate);
        /// <summary>
        /// 获取单个文章类别
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="sql">查询的sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        T GetArticleSort<T>(string sql, object param);

        /// <summary>
        /// 获取所有的文章类别
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetArticleSortAllList<T>();
    }
}
