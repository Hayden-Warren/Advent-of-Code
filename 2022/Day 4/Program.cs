

string[] data = File.ReadAllLines(@"data.txt");
var count = 0;

foreach (var line in data)
{
    string[] pair = line.Split(',');
    var startElf1 = Int32.Parse(pair[0].Split('-')[0]);
    var endinElf1 = Int32.Parse(pair[0].Split('-')[1]);
    var startElf2 = Int32.Parse(pair[1].Split('-')[0]);
    var endinElf2 = Int32.Parse(pair[1].Split('-')[1]);

    if(startElf1>=startElf2&& startElf1<=endinElf2 || endinElf1 >= startElf2 && endinElf1 <= endinElf2 || startElf2 >= startElf1 && startElf2 <= endinElf1 || endinElf2 >= startElf1 && endinElf2 <= endinElf1)
    {
        count++;
    }
}
Console.WriteLine(count);