using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.IDAL
{
    public interface IDAL_PersonalBase
    {

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="model">实体数据</param>
        /// <param name="operate">操作方式</param>
        /// <returns></returns>
        bool OpeData<T>(T model, OperatingModel operate);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="sql">查询数据</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        T GetDataSingle<T>(string sql, object param) where T : class;

        /// <summary>
        /// 获取所有数据（只针对固定数据的库）
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="model">实体数据</param>
        /// <returns></returns>
        List<T> GetDataList<T>(T model);

        /// <summary>
        /// 获取数据的数据量
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        int GetDataCount(string sql, object param);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="sql">操作语句</param>
        /// <param name="operate">操作方式(增删改)</param>
        /// <returns></returns>
        bool OpeData(string sql,object param);
    }
}
