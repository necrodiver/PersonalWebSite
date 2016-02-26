using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.DAL
{
    public abstract class AbstractFactoryDB
    {
        public abstract T Add<T>();
        public abstract T Delete<T>();
        public abstract T Get<T>();
        public abstract T Edit<T>();
    }
}
