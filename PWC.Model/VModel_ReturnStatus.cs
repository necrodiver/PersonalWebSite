using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWC.Model
{
    /// <summary>
    /// 返回数据，api的返回状态
    /// </summary>
    public class ReturnStatus_Model
    {
        public bool isRight { get; set; }
        public string title { get; set; }
        public string message { get; set; }
    }
}
