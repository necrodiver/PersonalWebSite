﻿using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
        public static bool SaveSession<T>(T sessionContent, string sessionKey) where T : class, new()
        {
            HttpContext.Current.Session[sessionKey] = sessionContent;
            return true;
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="sessionKey">Key值</param>
        /// <returns></returns>
        public static T GetSession<T>(string sessionKey) where T : class, new()
        {
            if (HttpContext.Current.Session[sessionKey] == null)
            {
                return null;
            }
            return HttpContext.Current.Session[sessionKey] as T;
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="sessionKey">Key值</param>
        /// <returns></returns>
        public static bool RemoveSession(string sessionKey)
        {
            if (HttpContext.Current.Session[sessionKey] == null)
            {
                return false;
            }
            HttpContext.Current.Session.Remove(sessionKey);
            return true;
        }
    }

}
