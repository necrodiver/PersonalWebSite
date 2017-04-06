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
        private static readonly string sqlSelectPLTemplate = "SELECT {0} FROM {1} WHERE {2}";
        private static readonly string sqlUpdatePLTemplate = "UPDATE [dbo].[PrivateLetter] SET {0} WHERE {1}";
        private static readonly string sqlDeletePLTemplate = "DELETE [dbo].[PrivateLetter] WHERE {0}";
        public List<T> GetMessageList<T>(MessageModel messageModel)
        {
            try
            {
                Type modelType = typeof(T);
                string modelName = modelType.Name;
                string sqlPLSelectList = @"
                                        SELECT * FROM 
                                        (
                                        	SELECT ROW_NUMBER()OVER(ORDER BY [PL_Sendtime] DESC)NUM,* 
                                        	FROM [dbo].[PrivateLetter]
                                        	WHERE [PL_FatherId] IS NULL
                                        )P
                                        WHERE NUM>0 AND NUM<10
                                        ";
                dal.

            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="senderId"></param>
        /// <returns></returns>
        public int GetPrivateLetterCount<T>(string whereStr,string senderId)
        {
            try
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
                                select Count(*) from Dep ";
                var args = new DynamicParameters();
                args.Add("@PL_SenderId", senderId);
                return dal.GetDataCount(sqlStr, args);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return 0;
        }
    }
}
