var pattern = File.ReadAllText("pattern5").Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(
    new string[] { Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries);

var stacks = new Dictionary<int, List<char>>()
{
    { 1, new List<char>() { 'N', 'W', 'F', 'R', 'Z', 'S', 'M', 'D' } }
};
var counter = 0;
foreach (var part in pattern)
{
    var pair = part.Split(new [] { "-", "," }, StringSplitOptions.None);
    int[] id = new int[4] { Int32.Parse(pair[0]), Int32.Parse(pair[1]), Int32.Parse(pair[2]), Int32.Parse(pair[3])};
    
    if (id[0] <= id[2] && id[1] >= id[2])
        counter++;
    else if (id[2] <= id[0] && id[3] >= id[0])
        counter++;
}


Console.WriteLine(counter);

