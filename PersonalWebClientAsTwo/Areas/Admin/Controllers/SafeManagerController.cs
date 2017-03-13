using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Areas.Admin.Controllers
{
    public class SafeManagerController : Controller
    {
        // GET: Admin/SafeManager
        public ActionResult Index()
        {
            return View();
        }
    }
}