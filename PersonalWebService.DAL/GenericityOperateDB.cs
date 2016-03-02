using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.DAL
{
    public class GenericityOperateDB
    {
        public bool Add<T>(T model)
        {
            StringBuilder sbSql = new StringBuilder();
            Type t = model.GetType();
           // t.Name current member
            foreach (var item in t.GetProperties())
            {
                object value = item.GetValue(model, null);
               // PropertyInfo
                string name = item.Name;
            }
            return false;
        }
    }
}
