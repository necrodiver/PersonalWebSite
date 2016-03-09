using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    /// <summary>
    /// 用户类别
    /// </summary>
    public enum UserType
    {
        None = -100,
        管理员 = -1,
        普通用户 = 1
    }
    public enum UserState
    {
        退出登录 = 0,
        已登录 = 1,
        禁用修改 = 3
    }
}
