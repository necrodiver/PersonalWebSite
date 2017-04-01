using System;
using System.Linq;
using System.Web.Security;
using System.Web.Configuration;
using PersonalWebService.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PersonalWebService.Helper
{
    /// <summary>
    /// 模型过滤器验证
    /// </summary>
    public class ModelValidationMVCFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                var errorMessage = modelState.Values
                    .SelectMany(m => m.Errors)
                    .Select(m => m.ErrorMessage)
                    .First();
                filterContext.Result = new JsonResult()
                {
                    Data = new { status = -1, msg = errorMessage }
                };
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    /// <summary>
    /// 用户权限过滤验证
    /// </summary>
    public class AuthorityAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserInfo userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            var errors = new Dictionary<string, string>();
            if (userInfo == null || string.IsNullOrEmpty(userInfo.NickName)) {
                errors.Add("SelectList", "非法查询！");
                filterContext.Result = new JsonResult()
                {
                    Data = new { status = -1, msg = errors }
                };
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class UserVisitValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            var errors = new Dictionary<string, string>();
            if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
            {
                errors.Add("VisitWeb", "请先登录后再进行访问");
                filterContext.Result = new JsonResult()
                {
                    Data = new { status = -1, msg = errors }
                };
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
