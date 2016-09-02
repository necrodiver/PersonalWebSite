using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    /// <summary>
    /// 图片类别
    /// </summary>
    public class PictureSort
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public int PictureSortId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string PictureSortName { get; set; }
        /// <summary>
        /// 说明内容
        /// </summary>
        public string PictureSortExplain { get; set; }
        /// <summary>
        /// 分类添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 分类修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 分类状态
        /// </summary>
        public int State { get; set; }
    }
}
