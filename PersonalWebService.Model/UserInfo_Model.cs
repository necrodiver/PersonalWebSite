using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    /// <summary>
    /// 用户信息Model
    /// </summary>
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
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}只能是字母、数字和下划线组成的")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string Password { get; set; }

        [DisplayName("用户状态")]
        public UserState? Status { get; set; }

        [DisplayName("用户类型")]
        public UserType? UserType { get; set; }
    }
    /// <summary>
    /// 用户登录Model
    /// </summary>
    public class UserLogin
    {
        [Required]
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}长度不能超过50个字段，不能小于5个字段")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}不符合规范")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string PassWord { get; set; }

        [Required]
        [DisplayName("验证码")]
        [StringLength(6, ErrorMessage = "{0} 长度必须是6位数字或字母组合")]
        public string ValidateCode { get; set; }
    }

    /// <summary>
    /// 找回密码Model
    /// </summary>
    public class RetrievePwdStart
    {
        [Required]
        [DisplayName("注册的用户名")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0}必须是邮箱，以便找回密码")]
        public string Email { get; set; }

        [Required]
        [DisplayName("验证码")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "{0} 长度必须是6位数字或字母组合")]
        public string ValidateCode { get; set; }
    }

    /// <summary>
    /// 存储验证码和时间Model
    /// </summary>
    public class RetrieveValue
    {
        [DisplayName("验证码")]
        public string ValidateCode { get; set; }
        [DisplayName("存储时间")]
        public DateTime SaveTime { get; set; }
    }

    public class ResetPwd
    {
        [Required]
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0}必须是邮箱，以便找回密码")]
        public string Email { get; set; }
        [Required]
        [DisplayName("验证码")]
        [RegularExpression(@"^[A-Za-z0-9]{6}$", ErrorMessage = "{0}不符合规范")]
        public string ValidateCode { get; set; }
        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}必须是字母、数字和下划线组成的")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string Password { get; set; }
    }
}
