using PersonalWebService.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.DAL
{
    public class Article_DAL : IDAL_Article
    {
        GenericityOperateDB generDB = new GenericityOperateDB();
        public bool ArticleOpe(Article_Model article, OperatingModel operate)
        {
            return generDB.Operate(article, operate);
        }

        public T GetArticle<T>(string sql, object param)
        {
            throw new NotImplementedException();
        }

        public int GetArticleCount(string sql, object param)
        {
            throw new NotImplementedException();
        }

        public List<T> GetArticleList<T>(T article)
        {
            throw new NotImplementedException();
        }

        public List<T> getArticleList<T>(string sql, object param)
        {
            throw new NotImplementedException();
        }
    }
}
