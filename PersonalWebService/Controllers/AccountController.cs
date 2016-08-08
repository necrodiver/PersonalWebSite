﻿using Microsoft.AspNet.Identity;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersonalWebService.Controllers
{
    [RoutePrefix("api/Account")]
    [ModelValidationFilter]
    public class AccountController : ApiController
    {
        BLL.Account_BLL accountBll = new BLL.Account_BLL();

        [HttpPost]
        [Route("Login")]
        public async Task<ReturnStatus_Model> Login([FromBody]UserLogin user)
        {
            YZMHelper yz = new YZMHelper();
            return await Task.Run(() =>
            {
                return accountBll.VerifyUserInfo(user);
            });
            //return await GetValueAsync(user.UserName, user.PassWord);
        }

        [HttpPost]
        [BasicAuthentication]
        [Route("Edit")]
        public async Task<ReturnStatus_Model> Edit([FromBody]UserInfo_Model userInfo)
        {
            return await Task.Run(() =>
            {
                return accountBll.EditUserInfo(userInfo);
            });
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ReturnStatus_Model> Add([FromBody]UserInfo_Model userInfo)
        {
            return await Task.Run(() =>
            {
                return accountBll.AddUserInfo(userInfo);
            });
        }

        [HttpPost]
        [BasicAuthentication]
        [Route("Get")]
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
        public async Task<ReturnStatus_Model> Logout()
        {
            return await Task.Run(() =>
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
            });
        }

        [HttpPost]
        [BasicAuthentication]
        [Route("RetrievePwd")]
        public async Task<ReturnStatus_Model> RetrievePwd([FromBody]RetrievePwdStart retrievePwd)
        {
            return await Task.Run(() =>
            {
                return accountBll.RetrievePwd(retrievePwd);
            });
        }

        [HttpPost]
        [Route("VertifyCode")]
        public async Task<ReturnStatus_Model> VertifyCode([FromBody]ResetPwd resetPwd)
        {
            return await Task.Run(() =>
            {
                return accountBll.VertifyCode(resetPwd);
            });
        }

        private Task<string> GetValueAsync(string userName, string passWord)
        {
            return Task.Run(() =>
            {
                return DateTime.Now.ToString() + "  UserName:" + userName + "  PWD:" + passWord;
            });
        }

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
        public string TestValues1([FromBody]string SS)
        {
            return DateTime.Now.ToString() + ":" + SS;
        }
    }
}
