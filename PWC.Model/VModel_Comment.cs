using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWC.Model
{
    public class UserComment_Model
    {
        [DisplayName("评论Id")]
        [StringLength(32)]
        public string CommentId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("评论内容")]
        [StringLength(3000, MinimumLength = 10, ErrorMessage = "{0}长度不能超过3000个字符，不能少于10个字符")]
        public string ReText { get; set; }
        [Required]
        [DisplayName("文章/图片Id")]
        [StringLength(32)]
        public string WorkId { get; set; }
        [Required]
        [StringLength(32)]
        [DisplayName("评论人Id")]
        public string UserId { get; set; }
        [DisplayName("上一级评论Id")]
        public string CommentParentId { get; set; }
        [Required]
        [DisplayName("评论时间")]
        public DateTime CommentTime { get; set; }
        [DisplayName("评论状态")]
        //(0:显示，1：屏蔽)
        public int State { get; set; }
        [DisplayName("子评论")]
        private UserComment_Model ChildUserComment { get; set; }
    }
    /// <summary>
    /// 评论查询条件
    /// </summary>
    public class UserCommentCondition_Model
    {
        [Required]
        [DisplayName("文章/图片Id")]
        [StringLength(32)]
        public string WorkId { get; set; }
        [Required]
        [DisplayName("评论页数")]
        public int PageIndex { get; set; }
    }
}
