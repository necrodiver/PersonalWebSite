using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    /// <summary>
    /// 文章状态
    /// </summary>
    public class ArticleState
    {
        /// <summary>
        /// 文章状态Id
        /// </summary>
        [Key]
        public int ArticleStateId { get; set; }
        /// <summary>
        /// 文章状态
        /// </summary>
        public int ArticleStateNum { get; set; }
        /// <summary>
        /// 文章状态说明
        /// </summary>
        public string ArticleStateExplain { get; set; }
    }
}
