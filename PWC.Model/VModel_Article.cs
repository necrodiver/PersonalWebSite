using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWC.Model
{
    public class Article_Model
    {
        [DisplayName("文章Id")]
        [StringLength(32)]
        public string ArticleId { get; set; }

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

        [Required]
        [DisplayName("文章是否公开")]
        public int IsExpose { get; set; }
    }

    /// <summary>
    /// 查询文章条件
    /// </summary>
    public class ArticleCondition_Model
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 文章类别
        /// </summary>
        public int? ArticleSortId { get; set; }
        /// <summary>
        /// 文章名称
        /// </summary>
        public string ArticleName { get; set; }
        /// <summary>
        /// 发布时间范围开始
        /// </summary>
        public DateTime? FirstTime { get; set; }
        /// <summary>
        /// 发布时间范围结束
        /// </summary>
        public DateTime? LastTime { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int? PageIndex { get; set; }

    }
}
