var pattern = System.IO.File.ReadAllLines("pattern");
List<List<string>> monkeyStrings = new List<List<string>>();
List<Monkey> monkeys = new List<Monkey>();
var counter = 0;
var monkeyCounter = 0;
foreach (var line in pattern)
{
    if(line.Equals(""))
        continue;
    
    if(counter == 0)
        monkeyStrings.Add(new List<string>());
    
    monkeyStrings[monkeyCounter].Add(line);
    counter++;
    if (counter == 6)
    {
        counter = 0;
        monkeyCounter++;
    }
}

foreach (var monkey in monkeyStrings)
{
    var id = Int32.Parse((monkey[0].Substring(7, 1)));
    
    var tmpItems = monkey[1]
        .Replace("Starting items: ", "")
        .Split(new[] { "," }, StringSplitOptions.TrimEntries);
    List<long> items = new List<long>();
    foreach (var item in tmpItems)
    {
        items.Add(Int32.Parse(item));
    }

    var operation = monkey[2].Replace("  Operation: new = old ", "");
    
    var divisible = Int32.Parse(monkey[3].Replace("  Test: divisible by ", ""));
    var ifTrue = Int32.Parse(monkey[4].Replace("    If true: throw to monkey ", ""));
    var ifFalse = Int32.Parse(monkey[5].Replace("    If false: throw to monkey ", ""));

    var inspectedItems = 0;

    monkeys.Add(
        new Monkey(id, 
            items,
            operation,
            new Test(divisible, ifTrue, ifFalse),
            inspectedItems));
}
var modulo = monkeys.Select(m => m.Test.Divisible).Aggregate((m,i) => m*i);
var rounds = 10000;
var mostInspectedItems = new List<Tuple<long, long>>(){ new(0, 0), new(0, 0)};

for (int i = 0; i < rounds; i++)
{
    foreach (var monkey in monkeys)
    {
        foreach (var item in monkey.Items)
        {
            long operationItem = parseOperation(monkey.Operation, item);
            //operationItem /= 3;
            operationItem %= modulo;
            if (testItem(operationItem, monkey.Test.Divisible))
                monkeys[monkey.Test.True].Items.Add(operationItem);
            else
            {
                monkeys[monkey.Test.False].Items.Add(operationItem);
            }

            monkey.InspectedItems++;
            if (mostInspectedItems[0].Item2 < monkey.InspectedItems)
            {
                if(monkey.Id != mostInspectedItems[0].Item1)
                    mostInspectedItems[1] = mostInspectedItems[0];
                mostInspectedItems[0] = new Tuple<long, long>(monkey.Id, monkey.InspectedItems);
            }
            else if (mostInspectedItems[1].Item2 < monkey.InspectedItems)
            {
                mostInspectedItems[1] = new Tuple<long, long>(monkey.Id, monkey.InspectedItems);
            }
        }

        monkey.Items.Clear();
    }
}

Console.WriteLine(mostInspectedItems[0].Item2 *mostInspectedItems[1].Item2);

bool testItem(long item, int divisible)
{
    if (item % divisible == 0)
        return true;
    return false;
}

long parseOperation(string operation, long old)
{
    var operations = operation.Split(new[] { ' ' });
    long lastPart = 0;
    if (operations[1].Equals("old"))
        lastPart = old;
    else
    {
        lastPart = Int32.Parse(operations[1]);
    }

    if (operations[0] == "*")
        old *= lastPart;
    if (operations[0] == "+")
        old += lastPart;
    if (operations[0] == "-")
        old -= lastPart;
    
    return old;
}

record Monkey (
    int Id,
    List<long> Items,
    string Operation,
    Test Test,
    int InspectedItems)
{
    public int InspectedItems { get; set; } = InspectedItems;
}

record Test(
    int Divisible,
    int True,
    int False);