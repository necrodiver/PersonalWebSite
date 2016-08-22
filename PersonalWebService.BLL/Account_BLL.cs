//using PersonalWebService.Helper;
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
    public class Account_BLL
    {
        private static YZMHelper yzM = new YZMHelper();
        private static AESEncryptS aesE = new AESEncryptS();
        private static Email_Helper emailHelper = new Email_Helper();
        private static double sendEmailInterval = Convert.ToDouble(ConfigurationManager.AppSettings["SendEmailInterval"]);
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[UserInfo] WHERE {1}";
        private static readonly string sqlUpdateTemple = "UPDATE [dbo].[UserInfo] SET {0}";
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="user">用户登录实体类</param>
        /// <returns></returns>
        public ReturnStatus_Model VerifyUserInfo(UserLogin user)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();

            rsModel.isRight = false;
            rsModel.title = "用户登录";

            //验证码验证错误时
            if (!user.ValidateCode.Equals(yzM.Text, StringComparison.OrdinalIgnoreCase))
            {
                rsModel.message = "验证码有误，请刷新验证码后重新输入";
                yzM.CreateImage();
                return rsModel;
            }
            UserInfo userInfo = new UserInfo();
            try
            {
                string sql = string.Format(sqlSelectTemplate, "TOP 1 *", " UserName=@UserName");
                userInfo = dal.GetDataSingle<UserInfo>(sql, new DataField { Name = "@UserName", Value = user.UserName });
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "服务器错误，请稍后重试";
                yzM.CreateImage();
                return rsModel;
            }

            //无用户
            if (userInfo == null || string.IsNullOrEmpty(userInfo.UserName))
            {
                rsModel.message = "不存在此账户，请重新登录或注册后进行登录";
            }
            else
            {
                if (userInfo.UserName == null || userInfo.Password == null)
                {
                    rsModel.message = "当前用户存在问题，请联系管理员进行处理";
                    LogRecord_Helper.RecordLog(LogLevels.Error, "用户ID为：" + userInfo.UserID + "的账户存在问题");
                    return rsModel;
                }

                if (userInfo.UserName.Equals(user.UserName) && userInfo.Password.Equals(aesE.AESEncrypt(user.PassWord)))
                {
                    rsModel.isRight = true;
                    rsModel.message = "用户登录成功！";
                    SessionState.SaveSession(userInfo, "UserInfo");
                }
                else
                {
                    rsModel.message = "用户名或密码有误，请重新输入登录账户";
                }
            }
            yzM.CreateImage();
            return rsModel;
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public UserInfo_Model GetUserInfo()
        {
            UserInfo_Model userInfo = SessionState.GetSession<UserInfo_Model>("UserInfo");
            if (userInfo == null || string.IsNullOrEmpty(userInfo.Nickname))
                return null;
            return userInfo;
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="retrievePwd"></param>
        /// <returns></returns>
        public ReturnStatus_Model RetrievePwd(RetrievePwdStart retrievePwd)
        {

            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "找回密码";

            //进行检查邮件验证码上次发送时间是否符合规定的发送时间间隔
            RetrieveValue rvPrev = SessionState.GetSession<RetrieveValue>("RetrieveValidateCode");
            if (rvPrev != null && rvPrev.SaveTime.AddMinutes(sendEmailInterval) < DateTime.Now)
            {
                rsModel.message = "上次发送时间为：" + rvPrev.SaveTime + ",请勿频繁发送";
                return rsModel;
            }

            try
            {
                string sql = string.Format(sqlSelectTemplate, "Count(*)", "UserName=@UserName");
                int count = dal.GetDataCount(sql, new DataField { Name = "@UserName", Value = retrievePwd.Email });
                if (count == 1)
                {
                    //准备进行发送邮件
                    YZMHelper yzmChild = new YZMHelper();
                    RetrieveValue rv = new RetrieveValue();
                    rv.ValidateCode = yzmChild.Text;
                    rv.SaveTime = DateTime.Now;
                    SessionState.SaveSession(rv, "RetrieveValidateCode");
                    if (emailHelper.SendEmail(retrievePwd.Email, yzmChild.Text))
                    {
                        rsModel.isRight = true;
                        rsModel.message = "邮件发送成功！";
                        return rsModel;
                    }
                }
                else if (count == 0)
                {
                    rsModel.message = "当前用户不存在，请输入正确的注册的用户Email";
                }
                else if (count > 1)
                {
                    rsModel.message = "当前用户存在问题，请联系管理员进行查看";
                    LogRecord_Helper.RecordLog(LogLevels.Fatal, "账户：" + retrievePwd.Email + " 存在问题，查询出现多个此账户，请检查程序和数据库是否存在问题");
                }
            }
            catch (Exception ex)
            {
                rsModel.message = "服务器错误，请稍后重试";
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            yzM.CreateImage();
            return rsModel;
        }

        /// <summary>
        /// 验证找回密码并重置密码
        /// </summary>
        /// <returns></returns>
        public ReturnStatus_Model VertifyCode(ResetPwd resetPwd)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "找回密码";
            //首先各种验证
            RetrieveValue rvPrev = SessionState.GetSession<RetrieveValue>("RetrieveValidateCode");
            if (rvPrev == null || rvPrev.SaveTime.AddMinutes(Convert.ToDouble(Email_Helper.emailTimeFrame)) > DateTime.Now)
            {
                rsModel.message = "当前验证码已过期，请重新发送邮件进行查看";
                return rsModel;
            }

            if (!rvPrev.ValidateCode.Equals(resetPwd.Password))
            {
                rsModel.message = "验证码输入有误，请重新输入";
                return rsModel;
            }

            string sql = string.Format(sqlSelectTemplate, "Count(*)", "UserName=@UserName");
            try
            {
                if (dal.GetDataCount(sql, new DataField { Name = "@UserName", Value = resetPwd.Email }) != 1)
                {
                    rsModel.message = "登录账号有问题或不存在，请重新输入";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.message = "服务器错误，请稍后重试（请重新发送邮件），或者联系管理员";
                return rsModel;
            }

            //然后进行数据导入
            UserInfo_Model userinfo = new UserInfo_Model();
            userinfo.Password = aesE.AESEncrypt(resetPwd.Password);
            userinfo.UserName = resetPwd.Email;
            SessionState.RemoveSession("RetrieveValidateCode");
            string sqlUpdate = string.Format(sqlUpdateTemple, "PassWord=@PassWord WHERE UserName=@UserName");
            try
            {
                List<DataField> param = new List<DataField>();
                param.Add(new DataField { Name = "@UserName", Value = resetPwd.Email });
                param.Add(new DataField { Name = "PassWord", Value = resetPwd.Password });
                if (dal.OpeData(sqlUpdate, param))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改密码成功，你现在可以登录了";
                }
                else
                {
                    rsModel.message = "修改失败，请稍后重试（请重新发送邮件）";
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.message = "服务器错误，请稍后重试（请重新发送邮件），或者联系管理员";
            }
            return rsModel;
        }

        /// <summary>
        /// 用户修改数据
        /// </summary>
        /// <param name="userInfo">用户信息实体类</param>
        /// <returns></returns>
        public ReturnStatus_Model EditUserInfo(UserInfo_Model userInfo)
        {
            UserInfo userInfoS = new UserInfo();
            userInfoS.UserID = userInfo.UserID;
            userInfoS.UserName = userInfo.UserName;
            userInfoS.Nickname = userInfo.Nickname;
            userInfoS.AccountPicture = userInfo.AccountPicture;
            userInfoS.Password = userInfo.Password;
            userInfoS.EditTime = userInfo.EditTime;
            userInfoS.Status = userInfo.Status;
            userInfoS.UserType = userInfo.UserType;

            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改用户信息";
            try
            {
                if (dal.OpeData(userInfoS, OperatingModel.Edit))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改用户数据成功";
                }
                else
                {
                    rsModel.message = "修改用户数据失败！";
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
                rsModel.message = "服务器错误，请稍后重试";
            }
            return rsModel;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userInfo">用户信息实体类</param>
        /// <returns></returns>
        public ReturnStatus_Model AddUserInfo(UserInfo_Model userInfo)
        {

            UserInfo userInfoS = new UserInfo();
            userInfoS.UserID = userInfo.UserID;
            userInfoS.UserName = userInfo.UserName;
            userInfoS.Nickname = userInfo.Nickname;
            userInfoS.AccountPicture = userInfo.AccountPicture;
            userInfoS.Password = userInfo.Password;
            userInfoS.CreationTime = DateTime.Now;
            userInfoS.EditTime = userInfo.EditTime;
            userInfoS.Status = userInfo.Status;
            userInfoS.UserType = userInfo.UserType;

            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "注册用户";

            try
            {
                if (dal.OpeData(userInfoS, OperatingModel.Add))
                {
                    rsModel.isRight = true;
                    rsModel.message = "注册成功";
                }
                else
                {
                    rsModel.message = "注册失败";
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
                rsModel.message = "服务器错误，请稍后重试";
            }
            yzM.CreateImage();
            return rsModel;
        }
        /// <summary>
        /// 获取具有条件的用户列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<UserInfo_Model> GetUserInfoList(UserInfoCondition condition)
        {
            //这条件太牛逼了，等数据库做出来了再弄吧（暂时还不知道全部表有哪些）
            throw new NotImplementedException();
        }
    }
}
