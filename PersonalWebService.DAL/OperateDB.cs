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
        public override bool Add<T>(string sql, object param)
        {
            int addNum = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                addNum = conn.Execute(sql, param);
            }
            return addNum > 0;
        }

        public override bool Delete<T>(string sql, object param)
        {
            int delNum = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                delNum=conn.Execute(sql, param);
            }
            return delNum > 0;
        }

        public override bool Edit<T>(string sql, object param)
        {
            int editNum = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                editNum = conn.Execute(sql, param);
            }
            return editNum > 0;
        }

        public override List<T> Get<T>(string sql, object param)
        {
            List<T> list = new List<T>();
            using (IDbConnection conn = GetOpenConnection())
            {
                IEnumerable<T> logs = conn.Query<T>(sql);
                list = logs as List<T>;
            }
            return list;
        }

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
        public static IDbConnection GetOpenConnection()
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
