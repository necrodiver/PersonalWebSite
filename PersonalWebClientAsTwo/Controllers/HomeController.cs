using PersonalWebService.Helper;
using PersonalWebService.Model;
using PWC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Controllers
{
    [ModelValidationMVCFilter]
    public class HomeController : BaseController
    {
        PersonalWebService.BLL.Account_BLL accountBll = new PersonalWebService.BLL.Account_BLL();

        public ActionResult Index()
        {
            return View(SessionState.GetSession<UserInfo>("UserInfo"));
        }
        public ActionResult RetrievePassword()
        {
            return View();
        }
        public ActionResult UserProfiles()
        {
            return View(SessionState.GetSession<UserInfo>("UserInfo"));
        }
        public ActionResult List(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View(SessionState.GetSession<UserInfo>("UserInfo"));
            }

            if (name.Equals("Scrawl"))
            {

            }
            if (name.Equals("Jottings"))
            {

            }
            return View(SessionState.GetSession<UserInfo>("UserInfo"));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(SessionState.GetSession<UserInfo>("UserInfo"));
        }

    }
}