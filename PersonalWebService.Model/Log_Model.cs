using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class LogInfo
    {
        public int Id { get; set; }
        public DateTime? Logged { get; set; }
        public LogLevels Level { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
    }

    public class LogCondition
    {
        public DateTime? LeftLogged { get; set; }
        public DateTime? RightLogged { get; set; }
        public LogLevels? Level { get; set; }
        public int? PageIndex { get; set; }
    }
}
