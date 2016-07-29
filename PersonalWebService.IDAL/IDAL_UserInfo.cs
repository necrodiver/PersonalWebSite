using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.IDAL
{
    public interface IDAL_UserInfo
    {
        bool UserInfoOpe(UserInfo_Model userInfo, OperatingModel operate);
        List<T> GetUserInfoList<T>(T userInfo);
        int GetUserInfoCount(string sql, object param);
        bool EditPwd(string sqlUpdate, DataField dataField);
    }
}
