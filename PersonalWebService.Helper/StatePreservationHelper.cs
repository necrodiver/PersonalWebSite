using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PersonalWebService.Helper
{
    public class SessionState
    {
        public SessionState()
        {
            HttpContext.Current.Session.Timeout = 10;
        }
        /// <summary>
        /// 存储Session
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="sessionContent">存储内容</param>
        /// <param name="sessionKey">Key值</param>
        /// <returns></returns>
        public bool SaveSession<T>(T sessionContent,string sessionKey)
        {
            HttpContext.Current.Session[sessionKey] = sessionContent;
            return true;
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="sessionKey">Key值</param>
        /// <returns></returns>
        public bool RemoveSession(string sessionKey)
        {
            if(HttpContext.Current.Session[sessionKey]==null)
            {
                return false;
            }
            HttpContext.Current.Session.Remove(sessionKey);
            return true;
        }
    }

}
