using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class Article_Model
    {
        [DisplayName("文章Id")]
        [StringLength(32)]
        public string ArtilceId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("文章名称")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度不能超过{1}个字段，不能小于{2}个字段")]
        [RegularExpression(@"^[([a-zA-Z\u4e00-\u9fa5])|(\w)]+$", ErrorMessage = "{0}必须是由汉字字母或数字组成")]
        //这里需要添加敏感词过滤
        public string ArticleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("文章内容")]
        [StringLength(5 * 1024 * 1024, MinimumLength = 20, ErrorMessage = "{0}长度不能超过{1}个字段，不能小于{2}个字段")]
        //这里需要添加敏感词过滤
        public string ArticleContent { get; set; }

        [Required]
        [DisplayName("文章分类")]
        public int ArticleSortId { get; set; }

        [Required]
        [DisplayName("文章状态")]
        public WorkState ArticleState { get; set; }
    }

    /// <summary>
    /// 查询文章条件
    /// </summary>
    public class ArticleCondition_Model
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 文章类别
        /// </summary>
        public string ArticleSort { get; set; }
        /// <summary>
        /// 文章名称
        /// </summary>
        public string ArticleName { get; set; }
        /// <summary>
        /// 发布时间范围开始
        /// </summary>
        public DateTime FirstTime { get; set; }
        /// <summary>
        /// 发布时间范围结束
        /// </summary>
        public DateTime LastTime { get; set; }
    }

    /// <summary>
    /// 文章模型（数据库通）
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public string ArtilceId { get; set; }
        /// <summary>
        /// 文章名称
        /// </summary>
        public string ArticleName { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string ArticleContent { get; set; }
        /// <summary>
        /// 文章类别
        /// </summary>
        public string ArticleSort { get; set; }
        /// <summary>
        /// 文章状态
        /// </summary>
        public WorkState ArticleState { get; set; }
        /// <summary>
        /// 文章创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 文章最新修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 文章点击量
        /// </summary>
        public int hits { get; set; }
        /// <summary>
        /// 文章是否公开
        /// </summary>
        public int IsExpose { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
