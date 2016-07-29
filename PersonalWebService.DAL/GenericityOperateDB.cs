using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.IDAL
{
    public class GenericityOperateDB : OperateDB
    {
        /// <summary>
        /// 操作进行增删，主要进行简单的增删操作
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="model">实例数据</param>
        /// <param name="operate">操作方式</param>
        /// <returns></returns>
        public bool Operate<T>(T model, OperatingModel operate)
        {
            string sql = GetSqlMasterlString(operate);

            if (string.IsNullOrEmpty(sql))
                return false;

            Type t = model.GetType();
            StringBuilder sql1 = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            string sql1S = string.Empty;
            string sql2S = string.Empty;

            foreach (var item in t.GetProperties())
            {
                object value = item.GetValue(model, null);
                if (value != null)
                {
                    switch (operate)
                    {
                        case OperatingModel.Add:
                            sql1.Append(item.Name + ",");
                            sql2.Append("@" + item.Name + ",");
                            break;
                        case OperatingModel.Delete:
                            sql1.Append(item.Name + "=@" + item.Name + " AND ");
                            break;
                        default:
                            break;
                    }
                }
            }

            sql1S = sql1.ToString();
            switch (operate)
            {
                case OperatingModel.Add:
                    sql1S = sql1S.Substring(0, sql1S.Length - 1);
                    sql2S = sql2.ToString();
                    sql2S = sql2S.Substring(0, sql2S.Length - 1);
                    break;
                case OperatingModel.Delete:
                    sql1S = sql1S.Substring(0, sql1S.Length - 4);
                    break;
                case OperatingModel.Edit:
                    sql1S = sql1S.Substring(0, sql1S.Length - 1);
                    break;
                default:
                    break;
            }
            sql = string.Format(sql, nameof(T), sql1S, sql2S);
            return Operate(sql, model);
        }

        /// <summary>
        /// 简单的获取数据内容
        /// </summary>
        /// <typeparam name="T">model类</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="param">传递参数</param>
        /// <returns></returns>
        public List<T> GetList<T>(T model)
        {
            Type t = model.GetType();
            string sql = "SELECT * FROM {0} WHERE {1} ";
            StringBuilder sb = new StringBuilder();
            string sbstring = string.Empty;
            foreach (var item in t.GetProperties())
            {
                object value = item.GetValue(model, null);
                if (value != null)
                {
                    sb.Append(item.Name + "=@" + item.Name + " AND ");
                }
            }
            sbstring = sb.ToString();
            sql = string.Format(sql, nameof(T), sbstring.Substring(0, sbstring.Length - 4));
            return Get<T>(sql, model);
        }

        /// <summary>
        /// 组织sql操作语句模板(增删改，不包含查)
        /// </summary>
        /// <param name="operate">操作方式</param>
        /// <returns></returns>
        private string GetSqlMasterlString(OperatingModel operate)
        {
            string sql = string.Empty;
            switch (operate)
            {
                case OperatingModel.Add:
                    sql = " INSERT {0} ({1}) VALUES ({2}) ";
                    break;
                case OperatingModel.Delete:
                    sql = " DELETE {0} WHERE {1} ";
                    break;
                default:
                    sql = null;
                    break;
            }
            return sql;
        }
    }
}
