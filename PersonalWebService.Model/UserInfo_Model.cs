﻿using System;
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
        public string Email { get; set; }

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

        [DisplayName("现住地址")]
        public string Address { get; set; }

        [DisplayName("经验")]
        [MaxLength(99999, ErrorMessage = "{0}值错误")]
        public int EXP { get; set; }

        [DisplayName("个人域名")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "{0}长度必须大于1且小于40")]
        public string SelfDmainName { get; set; }

        [DisplayName("创建日期")]
        public DateTime AddTime { get; set; }

        [DisplayName("修改日期")]
        public DateTime? EditTime { get; set; }

        [DisplayName("当前登录状态")]
        public NowStatus? NowStatus { get; set; }

        [DisplayName("账号状态")]
        public StateUser? State { get; set; }

        [DisplayName("等级")]
        [MaxLength(1, ErrorMessage = "{0}不符合规范")]
        public int Level { get; set; }

        [DisplayName("关注")]
        [MaxLength(8, ErrorMessage = "{0}不符合规范")]
        public int Focus { get; set; }

        [DisplayName("粉丝")]
        [MaxLength(8, ErrorMessage = "{0}不符合规范")]
        public int Fans { get; set; }
    }
    /// <summary>
    /// 用户登录Model
    /// </summary>
    public class UserLogin
    {
        [Required]
        [DisplayName("用户账号名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string Email { get; set; }

        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}不符合规范")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string PassWord { get; set; }

        [Required]
        [DisplayName("验证码")]
        [RegularExpression(@"^[A-Za-z0-9]{6}$", ErrorMessage = "{0}不符合规范")]
        public string ValidateCode { get; set; }
    }

    /// <summary>
    /// 用户登录Model 第二种
    /// </summary>
    public class UserLoginTwo
    {
        [Required]
        [DisplayName("用户账号名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string Email { get; set; }

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
        [DisplayName("用户账号名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string Email { get; set; }

        [Required]
        [DisplayName("昵称")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}长度不能超过20个字段，不能小于4个字段")]
        [DirtyWords]
        public string NickName { get; set; }

        [Required]
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}不符合规范")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string PassWord { get; set; }

        [Required]
        [DisplayName("验证码")]
        [RegularExpression(@"^[A-Za-z0-9]{6}$", ErrorMessage = "{0}不符合规范")]
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
        [RegularExpression(@"^[A-Za-z0-9]{6}$", ErrorMessage = "{0}不符合规范")]
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

    /// <summary>
    /// 存储验证码和时间Model ,用于主页面
    /// </summary>
    public class RetrieveValueCN
    {
        [DisplayName("验证码")]
        public int[] ValidateCode { get; set; }
        [DisplayName("存储时间")]
        public DateTime SaveTime { get; set; }
    }

    public class ResetPwd
    {
        [Required]
        [DisplayName("用户账号名")]
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

    public class ResetPwdSet
    {
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
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
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
        /// 
        public string Password { get; set; }
        /// <summary>
        /// 现住地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经验
        /// </summary>
        public int EXP { get; set; }
        /// <summary>
        /// 个人域名名称
        /// </summary>
        public string SelfDmainName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? EditTime { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastvisitDate { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public StateUser State { get; set; }
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
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
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
        [DisplayName("用户账号名")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0}必须是邮箱格式，以便找回密码")]
        public string Email { get; set; }
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
        [DisplayName("现住地址")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "{0}长度不能超过120个字段，不能小于2个字段")]
        public string Address { get; set; }
    }

    public class Coord
    {
        public float x { get; set; }
        public float y { get; set; }
    }
}
