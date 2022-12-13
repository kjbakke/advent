using System.Text.Json;

var pattern = File.ReadAllText("pattern").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
    .Select(Packet.FromJson); 

//Chunk(2)

// var inRightOrder = pattern
//     .Select((pair, index) => (pair, index))
//     .Where(p => p.pair.First().CompareTo(p.pair.Last()) == -1)
//     .Select(p => p.index + 1);

//.Sum()

var divider1 = Packet.FromJson("[[2]]");
var divider2 = Packet.FromJson("[[6]]");

var sorted = pattern
    .Append(divider1)
    .Append(divider2)
    .Order()
    .ToList();

Console.WriteLine((sorted.IndexOf(divider1) + 1)* (sorted.IndexOf(divider2) + 1));

abstract record Packet : IComparable
{
    public static Packet FromJson(string json)
        => FromJson((JsonElement)JsonSerializer.Deserialize<object>(json)!);
    public static Packet FromJson(JsonElement element)
        => element.ValueKind == JsonValueKind.Array
            ? new List(element.EnumerateArray().Select(FromJson).ToArray())
            : new Number(element.GetInt32());
    public int CompareTo(object? other)
    {
        var p1 = this;
        var p2 = other as Packet ?? throw new Exception("Not a packet");
        
        if (p1 is Number n1 && p2 is Number n2)
            return n1.Value.CompareTo(n2.Value);
        
        List l1 = p1 is Number n ? new List(new[] { n }) : (List)p1;
        List l2 = p2 is Number m ? new List(new[] { m }) : (List)p2;
        
        for (int i = 0;; i++)
        {
            var length1 = l1.Values.Length;
            var length2 = l2.Values.Length;
            
            if (length1 == length2 && length1 == i)
                return 0;

            if (i < length1 && i < length2)
            {
                var result = l1.Values[i].CompareTo(l2.Values[i]);
                if (result != 0)
                    return result;
                
                continue;
            }
            
            return length1.CompareTo(length2);
        }
    }
}
record Number(int Value) : Packet;
record List(Packet[] Values) : Packet;