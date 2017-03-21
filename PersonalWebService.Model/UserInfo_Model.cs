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
        [DisplayName("用户Id")]
        [StringLength(32)]
        public string UserId { get; set; }

        [Required]
        [DisplayName("用户账号名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("昵称")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}长度不能超过20个字段，不能小于4个字段")]
        //这里需要添加敏感词过滤
        public string NickName { get; set; }

        [DisplayName("个人简介")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "{0}长度不能超过300个字段，不能小于1个字段")]
        public string Introduce { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}只能是字母、数字和下划线组成的")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string Password { get; set; }


        [Required]
        [DisplayName("用户头像")]
        [StringLength(1 * 1024 * 1024, MinimumLength = 10, ErrorMessage = "{0}不符合规格")]
        //这里需要添加图片过滤
        public string AccountPicture { get; set; }

        [DisplayName("创建日期")]
        public DateTime AddTime { get; set; }

        [DisplayName("修改日期")]
        public DateTime EditTime { get; set; }

        [DisplayName("当前登录状态")]
        public NowStatus? NowStatus { get; set; }

        [DisplayName("账号状态")]
        public State? State { get; set; }
    }
    /// <summary>
    /// 用户登录Model
    /// </summary>
    public class UserLogin
    {
        [Required]
        [DisplayName("用户名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}不符合规范")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string PassWord { get; set; }

        [Required]
        [DisplayName("验证码")]
        public List<Coord> ValidateCode { get; set; }
    }
    public class SendEmail
    {
        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string Email { get; set; }
    }
    /// <summary>
    /// 初步注册账号
    /// </summary>
    public class UserRegister
    {
        [Required]
        [DisplayName("用户名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("昵称")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}长度不能超过20个字段，不能小于4个字段")]
        //这里需要添加敏感词过滤
        public string NickName { get; set; }

        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}不符合规范")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string PassWord { get; set; }

        [Required]
        [DisplayName("验证码")]
        [RegularExpression(@"^[A-Za-z0-9]{4}$", ErrorMessage = "{0}不符合规范")]
        public string ValidateCode { get; set; }
    }

    /// <summary>
    /// 找回密码Model
    /// </summary>
    public class RetrievePwdStart
    {
        [Required]
        [DisplayName("注册的用户名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string Email { get; set; }

        [Required]
        [DisplayName("验证码")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "{0} 长度必须是6位数字或字母组合")]
        public List<Coord> ValidateCode { get; set; }
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
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
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

    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名（邮箱地址）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string AccountPicture { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastvisitDate { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public State State { get; set; }
        /// <summary>
        /// 用户登录状态
        /// </summary>
        public NowStatus NowStatus { get; set; }
    }

    /// <summary>
    /// 用户搜索条件Model
    /// </summary>
    public class UserInfoCondition
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名（邮箱地址）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? EditTime { get; set; }
        /// <summary>
        /// 当前登录状态
        /// </summary>
        public NowStatus? Status { get; set; }
        /// <summary>
        /// 文章Id
        /// </summary>
        public string ArticleId { get; set; }
        /// <summary>
        /// 图片Id
        /// </summary>
        public string PictureId { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int? PageIndex { get; set; }
    }

    public class AdminEditUserInfo
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; }
        [DisplayName("用户名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string UserName { get; set; }
        [DisplayName("昵称")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}长度不能超过20个字段，不能小于4个字段")]
        public string NickName { get; set; }
        [DisplayName("用户头像")]
        [StringLength(1 * 1024 * 1024, MinimumLength = 10, ErrorMessage = "{0}不符合规格")]
        public string AccountPicture { get; set; }
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}只能是字母、数字和下划线组成的")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string Password { get; set; }
        [DisplayName("个人简介")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "{0}长度不能超过300个字段，不能小于1个字段")]
        public string Introduce { get; set; }
    }

    public class Coord
    {
        public float x { get; set; }
        public float y { get; set; }
    }
}
