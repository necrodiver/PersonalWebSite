﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;
using PersonalWebService.DAL;
using PersonalWebService.Helper;

namespace PersonalWebService.BLL
{
    public class UserComment_BLL
    {
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[UserComment] WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[UserComment] where {0}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[UserComment] SET {0} WHERE {1}";
        private static readonly int commentEveryCount = 10;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        public ReturnStatus_Model AddComment(UserComment_Model userComment)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.title = "添加评论";
            rsModel.isRight = false;
            try
            {
                string sqltemp1 = @"SELECT Count(*) FROM Article,Picture WHERE Article.ArticleId=@WorkId or Picture.PictureId=@WorkId ";
                if (dal.GetDataCount(sqltemp1, new DataField { Name = "@WorkId", Value = userComment.WorkId }) <= 0)
                {
                    rsModel.message = "您所要评论的文章或图片找不到了，请刷新后重试~~";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.message = "评论出错，请稍后重试";
                return rsModel;
            }
            UserInfo userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo == null)
            {
                rsModel.isRight = false;
                rsModel.message = "您已退出登录或当前未登录，请先登录后再进行评论";
                return rsModel;
            }
            UserComment userCommentModel = new UserComment();
            userCommentModel.ReText = userComment.ReText;
            userCommentModel.WorkId = userComment.WorkId;
            userCommentModel.UserId = userInfo.UserId;
            userCommentModel.CommentTime = DateTime.Now;
            userCommentModel.CommentParentId = userComment.CommentParentId;
            try
            {
                if (dal.OpeData<UserComment>(userCommentModel, OperatingModel.Add))
                {
                    rsModel.isRight = true;
                    rsModel.message = "评论成功！";
                    return rsModel;
                }
                else
                {
                    rsModel.isRight = false;
                    rsModel.message = "评论失败，请稍后重试";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.message = "评论出错，请稍后重试";
                return rsModel;
            }
        }

        public List<UserComment_Model> GetList(UserCommentCondition_Model condition)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            if (condition.Index < 0 || string.IsNullOrEmpty(condition.WorkId))
            {
                return null;
            }
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append("SELECT* FROM ");
            sbsql.Append(" (SELECT ROW_NUMBER() OVER(ORDER BY CommentId) NUM, *FROM[dbo].[UserComment])U ");
            sbsql.Append("  WHERE NUM > @FirstIndex AND NUM<@LastIndex");
            //10*n-9    10*n
            int firstIndex = (condition.Index - 1) * commentEveryCount + 1;
            int lastIndex = condition.Index * commentEveryCount;
            List<DataField> param = new List<DataField>();
            param.Add(new DataField { Name = "@FirstIndex", Value = firstIndex });
            param.Add(new DataField { Name = "@LastIndex", Value = lastIndex });
            List<UserComment_Model> userCommentList = new List<UserComment_Model>();
            try
            {
                List<UserComment> userCommentLists = dal.GetDataList<UserComment>(sbsql.ToString(), param);
                if (userCommentLists == null | userCommentLists.Count <= 0)
                {
                    return null;
                }
                userCommentLists.Select(u =>
                {
                    userCommentList.Add(new UserComment_Model
                    {
                        CommentId = u.CommentId,
                        ReText = u.ReText,
                        WorkId = u.WorkId,
                        UserId = u.UserId,
                        CommentParentId = u.CommentParentId,
                        CommentTime = u.CommentTime,
                        State = u.State
                    });
                    return true;
                });
                return userCommentList;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Error,ex.ToString());
                return null;
            }
        }

        public ReturnStatus_Model DeleteSingle(string userCommendId)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "删除评论";
            UserInfo userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            string sqlSelect = @"SELECT COUNT(*) FROM [dbo].[UserComment]
                            WHERE CommentId=@CommentId and UserId=@UserId";
            List<DataField> paramSelect = new List<DataField>();
            paramSelect.Add(new DataField { Name = "@CommentId", Value = userCommendId });
            paramSelect.Add(new DataField { Name = "@UserId", Value = userInfo.UserId });
            try
            {
                if (dal.GetDataCount(sqlSelect, paramSelect) <= 0)
                {
                    rsModel.isRight = false;
                    rsModel.message = "不存在此条评论，无法删除！";
                    return rsModel;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.isRight = false;
                rsModel.message = "评论删除出错，请稍后重试";
                return rsModel;
            }
            string sql = string.Format(sqlDeleteTemplate, "WHERE CommentId=@CommentId and UserId=@UserId");
            try
            {
                if (dal.OpeData(sql, paramSelect))
                {
                    rsModel.isRight = true;
                    rsModel.message = "删除评论成功！";
                    return rsModel;
                }
                rsModel.isRight = false;
                rsModel.message = "删除失败，请稍后重试！";
                return rsModel;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex.ToString());
                rsModel.isRight = false;
                rsModel.message = "评论删除出错，请稍后重试或联系管理员查看！";
                return rsModel;
            }

        }
    }
}
