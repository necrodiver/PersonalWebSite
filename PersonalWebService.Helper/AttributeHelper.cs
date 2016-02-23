using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Security;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http.Controllers;
using PersonalWebService.Model;
using System.Web;

namespace PersonalWebService.Helper
{
    public class AttributeHelper
    {
    }
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
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

            #region 第二种验证方式就是直接验证当前请求链接和当前账户存在情况
            string action = actionContext.ActionDescriptor.ActionName;
            //.RouteData.Values["controller"] as string;
            string controller = actionContext.Request.GetRouteData().Values["controller"] as string;

            //这里只验证权限，其他一切不相干的不管
            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(controller) && userInfo != null)
            {
                base.OnActionExecuting(actionContext);
            }
            #endregion
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
            //这里需要对用户名和密码进行验证
            return true;
        }

        public UserInfo userInfo
        {
            get { return GetUserInfo(); }
        }

        public static UserInfo GetUserInfo()
        {
            UserInfo userinfo = HttpContext.Current.Session["UserInfo"] as UserInfo;
            if (userinfo != null && !string.IsNullOrEmpty(userinfo.UserID) && !string.IsNullOrEmpty(userinfo.UserName) && !string.IsNullOrEmpty(userinfo.Password))
            {
                return userinfo;
            }
            return null;
        }
    }
}
