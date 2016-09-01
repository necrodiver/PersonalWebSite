using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace PersonalWebService.DAL
{
    public class OperateDB : AbstractFactoryDB
    {
        private static readonly string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;

        /// <summary>
        /// 操作数据，增删改，适用于自定义操作
        /// </summary>
        /// <param name="sql">操作语句</param>
        /// <param name="param">附加操作的内容</param>
        /// <returns></returns>
        public override bool Operate(string sql, object param)
        {
            int addNum = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                addNum = conn.Execute(sql, param);
            }
            return addNum > 0;
        }

        /// <summary>
        /// 查询语句，适用于自定义操作
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="param">附带条件内容</param>
        /// <returns></returns>
        public override List<T> GetList<T>(string sql, object param)
        {
            List<T> list = new List<T>();
            using (IDbConnection conn = GetOpenConnection())
            {
                IEnumerable<T> models = conn.Query<T>(sql, param);
                list = models as List<T>;
            }
            return list;
        }

        public override T GetSingle<T>(string sql, object param)
        {
            using (IDbConnection conn = GetOpenConnection())
            {
                return conn.QuerySingle<T>(sql, param) as T;
            }
        }

        /// <summary>
        /// 查找数据的第一行第一列的内容
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override string GetScaler(string sql, object param)
        {
            using (IDbConnection conn = GetOpenConnection())
            {
                return conn.ExecuteScalar(sql, param) as string;
            }
        }

        //这个方法自个玩的，不参与使用
        public static List<T> SelectData<T>(string sql)
        {
            Type type = typeof(T);
            List<string> modelNamelist = new List<string>();
            foreach (var item in type.GetProperties())
            {
                modelNamelist.Add(item.Name);
            }
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object obj = Activator.CreateInstance(type);
                            foreach (string name in modelNamelist)
                            {
                                Console.WriteLine(type.GetProperty(name).PropertyType);
                                object values = reader[name];
                                type.GetProperty(name).SetValue(obj, values == null ? null : values, null);
                            }
                            list.Add((T)obj);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 开启数据库
        /// </summary>
        /// <returns></returns>
        private static IDbConnection GetOpenConnection()
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }

        public override int Edit(string sql, object param)
        {
            int num = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                num= Convert.ToInt32(conn.Execute(sql, param));
            }
            return num;
        }
    }
}
