using PersonalWebService.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.DAL
{
    public class ArticleSort_DAL : IDAL_ArticleSort
    {
        public bool ArticleSortOpe(ArticleSort_Model articleSort, OperatingModel operate)
        {
            throw new NotImplementedException();
        }

        public T GetArticleSort<T>(string sql, object param)
        {
            throw new NotImplementedException();
        }

        public List<T> GetArticleSortAllList<T>()
        {
            throw new NotImplementedException();
        }

        public List<T> GetArticleSortList<T>(T articleSort)
        {
            throw new NotImplementedException();
        }
    }
}
