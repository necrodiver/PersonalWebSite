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
    public class FansAdditional_BLL
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public static readonly int PageNum = 12;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[UserFans] WHERE {1}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[UserFans] SET {0} WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[UserFans] WHERE {0}";
        public string GetFansCount(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var args = new DynamicParameters();
            string whereStr = "";
            args.Add("@Uad_UserId", userId);
            whereStr = " Uad_UserId=@Uad_UserId ";
            try
            {
                string sql = string.Format(sqlSelectTemplate, " Count(*) ", whereStr);
                int num = dal.GetDataCount(sql, args);
                return num.ToString();
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }
        public bool DeleteFans(UserFans userFans)
        {
            if (string.IsNullOrEmpty(userFans.Uad_UserId) || string.IsNullOrEmpty(userFans.Uad_FansId))
            {
                return false;
            }
            //var args = new DynamicParameters();
            //args.Add("@Uad_UserId", userId);
            //args.Add("@Uad_FansId", userId);
            //string whereStr = " Uad_UserId=@Uad_UserId AND Uad_FansId=@Uad_FansId ";
            try
            {
                //string sql = string.Format(sqlDeleteTemplate, whereStr);
                return dal.OpeData(userFans, OperatingModel.Delete);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return false;
        }
        public bool AddFans(UserFans userFans)
        {
            if (string.IsNullOrEmpty(userFans.Uad_UserId) || string.IsNullOrEmpty(userFans.Uad_FansId))
            {
                return false;
            }

            try
            {
                return dal.OpeData(userFans, OperatingModel.Add);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return false;
        }
        /// <summary>
        /// 获取fans用户
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public List<UserInfo_Model> GetFansList(int pageIndex, string userId)
        {
            List<UserInfo_Model> userInfoMList = new List<UserInfo_Model>();
            StringBuilder sbsql = new StringBuilder();
            var args = new DynamicParameters();
            string sqlIndex = string.Empty;

            int firstIndex = (pageIndex - 1) * PageNum + 1;
            int lastIndex = pageIndex * PageNum;
            sqlIndex = " NUM>=@firstIndex AND NUM<=@lastIndex ";
            args.Add("@firstIndex", firstIndex);
            args.Add("@lastIndex", lastIndex);

            sbsql.Append(" U.State>=0 ");
            sbsql.Append(" And F.Uad_UserId=@Uad_UserId ");
            args.Add("@Uad_UserId", userId);

            string sqlSelect = @"SELECT * FROM
                                (
                                	SELECT ROW_NUMBER()OVER(ORDER BY [EXP] DESC)NUM,* FROM
                                	(
                                		SELECT DISTINCT U.* FROM [dbo].[UserFans] F
                                		LEFT JOIN [dbo].[UserInfo] U ON F.Uad_FansId=U.UserId
                                		WHERE {0}
                                	)UF
                                )UL WHERE {1}";
            try
            {
                string sql = string.Format(sqlSelect, sbsql.ToString(), sqlIndex);
                List<UserInfo> userInfolist = dal.GetDataList<UserInfo>(sql, args);
                if (userInfolist != null || userInfolist.Count > 0)
                {
                    userInfolist.Select(l =>
                    {
                        userInfoMList.Add(new UserInfo_Model
                        {
                            UserId = l.UserId,
                            Email = l.Email,
                            NickName = l.NickName,
                            Introduce = l.Introduce,
                            AccountPicture = l.AccountPicture,
                            AddTime = l.AddTime,
                            EditTime = l.EditTime,
                            NowStatus = l.NowStatus,
                            State = l.State
                        });
                        return true;
                    });
                    return userInfoMList;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }

        /// <summary>
        /// 获取关注用户
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public List<UserInfo_Model> GetFocusList(int pageIndex, string userId)
        {
            List<UserInfo_Model> userInfoMList = new List<UserInfo_Model>();
            StringBuilder sbsql = new StringBuilder();
            var args = new DynamicParameters();
            string sqlIndex = string.Empty;

            int firstIndex = (pageIndex - 1) * PageNum + 1;
            int lastIndex = pageIndex * PageNum;
            sqlIndex = " NUM>=@firstIndex AND NUM<=@lastIndex ";
            args.Add("@firstIndex", firstIndex);
            args.Add("@lastIndex", lastIndex);

            sbsql.Append(" U.State>=0 ");
            sbsql.Append(" And F.Uad_FansId=@Uad_FansId ");
            args.Add("@Uad_FansId", userId);

            string sqlSelect = @"SELECT * FROM
                                (
                                	SELECT ROW_NUMBER()OVER(ORDER BY [EXP] DESC)NUM,* FROM
                                	(
                                		SELECT DISTINCT U.* FROM [dbo].[UserFans] F
                                		LEFT JOIN [dbo].[UserInfo] U ON F.Uad_UserId=U.UserId
                                		WHERE F.Uad_FansId={0}
                                	)UF
                                )UL WHERE {1}";
            try
            {
                string sql = string.Format(sqlSelect, sbsql.ToString(), sqlIndex);
                List<UserInfo> userInfolist = dal.GetDataList<UserInfo>(sql, args);
                if (userInfolist != null || userInfolist.Count > 0)
                {
                    userInfolist.Select(l =>
                    {
                        userInfoMList.Add(new UserInfo_Model
                        {
                            UserId = l.UserId,
                            Email = l.Email,
                            NickName = l.NickName,
                            Introduce = l.Introduce,
                            AccountPicture = l.AccountPicture,
                            AddTime = l.AddTime,
                            EditTime = l.EditTime,
                            NowStatus = l.NowStatus,
                            State = l.State
                        });
                        return true;
                    });
                    return userInfoMList;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return null;
        }
    }
}
