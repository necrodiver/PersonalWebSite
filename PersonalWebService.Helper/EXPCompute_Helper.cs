using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Helper
{
    public class EXPCompute_Helper
    {
        private const int Lv0 = 0;
        private const int Lv1 = 1000;
        private const int Lv2 = 2000;
        private const int Lv3 = 4000;
        private const int Lv4 = 7000;
        private const int Lv5 = 10000;
        private const int Lv6 = 12500;
        private const int Lv7 = 15000;
        private const int Lv8 = 20000;

        /// <summary>
        /// 提交一个涂鸦获得的经验
        /// </summary>
        public const int ScrawlOneUp = 20;
        /// <summary>
        /// 提交一个随笔获得的经验
        /// </summary>
        public const int JottingsOneUp = 20;
        /// <summary>
        /// 提交一个评论获得的经验
        /// </summary>
        public const int PostOneUp = 1;
        /// <summary>
        /// 添加一个粉丝获得的经验
        /// </summary>
        public const int FansOneAdd = 8;
        /// <summary>
        /// 被一个关注获得的经验
        /// </summary>
        public const int FocusOneAdd = 5;
        /// <summary>
        /// 签到一次获取的经验
        /// </summary>
        public const int ChickInOneAdd = 5;

        public static int GetLevel(int exp)
        {
            if (exp < Lv1)
                return 0;
            else if (exp < Lv2)
                return 1;
            else if (exp < Lv3)
                return 2;
            else if (exp < Lv4)
                return 3;
            else if (exp < Lv5)
                return 4;
            else if (exp < Lv6)
                return 5;
            else if (exp < Lv7)
                return 6;
            else if (exp < Lv8)
                return 7;
            else
                return 8;
        }
    }
}
