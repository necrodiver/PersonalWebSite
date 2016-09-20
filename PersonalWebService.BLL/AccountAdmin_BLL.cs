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
    public class AccountAdmin_BLL
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public static readonly int PageNum = 12;
        private static YZMHelper yzM = new YZMHelper();
        private static AESEncryptS aesE = new AESEncryptS();
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[AdminInfo] WHERE {1}";
        private static readonly string sqlUpdateTemple = "UPDATE [dbo].[AdminInfo] SET {0} WHERE {1}";
        private static readonly string sqlDeleteTemple = "DELETE [dbo].[Article] where {0}";
        /// <summary>
        /// 验证管理员登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ReturnStatus_Model VerifyAdmin(AdminLogin user)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();

            rsModel.isRight = false;
            rsModel.title = "管理员登录";

            //验证码验证错误时
            if (!user.ValidateCode.Equals(yzM.Text, StringComparison.OrdinalIgnoreCase))
            {
                rsModel.message = "验证码有误，请刷新验证码后重新输入";
                yzM.CreateImage();
                return rsModel;
            }
            AdminInfo adminInfo = new AdminInfo();
            try
            {
                string sql = string.Format(sqlSelectTemplate, "TOP 1 *", " Name=@Name");
                adminInfo = dal.GetDataSingle<AdminInfo>(sql, new DataField { Name = "@Name", Value = user.UserName });
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "服务器错误，请稍后重试";
                yzM.CreateImage();
                return rsModel;
            }

            //无用户
            if (adminInfo == null || string.IsNullOrEmpty(adminInfo.Name))
            {
                rsModel.message = "不存在此账户，请重新登录或注册后进行登录";
            }
            else
            {
                if (adminInfo.Name == null || adminInfo.Pwd == null)
                {
                    rsModel.message = "当前用户存在问题，请联系管理员进行处理";
                    LogRecord_Helper.RecordLog(LogLevels.Error, "用户ID为：" + adminInfo.AdminId + "的账户存在问题");
                    return rsModel;
                }

                if (adminInfo.Name.Equals(user.UserName) && adminInfo.Pwd.Equals(aesE.AESEncrypt(user.PassWord)))
                {
                    string sqlUpTime = "UPDATE [dbo].[AdminInfo] SET LastvisitDate=GETDATE() WHERE AdminId=@AdminId";
                    dal.OpeData(sqlUpTime, new DataField { Name = "@AdminId", Value = adminInfo.AdminId });
                    rsModel.isRight = true;
                    rsModel.message = "管理员登录成功！";
                    SessionState.SaveSession(adminInfo, "AdminInfo");
                }
                else
                {
                    rsModel.message = "管理员名或密码有误，请重新输入登录账户";
                }
            }
            yzM.CreateImage();
            return rsModel;
        }

        public ReturnStatus_Model EditAdminInfo(EditAdmin userInfo)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改用户信息";
            if (string.IsNullOrEmpty(userInfo.Name) && string.IsNullOrEmpty(userInfo.Pwd))
            {
                rsModel.message = "修改内容不能为空！";
                return rsModel;
            }

            AdminInfo userInfoS = SessionState.GetSession<AdminInfo>("AdminInfo");
            StringBuilder sbSql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!string.IsNullOrEmpty(userInfo.Name))
            {
                sbSql.Append("[Name]=@Name,");
                param.Add(new DataField { Name = "@Name", Value = userInfo.Name });
            }
            if (!string.IsNullOrEmpty(userInfo.Pwd))
            {
                sbSql.Append("Pwd=@Pwd,");
                param.Add(new DataField { Name = "@Pwd", Value = userInfo.Pwd });
            }
            if (userInfo.Level == AdminLevel.第一权限)
            {
                sbSql.Append("[Level]=@Level,");
                param.Add(new DataField { Name = "@Level", Value = userInfo.Level });
            }
            else
            {
                rsModel.message = "您的权限不足，无法修改更高级别的权限等级！";
                return rsModel;
            }
            param.Add(new DataField { Name = "@AdminId", Value = userInfoS.AdminId });
            sbSql.Append("[EditTime]=GETDATE()");
            try
            {

                string sqlEdit = string.Format(sqlUpdateTemple, sbSql.ToString(), "[AdminId=@AdminId]");

                if (dal.OpeData(sqlEdit, param))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改用户数据成功";
                }
                else
                {
                    rsModel.message = "修改用户数据失败！";
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
                rsModel.message = "服务器错误，请稍后重试";
            }
            return rsModel;
        }

        /// <summary>
        /// 获取具有条件的管理员列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AdminInfo_Model> GetAdminInfoList(AdminInfoCondition condition)
        {
            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (string.IsNullOrEmpty(condition.UserName))
            {
                sbsql.Append("[Name]=@Name AND");
                param.Add(new DataField { Name = "@Name", Value = condition.UserName });
            }
            if (condition.Level != null)
            {
                sbsql.Append("[Level] >= @Level AND");
                param.Add(new DataField { Name = "@Level", Value = condition.Level });
            }
            if (condition.AddTime != null)
            {
                sbsql.Append("[AddTime] >= @AddTime AND");
                param.Add(new DataField { Name = "@AddTime", Value = condition.AddTime });
            }
            if (condition.EditTime != null)
            {
                sbsql.Append("[EditTime] <= @EditTime AND");
                param.Add(new DataField { Name = "@EditTime", Value = condition.EditTime });
            }
            string sqlIndex = string.Empty;
            if (condition.PageIndex != null)
            {
                int pageIndex = Convert.ToInt32(condition.PageIndex);
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
                                	SELECT ROW_NUMBER() OVER(ORDER BY AddTime DESC) NUM,* 
                                    FROM [dbo].[AdminInfo]
                                    WHERE {0}
                                )M
                                WHERE {1}";

            sqlSelect = string.Format(sqlSelect,sbsql.ToString(),sqlIndex);
            //硬条件
            AdminInfo adminInfo = SessionState.GetSession<AdminInfo>("AdminInfo");
            List<AdminInfo> adminInfos = new List<AdminInfo>();
            if (adminInfo == null)
            {
                return null;
            }
            try
            {
                sbsql.Append("[Level]>" + adminInfo.Level);
                string sql = string.Format(sqlSelectTemplate, "*", sbsql.ToString());
                adminInfos = dal.GetDataList<AdminInfo>(sqlSelect, param);
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                return null;
            }
            List<AdminInfo_Model> adminInfoModelList = new List<AdminInfo_Model>();
            adminInfos.Select(a =>
            {
                adminInfoModelList.Add(new AdminInfo_Model
                {
                    UserName = a.Name,
                    Level = a.Level,
                    AddTime = a.AddTime,
                    EditTime = a.EditTime
                });
                return true;
            });
            return adminInfoModelList;
        }

        public ReturnStatus_Model DeleteList(string[] adminIds)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "管理员删除";
            if (adminIds.Length > 5)
            {
                rsModel.message = "你的一次删除操作太多，不予执行！";
                return rsModel;
            }
            if (adminIds == null || adminIds.Length <= 0)
            {
                rsModel.message = "需要操作删除的管理员为空，请先选择需要删除的文章";
                return rsModel;
            }
            AdminInfo adminInfo = SessionState.GetSession<AdminInfo>("AdminInfo");
            if (adminInfo == null || string.IsNullOrEmpty(adminInfo.AdminId))
            {
                rsModel.message = "你未登录账号或账号已过期，请重新登录！";
                return rsModel;
            }

            string ids = string.Empty;
            if (Utility_Helper.IsClassIds(adminIds))
            {
                rsModel.message = "你所需要操作的内容不合法！账号将被记录，请规范操作！";
                StringBuilder articleids = new StringBuilder();
                adminIds.Select(l => { articleids.Append(l); return true; });
                LogRecord_Helper.RecordLog(LogLevels.Warn, "错误删除管理员操作，怀疑为sql注入,管理员Id为" + adminInfo.AdminId + "，输入信息为" + articleids.ToString());
                return rsModel;
            }

            adminIds.Select(l =>
            {
                ids += "'" + l + "'" + ",";
                return true;
            });

            ids = ids.Substring(0, ids.Length - 1);
            string sql = string.Format(sqlDeleteTemple, "AdminId in(" + ids + ") AND [Level]>"+ adminInfo.Level);

            try
            {
                if (dal.OpeData(sql,null))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改成功！";
                    return rsModel;
                }
                else
                {
                    rsModel.isRight = false;
                    rsModel.message = "你删除的内容有误，请重试或联系管理员！";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.isRight = false;
                rsModel.message = "系统出现一个问题，请联系管理员或重试！";
                return rsModel;
            }
        }
    }
}
