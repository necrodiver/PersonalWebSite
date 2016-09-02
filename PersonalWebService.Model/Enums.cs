using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    /// <summary>
    /// 用户类别
    /// </summary>
    public enum UserType
    {
        None = -100,
        管理员 = -1,
        普通用户 = 1
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {
        冻结 = -100,
        退出登录 = 0,
        已登录 = 1,
        禁用修改 = 3
    }

    /// <summary>
    /// 文章/图片 状态
    /// </summary>
    public enum WorkState
    {
        冻结 = -100,
        草稿 = 0,
        已发布 = 1
    }

    /// <summary>
    /// 数据库操作方式
    /// </summary>
    public enum OperatingModel
    {
        /// <summary>
        /// 增
        /// </summary>
        Add,
        /// <summary>
        /// 删
        /// </summary>
        Delete,
        /// <summary>
        /// 改
        /// </summary>
        Edit,
        /// <summary>
        /// 查
        /// </summary>
        Get
    }
    /// <summary>
    /// 错误等级
    /// </summary>
    public enum LogLevels
    {
        /// <summary>
        /// 1/6.致命性错误
        /// </summary>
        Fatal,
        /// <summary>
        /// 2/6.标准错误
        /// </summary>
        Error,
        /// <summary>
        /// 3/6.警告
        /// </summary>
        Warn,
        /// <summary>
        /// 4/6.信息
        /// </summary>
        Info,
        /// <summary>
        /// 5/6.调试消息
        /// </summary>
        Debug,
        /// <summary>
        /// 6/6.设置信息
        /// </summary>
        Trace
    }
}
