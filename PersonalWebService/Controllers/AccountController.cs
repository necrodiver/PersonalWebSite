﻿using Microsoft.AspNet.Identity;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PersonalWebService.Controllers
{
    [RoutePrefix("api/Account")]
    [ModelValidationWebApiFilter]
    public class AccountController : ApiController
    {
        BLL.Account_BLL accountBll = new BLL.Account_BLL();

        [HttpPost]
        [Route("Login")]
        //登录
        public async Task<ReturnStatus_Model> Login([FromBody]UserLogin user)
        {
            return await Task.Run(() =>
            {
                return accountBll.VerifyUserInfo(user);
            });
        }

        [HttpPost]
        [Route("GetVFC")]
        public async Task<string> GetVerificationCode()
        {
            return await Task.Run(() =>
            {
                return accountBll.GetVerificationCode();
            });
        }

        [HttpPost]
        [Route("ContrastNickNameWeb")]
        public async Task<RModal> ContrastUserNameWeb([FromBody]string nickName)
        {
            return await Task.Run(() =>
            {
                ReturnStatus_Model rsModel = accountBll.ContrastUser(null, nickName);
                RModal rmodal = new RModal();
                rmodal.valid = !rsModel.isRight;
                return rmodal;
            });
        }

        [HttpPost]
        [Route("ContrastNickName")]
        public async Task<ReturnStatus_Model> ContrastNickName([FromBody]string nickName)
        {
            return await Task.Run(() =>
            {
                return accountBll.ContrastUser(null, nickName);
            });
        }

        [HttpPost]
        [Route("ContrastEmail")]
        public async Task<ReturnStatus_Model> ContrastUserEmail([FromBody]string email)
        {
            return await Task.Run(() =>
            {
                return accountBll.ContrastUser(email, null);
            });
        }

        [HttpPost]
        [Route("ContrastEmailWeb")]
        public async Task<RModal> ContrastUserEmailWeb([FromBody]string email)
        {
            return await Task.Run(() =>
            {
                ReturnStatus_Model rsModel = accountBll.ContrastUser(email, null);
                RModal rmodal = new RModal();
                rmodal.valid = !rsModel.isRight;
                return rmodal;
            });
        }

        [HttpPost]
        [BasicAuthentication]
        [Route("Edit")]
        //修改用户信息
        public async Task<ReturnStatus_Model> Edit([FromBody]UserInfo_Model userInfo)
        {
            return await Task.Run(() =>
            {
                return accountBll.EditUserInfo(userInfo);
            });
        }

        [HttpPost]
        [Route("Add")]
        //添加用户
        public async Task<ReturnStatus_Model> Add([FromBody]UserInfo_Model userInfo)
        {
            return await Task.Run(() =>
            {
                return accountBll.AddUserInfo(userInfo);
            });
        }

        [HttpPost]
        [Route("RegisterSendEmail")]
        //发送邮件
        public ReturnStatus_Model RegisterSendEmail([FromBody]SendEmail sendEmail)
        {
            return accountBll.SendEmail(sendEmail.Email, "RegisterSendEmail");
        }

        [HttpPost]
        [Route("FirstRegisterUserInfo")]
        //用户注册
        public ReturnStatus_Model Register([FromBody]UserRegister userRegister)
        {
            return accountBll.FirstRegisterUserInfo(userRegister);
        }

        [HttpPost]
        [BasicAuthentication]
        [Route("Get")]
        //获取个人信息
        public async Task<UserInfo_Model> Get()
        {
            return await Task.Run(() =>
            {
                return accountBll.GetUserInfo();
            });
        }

        //还有一个获取用户所有数据，这个放在管理员端

        [HttpPost]
        [BasicAuthentication]
        [Route("Logout")]
        //退出登录
        public ReturnStatus_Model Logout()
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "注销登录";
            rsModel.message = "注销失败";
            if (SessionState.RemoveSession("UserInfo"))
            {
                rsModel.isRight = true;
                rsModel.message = "注销成功";
            }
            return rsModel;
        }

        [HttpPost]
        [Route("RetrievePwd")]
        //找回密码，发送验证码
        public ReturnStatus_Model RetrievePwd([FromBody]RetrievePwdStart retrievePwd)
        {
            return accountBll.RetrievePwd(retrievePwd);
        }

        [HttpPost]
        [Route("VertifyCode")]
        //找回密码后的修改密码
        public ReturnStatus_Model VertifyCode([FromBody]ResetPwd resetPwd)
        {
            return accountBll.VertifyCode(resetPwd);
        }

        [HttpGet]
        [Route("GetTestValues")]
        public string GetTestValues(int DM)
        {
            try
            {
                int a = 100 / DM;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }

            return DateTime.Now.ToString() + ":" + DM;
        }

        [HttpPost]
        [Route("GetTestValuesModal")]
        public async Task<ReturnStatus_Model> GetTestValuesModel([FromBody]int DM)
        {
            return await Task.Run(() =>
            {
                ReturnStatus_Model rsModel = new ReturnStatus_Model();
                rsModel.isRight = false;
                rsModel.title = "测试";
                rsModel.message = "测试显示，传值DM：" + DM;
                return rsModel;
            });
        }

        [HttpGet]
        [Route("TestValues")]
        public string TestValues1([FromBody]string SS)
        {
            HttpContext.Current.Session["time"] = DateTime.Now;
            return HttpContext.Current.Session["time"] + ":" + SS;
        }
    }
    public class RModal
    {
        public bool valid;
    }
}
