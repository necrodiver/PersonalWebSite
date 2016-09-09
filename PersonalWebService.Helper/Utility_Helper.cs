using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalWebService.Helper
{
    public static class Utility_Helper
    {
        /// <summary>
        /// 判断是否是分类的Id（是否为GUID字符串）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsClassIds(string[] str)
        {
            Regex re = new Regex(@"^[a-zA-Z0-9]+$");
            foreach (var item in str)
            {
                if (string.IsNullOrEmpty(item))
                    return false;
                if (!re.IsMatch(item))
                    return false;
            }
            return true;
        }
    }
}
