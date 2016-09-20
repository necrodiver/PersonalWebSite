using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class Picture_Model
    {
        [DisplayName("图片Id")]
        [StringLength(32)]
        public string PictureId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("图片名称")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度不能超过{1}个字段，不能小于{2}个字段")]
        [RegularExpression(@"^[([a-zA-Z\u4e00-\u9fa5])|(\w)]+$", ErrorMessage = "{0}必须是由汉字字母或数字组成")]
        //这里需添加敏感词过滤
        public string PictureName { get; set; }
        [Required]
        [DisplayName("图片类别Id")]
        public int PictureSortId { get; set; }
        [Required]
        [DisplayName("图片链接")]
        [StringLength(200, MinimumLength=5, ErrorMessage ="{0}不符合要求长度")]
        public string PictureUrl { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("图片说明")]
        public string PictureExplain { get; set; }
        [Required]
        [DisplayName("发布时间/修改时间")]
        public DateTime ReleaseTime { get; set; }
        [Required]
        [DisplayName("是否发布")]
        public int IsExpose { get; set; }

        [Required]
        [DisplayName("图片状态")]
        public WorkState PictureState { get; set; }

    }

    /// <summary>
    /// 图片条件查询
    /// </summary>
    public class PictureCondition_Model
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 图片类别
        /// </summary>
        public int? PictureSortId { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string PictureName { get; set; }
        /// <summary>
        /// 发布时间范围开始
        /// </summary>
        public DateTime? FirstTime { get; set; }
        /// <summary>
        /// 发布时间范围结束
        /// </summary>
        public DateTime? LastTime { get; set; }
    }
    public class Picture
    {
        [Key]
        public string PictureId { get; set; }
        public string PictureName { get; set; }
        public string PictureUrl { get; set; }
        /// <summary>
        /// 图片类别Id
        /// </summary>
        public int PictureSortId { get; set; }
        /// <summary>
        /// 图片说明
        /// </summary>
        public string PictureExplain { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 热度(点击量)
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 是否公开（0：不公开，1：公开）
        /// </summary>
        public int IsExpose { get; set; }
        /// <summary>
        /// 图片状态(草稿 = 0, 已发布 = 1)
        /// </summary>
        public WorkState PictureState { get; set; }
        /// <summary>
        ///是否被冻结(0:是,1:否)
        /// </summary>
        public int IsFreeze { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
