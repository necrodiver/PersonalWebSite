using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace PersonalWebService.Helper
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email">接收者邮箱地址</param>
        /// <param name="verificationCodeNum">验证码</param>
        /// <returns></returns>
        public bool SendEmail(string email, string verificationCodeNum, int emailTimeFrame)
        {
            string SendEmail = ConfigurationManager.AppSettings["SendEmail"];
            string SendEmailPwd = ConfigurationManager.AppSettings["SendEmailPwd"];
            string FeedBackEmail = ConfigurationManager.AppSettings["FeedBackEmail"];
            string sHos = ConfigurationManager.AppSettings["Host"];
            //用户跳转链接
            StringBuilder sb = new StringBuilder();
            sb.Append("<div  style='border:6px solid gray;width:80%;margin:0 auto;' >");
            sb.Append("	<div  style='border:3px solid black;background-color:#DFE7EE'>");
            sb.Append("		<div align='center'><h3>亲爱的个人网站用户 {0}：</h3></div>");
            sb.Append("     <br />");
            sb.Append("		<div style='padding-left:30px;'>");
            sb.Append("			<p>请将此验证码  <strong style='font-size:20px;color:blue;'>{1}</strong>  输入到验证框中以完成密码重置</p>");
            sb.Append("			<p>为了安全起见，请勿把验证码泄露给第三者</p>");
            sb.Append("			<p style='color:red;'>此链验证码 {2} 分钟以内使用有效。</p>");
            sb.Append("			<br />");
            sb.Append("			<div>");
            sb.Append("				<p>① 此邮件为系统自动发出，请勿回复邮件</p>");
            sb.Append("				<p>② 如果您有任何问题，可以随时与我们联系<u>{3}</u></p>");
            sb.Append("			</div>");
            sb.Append("			<div align='right' style='margin-right:20px;'>");
            sb.Append("				<p><strong>个人网站邮件管理中心</strong></p>");
            sb.Append("				<p><strong>{4}</strong></p>");
            sb.Append("			</div>");
            sb.Append("		</div>");
            sb.Append("	</div>");
            sb.Append("</div>");
            string emailBody = string.Format(sb.ToString(), email,verificationCodeNum, emailTimeFrame, FeedBackEmail, DateTime.Now.ToString("yyyy-MM-dd"));
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = sHos;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(SendEmail, SendEmailPwd);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(SendEmail),
                        Subject = "找回密码——个人网站XXX",
                        BodyEncoding = Encoding.UTF8,
                        IsBodyHtml = true
                    };
                    mail.From = new MailAddress(SendEmail, "个人网站XXX邮件中心");
                    mail.To.Add(new MailAddress(email));
                    mail.Body = emailBody;
                    client.Send(mail);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
