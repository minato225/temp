namespace Huffman
{
    public class CodeTreeNode : IComparable<CodeTreeNode>
    {
        public char ch { get; set; }
        public double w { get; set; }
        public CodeTreeNode l { get; set; }
        public CodeTreeNode r { get; set; }

        public int CompareTo(CodeTreeNode? other) => (int)(other?.w ?? 0 - w);

        public string GetCode(char c, string parentCode)
        {
            if (ch == c)
                return parentCode;
            else
            {
                if (l is not null)
                {
                    var code = l.GetCode(c, $"{parentCode}0");
                    if (code is not null)
                        return code;
                }
                if (r is not null)
                {
                    var code = r.GetCode(c, $"{parentCode}1");
                    if (code is not null)
                        return code;
                }
            }

            return null;
        }

        public override string? ToString() => $"[{ch}-{w}]";
    }
}
