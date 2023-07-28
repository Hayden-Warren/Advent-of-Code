
using System.Diagnostics;

part_1();

part_2();

void part_1()
{
    var watch = new Stopwatch();
    watch.Start();

    var rawData = File.ReadAllLines("data.txt");

    foreach (var line in rawData)
    {
        var monkey = new Monkey();

        monkey.name = line.Substring(0, 4);

        var Parsable = int.TryParse(line.Substring(6), out int value);

        if (Parsable) { monkey.value = value; }
        else
        {
            monkey.int1 = line.Substring(6, 4);
            monkey.op = line[11];
            monkey.int2 = line.Substring(13, 4);
        }
        Global.Dictionary.Add(monkey.name, monkey);
    }
    var result = Dive("root");
    Console.WriteLine($"Part 1 Answer: {result} [{watch.ElapsedMilliseconds} ms]");
}

void part_2()
{
    var watch = new Stopwatch();
    watch.Start();

    var newList = new List<(string currentMonkey,char op, long value, string intside)>();
    var humnSide = Climb("humn", newList);
    var diveMonkey = "";

    if (Global.Dictionary["root"].int1==humnSide.Last().currentMonkey)
    {
        diveMonkey=Global.Dictionary["root"].int2;
    }
    else { diveMonkey=Global.Dictionary["root"].int1; }

    var otherSide = Dive(diveMonkey);
    humnSide.Reverse();

    var result = Result(humnSide, otherSide);
    
    Console.WriteLine($"Part 2 Answer: {result} [{watch.ElapsedMilliseconds} ms]");
}


long Result (List<(string currentMonkey, char op, long value, string intside)> humnSide, long otherSide)
{
    foreach (var operation in humnSide)
    {
        switch (operation.op)
        {
            case '+':
                otherSide = otherSide - operation.value;
                break;

            case '-':
                if (operation.intside == "left")
                {
                    otherSide = -(otherSide - operation.value);
                }
                else if (operation.intside == "right")
                {
                    otherSide = otherSide + operation.value;
                }
                break;

            case '/':
                if (operation.intside == "left")
                {
                    otherSide = operation.value / otherSide;
                }
                else if (operation.intside == "right")
                {
                    otherSide = otherSide * operation.value;
                }
                break;

            case '*':
                otherSide = otherSide / operation.value;
                break;
        }
    }
    return otherSide;
}



List<(string currentMonkey,char op,long value, string intside)> Climb(string monkeyName, List<(string currentMonkey,char op, long value, string intside)> currentList)
{   
    var monkeyKey = Global.Dictionary.FirstOrDefault(x => x.Value.int1 == monkeyName || x.Value.int2 == monkeyName).Key;

    var next = Global.Dictionary[monkeyKey];
    if (monkeyKey == "root") { return currentList; }
    var left = next.int1;
    var right = next.int2;
    var op = (char)next.op;

    if(left==monkeyName)
    {
        currentList.Add((monkeyKey, op, Dive(right),"right"));
    }
    else if (right == monkeyName)
    {
        currentList.Add((monkeyKey, op, Dive(left),"left"));
    }

    return Climb(monkeyKey,currentList);
}



long Dive(string monkeyName)
{
    var monkey = Global.Dictionary[monkeyName];
    if (monkey.value == null)
    {
        long int1 = Dive(monkey.int1);
        long int2 = Dive(monkey.int2);
        var op = monkey.op;

        switch (op)
        {
            case '+': return int1 + int2;

            case '-': return int1 - int2;

            case '/': return int1 / int2;

            case '*': return int1 * int2;
        }
        return 0;
    }

    else
    {
        return (long)monkey.value;
    }
}

public class Global
{
    public static Dictionary<string, Monkey> Dictionary = new Dictionary<string, Monkey> { };
}

public struct Monkey { public string name; public int? value; public string? int1; public char? op; public string? int2; }











