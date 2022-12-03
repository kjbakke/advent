Dictionary<string, int> decode = new Dictionary<string, int>
{
    {"A", 1},
    {"B", 2},
    {"C", 3},
    {"X", 1},
    {"Y", 2},
    {"Z", 3}
};

var input = File.ReadAllText("./pattern2");
var collection = input.Split(
    new string[] { Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries);

var points = 0;
var opponent = 0;
var me = 0;
var loose = new int[4] { 0, 3, 1, 2 };
var win = new int[4] { 0, 2, 3, 1 };
    
foreach (string match in collection)
{
    opponent = decode[match[0].ToString()];
    me = decode[match[2].ToString()];
    points += calculate(opponent, me);
}

int calculate(int opponent, int me)
{
    if (me == 3)
        return win[opponent] + 6;
    if (me == 2)
        return opponent + 3;
    if (me == 1)
        return loose[opponent];
    
    return 0;
}

Console.Write(points);





// 1 3 0
// 2 1 0
// 3 2 0

// 1 2 6
// 2 3 6
// 3 1 6

// 1 1 3
// 2 2 3
// 3 3 3
