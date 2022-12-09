var pattern = File.ReadAllText("pattern6");
//pattern = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
var code = "";
var count = 0;

foreach (var bit in pattern)
{
    foreach (var part in code)
    {
        if (part.Equals(bit))
        {
            if (code.Length > 1)
                code = code.Substring(code.IndexOf(part) + 1, code.Length - (code.IndexOf(part) + 1));
            else
                code = "";
            break;
        }
    }
    
    code += bit;
    count++;
    if (code.Length == 14)
    {
        Console.WriteLine(count);
        break;
    }

}
