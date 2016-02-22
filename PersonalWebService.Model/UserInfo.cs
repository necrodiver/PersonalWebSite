using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class UserInfo
    {
        [Required]
        [DisplayName("用户Id")]
        [RegularExpression(@"^\d+", ErrorMessage = "{0}只能是数字")]
        [StringLength(32)]
        public string UserID { get; set; }
        [Required]
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "{0}只能是字母、数字或字母和数字的混合")]
        public string Password { get; set; }
        [DisplayName("用户状态")]
        public int Status { get; set; }
        [DisplayName("用户类型")]
        public UserType UserType { get; set; }
    }
}
