var pattern = File.ReadAllLines("pattern");
var x = 1;
var cycle = 0;
var signal = 0;
var screen = "";

foreach (var line in pattern)
{
    if (line.Equals("noop")) doCycle(1);

    if (line.StartsWith("addx"))
    {
        doCycle(2);
        x += int.Parse(line.Split(' ', 2)[1]);
    }
}

void doCycle(int n)
{
    for (var i = 0; i < n; i++)
    {
        addPixel();
        cycle++;
        if (cycle == 20 || (cycle < 221 && (cycle - 20) % 40 == 0))
            signal += cycle * x;
    }
}

void addPixel()
{
    var pixel = cycle % 40;
    if (cycle % 40 == 0)
        screen += Environment.NewLine;
    if (pixel == x - 1 || pixel == x || pixel == x + 1)
        screen += '#';
    else
        screen += '.';
}

Console.WriteLine(screen);