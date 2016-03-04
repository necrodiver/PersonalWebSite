using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.BLL
{
    public class Account_BLL
    {
        private static Helper.YZMHelper yzM = new Helper.YZMHelper();
        private DAL.UserInfo_DAL userinfoDal = new DAL.UserInfo_DAL();
        public ReturnStatus_Model VerifyUserInfo(UserLogin user)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
          

            rsModel.isRight = false;
            rsModel.title = "登录问题";

            if (!user.ValidateCode.Equals(yzM.Text, StringComparison.OrdinalIgnoreCase))
            {
                rsModel.message = "验证码有误，请刷新验证码后重新输入";
            }


            List<UserInfo_Model> userInfoList = userinfoDal.GetUserInfoList(new UserInfo_Model { UserName = user.UserName });
            UserInfo_Model userInfo = new UserInfo_Model();
            if (userInfoList == null)
            {
                rsModel.message = "当前不存在此账户，请刷新验证码后重新输入";
            }


            if (userInfo == null || userInfo.UserID == null)
                rsModel.message = "当前不存在此账户，请刷新验证码后重新输入";
            if (!userInfo.UserName.Equals(user.UserName) || !userInfo.Password.Equals(user.PassWord))
                rsModel.message = "用户名或密码有误，请刷新验证码后重新输入";
            if (true)
            { 
                rsModel.isRight = true;
                rsModel.title = "登录成功";
                rsModel.message = userInfo.UserName;
                new SessionState().SaveSession<UserInfo_Model>(userInfo, "UserInfo");
            }
            yzM.CreateImage();
            return rsModel;
        }
    }
}
