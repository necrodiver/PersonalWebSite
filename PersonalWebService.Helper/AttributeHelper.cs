using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Security;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http.Controllers;

namespace PersonalWebService.Helper
{
    public class AttributeHelper
    {
    }
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var encryptTicket = actionContext.Request.Headers.Authorization.Parameter;
                if (ValidateUserTicket(encryptTicket))
                    base.OnActionExecuting(actionContext);
                else
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;

            }

            bool isRequired = (WebConfigurationManager.AppSettings["WebApiAuthenticatedFlag"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase));
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

        }
        //https://github.com/besley/DemoUserAuthorization/blob/master/WebUtility/Security/BasicAuthenticationAttribute.cs
        //http://blog.sina.com.cn/s/blog_4e0987310101imce.html
        //http://www.cnblogs.com/shan333chao/p/5002054.html
        private bool ValidateUserTicket(string encryotTicket)
        {
            var userTicket = FormsAuthentication.Decrypt(encryotTicket);
            var userTicketData = userTicket.UserData;
            string userName = userTicketData.Substring(0, userTicketData.IndexOf(":"));
            string password = userTicketData.Substring(userTicketData.IndexOf(":") + 1);
            return true;
        }
    }
}
