using System.Runtime.InteropServices.ComTypes;

var stackPattern = 
    File.ReadAllText("pattern5stacks")
        .Replace("[", "")
        .Replace("]", "")
        .Replace("    ", "")
        .Replace("     ", "")
        .Split(                          
            new string[] { Environment.NewLine }, 
            StringSplitOptions.RemoveEmptyEntries
        );

var movePattern = 
    File.ReadAllText("pattern5")
        .Replace("move ", "")
        .Replace("from ", "")
        .Replace("to ", "")
        .Split(                          
            new string[] { Environment.NewLine }, 
            StringSplitOptions.RemoveEmptyEntries
        );

var moveOrders = new List<List<int>>();
foreach (var level in movePattern)
{
    var moveString = level.Split(
        new string[] { " " },
        StringSplitOptions.None);
    var numList = new List<int>();
    foreach (var num in moveString)
    {
        numList.Add(Int32.Parse(num));
    }
    moveOrders.Add(numList);
}

var stacks = new List<string>()
{    
    "",
    "NWFRZSMD",
    "SGQPW",
    "CJNFQVRW",
    "LDGCPZF",
    "SPT",
    "LRWFDH",
    "CDNZ",
    "QJSVFRNW",
    "VWZGSMR"
};

// var stacks = new List<string>()
// {     
//     "",
//     "NZ",
//     "DCM",
//     "P",
//
// };
foreach (var order in moveOrders)
{
    var ammount = order[0];
    var from = order[1];
    var to = order[2];
    stacks[to] = stacks[from].Substring(0, ammount) + stacks[to];
    stacks[from] = stacks[from].Substring(ammount, stacks[from].Length - ammount);
}

var answer = "";

foreach(var stack in stacks)
{
    if(!String.IsNullOrEmpty(stack))
        answer += stack[0];
}

Console.WriteLine(answer);