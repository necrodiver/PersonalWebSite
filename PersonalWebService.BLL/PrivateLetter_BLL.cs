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
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[Message] WHERE {2}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[Message] SET {0} WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[Message] WHERE {0}";
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="senderId"></param>
        /// <returns></returns>
        public int GetPrivateLetterCount<T>(string whereStr, string senderId)
        {
            Type modelType = typeof(T);
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

        public List<PrivateLetter> GetPLList(string senderId, string AddresseeId, List<Message> msgList)
        {
            if (msgList == null || msgList.Count < 0)
            {
                return null;
            }

            msgList = msgList.FindAll(msg => (msg.M_Type == MessageType.私信 && (msg.M_SenderId == senderId || msg.M_ReceiverId == senderId)));

            for (int i = 0; i < msgList.Count; i++)
            {
                
            }
            return null;
        }
    }
}
