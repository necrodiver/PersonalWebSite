using System;
using System.Collections.Generic;
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
}
