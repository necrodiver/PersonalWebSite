using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class UserComment
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public string CommentId { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string ReText { get; set; }
        /// <summary>
        /// 针对文章Id/图片Id
        /// </summary>
        public string WorkId { get; set; }
        /// <summary>
        /// 评论人
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 上级评论人（若无则为直接1级评论，若有则为在其他评论下的评论）
        /// </summary>
        public string CommentParentId { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }
        /// <summary>
        /// 评论状态(0:显示，1：屏蔽)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 子评论
        /// </summary>
        private UserComment ChildUserComment { get; set; }
    }

    public class UserComment_Model
    {
        [DisplayName("评论Id")]
        [StringLength(32)]
        public string CommentId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("评论内容")]
        [StringLength(3000,MinimumLength =10,ErrorMessage ="{0}长度不能超过3000个字符，不能少于10个字符")]
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
