using PersonalWebService.BLL;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebClient.Controllers
{
    [ModelValidationMVCFilter]
    //[UserVisitValidationFilter]
    public class MessageCenterController : Controller
    {
        private static MessageCenter_BLL messageBll = new MessageCenter_BLL();
        public JsonResult GetMessageList(MessageModel messageModel)
        {
            UserInfo_Model userModel = SessionState.GetSession<UserInfo_Model>("UserInfo");
            switch (messageModel.MessagType)
            {
                case MessageType.私信:
                    return Json(messageBll.GetMessageList(messageModel.MessagType, userModel.UserId,0,null),JsonRequestBehavior.DenyGet);
                case MessageType.提醒:

                    break;
                case MessageType.作品评论:
                    break;
                case MessageType.作品喜欢:
                    break;
                case MessageType.新增粉丝:
                    break;
                case MessageType.系统通知:
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}