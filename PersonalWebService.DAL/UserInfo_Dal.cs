using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebService.Model;

namespace PersonalWebService.DAL
{
    public class UserInfo_DAL
    {
        GenericityOperateDB generDB = new GenericityOperateDB();
        public bool UserInfoOpe(UserInfo_Model userInfo,OperatingModel operate)
        {
            return generDB.Operate(userInfo, operate);
        }
    }
}
