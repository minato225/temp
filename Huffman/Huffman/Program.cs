using Huffman;

//var text = File.ReadAllText("text.txt");
var text = "where there's a will there's a way";
var frecDict = HuffmanHelper.FrecDict(text);
var list = (from x in frecDict
            select new CodeTreeNode
            {
                ch = x.Key,
                w = x.Value
            }).ToList();

var tree = HuffmanHelper.Huffman(list);
var codeDict = HuffmanHelper.CodeDict(list, tree);
var encoded = HuffmanHelper.HuffmanEncode(text, codeDict);
var decoded = HuffmanHelper.HuffmanDecode(tree, encoded);
var average = list.Average(c => frecDict[c.ch] * codeDict[c.ch].Length);
var entropy = -(from x in frecDict select x.Value / text.Length).Sum(x => x * Math.Log2(x));

Console.WriteLine("Frec List");
Console.WriteLine(string.Join('\n', list));

Console.WriteLine("Code Page");
Console.WriteLine(string.Join('\n', codeDict));

Console.WriteLine($"Source memory = {text.Length * 8} bit");
Console.WriteLine($"Zip memory = {encoded.Length} bit");
Console.WriteLine($"Compression = {text.Length * 8.0 / encoded.Length} times");

Console.WriteLine($"text = {text}");
Console.WriteLine($"encoded = {encoded}");
Console.WriteLine($"decoded = {decoded}");
Console.WriteLine($"Avarage Length = {average}");
Console.WriteLine($"Entropy = {entropy}");