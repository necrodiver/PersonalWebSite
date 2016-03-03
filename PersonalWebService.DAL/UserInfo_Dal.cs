using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.DAL
{
    public class UserInfo_DAL
    {
        GenericityOperateDB generDB = new GenericityOperateDB();
        public bool UserInfoOpe(UserInfo_Model userInfo, OperatingModel operate)
        {
            return generDB.Operate(userInfo, operate);
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
