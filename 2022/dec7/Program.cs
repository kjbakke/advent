var pattern = File.ReadAllText("pattern").Replace("$ ls", "").Split(
    new string[] { Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries);;



foreach (var line in pattern) 
{

}
Console.WriteLine("Hello, World!");


var root = new Folder("/", 0, new (), new());

void ActionLine(string line, Folder folder)
{
    if (line.StartsWith("$ cd") && !line.Contains(".."))
    {
        folder.Folders.Add(new Folder(line.Replace("$ cd ", ""), 0, new (), new()));
    }
}

class Entity
 {
     public string Name { get; set; }
     public int Size { get; set; }
     
     public Entity (string name, int size)
     {
         this.Name = name;
         this.Size = size;
     }
 }

 class Folder : Entity
 {
     public List<Fil> Files { get; set; }
     public List<Folder> Folders { get; set; }

     public Folder(string name, int size, List<Fil> files, List<Folder> folders) : base(name, size)
     {
         Files = files;
         Folders = folders;
     }
 }

 class Fil : Entity
 {
     public Fil(string name, int size) : base(name, size)
     {
     }
 }