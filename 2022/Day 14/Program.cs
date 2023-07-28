using System.Linq;
using  System.Text.RegularExpressions;
using Day_14;


// learnings: we fuck with hashmaps

Part_2();


//sand fall down from 500,0 untill it hit a point of infinate falling
static void Part_1()
{
    var data = File.ReadLines(@"data.txt").ToArray();

    var rockList = RockList(data);
    var sandList = new HashSet<CoolThing>();

    var bottom = rockList.Max(r => r.Y);
    var sandAtBottom = false;
    while (!sandAtBottom)
    {
        sandList = SandList(bottom,rockList, sandList,out sandAtBottom, out var _); 
    }
       Console.Clear();
       print(rockList, sandList);

    Console.WriteLine();
    Console.WriteLine($"Units of Sand: {sandList.Count -1}");
}

//sand falls down from 500,0 untill it hits a point 2 below the bolloow of teh last rock formation in "data"
//sand then builds up until it hit the start point 
static void Part_2()
{
    var data = File.ReadLines(@"data.txt").ToArray();

    var rockList= RockList(data);
    var sandList = new HashSet<CoolThing>();    

    var bottom = rockList.Max(r => r.Y);
    var sandAtTop = false;
    while (!sandAtTop)
    {
       sandList = SandList(bottom,rockList, sandList, out var _, out sandAtTop);
    }
    Console.Clear();
    print(rockList, sandList);

    Console.WriteLine();
    Console.WriteLine($"Units of Sand: {sandList.Count}");
}

//generates the list of Coordinates for rock from the original data
static HashSet<CoolThing> RockList(string[] data)
{
    var rockList = new HashSet<CoolThing>();

    foreach ((string line, int i) in data.Select((x,i)=>(x,i)))
    {
        string pattern = @"(\d+),(\d+)";
        var comands = Regex.Matches(line, pattern);
        for (int j = 1; j < comands.Count; j++)
        {
            var start = comands[j - 1];
            var startx = int.Parse(start.Groups[1].ToString());
            var starty = int.Parse(start.Groups[2].ToString());

            var end = comands[j];
            var endx = int.Parse(end.Groups[1].ToString());
            var endy = int.Parse(end.Groups[2].ToString());

            var xDifference = endx - startx;
            var yDifference = endy - starty;

            var xsign = Math.Sign(xDifference);
            var ysign = Math.Sign(yDifference);

            for (var x = 1; x < xsign * xDifference+1; x++)
            {
                var NewRock = new CoolThing(startx + xsign * x,starty);                
                rockList.Add(NewRock);
            }
            for (var y = 0; y < ysign * yDifference+1; y++)
            {
                var NewRock = new CoolThing(startx, starty+ ysign * y);
                rockList.Add(NewRock);
            }
        }
    }
    return rockList;
}

//create a new sand partical and runs it down the existing sand+rock list untill it can't move any more 
static HashSet<CoolThing> SandList(int bottom, HashSet<CoolThing> rockList, HashSet<CoolThing> sandList,out bool isBottom, out bool isTop)
{    
    isBottom = false;
    isTop = false;
    var sandPosition = new CoolThing(500,0);

    if(rockList.Contains(sandPosition)|| sandList.Contains(sandPosition))    
    {
        isTop = true;
        return sandList;
    }

    while (true)
    {
        if (sandPosition.Y >= bottom+1) 
        { 
            isBottom = true;
            break; 
        }        
        if (!rockList.Contains(new CoolThing(sandPosition.X,sandPosition.Y+1))&&!sandList.Contains(new CoolThing(sandPosition.X, sandPosition.Y + 1)))
        {
            sandPosition.Y++;            
        }
        else if (!rockList.Contains(new CoolThing(sandPosition.X-1, sandPosition.Y + 1))&&!sandList.Contains(new CoolThing(sandPosition.X - 1, sandPosition.Y + 1)))
        {
            sandPosition.Y++;
            sandPosition.X--;
        }
        else if (!rockList.Contains(new CoolThing(sandPosition.X+1, sandPosition.Y + 1))&& !sandList.Contains(new CoolThing(sandPosition.X + 1, sandPosition.Y + 1)))
        {
            sandPosition.Y++;
            sandPosition.X++;
        }
        else 
        { break; }
    }         
    sandList.Add(sandPosition);
    return sandList;
}

//prints the postion of rocks and sand
static void print(HashSet<CoolThing> rockList, HashSet<CoolThing> sandList)
{
    var top = 0;
    var bottom=rockList.Max(r => r.Y)+ 2;
    var left= Math.Min(rockList.Min(r => r.X), sandList.Min(r => r.X));
    var right= Math.Max(rockList.Max(r => r.X), sandList.Max(r => r.X));

    for (int y = top; y < bottom; y++)
    {
        for (int x = left; x <= right; x++)
        {
            var hasMatchRock = rockList.Contains(new CoolThing(x,y));
            var hasMatchSand = sandList.Contains(new CoolThing(x, y));

            if (hasMatchRock||y==bottom) { Console.Write('#'); }
            else if (hasMatchSand) { Console.Write('O'); }
            else if (x == 500 && y == 0) { Console.Write('+'); }
            else { Console.Write('.'); }
        }
        Console.WriteLine();
    }
}