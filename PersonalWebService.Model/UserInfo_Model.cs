using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class UserInfo_Model
    {
        [Required]
        [DisplayName("用户Id")]
        [StringLength(32)]
        public string UserID { get; set; }

        [Required]
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0}必须是邮箱，以便找回密码")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("昵称")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}长度不能超过20个字段，不能小于4个字段")]
        public string Nickname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("登录密码")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "{0}只能是字母、数字或字母和数字的混合")]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("用户状态")]
        public int? Status { get; set; }

        [DisplayName("用户类型")]
        public UserType? UserType { get; set; }
    }
    public class UserLogin
    {
        [Required]
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}长度不能超过50个字段，不能小于5个字段")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "{0}只能是字母、数字或字母和数字的混合")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string PassWord { get; set; }

        [Required]
        [DisplayName("验证码")]
        [StringLength(6, ErrorMessage = "{0} 长度必须是6位数字或字母组合")]
        public string ValidateCode { get; set; }
    }

    public class RetrievePwdStart
    {
        [Required]
        [DisplayName("注册的用户名")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0}必须是邮箱，以便找回密码")]
        public string Email { get; set; }

        [Required]
        [DisplayName("验证码")]
        [StringLength(6, ErrorMessage = "{0} 长度必须是6位数字或字母组合")]
        public string ValidateCode { get; set; }
    }
}
