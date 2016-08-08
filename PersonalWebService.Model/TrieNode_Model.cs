using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebService.Model
{
    public class TrieNode
    {
        /// <summary>
        /// 词频统计
        /// </summary>
        public int freq;
        /// <summary>
        /// 记录该节点的字符
        /// </summary>
        public char nodeChar;
        /// <summary>
        /// 子数据
        /// </summary>
        public Dictionary<char, TrieNode> childNodes;
        ///是否是末尾
        public bool isEnd;
        public TrieNode()
        {
            childNodes = new Dictionary<char, TrieNode>();
        }

    }
}
