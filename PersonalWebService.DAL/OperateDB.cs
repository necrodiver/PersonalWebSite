using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.DAL
{
    public class OperateDB : AbstractFactoryDB
    {
        public override T Add<T>()
        {
            throw new NotImplementedException();
        }

        public override T Delete<T>()
        {
            throw new NotImplementedException();
        }

        public override T Edit<T>()
        {
            throw new NotImplementedException();
        }

        public override T Get<T>()
        {
            throw new NotImplementedException();
        }
    }
}
