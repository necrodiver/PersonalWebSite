using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.DAL
{
    public class UserInfo_DAL: IDAL_UserInfo
    {
        GenericityOperateDB generDB = new GenericityOperateDB();

        /// <summary>
        /// 进行查找操作
        /// </summary>
        /// <typeparam name="T">Model类</typeparam>
        /// <param name="userInfo">传入的实体类</param>
        /// <returns>查找到的实体类的List集合</returns>
        public List<T> GetUserInfoList<T>(T userInfo)
        {
             return generDB.GetList(userInfo);
        }

        //public T GetUserInfo<T>(string sql,object param) where T :class
        //{
        //    return generDB.GetModel<T>(sql, param);
        //}

        public int GetUserInfoCount(string sql,object param)
        {
            string value= generDB.GetScaler(sql,param);
            if (string.IsNullOrEmpty(value))
            {
                return Convert.ToInt32(value);
            }
            return 0;
        }

        public bool EditPwd(string sqlUpdate, DataField dataField)
        {
            return generDB.Edit(sqlUpdate, dataField)>0;
        }

        /// <summary>
        /// 进行增删改操作
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="userInfo">用户model</param>
        /// <param name="operate">参数</param>
        /// <returns></returns>
        public bool UserInfoOpe<T>(T userInfo, OperatingModel operate)
        {
            return generDB.Operate(userInfo, operate);
        }

        public T GetUserInfo<T>(string sql, object param)where T:class
        {
            return generDB.GetModel<T>(sql, param);
        }
        //public bool AddUserInfo(UserInfo_Model userinfo)
        //{
        //    StringBuilder sbsql = new StringBuilder();
        //    sbsql.Append(" SELECT * FROM UserInfo WHERE ");
        //    List<DataField> parameters = new List<DataField>();
        //    if (string.IsNullOrWhiteSpace(userinfo.UserID))
        //    {
        //        sbsql.Append(" UserID=@UserID and");
        //        parameters.Add(new DataField() { Name = "@UserID", Value = userinfo.UserID });
        //    }
        //    if (string.IsNullOrWhiteSpace(userinfo.UserName))
        //    {
        //        sbsql.Append(" UserName=@UserName and");
        //        parameters.Add(new DataField() { Name = "@UserName", Value = userinfo.UserName });
        //    }
        //    if (string.IsNullOrWhiteSpace(userinfo.Nickname))
        //    {
        //        sbsql.Append(" Nickname=@Nickname and");
        //        parameters.Add(new DataField() { Name = "@Nickname", Value = userinfo.Nickname });
        //    }
        //    if (string.IsNullOrWhiteSpace(userinfo.Password))
        //    {
        //        sbsql.Append(" Password=@Password and");
        //        parameters.Add(new DataField() { Name = "@Password", Value = userinfo.Password });
        //    }
        //    if (userinfo.Status != null)
        //    {
        //        sbsql.Append(" Status=@Status and");
        //        parameters.Add(new DataField() { Name = "@Status", Value = userinfo.Status });
        //    }
        //    if (userinfo.UserType != null)
        //    {
        //        sbsql.Append(" UserType=@UserType and");
        //        parameters.Add(new DataField() { Name = "@UserType", Value = userinfo.UserType });
        //    }
        //    string sql = sbsql.ToString();
        //    sql = sql.Substring(0, sql.Length - 4);
        //    return new OperateDB().Add<UserInfo_DAL>(sbsql.ToString(), parameters);
        //}
    }
}
