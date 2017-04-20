using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;
using PersonalWebService.Helper;
using PersonalWebService.DAL;
using Dapper;

namespace PersonalWebService.BLL
{
    public class MessageCenter_BLL
    {
        public static readonly int PageNum = 12;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[Message] WHERE {2}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[Message] SET {0} WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[Message] WHERE {0}";

        /// <summary>
        /// 获取对应消息的数量
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="userId">用户id</param>
        /// <param name="isRead">是否已读,若为null则查询全部</param>
        /// <returns></returns>
        public int GetMessageCount(MessageType messageType, string userId, bool? isRead)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return 0;
            }
            try
            {
                var args = new DynamicParameters();
                string whereStr = " M_Type=@M_Type AND M_UserId=@M_UserId ";
                args.Add("@M_Type", messageType);
                args.Add("@M_UserId", userId);
                args.Add("@M_IsRead", Convert.ToInt32(isRead));

                if (isRead != null)
                {
                    whereStr += " AND M_IsRead=@M_IsRead ";
                }
                string selectStr = " Count(*) ";
                string sqlStr = string.Format(sqlSelectTemplate, selectStr, whereStr);

                return dal.GetDataCount(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return 0;
        }
        /// <summary>
        /// 获取消息List分页集合
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="userId">消息接收者</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="isRead">是否已读</param>
        /// <returns></returns>
        public List<Message> GetMessageList(MessageType messageType, string userId, int pageIndex, bool? isRead)
        {

            int firstIndex = (pageIndex - 1) * PageNum + 1;
            int lastIndex = pageIndex * PageNum;
            var args = new DynamicParameters();

            string sqlWhere1 = " M_Type=@M_Type AND M_UserId=@M_UserId ";
            args.Add("@M_Type", firstIndex);
            args.Add("@M_UserId", lastIndex);

            if (isRead != null)
            {
                sqlWhere1 += " AND M_IsRead=@M_IsRead ";
                args.Add("@M_IsRead", isRead);
            }

            string sqlWhere2 = " NUM>=@firstIndex AND NUM<=@lastIndex ";
            args.Add("@firstIndex", firstIndex);
            args.Add("@lastIndex", lastIndex);

            string sqlStrTemp = @"
                            SELECT * FROM (
	                            SELECT ROW_NUMBER()OVER(ORDER BY M_AddTime desc)AS NUM,* 
	                            FROM [dbo].[Message]
	                            WHERE {0}
                            )AS M 
                            where {1}";
            string sqlStr = string.Format(sqlStrTemp, sqlWhere1, sqlWhere2);
            try
            {
                return dal.GetDataList<Message>(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        /// <summary>
        /// 设置为已读
        /// </summary>
        /// <param name="mIdList">消息Id集合</param>
        /// <returns></returns>
        public bool SetMessageToRead(List<string> mIdList)
        {
            if (mIdList == null || mIdList.Count < 1)
            {
                return false;
            }
            var args = new DynamicParameters();
            var whereSb = new StringBuilder();

            mIdList = mIdList.FindAll(mid => (mid != null && mid.Length == 32));
            for (int i = 0; i < mIdList.Count; i++)
            {
                string childmId = "@mId" + i;
                args.Add(childmId, mIdList[i]);
                if (mIdList.Count != i + 1)
                {
                    whereSb.Append(childmId + ",");
                }
                else
                {
                    whereSb.Append(childmId);
                }
            }
            string upStr = " M_IsRead=1 ";
            string sqlStr = string.Format(sqlUpdateTemplate, upStr, $" M_Id in ( {whereSb.ToString()} )");

            try
            {
                return dal.OpeData(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return false;
        }
        public bool AddMessage(Message msgModel)
        {
            if (msgModel == null || string.IsNullOrEmpty(msgModel.M_NameId))
                return false;
            try
            {
                return dal.OpeData(msgModel, OperatingModel.Add);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return false;
        }

    }
}
