﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Controllers
{
    public class MyCenterController : Controller
    {
        // GET: MyCenter
        public ActionResult MyCenter()
        {
            return View();
        }
        public ActionResult EditInformation()
        {
            return View();
        }
        public ActionResult AttentionPerson()
        {
            return View();
        }
        public ActionResult CollectScrawlAndJottings()
        {
            return View();
        }
        public ActionResult MessageCenter()
        {
            return View();
        }
        public ActionResult MyFans()
        {
            return View();
        }

        public ActionResult MyScrawlAndJottings()
        {
            return View();
        }

        public ActionResult NewScrawlAndJottings()
        {
            return View();
        }
    }
}