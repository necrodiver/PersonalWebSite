using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using PersonalWebService.Model;

namespace PersonalWebService.Helper
{
    public class LogRecord_Helper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private LogRecord_Helper()
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
        /// <param name="loglevels">日志等级</param>
        /// <param name="e">日志内容</param>
        public static void RecordLog(LogLevels loglevels,object e)
        {
            switch (loglevels)
            {
                case LogLevels.Fatal:
                    logger.Log(LogLevel.Fatal, e.ToString());
                    break;
                case LogLevels.Error:
                    logger.Log(LogLevel.Error, e.ToString());
                    break;
                case LogLevels.Warn:
                    logger.Log(LogLevel.Warn, e.ToString());
                    break;
                case LogLevels.Info:
                    logger.Log(LogLevel.Info, e.ToString());
                    break;
                case LogLevels.Debug:
                    logger.Log(LogLevel.Debug, e.ToString());
                    break;
                case LogLevels.Trace:
                    logger.Log(LogLevel.Trace, e.ToString());
                    break;
                default:
                    break;
            }
        }
    }
}
