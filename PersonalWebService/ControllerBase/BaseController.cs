using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace PersonalWebService.ControllerBase
{
    [BasicAuthentication]
    public class BaseController : ApiController
    {
        /// <summary>
        /// 使用封装用户票据的方法来验证用户权限
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strPassword"></param>
        internal void CreateLoginUserTicket(string strUserName, string strPassword)
        {
            //构造Form验证的票据信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, strUserName, DateTime.Now, DateTime.Now.AddMinutes(240),
                true, string.Format("{0}:{1}", strUserName, strPassword), FormsAuthentication.FormsCookiePath);

            string ticString = FormsAuthentication.Encrypt(ticket);

            //把票据信息写入Cookie和Session
            //SetAuthCookie方法用于标识用户的Identity状态为true
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, ticString));
            FormsAuthentication.SetAuthCookie(strUserName, true);
            HttpContext.Current.Session["USER_LOGON_TICKET"] = ticString;

            //重写HttpContext中的用户身份，可以封装自定义角色数据；
            //判断是否合法用户，可以检查：HttpContext.User.Identity.IsAuthenticated的属性值
            string[] roles = ticket.UserData.Split(',');
            IIdentity identity = new FormsIdentity(ticket);
            IPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;
        }

        //public UserInfo GetUserInfo()
        //{
        //   UserInfo userinfo= HttpContext.Current.Session["UserInfo"] as UserInfo;
        //   if(userinfo!=null&&!string.IsNullOrEmpty(userinfo.UserID)&& !string.IsNullOrEmpty(userinfo.UserName)&&!string.IsNullOrEmpty(userinfo.Password))
        //    {
        //        return userinfo;
        //    }
        //    return null;
        //}

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>用户UserInfo的Model</returns>
        public UserInfo GetUserInfo()
        {
            return BasicAuthenticationAttribute.GetUserInfo();
        }
    }
}
