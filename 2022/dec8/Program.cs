var pattern = File.ReadAllText("pattern").Split(
    new[] { Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries);;

var matrix = new List<List<int>>();
foreach (var line in pattern)
{
    var matrixLine = new List<int>();
    foreach (var tree in line)
    {
        matrixLine.Add(Int32.Parse(tree.ToString()));
    }
    matrix.Add(matrixLine);
}

var score = 0;
var checkScore = 0;

for (int i = 0; i < matrix.Count; i++)
{
    for (int j = 0; j < matrix[i].Count; j++)
    {
        checkScore = visible(matrix, i, j);
        if (checkScore > score)
        {
            score = checkScore;
        }

    }
}

Console.WriteLine(score);

int visible(List<List<int>> grid, int y, int x)
{
    var current = grid[y][x];
    
    var west = grid[y].GetRange(0, x);
    west.Reverse();
    var east = grid[y].GetRange(x + 1, grid[y].Count - x - 1);
    var north = new List<int>();
    grid.GetRange(0, y).ForEach(l => north.Add(l[x]));
    north.Reverse();
    var south = new List<int>();
    grid.GetRange(y + 1, grid.Count - y - 1).ForEach(l => south.Add(l[x]));

    return checkPart(current, west) * checkPart(current, east) * checkPart(current, north) *
           checkPart(current, south);

}

int checkPart(int current, List<int> part)
{
    var tmpScore = 0;
    foreach (var tree in part)
    {
        if (tree >= current)
        {
            tmpScore++;
            return tmpScore;
        }
        tmpScore++;
    }

    return tmpScore;
}
