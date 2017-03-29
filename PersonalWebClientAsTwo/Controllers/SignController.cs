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
            var userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult SignUp()
        {
            var userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// 找回密码First
        /// </summary>
        /// <returns></returns>
        public ActionResult RetrievePwd()
        {
            var userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// 找回密码Second
        /// </summary>
        /// <returns></returns>
        public ActionResult RetrievePwdSet()
        {
            var userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var rtEmail = SessionState.GetSession<RetrieveValue>("RetrieveSetPwd");
            if (rtEmail == null || rtEmail.SaveTime.AddMinutes(20) < DateTime.Now)
            {
                return RedirectToAction("SignIn");
            }

            ViewBag.rtEmailStr = rtEmail.ValidateCode;
            return View();
        }

        [HttpPost]
        public JsonResult Login(UserLogin user)
        {
            return Json(accountBll.VerifyUserInfo(user), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult LoginIndex(UserLoginTwo user)
        {
            return Json(accountBll.VerifyUserInfoIndex(user), JsonRequestBehavior.DenyGet);
        }


        [HttpGet]
        //获取验证码,用于主页面
        public ActionResult GetVerificationCode()
        {
            byte[] imageByte = accountBll.GetVerificationCode();
            return File(imageByte, @"image/gif");
        }

        [HttpGet]
        //获取验证码2 用于分页面（主要）
        public ActionResult GetVerificationCode2()
        {
            byte[] imageByte = accountBll.GetVerificationCode2();
            return File(imageByte, @"image/gif");
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

        [HttpPost]
        //验证用户邮箱地址是否存在，返回固定格式
        public JsonResult ContrastNotEmail(string email)
        {
            ReturnStatus_Model rsModel = accountBll.ContrastUser(email, null);
            RModal rmodal = new RModal();
            rmodal.valid = rsModel.isRight;
            return Json(rmodal, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //找回密码的发送邮件
        public JsonResult RetrievePwdEmail(SendEmail sendEmail)
        {
            return Json(accountBll.SendEmail(sendEmail.Email, "RetrieveValidateCode"), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        //找回密码-验证Email地址和验证码是否正确
        public JsonResult RetrieveVFCAndEmail(RetrievePwdStart rps)
        {
            var vcCode = SessionState.GetSession<RetrieveValue>("RetrieveValidateCode");
            var rtEmail = SessionState.GetSession<RetrieveValue>("RetrieveValidateCode" + "Email");
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "找回密码";
            if (vcCode == null || vcCode.SaveTime.AddMinutes(20) < DateTime.Now || rtEmail == null || rtEmail.SaveTime.AddMinutes(20) < DateTime.Now)
            {
                rsModel.message = "当前验证码或Email地址不存在或已过期，请重新输入";
                return Json(rsModel, JsonRequestBehavior.DenyGet);
            }
            if (vcCode.ValidateCode.Equals(rps.ValidateCode) && rtEmail.ValidateCode.Equals(rps.Email))
            {
                rsModel.isRight = true;
                rsModel.message = "恭喜校验成功，可以进行修改密码操作";
                RetrieveValue rv = new RetrieveValue();
                rv.SaveTime = DateTime.Now;
                rv.ValidateCode = rtEmail.ValidateCode;
                SessionState.SaveSession(rv, "RetrieveSetPwd");

                SessionState.RemoveSession("RetrieveValidateCode");
                SessionState.RemoveSession("RetrieveValidateCode" + "Email");

                return Json(rsModel, JsonRequestBehavior.DenyGet);
            }
            else
            {
                rsModel.message = "当前验证码或Email地址错误，请重新输入";
                return Json(rsModel, JsonRequestBehavior.DenyGet);
            }

        }

        [HttpPost]
        //找回密码后的修改密码
        public JsonResult VertifyCode(ResetPwdSet resetPwd)
        {
            return Json(accountBll.VertifyCodeSet(resetPwd.Password), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [UserVisitValidationFilter]
        //退出登录
        public ReturnStatus_Model Logout()
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "注销登录";
            rsModel.message = "注销失败";
            if (SessionState.RemoveSession("UserInfo"))
            {
                HttpContext.Session.Clear();
                rsModel.isRight = true;
                rsModel.message = "注销成功";
            }
            return rsModel;
        }
    }

    public class RModal
    {
        public bool valid;
    }
}