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
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo">用户model</param>
        /// <param name="operate">操作方式（增删改查）</param>
        /// <returns></returns>
        bool UserInfoOpe(UserInfo_Model userInfo, OperatingModel operate);
        /// <summary>
        /// 获取用户的所有信息
        /// </summary>
        /// <typeparam name="T">输入的类</typeparam>
        /// <param name="userInfo">模型</param>
        /// <returns></returns>
        List<T> GetUserInfoList<T>(T userInfo);
        /// <summary>
        /// 获取相应用户的数量
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        int GetUserInfoCount(string sql, object param);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sqlUpdate">sql语句</param>
        /// <param name="dataField">键值对</param>
        /// <returns></returns>
        bool EditPwd(string sqlUpdate, DataField dataField);
    }
}
