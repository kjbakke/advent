var collection = File.ReadAllText("./pattern1").Split(
    new string[] { Environment.NewLine },
    StringSplitOptions.None);

Dictionary<int, int> elves = new Dictionary<int, int>();
int x = 1;
elves.Add(1, 0);
foreach (var bit in collection)
{
    if (bit.Equals(""))
    {
        x++;
        elves.Add(x, 0);
    }
    else
        elves[x] += Int32.Parse(bit);
}

x = 0;
int value = 0;
foreach (var elf in elves.OrderByDescending(y => y.Value))
{
    x++;
    value += elf.Value;
    if (x == 3)
        break;
}

