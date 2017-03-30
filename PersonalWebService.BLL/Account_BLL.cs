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
using Dapper;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PersonalWebService.BLL
{
    public class Account_BLL
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

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public byte[] GetVerificationCode()
        {
            RetrieveValueCN rvPrev1 = SessionState.GetSession<RetrieveValueCN>("VFCCodeCN");
            if (rvPrev1 != null && rvPrev1.SaveTime.AddMilliseconds(100) > DateTime.Now)
            {
                System.Threading.Thread.Sleep(100);
            }
            try
            {
                int[] vcfCode;
                VerificationCode2_Helper vcfHelper2 = new VerificationCode2_Helper();
                Bitmap vcfImage = vcfHelper2.GetVerificationCodeAsImageDate(out vcfCode);
                MemoryStream stream = new MemoryStream();
                vcfImage.Save(stream, ImageFormat.Gif);

                RetrieveValueCN rvPrevCN = new RetrieveValueCN();
                rvPrevCN.SaveTime = DateTime.Now;
                rvPrevCN.ValidateCode = vcfCode;
                SessionState.SaveSession(rvPrevCN, "VFCCodeCN");
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        public ReturnStatus_Model VerifyUserInfoIndex(UserLoginTwo user)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "登录提示";

            RetrieveValueCN rvPrevIndex = SessionState.GetSession<RetrieveValueCN>("VFCCodeCN");
            SessionState.RemoveSession("VFCCodeCN");
            if (rvPrevIndex == null || !VerificationCode2_Helper.IsPass(user.ValidateCode, rvPrevIndex.ValidateCode) || rvPrevIndex.SaveTime.AddMinutes(5) < DateTime.Now)
            {
                rsModel.message = "验证码有误或已过期，请重新输入";
                return rsModel;
            }

            UserInfo userInfo = new UserInfo();
            try
            {
                string sql = string.Format(sqlSelectTemplate, "TOP 1 *", " UserName=@UserName AND State!=-100");
                var args = new DynamicParameters();
                args.Add("@UserName", user.UserName);
                userInfo = dal.GetDataSingle<UserInfo>(sql, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "服务器错误，请稍后重试";
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
                    LogRecord_Helper.RecordLog(LogLevels.Error, "用户ID为：" + userInfo.UserId + "的账户存在问题");
                    return rsModel;
                }

                if (userInfo.UserName.Equals(user.UserName) && userInfo.Password.Equals(aesE.AESEncrypt(user.PassWord)))
                {
                    string sqlUpTime = string.Format(sqlUpdateTemplate, " LastvisitDate=GETDATE(),NowStatus=@NowStatus ", " UserId=@UserId ");
                    var args = new DynamicParameters();
                    args.Add("@NowStatus", NowStatus.已登录);
                    args.Add("@UserId", userInfo.UserId);
                    dal.OpeData(sqlUpTime, args);

                    rsModel.isRight = true;
                    rsModel.message = "用户登录成功！";

                    userInfo.NowStatus = NowStatus.已登录;
                    SessionState.SaveSession(userInfo, "UserInfo");
                }
                else
                {
                    rsModel.message = "用户名或密码有误，请重新输入登录账户";
                }
            }
            return rsModel;
        }

        public byte[] GetVerificationCode2()
        {
            RetrieveValue rvPrevCode2 = SessionState.GetSession<RetrieveValue>("VFCCode");
            if (rvPrevCode2 != null && rvPrevCode2.SaveTime.AddMilliseconds(300) > DateTime.Now)
            {
                System.Threading.Thread.Sleep(300);
            }
            try
            {
                YZMHelper yzmChild = new YZMHelper();
                yzmChild.CreateImage();

                RetrieveValue rv = new RetrieveValue();
                rv.ValidateCode = yzmChild.Text;
                rv.SaveTime = DateTime.Now;

                SessionState.SaveSession(rv, "VFCCode");

                Bitmap vcfImage = yzmChild.Image;
                MemoryStream stream = new MemoryStream();
                vcfImage.Save(stream, ImageFormat.Gif);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
            }
            return null;

        }

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

            var userInfoSession = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfoSession != null)
            {
                rsModel.message = "当前用户已存在，请勿重复登录";
                return rsModel;
            }

            RetrieveValue rvPrevVertify = SessionState.GetSession<RetrieveValue>("VFCCode");
            SessionState.RemoveSession("VFCCode");
            if (rvPrevVertify == null || !rvPrevVertify.ValidateCode.Equals(user.ValidateCode) || rvPrevVertify.SaveTime.AddMinutes(5) < DateTime.Now)
            {
                rsModel.message = "验证码有误，请重新输入";
                return rsModel;
            }

            UserInfo userInfo = new UserInfo();
            try
            {
                string sql = string.Format(sqlSelectTemplate, "TOP 1 *", " UserName=@UserName AND State!=-100");
                var args = new DynamicParameters();
                args.Add("@UserName", user.UserName);
                userInfo = dal.GetDataSingle<UserInfo>(sql, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "服务器错误，请稍后重试";
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
                    LogRecord_Helper.RecordLog(LogLevels.Error, "用户ID为：" + userInfo.UserId + "的账户存在问题");
                    return rsModel;
                }

                if (userInfo.UserName.Equals(user.UserName) && userInfo.Password.Equals(aesE.AESEncrypt(user.PassWord)))
                {
                    string sqlUpTime = string.Format(sqlUpdateTemplate, " LastvisitDate=GETDATE(),NowStatus=@NowStatus ", " UserId=@UserId ");
                    var args = new DynamicParameters();
                    args.Add("@NowStatus", NowStatus.已登录);
                    args.Add("@UserId", userInfo.UserId);
                    dal.OpeData(sqlUpTime, args);

                    rsModel.isRight = true;
                    rsModel.message = "用户登录成功！";

                    userInfo.NowStatus = NowStatus.已登录;
                    SessionState.SaveSession(userInfo, "UserInfo");
                }
                else
                {
                    rsModel.message = "用户名或密码有误，请重新输入登录账户";
                }
            }
            return rsModel;
        }

        public ReturnStatus_Model SendEmail(string email, string sessionKey)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.title = "验证码发送";
            rsModel.isRight = false;
            //进行检查邮件验证码上次发送时间是否符合规定的发送时间间隔
            RetrieveValue rvPrevEmail = SessionState.GetSession<RetrieveValue>(sessionKey);
            if (rvPrevEmail != null && rvPrevEmail.SaveTime.AddMinutes(sendEmailInterval) > DateTime.Now)
            {
                rsModel.message = "上次发送时间为：" + rvPrevEmail.SaveTime + ",请勿频繁发送";
                return rsModel;
            }

            var args = new DynamicParameters();
            string whereStr = "";
            args.Add("@UserName", email);
            whereStr = " UserName=@UserName ";
            if (!sessionKey.Equals("RegisterSendEmail"))
            {
                try
                {
                    string sql = string.Format(sqlSelectTemplate, " Count(*) ", whereStr);
                    if (dal.GetDataCount(sql, args) < 1)
                    {
                        rsModel.message = "当前邮箱地址不存在，请重新填写";
                        return rsModel;
                    }
                }
                catch (Exception ex)
                {
                    LogRecord_Helper.RecordLog(LogLevels.Error, ex);
                    rsModel.message = "服务器错误，请稍后尝试";
                    return rsModel;
                }
            }

            YZMHelper yzmChild = new YZMHelper();
            try
            {
                if (emailHelper.SendEmail(email, yzmChild.Text))
                {
                    RetrieveValue rvEmail = new RetrieveValue();

                    rvEmail.ValidateCode = email;
                    rvEmail.SaveTime = DateTime.Now;
                    SessionState.SaveSession(rvEmail, sessionKey + "Email");

                    RetrieveValue rv = new RetrieveValue();
                    rv.ValidateCode = yzmChild.Text;
                    rv.SaveTime = DateTime.Now;
                    SessionState.SaveSession(rv, sessionKey);

                    rsModel.isRight = true;
                    rsModel.message = "邮件发送成功！";
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
                rsModel.message = "邮件发送失败！请稍后重试";
            }
            return rsModel;
        }

        /// <summary>
        /// 初步注册用户，详细内容将在登录后填写
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        public ReturnStatus_Model FirstRegisterUserInfo(UserRegister userRegister)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "注册用户";
            //首先各种验证
            var userInfoSession = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfoSession != null)
            {
                rsModel.message = "当前用户已存在，请勿重复注册";
                return rsModel;
            }

            RetrieveValue rvPrevFirst = SessionState.GetSession<RetrieveValue>("RegisterSendEmail");
            RetrieveValue rvPrevEmail = SessionState.GetSession<RetrieveValue>("RegisterSendEmail" + "Email");
            SessionState.RemoveSession("RegisterSendEmail");//注册走完后就删除session,防止二次使用
            SessionState.RemoveSession("RegisterSendEmail" + "Email");
            if (rvPrevFirst == null || rvPrevFirst.SaveTime.AddMinutes(Convert.ToDouble(Email_Helper.emailTimeFrame)) < DateTime.Now)
            {
                rsModel.message = "当前验证码已过期，请重新发送邮件进行查看";
                return rsModel;
            }

            if (rvPrevEmail == null || rvPrevEmail.SaveTime.AddMinutes(Convert.ToDouble(Email_Helper.emailTimeFrame)) < DateTime.Now ||
                !rvPrevEmail.ValidateCode.Equals(userRegister.UserName))
            {
                rsModel.message = "当前邮箱地址非发送邮箱地址，请认真填写";
                return rsModel;
            }

            if (!rvPrevFirst.ValidateCode.Equals(userRegister.ValidateCode))
            {
                rsModel.message = "验证码输入有误，请重新发送验证码并验证";
                return rsModel;
            }

            //数据装入model
            UserInfo userInfoS = new UserInfo();
            userInfoS.UserName = userRegister.UserName;
            userInfoS.NickName = userRegister.NickName;
            userInfoS.Password = aesE.AESEncrypt(userRegister.PassWord);
            userInfoS.AddTime = DateTime.Now;
            userInfoS.State = State.正常;
            userInfoS.NowStatus = NowStatus.未登录;

            //查询email和昵称是否存在于数据库中
            List<DataField> param = new List<DataField>();
            var args = new DynamicParameters();
            args.Add("@UserName", userInfoS.UserName);
            string sql = string.Format(sqlSelectTemplate, "  Count(*) ", " UserName=@UserName ");
            if (dal.GetDataCount(sql, args) == 1)
            {
                rsModel.message = "当前email账号已存在,请重新选择Email账号进行注册";
                return rsModel;
            }

            args.Add("@NickName", userInfoS.NickName);
            sql = string.Format(sqlSelectTemplate, " Count(*) ", " NickName=@NickName");
            if (dal.GetDataCount(sql, args) == 1)
            {
                rsModel.message = "当前用户昵称已存在,请重新选择昵称进行注册";
                return rsModel;
            }

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
            return rsModel;
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        public UserInfo_Model GetUserInfo()
        {
            UserInfo userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo == null || string.IsNullOrEmpty(userInfo.NickName))
                return null;
            UserInfo_Model userInfoModel = new UserInfo_Model();
            userInfoModel.UserId = userInfo.UserId;
            userInfoModel.NickName = userInfo.NickName;
            userInfoModel.Introduce = userInfo.Introduce;
            userInfoModel.AccountPicture = userInfo.AccountPicture;
            userInfoModel.AddTime = userInfo.AddTime;
            return userInfoModel;
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
            RetrieveValue rvPrevRPwd = SessionState.GetSession<RetrieveValue>("RetrieveValidateCode");
            if (rvPrevRPwd != null && rvPrevRPwd.SaveTime.AddMinutes(sendEmailInterval) > DateTime.Now)
            {
                rsModel.message = "上次发送时间为：" + rvPrevRPwd.SaveTime + ",请勿频繁发送";
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
            RetrieveValue rvPrevVC = SessionState.GetSession<RetrieveValue>("RetrieveValidateCode");
            SessionState.RemoveSession("RetrieveValidateCode");
            if (rvPrevVC == null || rvPrevVC.SaveTime.AddMinutes(Convert.ToDouble(Email_Helper.emailTimeFrame)) > DateTime.Now)
            {
                rsModel.message = "当前验证码已过期，请重新发送邮件进行查看";
                return rsModel;
            }

            if (!rvPrevVC.ValidateCode.Equals(resetPwd.Password))
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
            string sqlUpdate = string.Format(sqlUpdateTemplate, "PassWord=@PassWord", "UserName=@UserName");
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

        public ReturnStatus_Model VertifyCodeSet(string pwd)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "重置密码";
            var rtEmail = SessionState.GetSession<RetrieveValue>("RetrieveSetPwd");
            SessionState.RemoveSession("RetrieveSetPwd");
            if (rtEmail == null || rtEmail.SaveTime.AddMinutes(20) < DateTime.Now)
            {
                rsModel.message = "当前重置密码操作过期，请重新操作";
                return rsModel;
            }

            try
            {
                string sqlUpdate = string.Format(sqlUpdateTemplate, "PassWord=@PassWord", "UserName=@UserName");
                var args = new DynamicParameters();
                args.Add("@UserName", rtEmail.ValidateCode);
                args.Add("@PassWord", aesE.AESEncrypt(pwd));
                if (dal.OpeData(sqlUpdate, args))
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
                rsModel.message = "服务器错误，请稍后重试或重新操作找回密码";
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
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改用户信息";

            UserInfo userInfos = SessionState.GetSession<UserInfo>("UserInfo");
            userInfo.Password = aesE.AESEncrypt(userInfo.Password);

            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!userInfos.Introduce.Equals(userInfo.Introduce))
            {
                sbsql.Append("Introduce=@Introduce,");
                param.Add(new DataField { Name = "@Introduce", Value = userInfo.Introduce });
            }
            if (!userInfos.AccountPicture.Equals(userInfo.AccountPicture))
            {
                sbsql.Append("Introduce=@Introduce,");
                param.Add(new DataField { Name = "@Introduce", Value = userInfo.AccountPicture });
            }
            if (!userInfos.Password.Equals(userInfo.Password))
            {
                sbsql.Append("Password=@Password,");
                param.Add(new DataField { Name = "@Password", Value = userInfo.Password });
            }
            sbsql.Append("EditTime=GETDATE()");
            param.Add(new DataField { Name = "@UserId", Value = userInfos.UserId });
            string sqlupdate = string.Format(sqlUpdateTemplate, sbsql.ToString(), "UserId=@UserId");
            try
            {
                if (dal.OpeData(sqlupdate, param))
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
            userInfoS.UserId = userInfo.UserId;
            userInfoS.UserName = userInfo.UserName;
            userInfoS.NickName = userInfo.NickName;
            userInfoS.AccountPicture = userInfo.AccountPicture;
            userInfoS.Password = userInfo.Password;
            userInfoS.AddTime = DateTime.Now;
            userInfoS.EditTime = userInfo.EditTime;
            userInfoS.State = State.正常;
            userInfoS.NowStatus = NowStatus.未登录;

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
            return rsModel;
        }
        /// <summary>
        /// 获取具有条件的用户列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<UserInfo_Model> GetUserInfoList(UserInfoCondition condition)
        {
            List<UserInfo_Model> userInfoMList = new List<UserInfo_Model>();
            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!string.IsNullOrEmpty(condition.UserId))
            {
                sbsql.Append("U.UserId=@UserId AND ");
                param.Add(new DataField { Name = "@UserId", Value = condition.UserId });
            }
            if (!string.IsNullOrEmpty(condition.UserName))
            {
                sbsql.Append("U.UserName=@UserName AND ");
                param.Add(new DataField { Name = "@UserName", Value = condition.UserName });
            }
            if (!string.IsNullOrEmpty(condition.NickName))
            {
                sbsql.Append("U.NickName=@NickName AND ");
                param.Add(new DataField { Name = "@NickName", Value = condition.NickName });
            }

            if (condition.CreationTime != null)
            {
                sbsql.Append("U.CreationTime>=@CreationTime AND ");
                param.Add(new DataField { Name = "@CreationTime", Value = condition.CreationTime });
            }
            if (condition.EditTime != null)
            {
                sbsql.Append("U.EditTime<=@EditTime AND ");
                param.Add(new DataField { Name = "@EditTime", Value = condition.EditTime });
            }
            if (condition.Status != null)
            {
                sbsql.Append("U.Status=@Status AND ");
                param.Add(new DataField { Name = "@Status", Value = Convert.ToInt32(condition.Status) });
            }
            if (!string.IsNullOrEmpty(condition.ArticleId))
            {
                sbsql.Append("A.ArticleId=@ArticleId AND ");
                param.Add(new DataField { Name = "@ArticleId", Value = condition.ArticleId });
            }
            if (!string.IsNullOrEmpty(condition.PictureId))
            {
                sbsql.Append("P.PictureId=@PictureId AND ");
                param.Add(new DataField { Name = "@PictureId", Value = condition.PictureId });
            }
            string sqlIndex = string.Empty;
            if (condition.PageIndex != null)
            {
                int pageIndex = Convert.ToInt32(condition.PageIndex);
                int firstIndex = (pageIndex - 1) * PageNum + 1;
                int lastIndex = pageIndex * PageNum;
                sqlIndex = " NUM>=@firstIndex AND NUM<=@lastIndex ";
                param.Add(new DataField { Name = "@firstIndex", Value = firstIndex });
                param.Add(new DataField { Name = "@lastIndex", Value = lastIndex });
            }
            else
            {
                sbsql.Append(" NUM<=@PageNum ");
                param.Add(new DataField { Name = "@PageNum", Value = PageNum });
            }
            sbsql.Append("U.State>=0");
            string sqlSelect = @"SELECT * FROM
                                 (
	                                SELECT ROW_NUMBER() OVER(ORDER BY AddTime DESC) NUM,* FROM
	                                (  
	                                   SELECT DISTINCT U.* FROM [dbo].[UserInfo] U
	                                   LEFT JOIN [dbo].[Article] A on U.UserId=A.UserId
	                                   LEFT JOIN [dbo].[Picture] P on U.UserId=P.UserId
	                                   WHERE {0}
	                                ) UA
                                 )UI WHERE {1}";
            try
            {
                string sql = string.Format(sqlSelect, sbsql.ToString(), sqlIndex);
                List<UserInfo> userInfolist = dal.GetDataList<UserInfo>(sql, param);
                if (userInfolist != null || userInfolist.Count > 0)
                {
                    userInfolist.Select(l =>
                    {
                        userInfoMList.Add(new UserInfo_Model
                        {
                            UserId = l.UserId,
                            UserName = l.UserName,
                            NickName = l.NickName,
                            Introduce = l.Introduce,
                            AccountPicture = l.AccountPicture,
                            AddTime = l.AddTime,
                            EditTime = l.EditTime,
                            NowStatus = l.NowStatus,
                            State = l.State
                        });
                        return true;
                    });
                    return userInfoMList;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }


        public ReturnStatus_Model ContrastUser(string email, string nickName)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "查询用户数据";

            bool isEmailnull = string.IsNullOrEmpty(email);
            bool isUserNamenull = string.IsNullOrEmpty(nickName);

            if (isEmailnull && isUserNamenull)
            {
                rsModel.message = "你验证的内容为空，请重新添加后进行验证";
                return rsModel;
            }
            List<DataField> param = new List<DataField>();
            var args = new DynamicParameters();
            string whereStr = "";
            if (!isEmailnull)
            {
                args.Add("@UserName", email);
                whereStr = " UserName=@UserName ";
            }
            else
            {
                args.Add("@NickName", nickName);
                whereStr = " NickName=@NickName ";
            }
            try
            {
                string sql = string.Format(sqlSelectTemplate, " Count(*) ", whereStr);
                if (dal.GetDataCount(sql, args) < 1)
                {
                    rsModel.isRight = false;
                    rsModel.message = "不存在当前用户";
                    return rsModel;
                }

                rsModel.isRight = true;
                rsModel.message = "查询已存在";
                return rsModel;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        /// <summary>
        /// 修改用户数据（只限管理员）
        /// </summary>
        /// <param name="editUserInfo"></param>
        /// <returns></returns>
        public ReturnStatus_Model AdminEditUserInfo(AdminEditUserInfo editUserInfo)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改用户信息";
            if (string.IsNullOrEmpty(editUserInfo.UserId) || Utility_Helper.IsClassIds(new string[] { editUserInfo.UserId }))
            {
                rsModel.message = "删除的用户Id为为空或不存在，请检查后再次尝试";
                return rsModel;
            }
            AdminInfo adminInfo = SessionState.GetSession<AdminInfo>("AdminInfo");
            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!string.IsNullOrEmpty(editUserInfo.UserName))
            {
                sbsql.Append("UserName=@UserName,");
                param.Add(new DataField { Name = "@UserName", Value = editUserInfo.UserName });
            }
            if (!string.IsNullOrEmpty(editUserInfo.NickName))
            {
                sbsql.Append("Nickname=@Nickname,");
                param.Add(new DataField { Name = "@Nickname", Value = editUserInfo.NickName });
            }
            if (!string.IsNullOrEmpty(editUserInfo.Password))
            {
                sbsql.Append("Password=@Password,");
                param.Add(new DataField { Name = "@Password", Value = editUserInfo.Password });
            }
            sbsql.Append("EditTime=GETDATE()");
            param.Add(new DataField { Name = "@UserId", Value = editUserInfo.UserId });
            string sqlupdate = string.Format(sqlUpdateTemplate, sbsql.ToString(), "UserId=@UserId");
            try
            {
                if (dal.OpeData(sqlupdate, param))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改用户数据成功";
                    LogRecord_Helper.RecordLog(LogLevels.Trace, "管理员：" + adminInfo.AdminId + " 修改了" + editUserInfo.UserId + " 的用户数据");
                }
                else
                {
                    rsModel.isRight = false;
                    rsModel.message = "修改用户数据失败！";
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
                rsModel.isRight = false;
                rsModel.message = "服务器错误，请稍后重试";
                LogRecord_Helper.RecordLog(LogLevels.Trace, "管理员：" + adminInfo.AdminId + " 尝试修改" + editUserInfo.UserId + " 的用户数据失败");
            }
            return rsModel;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userInfoList"></param>
        /// <returns></returns>
        public ReturnStatus_Model DeleteUserinfoList(string[] userInfoIdList)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "用户删除";

            if (userInfoIdList.Length > 100)
            {
                rsModel.message = "你的一次删除操作太多，不予执行！";
                return rsModel;
            }

            if (userInfoIdList == null || userInfoIdList.Length <= 0)
            {
                rsModel.message = "需要操作删除的图片为空，请先选择需要删除的图片";
                return rsModel;
            }

            AdminInfo adminInfo = SessionState.GetSession<AdminInfo>("AdminInfo");
            if (adminInfo == null || string.IsNullOrEmpty(adminInfo.AdminId))
            {
                rsModel.message = "你未登录账号或账号已过期，请重新登录！";
                return rsModel;
            }

            if (adminInfo.Level >= 0)
            {
                rsModel.isRight = false;
                rsModel.message = "您的权限不足，不能进行删除操作！";
            }

            if (dal.GetDataCount("SELECT COUNT(*) FROM [dbo].[AdminInfo] WHERE AdminId='" + adminInfo.AdminId + "' AND Pwd='" + adminInfo.Pwd + "'", null) != 1)
            {
                rsModel.isRight = false;
                rsModel.message = "管理员账户存在问题，请稍后重试";
                LogRecord_Helper.RecordLog(LogLevels.Warn, "管理员账户在删除用户操作时错误,管理员Id为：" + adminInfo.AdminId);
                SessionState.RemoveSession("AdminInfo");
                return rsModel;
            }

            string ids = string.Empty;
            if (Utility_Helper.IsClassIds(userInfoIdList))
            {
                rsModel.message = "你所需要操作的内容不合法！账号已被记录，请规范操作！";
                StringBuilder pcitureids = new StringBuilder();
                userInfoIdList.Select(l => { pcitureids.Append(l); return true; });
                LogRecord_Helper.RecordLog(LogLevels.Warn, "错误删除用户操作，怀疑为sql注入,管理员Id为" + adminInfo.AdminId + "，输入信息为" + pcitureids.ToString());
                return rsModel;
            }

            userInfoIdList.Select(l =>
            {
                ids += "'" + l + "'" + ",";
                return true;
            });

            ids = ids.Substring(0, ids.Length - 1);
            string sql = string.Format(sqlDeleteTemplate, "UserId in(" + ids + ")");

            try
            {
                if (dal.OpeData(sql, null))
                {
                    rsModel.isRight = true;
                    rsModel.message = "删除成功！";
                    return rsModel;
                }
                else
                {
                    rsModel.isRight = false;
                    rsModel.message = "你删除的内容有误，请查看日志！";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.isRight = false;
                rsModel.message = "系统出现一个问题，请查看日志！";
                return rsModel;
            }
        }

    }
}
