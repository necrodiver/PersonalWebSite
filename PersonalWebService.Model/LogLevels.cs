using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public enum LogLevels
    {
        /// <summary>
        /// 1/6.致命性错误
        /// </summary>
        Fatal,
        /// <summary>
        /// 2/6.标准错误
        /// </summary>
        Error,
        /// <summary>
        /// 3/6.警告
        /// </summary>
        Warn,
        /// <summary>
        /// 4/6.信息
        /// </summary>
        Info,
        /// <summary>
        /// 5/6.调试消息
        /// </summary>
        Debug,
        /// <summary>
        /// 6/6.设置信息
        /// </summary>
        Trace
    }
}
