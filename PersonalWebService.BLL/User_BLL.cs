using Dapper;
using PersonalWebService.DAL;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.BLL
{
    public class User_BLL
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public static readonly int PageNum = 12;
        private static AESEncryptS aesE = new AESEncryptS();
        private static Email_Helper emailHelper = new Email_Helper();
        private static double sendEmailInterval = Convert.ToDouble(ConfigurationManager.AppSettings["SendEmailInterval"]);
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[UserInfo] WHERE {1}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[UserInfo] SET {0} WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[UserInfo] WHERE {0}";
        public UserInfo_Model GetUserInfo_Model(string email)
        {
            UserInfo_Model model = new UserInfo_Model();
            UserInfo userInfo = new UserInfo();
            try
            {
                userInfo = GetUserInfo(email);
                if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
                {
                    return null;
                }
                model.UserId = userInfo.UserId;
                model.Email = userInfo.Email;
                model.NickName = userInfo.NickName;
                model.Introduce = userInfo.Introduce;
                model.AccountPicture = userInfo.AccountPicture;
                model.Password = userInfo.Password;
                model.Address = userInfo.Address;
                model.SelfDmainName = userInfo.SelfDmainName;
                model.EXP = userInfo.EXP;
                model.AddTime = userInfo.AddTime;
                model.EditTime = userInfo.EditTime;
                model.NowStatus = userInfo.NowStatus;
                model.State = userInfo.State;

                return model;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
            }
            return null;
        }

        /// <summary>
        /// 获取用户数
        /// </summary>
        /// <param name="email"></param>
        /// <param name="nickName"></param>
        /// <param name="isEmail"></param>
        /// <returns></returns>
        public bool getUserCount(string email, string nickName, bool isEmail)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(nickName))
            {
                return false;
            }
            try
            {
                var args = new DynamicParameters();
                string whereStr = " Email=@Email ";
                if (isEmail)
                {
                    args.Add("@Email", email);
                }
                else
                {
                    args.Add("@NickName", nickName);
                    whereStr = " NickName=@NickName ";
                }
                string sql = string.Format(sqlSelectTemplate, "  Count(*) ", whereStr);
                return dal.GetDataCount(sql, args)==1;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
            }
            return false;
        }

        /// <summary>
        /// 获取UserInfo
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string email)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                string sql = string.Format(sqlSelectTemplate, "*", " Email=@Email AND State!=-100");
                var args = new DynamicParameters();
                args.Add("@Email", email);
                userInfo = dal.GetDataSingle<UserInfo>(sql, args);
                if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
            }
            return null;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool DeleteUserInfo(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            UserInfo userInfo = new UserInfo();
            userInfo = GetUserInfo(email);
            if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
            {
                return false;
            }
            try
            {
                UserInfo userInfoChild = new UserInfo();
                userInfoChild.UserId = userInfo.UserId;
                userInfoChild.Email = userInfo.Email;
                return dal.OpeData(userInfoChild, OperatingModel.Delete);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return false;
        }

        /// <summary>
        /// 增加用户，用户注册
        /// </summary>
        /// <param name="userInfoF"></param>
        /// <returns></returns>
        public bool AddUserInfo(UserInfo userInfoF)
        {
            UserInfo userInfo = new UserInfo();
            userInfo = GetUserInfo(userInfoF.Email);
            if (userInfo != null)
            {
                return false;
            }
            try
            {
                return dal.OpeData(userInfoF, OperatingModel.Add);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return false;
        }
    }
}
