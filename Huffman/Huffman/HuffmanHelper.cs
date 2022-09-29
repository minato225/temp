using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    public static class HuffmanHelper
    {
        public static CodeTreeNode Huffman(List<CodeTreeNode> codes)
        {
            while (codes.Count > 1)
            {
                codes = codes.OrderByDescending(x => x.w).ToList();

                var l = codes.Last();
                codes.RemoveAt(codes.Count - 1);
                var r = codes.Last();
                codes.RemoveAt(codes.Count - 1);

                codes.Add(new CodeTreeNode
                {
                    w = l.w + r.w,
                    l = l,
                    r = r
                });
            }

            return codes.First();
        }

        public static Dictionary<char, string> CodeDict(List<CodeTreeNode> list, CodeTreeNode tree) =>
            list.ToDictionary(val => val.ch, val => tree.GetCode(val.ch, string.Empty));

        public static string HuffmanEncode(string text, Dictionary<char, string> codeDict) =>
            string.Join(string.Empty, from c in text select codeDict[c]);
        public static string HuffmanDecode(CodeTreeNode tree, string encode)
        {
            var str = new StringBuilder();
            var node = tree;
            foreach (var c in encode)
            {
                node = c == '0' ? node.l : node.r;
                if (node.ch != default)
                {
                    str.Append(node.ch);
                    node = tree;
                }
            }

            return str.ToString();
        }

        public static Dictionary<char, double> FrecDict(string text)
        {
            var dict = new Dictionary<char, double>();
            foreach (var w in text)
                if (!dict.ContainsKey(w))
                    dict[w] = 1.0;
                else
                    dict[w]++;

            return dict;
        }

        public static void PrintDict(Dictionary<char, int> dict)
        {
            foreach (var (k, v) in dict)
                Console.WriteLine($"{k} - {v}");
        }

        public static Dictionary<char, int> SortByValueDesc(this Dictionary<char, int> dict) =>
            dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    }
}
