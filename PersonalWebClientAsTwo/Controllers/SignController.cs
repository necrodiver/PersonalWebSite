using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Controllers
{
    [ModelValidationFilter]
    public class SignController : Controller
    {
        PersonalWebService.BLL.Account_BLL accountBll = new PersonalWebService.BLL.Account_BLL();
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        //验证用户昵称是否存在
        public JsonResult ContrastNickName(string nickName)
        {
            return Json(accountBll.ContrastUser(null, nickName), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //验证用户昵称是否存在，返回固定格式
        public JsonResult ContrastNickNameWeb(string nickName)
        {
            ReturnStatus_Model rsModel = accountBll.ContrastUser(null, nickName);
            RModal rmodal = new RModal();
            rmodal.valid = !rsModel.isRight;
            return Json(rmodal, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //验证用户邮箱地址是否存在
        public JsonResult ContrastEmail(string email)
        {
            return Json(accountBll.ContrastUser(email, null), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //验证用户邮箱地址是否存在，返回固定格式
        public JsonResult ContrastEmailWeb(string email)
        {
            ReturnStatus_Model rsModel = accountBll.ContrastUser(email, null);
            RModal rmodal = new RModal();
            rmodal.valid = !rsModel.isRight;
            return Json(rmodal, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //发送邮件
        public JsonResult RegisterSendEmail(SendEmail sendEmail)
        {
            return Json(accountBll.SendEmail(sendEmail.Email, "RegisterSendEmail"), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //用户注册
        public JsonResult Register(UserRegister userRegister)
        {
            return Json(accountBll.FirstRegisterUserInfo(userRegister), JsonRequestBehavior.DenyGet);
        }

    }

    public class RModal
    {
        public bool valid;
    }
}