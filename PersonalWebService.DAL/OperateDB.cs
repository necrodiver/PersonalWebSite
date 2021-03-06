﻿using System;
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
                var tModel=conn.QueryFirstOrDefault<T>(sql, param);
                return tModel;
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
                return conn.ExecuteScalar(sql, param).ToString();
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
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public override int Edit(string sql, object param)
        {
            int num = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                num = Convert.ToInt32(conn.Execute(sql, param));
            }
            return num;
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">存储过程名字</param>
        /// <param name="dp">参数</param>
        /// <param name="buffered">是否缓冲（默认是）</param>
        /// <returns></returns>
        public override List<T> GetListSP<T>(string spName, object dp, bool buffered = true)
        {
            List<T> list = new List<T>();
            using (IDbConnection conn = GetOpenConnection())
            {
                IEnumerable<T> models = conn.Query<T>(spName, dp, null, buffered, null, CommandType.StoredProcedure);
                list = models as List<T>;
            }
            return list;
        }
    }
}
