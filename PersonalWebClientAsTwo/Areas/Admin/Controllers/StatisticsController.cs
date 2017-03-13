using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics
        public ActionResult ErrorStatustucs()
        {
            return View();
        }
        public ActionResult VisitStatistics()
        {
            return View();
        }
    }
}