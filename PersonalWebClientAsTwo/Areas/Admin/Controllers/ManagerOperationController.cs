using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Areas.Admin.Controllers
{
    public class ManagerOperationController : Controller
    {
        // GET: Admin/ManagerOperation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mail()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }
        public ActionResult SystemNotice()
        {
            return View();
        }
        public ActionResult UserMessage()
        {
            return View();
        }
        public ActionResult WorkReminder()
        {
            return View();
        }

    }
}