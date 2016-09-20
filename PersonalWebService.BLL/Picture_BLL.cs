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
   
    public class Picture_BLL
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public static readonly int PageNum = 12;
        private WordsFilterDt wordsFilter = new WordsFilterDt();
        public static List<PictureSort> pictureSortList;
        private static IDAL.IDAL_PersonalBase dal = new Operate_DAL();
        private static readonly string sqlSelectTemplate = "SELECT {0} FROM [dbo].[Picture] WHERE {1}";
        private static readonly string sqlDeleteTemplate = "DELETE [dbo].[Picture] WHERE {0}";
        private static readonly string sqlUpdateTemplate = "UPDATE [dbo].[Picture] SET {0} WHERE {1}";
        public ReturnStatus_Model AddPicture(Picture_Model picture)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "新增图片";
            //首先各种验证
            if (!wordsFilter.DetectionWords(picture.PictureName))
            {
                rsModel.isRight = false;
                rsModel.message = "新增图片标题存在脏词，请检查后再次进行提交";
                return rsModel;
            }

            if (!wordsFilter.DetectionWords(picture.PictureExplain))
            {
                rsModel.isRight = false;
                rsModel.message = "新增图片说明存在脏词，请检查后再次进行提交";
                return rsModel;
            }

            //这里直接获取内容
            if (pictureSortList == null)
            {
                pictureSortList = dal.GetDataList<PictureSort>(null);
            }

            if (!pictureSortList.Exists(imgSort => imgSort.PictureSortId == picture.PictureSortId))
            {
                rsModel.isRight = false;
                rsModel.message = "新增图片的类别无效，请重新设定类别";
                return rsModel;
            }

            try
            {
                if (dal.OpeData(picture, OperatingModel.Add))
                {
                    rsModel.message = "新增图片成功";
                }
                else
                {
                    rsModel.message = "新增图片失败，请整理后再次发布";
                }
            }
            catch (Exception ex)
            {
                rsModel.message = "服务器错误,请稍后重试,如果不行请联系管理员进行处理（请先保存好您的图片）";
                LogRecord_Helper.RecordLog(LogLevels.Error, ex);
            }
            return rsModel;
        }

        public List<Picture_Model> GetPictureList(PictureCondition_Model pictureCondition)
        {
            //sql先不写了，根据数据库来写
            StringBuilder sbsql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!string.IsNullOrEmpty(pictureCondition.NickName))
            {
                sbsql.Append("U.NickName=@NickName AND");
                param.Add(new DataField { Name = "@NickName", Value = pictureCondition.NickName });
            }
            if (!string.IsNullOrEmpty(pictureCondition.PictureName))
            {
                sbsql.Append("P.PictureName=@PictureName AND");
                param.Add(new DataField { Name = "@PictureName", Value = pictureCondition.PictureName });
            }
            if (pictureCondition.PictureSortId != null)
            {
                sbsql.Append("P.PictureSortId=@PictureSortId AND");
                param.Add(new DataField { Name = "@PictureSortId", Value = pictureCondition.PictureSortId });
            }
            if (pictureCondition.FirstTime != null)
            {
                sbsql.Append("P.EditTime>=@FirstTime AND");
                param.Add(new DataField { Name = "@FirstTime", Value = pictureCondition.FirstTime });
            }
            if (pictureCondition.LastTime != null)
            {
                sbsql.Append("P.EditTime<=@LastTime AND");
                param.Add(new DataField { Name = "@LastTime", Value = pictureCondition.LastTime });
            }
            string sqlIndex = string.Empty;
            if (pictureCondition.PageIndex != null)
            {
                int pageIndex = Convert.ToInt32(pictureCondition.PageIndex);
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
	                                SELECT ROW_NUMBER() OVER(ORDER BY Hits DESC)NUM,* FROM
	                                (	
	                                	SELECT P.* FROM [dbo].[Picture] P
	                                	LEFT JOIN [dbo].[UserInfo] U ON P.UserId=U.UserId
	                                	WHERE {0}
	                                )UA
                                ) UI
                                WHERE {1}";
            //硬条件只给用户，管理员就全部了
            sbsql.Append("P.IsFreeze=1 AND P.PictureState=1");
            sqlSelect = string.Format(sqlSelect,sbsql.ToString(),sqlIndex);
            List<Picture> pictureList = new List<Picture>();
            try
            {
                pictureList = dal.GetDataList<Picture>(sqlSelect, param);
                if (pictureList == null || pictureList.Count == 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                return null;
            }
            List<Picture_Model> pictureModelList = new List<Picture_Model>();
            pictureList.Select(al =>
            {
                pictureModelList.Add(
                    new Picture_Model
                    {
                        PictureId = al.PictureId,
                        PictureName = al.PictureName,
                        PictureUrl = al.PictureUrl,
                        PictureExplain = al.PictureExplain,
                        ReleaseTime = al.EditTime,
                        IsExpose = al.IsExpose
                    });
                return true;
            });
            return pictureModelList;
        }

        public ReturnStatus_Model EditPicture(Picture_Model picture)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "修改图片";
            if (string.IsNullOrEmpty(picture.PictureId))
            {
                rsModel.message = "数据传入错误，请重新填写修改内容！";
                return rsModel;
            }
            string sql = string.Format(sqlSelectTemplate, "TOP (1) *", "PictureId=@PictureId");
            Picture pictureInfo = null;
            try
            {
                pictureInfo = dal.GetDataSingle<Picture>(sql, new DataField { Name = "@PictureId", Value = picture.PictureId });
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.message = "数据处理错误，请联系管理员";
                return rsModel;
            }

            if (pictureInfo == null)
            {
                rsModel.message = "图片不存在，无法修改，请重试！";
                return rsModel;
            }
            StringBuilder sbSql = new StringBuilder();
            List<DataField> param = new List<DataField>();
            if (!picture.PictureName.Equals(pictureInfo.PictureName))
            {
                sbSql.Append("[PictureName]=@PictureName,");
                param.Add(new DataField { Name = "@PictureName", Value = picture.PictureName });
            }
            if (!picture.PictureExplain.Equals(pictureInfo.PictureExplain))
            {
                sbSql.Append("[PictureExplain]=@PictureExplain,");
                param.Add(new DataField { Name = "@PictureExplain", Value = picture.PictureExplain });
            }
            if (!picture.PictureUrl.Equals(pictureInfo.PictureUrl))
            {
                sbSql.Append("[PictureUrl]=@PictureUrl,");
                param.Add(new DataField { Name = "@PictureUrl", Value = picture.PictureUrl });
            }
            if (picture.PictureSortId != pictureInfo.PictureSortId)
            {
                sbSql.Append("[PictureSortId]=@PictureSortId,");
                param.Add(new DataField { Name = "@PictureSortId", Value = picture.PictureSortId });
            }
            if (picture.PictureState!= pictureInfo.PictureState)
            {
                sbSql.Append("[PictureState]=@PictureState,");
                param.Add(new DataField { Name = "@PictureState", Value = picture.PictureState });
            }
            if (picture.IsExpose != pictureInfo.IsExpose)
            {
                sbSql.Append("[IsExpose]=@IsExpose");
                param.Add(new DataField { Name = "@IsExpose", Value = picture.IsExpose });
            }
            sbSql.Append("[EditTime]=GETDATE()");
            param.Add(new DataField { Name = "@PictureId", Value = picture.PictureId });
            string sqlEdit = string.Format(sqlUpdateTemplate, sbSql.ToString(), "[PictureId]=@PictureId");
            try
            {
                if (dal.OpeData(sqlEdit, param))
                {
                    rsModel.isRight = true;
                    rsModel.message = "修改成功！";
                    return rsModel;
                }
                rsModel.isRight = false;
                rsModel.message = "无法修改内容，请重新查看或联系管理员！";
                return rsModel;
            }
            catch (Exception ex)
            {
                LogRecord_Helper.RecordLog(LogLevels.Fatal, ex);
                rsModel.isRight = false;
                rsModel.message = "数据处理错误，请联系管理员";
                return rsModel;
            }
        }

        public ReturnStatus_Model DeletePicture(string[] pictureIdList)
        {
            ReturnStatus_Model rsModel = new ReturnStatus_Model();
            rsModel.isRight = false;
            rsModel.title = "图片删除";
            if (pictureIdList.Length > 100)
            {
                rsModel.message = "你的一次删除操作太多，不予执行！";
                return rsModel;
            }
            if (pictureIdList == null || pictureIdList.Length <= 0)
            {
                rsModel.message = "需要操作删除的图片为空，请先选择需要删除的图片";
                return rsModel;
            }
            UserInfo userInfo = SessionState.GetSession<UserInfo>("UserInfo");
            if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
            {
                rsModel.message = "你未登录账号或账号已过期，请重新登录！";
                return rsModel;
            }

            string ids = string.Empty;
            if (Utility_Helper.IsClassIds(pictureIdList))
            {
                rsModel.message = "你所需要操作的内容不合法！账号将被记录，请规范操作！";
                StringBuilder pcitureids = new StringBuilder();
                pictureIdList.Select(l => { pcitureids.Append(l); return true; });
                LogRecord_Helper.RecordLog(LogLevels.Warn, "错误删除图片操作，怀疑为sql注入,用户Id为" + userInfo.UserId + "，输入信息为" + pcitureids.ToString());
                return rsModel;
            }

            pictureIdList.Select(l =>
            {
                ids += "'" + l + "'" + ",";
                return true;
            });

            ids = ids.Substring(0, ids.Length - 1);
            string sql = string.Format(sqlDeleteTemplate, "Picture in(" + ids + ")", "[UserId]=@UserId");

            try
            {
                if (dal.OpeData(sql, new DataField { Name = "@UserId", Value = userInfo.UserId }))
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
