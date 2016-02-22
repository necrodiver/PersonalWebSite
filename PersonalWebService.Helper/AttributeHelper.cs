using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Security;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using PersonalWebService.Model;

namespace PersonalWebService.Helper
{
    public class AttributeHelper
    {
    }
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public UserInfo userInfo
        {

        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string action = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            string controller = actionExecutedContext.ActionContext.Request.GetRouteData().Values["controller"] as string;
            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(controller) && UserInfo != null)
            {

            }
            //if (actionExecutedContext.Request.Headers.Authorization != null)
            //{
            //    var encryptTicket = actionExecutedContext.Request.Headers.Authorization.Parameter;
            //    if (ValidateUserTicket(encryptTicket))
            //        base.OnActionExecuted(actionExecutedContext);
            //    else
            //        actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            //}
            //else
            //{
            //    bool isRequired = (WebConfigurationManager.AppSettings["WebApiAuthenticatedFlag"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase));
            //    if(isRequired)
            //    {
            //        var attr = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<T>().OfType();
            //    }
            //}

        }

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
