using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Areas.Admin.Controllers
{
    public class ProductionOperationController : Controller
    {
        // GET: Admin/ProductionOperation
        public ActionResult JottingsManage()
        {
            return View();
        }
        public ActionResult ScrawlManage()
        {
            return View();
        }
        public ActionResult ScrawlView()
        {
            return View();
        }
    }
}