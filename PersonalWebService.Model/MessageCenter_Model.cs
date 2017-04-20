using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class MessageModel
    {
        [Required]
        [DisplayName("单页数量")]
        public int PageSize { get; set; }

        [Required]
        [DisplayName("当前页码")]
        public int PageCurrent { get; set; }

        [Required]
        [DisplayName("消息类型")]
        public MessageType MessagType { get; set; }
    }

    public class PrivateLetter
    {
        [Required]
        [DisplayName("私信Id")]
        public string PL_Id { get; set; }

        [Required]
        [DisplayName("发送者")]
        public string PL_SenderId { get; set; }

        [Required]
        [DisplayName("接收者")]
        public string PL_AddresseeId { get; set; }

        [Required]
        [DisplayName("发送内容")]
        public string PL_Message { get; set; }

        [Required]
        [DisplayName("发送时间")]
        public DateTime PL_Sendtime { get; set; }

        [DisplayName("回信上一个私信的Id")]
        public string PL_ParentId { get; set; }

        [DisplayName("是否已读")]
        public IsReaded PL_IsRead { get; set; }

        [DisplayName("是否已被删除")]
        public int PL_IsDeleted { get; set; }
    }

    public class Message
    {
        [Required]
        [DisplayName("消息Id")]
        public int M_Id { get; set; }

        [Required]
        [DisplayName("消息类型Id")]
        public MessageType M_Type { get; set; }

        [Required]
        [DisplayName("添加时间")]
        public DateTime M_AddTime { get; set; }

        [Required]
        [DisplayName("消息内容Id")]
        public string M_NameId { get; set; }

        [Required]
        [DisplayName("发送者Id")]
        public string M_SenderId { get; set; }

        [Required]
        [DisplayName("接收者Id")]
        public string M_ReceiverId { get; set; }

        [Required]
        [DisplayName("是否已读")]
        /// <summary>
        /// 0：未读，1：已读
        /// </summary>
        public IsReaded M_IsRead { get; set; }
    }
}
