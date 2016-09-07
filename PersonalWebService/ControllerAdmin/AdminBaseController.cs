using PersonalWebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonalWebService.ControllerAdmin
{
    [ModelValidationFilter]
    public class AdminBaseController : ApiController
    {
        public AdminBaseController()
        {

        }
    }
}
