using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalWebService.Helper
{
    public class WordsFilterDt_Helper
    {
    }

    /// <summary>
    /// 敏感词模型验证
    /// </summary>
    public class DirtyWordsAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}中存在不文明词语", name);
        }
        public override bool IsValid(object value)
        {
            string str = value.ToString();
            if (string.IsNullOrEmpty(str))
                return true;
            return new WordsFilterDt().DetectionWords(str);
        }
    }

    public class WordsFilterDt
    {
        private TrieNodeOperate trie = new TrieNodeOperate();
        /// <summary>
        /// 判断是否存在敏感词
        /// </summary>
        /// <param name="wordsStr">传入需要检测的字符串</param>
        /// <returns></returns>
        public bool DetectionWords(string wordsStr)
        {
            if (string.IsNullOrEmpty(wordsStr))
            {
                return true;
            }
            GetWordsLibrary();

            return trie.DetectionWords(wordsStr);
        }
        private void GetWordsLibrary()
        {
            var file = File.ReadAllLines("", Encoding.UTF8).ToList();
            List<string> words = new List<string>();
            file.FindAll(child =>
            {
                words.Add(child.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault().ToLower().ToString() + "#");
                return true;
            });
            trie.AddTrieNodes(words);
        }
    }

    public class TrieNodeOperate
    {
        private static TrieNode trieNodes;
        //把敏感词库添加到Trie树里边
        public void AddTrieNodes(List<string> list)
        {
            if (trieNodes == null)
                trieNodes = new TrieNode();
            else
                return;

            if (list == null || list.Count == 0)
                return;
            list.FindAll(child =>
            {
                if (string.IsNullOrEmpty(child))
                    return false;
                AddTrieNode(ref trieNodes, child);
                return true;
            });
            return;
        }

        private void AddTrieNode(ref TrieNode childNode, string str)
        {
            if (string.IsNullOrEmpty(str))
                return;

            if (!childNode.childNodes.ContainsKey(str[0]))
            {
                childNode.childNodes[str[0]] = new TrieNode();
                childNode.childNodes[str[0]].nodeChar = str[0];
            }

            string nextstr = str.Substring(1);
            TrieNode child = childNode.childNodes[str[0]];
            if (str.Length == 2 && str[1].Equals('#'))
            {
                childNode.childNodes[str[0]].isEnd = true;
                return;
            }
            AddTrieNode(ref child, nextstr);
            childNode.childNodes[str[0]] = child;
        }
        public bool DetectionWords(string words)
        {
            if (string.IsNullOrEmpty(words))
            {
                return true;
            }
            //进行正则匹配获取所有的汉字和字母
            Regex reg = new Regex(@"([\u4E00-\u9FFF]+)|([a-zA-Z]+)");
            var wordList = reg.Matches(words.ToLower());
            string word = string.Empty;
            foreach (var item in wordList)
            {
                word += item.ToString();
            }
            return SearchWords(trieNodes, word);
        }
        /// <summary>
        /// 进行检测
        /// </summary>
        /// <param name="childNode">子库</param>
        /// <param name="words">检测的字符串</param>
        /// <returns></returns>
        private bool SearchWords(TrieNode childNode, string words)
        {
            if (string.IsNullOrEmpty(words))
                return true;
            char wordchar = words[0];
            string nextwords = words.Substring(1);
            if (childNode.childNodes.ContainsKey(wordchar))
            {
                if (childNode.childNodes[wordchar].isEnd)
                    return false;
                else
                {
                    if (!childNode.childNodes[wordchar].childNodes.ContainsKey(nextwords[0]))
                    {
                        return SearchWords(trieNodes, nextwords);
                    }
                    return SearchWords(childNode.childNodes[wordchar], nextwords);
                }
            }
            return SearchWords(trieNodes, nextwords);
        }
    }
}
