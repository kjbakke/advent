var pattern = System.IO.File.ReadAllLines("pattern");

var root = new Dir("/", null, new Dictionary<string, SubDir>());
parseLines(root, pattern);

var sizes = new Dictionary<Dir, long>();
calcSizes(root, sizes);

Console.WriteLine(sizes.Values.Where(size => size <= 100_000).Sum());
Console.WriteLine(sizes.Values
    .Where(size => size >= sizes[root] - 40_000_000)
    .Min());

void parseLines(Dir root, string[] input)
{
    var cd = root;
    foreach (var line in input)
    {
        if (line.StartsWith(@"$ cd /"))
        {
            cd = root;
        }
        else if (line.StartsWith(@"$ cd .."))
        {
            cd = cd.ParentDir;
        }
        else if (line.StartsWith(@"$ cd "))
        {
            var name = line.Split(" ").Last().Trim();
            cd = (Dir)cd.SubDirs[name];
        }
        else if (line.StartsWith("dir"))
        {
            var name = line.Split(" ").Last().Trim();
            cd.SubDirs[name] = new Dir(name, cd, new Dictionary<string, SubDir>());
        }
        else if (Char.IsNumber(line[0]))
        {
            var split = line.Split(" ");
            var size = long.Parse(split.First().Trim());
            var name = split.Last().Trim();
            cd.SubDirs[name] = new File(name, size);
        }
    }
}


void calcSizes(Dir dir, Dictionary<Dir, long> sizes)
{
    long size = 0;
    foreach (var child in dir.SubDirs)
    {
        if (child.Value is File f)
        {
            size += f.Size;
        }
        else if (child.Value is Dir d)
        {
            calcSizes(d, sizes);
            size += sizes[d];
        }
    }
    sizes[dir] = size;
}

record SubDir(string Name);
record File(string Name, long Size) : SubDir(Name);
record Dir(
    string Name, 
    Dir ParentDir, 
    Dictionary<string, SubDir> SubDirs) : SubDir(Name);
    
    