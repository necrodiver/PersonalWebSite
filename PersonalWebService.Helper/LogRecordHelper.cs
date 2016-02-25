using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace PersonalWebService.Helper
{
    public class LogRecordHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private LogRecordHelper()
        {
            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="message">日志消息</param>
        public static void RecordLog(string message)
        {
            logger.Log(LogLevel.Warn, message);
        }


    }
}
