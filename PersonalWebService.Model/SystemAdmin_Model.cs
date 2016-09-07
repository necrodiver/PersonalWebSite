using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class SystemAdmin
    {
        public string AdminId { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastvisitDate { get; set; }
        /// <summary>
        /// 管理员权限  
        /// 最高权限 = -100,
        /// 第一权限 = 1
        /// </summary>
        public AdminLevel Level { get; set; }
    }
    public class EditAdmin
    {
        [DisplayName("用户名")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}长度不能超过50个字段，不能小于5个字段")]
        public string Name { get; set; }
        [DisplayName("登录密码")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}不符合规范")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}长度不能超过20个字段，不能小于6个字段")]
        public string Pwd { get; set; }
        [DisplayName("管理等级")]
        public AdminLevel Level { get; set; }
    }

    /// <summary>
    /// 管理员登录Model
    /// </summary>
    public class AdminLogin
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
}
