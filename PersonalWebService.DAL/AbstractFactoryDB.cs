using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.DAL
{
    public abstract class AbstractFactoryDB
    {
        public abstract bool Operate(string sql, object param);
        public abstract List<T> Get<T>(string sql, object param);
        //public abstract bool Add<T>(string sql, object param);
        //public abstract bool Delete<T>(string sql, object param);
        //public abstract bool Edit<T>(string sql, object param);
    }
}
