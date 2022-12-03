var backpacks = File.ReadAllText("pattern3").Split(
    new string[] { Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries);
var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

var prio = new Dictionary<char, int>();
int value = 1;

foreach (var letter in letters)
{
    prio.Add(Char.ToLower(letter), value);
    prio.Add(letter, value + 26);
    value++;
}

value = 0;
var group = new List<string>();

foreach (var backpack in backpacks)
{
    group.Add(backpack);
    if (group.Count == 3)
    {
        value += prio[group[0].FirstOrDefault(_ => group[1].Contains(_) && group[2].Contains(_))];
        group.Clear();
    }
}

Console.Write(value);


