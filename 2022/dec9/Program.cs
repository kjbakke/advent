using System.Drawing;

var moves = File.ReadLines("pattern")
    .Select(l => l.Split(' ', 2))
    .Select(ParseMovement)
    .ToList();

var tailVisited = new List<Point>() { new(0, 0) };
var rope = new List<Point>();
for (int i = 0; i < 10; i++)
    rope.Add(new(0, 0));

Dictionary<string, int>  direction = new Dictionary<string, int> 
{
    {"U", 1},
    {"D", -1},
    {"L", -1},
    {"R", 1},
};

foreach (var mov in moves)
{
    for (int i = 0; i < mov.Length; i++)
    {
        rope[0] = MoveHead(mov, rope[0]);
        for (int j = 1; j < rope.Count; j++)
        {
            rope[j] = MoveTail(rope[j-1], rope[j]);
        }

        if(!tailVisited.Contains(rope[rope.Count - 1]))
            tailVisited.Add(rope[rope.Count - 1]); 
    }
    
}

Console.WriteLine(tailVisited.Count);


Point MoveHead(Movement mv, Point tmpHead)
{
    if(mv.Direction.Equals("U") || mv.Direction.Equals("D"))
        tmpHead.Y += direction[mv.Direction];
    if(mv.Direction.Equals("L") || mv.Direction.Equals("R"))
        tmpHead.X += direction[mv.Direction];
    
    return tmpHead;
}

Point MoveTail(Point tmpHead, Point tmpTail)
{
    if (!(Math.Max(Math.Abs(tmpHead.X - tmpTail.X), Math.Abs(tmpHead.Y - tmpTail.Y)) > 1))
        return tmpTail;
    if (tmpHead.Y > tmpTail.Y) 
        tmpTail.Y++;
    if (tmpHead.Y < tmpTail.Y) 
        tmpTail.Y--;
    if (tmpHead.X > tmpTail.X) 
        tmpTail.X++;
    if (tmpHead.X < tmpTail.X) 
        tmpTail.X--;

    return tmpTail;
}

Movement ParseMovement(string[] movement)
{
    return new Movement(movement[0], Int32.Parse(movement[1]));
}

record Movement (string Direction, int Length);