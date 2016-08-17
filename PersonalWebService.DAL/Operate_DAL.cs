using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.DAL
{
    public class Operate_DAL : IDAL.IDAL_PersonalBase
    {
        GenericityOperateDB generDB = new GenericityOperateDB();
        public int GetDataCount(string sql, object param)
        {
            // return generDB.Edit(sql, param);

            string value = generDB.GetScaler(sql, param);
            if (string.IsNullOrEmpty(value))
            {
                return Convert.ToInt32(value);
            }
            return 0;
        }

        public List<T> GetDataList<T>(T model)
        {
            return generDB.GetList(model);
        }

        public List<T> GetDataList<T>(string sql, object param) where T : class
        {
            return generDB.Get<T>(sql, param);
        }

        public T GetDataSingle<T>(string sql, object param) where T : class
        {
            return generDB.GetModel<T>(sql, param);
        }

        public bool OpeData(string sql, object param)
        {
            return generDB.Operate(sql, param);
        }

        public bool OpeData<T>(T model, OperatingModel operate)
        {
            return generDB.Operate(model, operate);
        }
    }
}
