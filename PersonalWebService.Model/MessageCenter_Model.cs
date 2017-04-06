using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class MessageModel {
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

        [Required]
        [DisplayName("是否已读")]
        public int IsRead { get; set; }
    }
}
