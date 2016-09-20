using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;
using PersonalWebService.Helper;
using PersonalWebService.DAL;

namespace PersonalWebService.BLL
{
    public class Log_BLL
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public static readonly int PageNum = 12;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[Log] WHERE {0}";
        public List<LogInfo> GetList(LogCondition logCondition)
        {
            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (logCondition.LeftLogged != null)
            {
                sbsql.Append("Logged >= @LeftLogged AND");
                param.Add(new DataField { Name = "@LeftLogged", Value = logCondition.LeftLogged });
            }
            if (logCondition.RightLogged != null)
            {
                sbsql.Append("Logged >= @RightLogged AND");
                param.Add(new DataField { Name = "@RightLogged", Value = logCondition.RightLogged });
            }
            if (logCondition.Level != null)
            {
                sbsql.Append("[Level] == @Level AND");
                param.Add(new DataField { Name = "@Level", Value = logCondition.Level });
            }
            string sqlIndex = string.Empty;
            if (logCondition.PageIndex != null)
            {
                int pageIndex = Convert.ToInt32(logCondition.PageIndex);
                int firstIndex = (pageIndex - 1) * PageNum + 1;
                int lastIndex = pageIndex * PageNum;
                sqlIndex = " NUM>=@firstIndex AND NUM<=@lastIndex ";
                param.Add(new DataField { Name = "@firstIndex", Value = firstIndex });
                param.Add(new DataField { Name = "@lastIndex", Value = lastIndex });
            }
            else
            {
                sbsql.Append(" NUM<=@PageNum ");
                param.Add(new DataField { Name = "@PageNum", Value = PageNum });
            }
            string sqlSelect = @"SELECT * FROM
                                (
                                	SELECT ROW_NUMBER() OVER(ORDER BY Logged DESC) NUM,* 
                                    FROM [dbo].[Log]
                                    WHERE {0}
                                )L
                                WHERE {1}";

            sqlSelect = string.Format(sqlSelect, sbsql.ToString(), sqlIndex);
            //硬条件
            AdminInfo adminInfo = SessionState.GetSession<AdminInfo>("AdminInfo");
            if (adminInfo.Level > AdminLevel.最高权限)
            {
                return null;
            }
            List<LogInfo> logList = new List<LogInfo>();
            if (adminInfo == null)
            {
                return null;
            }

            try
            {
                sbsql.Append("[Level]>" + adminInfo.Level);
                logList = dal.GetDataList<LogInfo>(sqlSelect, param);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                return null;
            }
            return logList;
        }

        public ReturnStatus_Model DeleteList(int[] logIds)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "日志删除";

            if (logIds == null || logIds.Length <= 0)
            {
                rsModel.message = "需要操作删除的日志为空，请先选择需要删除的日志";
                return rsModel;
            }
            AdminInfo adminInfo = SessionState.GetSession<AdminInfo>("AdminInfo");
            if (adminInfo.Level > AdminLevel.最高权限)
            {
                rsModel.isRight = false;
                rsModel.message = "您的权限太低，无法进行操作！";
            }
            if (adminInfo == null || string.IsNullOrEmpty(adminInfo.AdminId))
            {
                rsModel.message = "你未登录账号或账号已过期，请重新登录！";
                return rsModel;
            }

            string ids = string.Empty;
            logIds.Select(l=> {
                ids += l + ",";
                return true;
            });

            ids = ids.Substring(0, ids.Length - 1);
            string sql = string.Format(sqlDeleteTemplate, "[Id] in(" + ids + ")");

            try
            {
                if (dal.OpeData(sql, null))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改成功！";
                    return rsModel;
                }
                else
                {
                    rsModel.isRight = false;
                    rsModel.message = "你删除的内容有误，请重试或查看情况！";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.isRight = false;
                rsModel.message = "系统出现一个问题，请查看解决或重试！";
                return rsModel;
            }
        }
    }
}
