using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Controllers
{
    public class BaseController : Controller
    {
        public static UserInfo baseUserInfo;
        public BaseController()
        {
            baseUserInfo = GetUserInfo();
        }
        public UserInfo GetUserInfo()
        {
            return BasicAuthenticationAttribute.GetUserInfo();
        }
    }
}