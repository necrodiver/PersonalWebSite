using PersonalWebService.DAL;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.BLL
{
    public class AccountAdmin_BLL
    {
        private static YZMHelper yzM = new YZMHelper();
        private static AESEncryptS aesE = new AESEncryptS();
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[SystemAdmin] WHERE {1}";
        /// <summary>
        /// 验证管理员登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ReturnStatus_Model VerifyAdmin(AdminLogin user)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();

            rsModel.isRight = false;
            rsModel.title = "管理员登录";

            //验证码验证错误时
            if (!user.ValidateCode.Equals(yzM.Text, StringComparison.OrdinalIgnoreCase))
            {
                rsModel.message = "验证码有误，请刷新验证码后重新输入";
                yzM.CreateImage();
                return rsModel;
            }
            UserInfo adminInfo = new UserInfo();
            try
            {
                string sql = string.Format(sqlSelectTemplate, "TOP 1 *", " Name=@Name");
                adminInfo = dal.GetDataSingle<UserInfo>(sql, new DataField { Name = "@Name", Value = user.UserName });
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "服务器错误，请稍后重试";
                yzM.CreateImage();
                return rsModel;
            }

            //无用户
            if (adminInfo == null || string.IsNullOrEmpty(adminInfo.UserName))
            {
                rsModel.message = "不存在此账户，请重新登录或注册后进行登录";
            }
            else
            {
                if (adminInfo.UserName == null || adminInfo.Password == null)
                {
                    rsModel.message = "当前用户存在问题，请联系管理员进行处理";
                    LogRecord_Helper.RecordLog(LogLevels.Error, "用户ID为：" + adminInfo.UserId + "的账户存在问题");
                    return rsModel;
                }

                if (adminInfo.UserName.Equals(user.UserName) && adminInfo.Password.Equals(aesE.AESEncrypt(user.PassWord)))
                {
                    string sqlUpTime = "UPDATE [dbo].[SystemAdmin] SET LastvisitDate=GETDATE() WHERE AdminId=@AdminId";
                    dal.OpeData(sqlUpTime, new DataField { Name = "@AdminId", Value = adminInfo.UserId });
                    rsModel.isRight = true;
                    rsModel.message = "管理员登录成功！";
                    SessionState.SaveSession(adminInfo, "SystemAdmin");
                }
                else
                {
                    rsModel.message = "管理员名或密码有误，请重新输入登录账户";
                }
            }
            yzM.CreateImage();
            return rsModel;
        }

        public ReturnStatus_Model EditUserInfo(EditAdmin userInfo)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改用户信息";
            if (string.IsNullOrEmpty(userInfo.Name)&&string.IsNullOrEmpty(userInfo.Pwd))
            {
                rsModel.message = "修改内容不能为空！";
                return rsModel;
            }

            SystemAdmin userInfoS = SessionState.GetSession<SystemAdmin>("SystemAdmin");
            if(!string.IsNullOrEmpty(userInfo.Name))
            {
                userInfoS.Name = userInfo.Name;
            }
            if (!string.IsNullOrEmpty(userInfo.Pwd))
                userInfoS.Pwd = aesE.AESEncrypt(userInfo.Pwd);
            if (userInfo.Level==AdminLevel.第一权限)
                userInfoS.Level = userInfo.Level;
            else
            {
                rsModel.message = "您的权限不足，无法修改更高级别的权限等级！";
                return rsModel;
            }

            userInfoS.EditTime = DateTime.Now;
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
        /// 获取具有条件的管理员列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<UserInfo_Model> GetAdminInfoList(AdminInfoCondition condition)
        {
            //这条件太牛逼了，等数据库做出来了再弄吧（暂时还不知道全部表有哪些）
            throw new NotImplementedException();
        }
    }
}
