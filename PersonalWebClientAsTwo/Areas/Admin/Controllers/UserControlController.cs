using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Areas.Admin.Controllers
{
    public class UserControlController : Controller
    {
        // GET: Admin/UserControl
        public ActionResult UserCtrl()
        {
            return View();
        }

        public ActionResult EditUser()
        {
            return View();
        }
    }
}