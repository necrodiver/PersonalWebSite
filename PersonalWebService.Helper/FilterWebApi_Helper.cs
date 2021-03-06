﻿using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;

namespace PersonalWebService.Helper
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            ValidateAsOne(actionContext);//第一种方法 暂时先用着

            //ValidateAsTwo(actionContext);//第二种方法

            //ValidateAsThree(actionContext);//第三种方法

        }
        //https://github.com/besley/DemoUserAuthorization/blob/master/WebUtility/Security/BasicAuthenticationAttribute.cs
        //http://blog.sina.com.cn/s/blog_4e0987310101imce.html
        //http://www.cnblogs.com/shan333chao/p/5002054.html
        /// <summary>
        /// 使用封装用户票据的方法来验证用户权限
        /// </summary>
        /// <param name="encryotTicket"></param>
        /// <returns></returns>
        private bool ValidateUserTicket(string encryotTicket)
        {
            var userTicket = FormsAuthentication.Decrypt(encryotTicket);
            var userTicketData = userTicket.UserData;
            string userName = userTicketData.Substring(0, userTicketData.IndexOf(":"));
            string password = userTicketData.Substring(userTicketData.IndexOf(":") + 1);
            //这里对用户名和密码进行验证
            var userModel = SessionState.GetSession<UserInfo_Model>("UserInfo");
            if (userModel != null && userModel.Email.Equals(userName, StringComparison.OrdinalIgnoreCase) && userModel.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public UserInfo_Model userInfo
        {
            get { return GetUserInfo(); }
        }

        public static UserInfo_Model GetUserInfo()
        {
            var userinfo = SessionState.GetSession<UserInfo_Model>("UserInfo");
            if (userinfo != null && !string.IsNullOrEmpty(userinfo.UserId) && !string.IsNullOrEmpty(userinfo.Email) && !string.IsNullOrEmpty(userinfo.Password))
            {
                return userinfo;
            }
            return null;
        }

        //第一种方法验证用户
        private void ValidateAsOne(HttpActionContext actionContext)
        {
            #region 这一块就是验证用户登录信息的,使用方式是用户票据

            if (actionContext.Request.Headers.Authorization != null)
            {
                var encryptTicket = actionContext.Request.Headers.Authorization.Parameter;
                if (ValidateUserTicket(encryptTicket))
                    base.OnActionExecuting(actionContext);
                else
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            #endregion
        }
        //第二种方法验证用户
        private void ValidateAsTwo(HttpActionContext actionContext)
        {
            #region 第二种方法
            var configFlag = WebConfigurationManager.AppSettings["WebApiAuthenticatedFlag"] as string;
            if (string.IsNullOrEmpty(configFlag))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            bool isRequired = (configFlag.Equals("true", StringComparison.OrdinalIgnoreCase));
            if (isRequired)
            {
                var attr = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
                if (attr)
                {
                    base.OnActionExecuting(actionContext);
                    return;
                }
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            #endregion
        }
        private void ValidateAsThree(HttpActionContext actionContext)
        {
            #region 第三种方法 第二种验证方式就是直接验证当前请求链接和当前账户存在情况
            string action = actionContext.ActionDescriptor.ActionName;
            //.RouteData.Values["controller"] as string;
            string controller = actionContext.Request.GetRouteData().Values["controller"] as string;

            //这里只验证权限，其他一切不相干的不管
            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(controller) && userInfo != null)
            {
                base.OnActionExecuting(actionContext);
            }
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            #endregion
        }
    }

    public class ModelValidationWebApiFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errors = new Dictionary<string, IEnumerable<string>>();
                actionContext.ModelState.All(m =>
                {
                    errors[m.Key] = m.Value.Errors.Select(e => e.ErrorMessage);
                    return true;
                });
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                return;
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
