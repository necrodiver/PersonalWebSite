using Dapper;
using PersonalWebService.DAL;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.BLL
{
    public class PrivateLetter_BLL
    {
        public static readonly int PageNum = 12;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[PrivateLetter] WHERE {2}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[PrivateLetter] SET {0} WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[PrivateLetter] WHERE {0}";
        /// <summary>
        /// 获取当前私信对话条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="senderId"></param>
        /// <returns></returns>
        public int GetPrivateLetterCount(string whereStr, string senderId)
        {
            string sqlStr = @"
                                with Dep as 
                                ( 
                                    select PL_Id,PL_SenderId,PL_AddresseeId,PL_Message,PL_Sendtime,PL_ParentId,PL_IsRead 
                                	from [dbo].[PrivateLetter] where PL_SenderId=@PL_SenderId
                                    union all 
                                    select Dep.PL_Id,Dep.PL_SenderId,Dep.PL_AddresseeId,Dep.PL_Message,Dep.PL_Sendtime,Dep.PL_ParentId,Dep.PL_IsRead 
                                	from Dep inner join  [dbo].[PrivateLetter] p on dep.PL_SenderId = p.PL_ParentId
                                ) 
                                select * from Dep ";
            var args = new DynamicParameters();
            args.Add("@PL_SenderId", senderId);
            try
            {
                return dal.GetDataCount(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return 0;
        }

        /// <summary>
        /// 获取私信大集合(指的是没有父发送者)，由传入的msgList确定集合条数
        /// </summary>
        /// <param name="senderId">发送者</param>
        /// <param name="AddresseeId">接收者</param>
        /// <param name="msgList"></param>
        /// <returns></returns>
        public List<PrivateLetter> GetPLList(string senderId, string AddresseeId, List<Message> msgList)
        {
            if (msgList == null || msgList.Count < 0)
            {
                return null;
            }

            msgList = msgList.FindAll(msg => (msg.M_Type == MessageType.私信 && (msg.M_SenderId == senderId || msg.M_ReceiverId == senderId)));

            var args = new DynamicParameters();
            StringBuilder sbWhereStr = new StringBuilder();
            for (int i = 0; i < msgList.Count; i++)
            {
                string childIdStr = "@PL_Id" + i;
                if (msgList.Count > i + 1)
                {
                    sbWhereStr.Append(childIdStr + ",");
                }
                else
                {
                    sbWhereStr.Append(childIdStr);
                }
                args.Add(childIdStr, msgList[i].M_NameId);
            }
            string sqlStr = string.Format(sqlSelectTemplate, " * ", sbWhereStr);
            try
            {
                return dal.GetDataList<PrivateLetter>(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        /// <summary>
        /// 查询详细的对话内容
        /// </summary>
        /// <param name="pageIndex">对话页数</param>
        /// <param name="nameId">对话定位，nameId为Message的nameId</param>
        /// <param name="isAhead">是否向前读取</param>
        /// <param name="senderId">发送者Id(userId)</param>
        /// <param name="addresseeId">接收者Id(userId)</param>
        /// <returns></returns>
        public List<PrivateLetter> GetPLChildList(int pageIndex, string nameId, bool isAhead, string senderId, string addresseeId)
        {
            int firstIndex = (pageIndex - 1) * PageNum + 1;
            int lastIndex = pageIndex * PageNum;
            var args = new DynamicParameters();
            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(senderId))
            {
                sbWhere.Append(" PL_SenderId=@PL_SenderId AND ");
                args.Add("@PL_SenderId", senderId);
            }
            if (!string.IsNullOrEmpty(addresseeId))
            {
                sbWhere.Append(" PL_AddresseeId=@PL_AddresseeId AND ");
                args.Add("@PL_AddresseeId", addresseeId);
            }
            sbWhere.Append(" 1=1 ");

            string onStr = isAhead ? "Dep.PL_Id = p.PL_ParentId" : "Dep.PL_ParentId = p.PL_Id";
            string sqlStr = $@"with Dep as
                             (
                             	select PL_Id,PL_SenderId,PL_AddresseeId,PL_Message,PL_Sendtime,PL_ParentId 
                             	from [dbo].[PrivateLetter] 
                             	where PL_Id=@PL_Id AND {sbWhere.ToString()}
                             	union all
                             	select Dep.PL_Id,Dep.PL_SenderId,Dep.PL_AddresseeId,Dep.PL_Message,Dep.PL_Sendtime,Dep.PL_ParentId 
                             	from Dep inner join [dbo].[PrivateLetter] p on {onStr}
                             )
                             select * from(
                             	select ROW_NUMBER()Over(Order by PL_Sendtime)NUM,* from Dep
                             ) PL where NUM>=@firstIndex AND NUM<=@lastIndex";
            args.Add("@PL_Id", nameId);
            args.Add("@firstIndex", firstIndex);
            args.Add("@lastIndex", lastIndex);

            try
            {
                dal.GetDataList<PrivateLetter>(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        /// <summary>
        /// 删除PL某些消息,对应的必须是当前发送人发的
        /// </summary>
        /// <param name="plIdlist">私信Id列表</param>
        /// <param name="senderId">发送者Id</param>
        /// <param name="addresseeId">接收者Id</param>
        /// <returns></returns>
        public bool DeletePLList(List<string> plIdlist, string senderId, string addresseeId)
        {
            if (plIdlist == null || plIdlist.Count < 1)
            {
                return false;
            }
            var args = new DynamicParameters();
            var whereSb = new StringBuilder();

            plIdlist = plIdlist.FindAll(mid => (mid != null && mid.Length == 32));
            for (int i = 0; i < plIdlist.Count; i++)
            {
                string childmId = "@plId" + i;
                args.Add(childmId, plIdlist[i]);
                if (plIdlist.Count != i + 1)
                {
                    whereSb.Append(childmId + ",");
                }
                else
                {
                    whereSb.Append(childmId);
                }
            }
            string upStr = " PL_IsDeleted=1 ";
            string whereStr = $" M_Id in ( {whereSb.ToString()} )  AND PL_SenderId=@PL_SenderId AND PL_AddresseeId=@PL_AddresseeId";
            args.Add("@PL_SenderId", senderId);
            args.Add("@PL_AddresseeId", addresseeId);
            string sqlStr = string.Format(sqlUpdateTemplate, upStr, whereStr);

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

        /// <summary>
        /// 添加私信内容(注意添加私信前首先需要在Message表中添加引导列)
        /// </summary>
        /// <param name="plModel"></param>
        /// <returns></returns>
        public bool AddPL(PrivateLetter plModel)
        {
            try
            {
                return dal.OpeData(plModel, OperatingModel.Add);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }

            return false;
        }
    }
}
