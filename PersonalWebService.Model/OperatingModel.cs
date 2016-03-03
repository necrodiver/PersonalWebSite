using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
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
}
