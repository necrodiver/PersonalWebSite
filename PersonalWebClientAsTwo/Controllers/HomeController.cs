using PWC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //PWCHelper_HttpClientService hcs = new PWCHelper_HttpClientService();
            //string a=await hcs.TestDM2();
            //ViewBag.a = a;
            return View();
        }
        public ActionResult RetrievePassword()
        {
            return View();
        }

        public ActionResult UserProfiles()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}