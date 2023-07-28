using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;


Part_1();

Part_2();

void Part_1()
{
    var globalwatch = Stopwatch.StartNew();
    var pattern = @"\d+";
    var data=File.ReadAllText("data.txt");
    var matchData = Regex.Matches(data, pattern);

    var bluePrints= new List<BluePrint>();
    for(int i = 0; i < matchData.Count; i=i+7)
    {
        var number = int.Parse(matchData[i].Value.ToString());
        var oreOre = int.Parse(matchData[i+1].Value.ToString());
        var clayOre = int.Parse(matchData[i+2].Value.ToString());
        var obOre = int.Parse(matchData[i+3].Value.ToString());
        var obClay = int.Parse(matchData[i + 4].Value.ToString());
        var geoOre = int.Parse(matchData[i+5].Value.ToString());
        var geoOb = int.Parse(matchData[i+6].Value.ToString());

        var newbluePrint = new BluePrint(number, oreOre, clayOre, obOre, obClay, geoOre, geoOb);

        bluePrints.Add(newbluePrint);
    }





    var geodeList = new List< int>();
    var blankList= new List<string>();
    Console.WriteLine($"Part 1:");
    for (int i = 1; i <= bluePrints.Count; i++)
    {
        var watch = Stopwatch.StartNew();
        var bluePrint = bluePrints[i-1];
        var maxOreRobots = new[] { bluePrint.oreOre, bluePrint.clayOre, bluePrint.obOre, bluePrint.geoOre }.Max();
        var geodeCount = GeodeCount(bluePrint,24 ,new Resources(0,0,0,0),new Robots(1,0,0,0),maxOreRobots);
        
        geodeList.Add(geodeCount);
        Console.WriteLine($"BluePrint:{i.ToString("D2")}  Geodes:{geodeList[i - 1].ToString("D2")}  Quality:{(geodeList[i - 1] * i).ToString("D3")} [{watch.ElapsedMilliseconds.ToString("D3")} ms]");
    }
    var sum = 0;

    for(int i = 1; i <= geodeList.Count; i++) { sum = sum + i * geodeList[i-1]; }

    Console.WriteLine($"Sum: {sum} [{globalwatch.ElapsedMilliseconds/1000} s]");
    Console.WriteLine();


}



void Part_2()
{
    var globalwatch = Stopwatch.StartNew();
    var pattern = @"\d+";
    var data = File.ReadAllText("data.txt");
    var matchData = Regex.Matches(data, pattern);

    var bluePrints = new List<BluePrint>();
    for (int i = 0; i < matchData.Count; i = i + 7)
    {
        var number = int.Parse(matchData[i].Value.ToString());
        var oreOre = int.Parse(matchData[i + 1].Value.ToString());
        var clayOre = int.Parse(matchData[i + 2].Value.ToString());
        var obOre = int.Parse(matchData[i + 3].Value.ToString());
        var obClay = int.Parse(matchData[i + 4].Value.ToString());
        var geoOre = int.Parse(matchData[i + 5].Value.ToString());
        var geoOb = int.Parse(matchData[i + 6].Value.ToString());

        var newbluePrint = new BluePrint(number, oreOre, clayOre, obOre, obClay, geoOre, geoOb);

        bluePrints.Add(newbluePrint);
    }

   
    var geodeList = new List<int>();
    var blankList = new List<string>();




    var total = 1;
    Console.WriteLine($"Part 2:");
    for (int i = 0; i < 3; i++)
    {
        var watch = Stopwatch.StartNew();
        var bluePrint = bluePrints[i];
        var maxOreRobots = new[] {bluePrint.clayOre, bluePrint.obOre, bluePrint.geoOre }.Max();
        var geodeCount = GeodeCount_2(bluePrint, 32, new Resources(0, 0, 0, 0), new Robots(1, 0, 0, 0), maxOreRobots,0);
        Console.WriteLine($"Blue Print {i+1}: {geodeCount} [{watch.ElapsedMilliseconds/1000} s]");
    
        geodeList.Add(geodeCount);
       
        total=total*geodeCount;
    }
    Console.WriteLine($"Product: {total} [{globalwatch.ElapsedMilliseconds/1000} s]");


}






static int GeodeCount(BluePrint bluePrint, int minuets, Resources resources, Robots robots, int maxOreRobots)
{

    var maxGeodes = 0;


    //if (resources.ore >  4 * maxOreRobots || resources.clay > 4 * bluePrint.obClay || minuets == 0) { return resources.geode; }
    if (minuets == 0) { return (resources.geode); }

    //make geode robot
    if (resources.ore >= bluePrint.geoOre && resources.obsidian >= bluePrint.geoOb)
    {
        var newresources = new Resources(resources.ore - bluePrint.geoOre + robots.ore, resources.clay + robots.clay, resources.obsidian - bluePrint.geoOb + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore, robots.clay, robots.obsidian, robots.geode + 1);

        var createGeodeRobot = GeodeCount(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots);
        if (createGeodeRobot > maxGeodes) { maxGeodes = createGeodeRobot; }
    }

    //make obsidian robot
    else if (resources.ore >= bluePrint.obOre && resources.clay >= bluePrint.obClay && robots.obsidian < bluePrint.geoOb)
    {
        var newresources = new Resources(resources.ore - bluePrint.obOre + robots.ore, resources.clay - bluePrint.obClay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore, robots.clay, robots.obsidian + 1, robots.geode);

        var createObsidianRobot = GeodeCount(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots);
        if (createObsidianRobot > maxGeodes) { maxGeodes = createObsidianRobot; }
    }

    else
    {
        //make clay robot
        if (resources.ore >= bluePrint.clayOre && robots.clay < bluePrint.obClay)
        {
            var newresources = new Resources(resources.ore - bluePrint.clayOre + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
            var newRobosts = new Robots(robots.ore, robots.clay + 1, robots.obsidian, robots.geode);

            var createClayRobot = GeodeCount(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots);
            if (createClayRobot > maxGeodes) { maxGeodes = createClayRobot; }
        }

        // make ore robot
        if (resources.ore >= bluePrint.oreOre && robots.ore < maxOreRobots)
        {
            var newresources = new Resources(resources.ore - bluePrint.oreOre + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
            var newRobosts = new Robots(robots.ore + 1, robots.clay, robots.obsidian, robots.geode);

            var createOreRobot = GeodeCount(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots);
            if (createOreRobot > maxGeodes) { maxGeodes = createOreRobot; }
        }

        // make no robot
        if (true)
        {
            var newresources = new Resources(resources.ore + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);

            var createNoRobot = GeodeCount(bluePrint, minuets - 1, newresources, robots, maxOreRobots);
            if (createNoRobot > maxGeodes) { maxGeodes = createNoRobot; }
        }
    }

    return (maxGeodes);
}


static int GeodeCount_2(BluePrint bluePrint, int minuets, Resources resources, Robots robots, int maxOreRobots,int maxGeodes)
{
    
    

    var dict = MaxFromRemaining.test()[1];

    if ( minuets == 0 ) { return resources.geode; }

    if (minuets < 9 && resources.geode + robots.geode * minuets + MaxFromRemaining.test()[minuets] < maxGeodes) 
    { 
        return resources.geode; 
    }



    if (      
      minuets <= 9 && robots.geode == 0 ||
      minuets <= 15 && robots.obsidian == 0 ||
      minuets <= 27 && robots.clay + robots.ore == 0
      )
    { return ( resources.geode); }

    //make geode robot
    if (resources.ore >= bluePrint.geoOre && resources.obsidian >= bluePrint.geoOb)
    {
        var newresources = new Resources(resources.ore - bluePrint.geoOre + robots.ore, resources.clay + robots.clay, resources.obsidian - bluePrint.geoOb + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore, robots.clay, robots.obsidian, robots.geode + 1);

        var createGeodeRobot = GeodeCount_2(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, maxGeodes);
        maxGeodes = createGeodeRobot;
    }
    
    else
    {
       //make obsidian robot
        if (resources.ore >= bluePrint.obOre && resources.clay  >= bluePrint.obClay && robots.obsidian < bluePrint.geoOb)
        {
            var newresources = new Resources(resources.ore - bluePrint.obOre + robots.ore, resources.clay - bluePrint.obClay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
            var newRobosts = new Robots(robots.ore, robots.clay, robots.obsidian + 1, robots.geode);

            var createObsidianRobot = GeodeCount_2(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, maxGeodes);
            if (createObsidianRobot > maxGeodes) { maxGeodes = createObsidianRobot; }
        }

        //make clay robot
        if (resources.ore >= bluePrint.clayOre && robots.clay < bluePrint.obClay)
        {
            var newresources = new Resources(resources.ore - bluePrint.clayOre + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
            var newRobosts = new Robots(robots.ore, robots.clay + 1, robots.obsidian, robots.geode);

            var createClayRobot = GeodeCount_2(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, maxGeodes);
            if (createClayRobot > maxGeodes) { maxGeodes = createClayRobot; }
        }

        // make ore robot
        if (resources.ore >= bluePrint.oreOre && robots.ore < maxOreRobots)
        {
            var newresources = new Resources(resources.ore - bluePrint.oreOre + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
            var newRobosts = new Robots(robots.ore + 1, robots.clay, robots.obsidian, robots.geode);

            var createOreRobot = GeodeCount_2(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, maxGeodes);
            if (createOreRobot > maxGeodes) { maxGeodes = createOreRobot; }
        }

        // make no robot
        if (true)
        {
            var newresources = new Resources(resources.ore + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);      

            var createNoRobot = GeodeCount_2(bluePrint, minuets - 1, newresources, robots, maxOreRobots,maxGeodes);
            if (createNoRobot > maxGeodes) { maxGeodes = createNoRobot;}
        }
    }

    return (maxGeodes);
}

static (List<string>, int) GeodeCount_WithList(BluePrint bluePrint, int minuets, Resources resources, Robots robots, int maxOreRobots, List<string> path)
{

    var maxGeodes = 0;
    var maxPath = new List<string>(path);

    
    if (minuets == 0||
        minuets==10&&robots.geode==0||
        minuets==15&&robots.obsidian==0||
        resources.ore>3*maxOreRobots||
        resources.clay > 7 * bluePrint.obClay ||
        minuets ==28&&robots.clay+ robots.ore == 0
        ) 
    { return (maxPath, resources.geode); }

    //make geode robot
    if (resources.ore >= bluePrint.geoOre && resources.obsidian >= bluePrint.geoOb)
    {
        var newresources = new Resources(resources.ore - bluePrint.geoOre + robots.ore, resources.clay + robots.clay, resources.obsidian - bluePrint.geoOb + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore, robots.clay, robots.obsidian, robots.geode + 1);

        var newPath = new List<string>(path); newPath.Add("Geode");

        var createGeodeRobot = GeodeCount_WithList(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, newPath);
        if (createGeodeRobot.Item2 > maxGeodes) { maxGeodes = createGeodeRobot.Item2; maxPath = createGeodeRobot.Item1; }
    }
     
    //make obsidian robot
    if (resources.ore >= bluePrint.obOre && resources.clay >= bluePrint.obClay && robots.obsidian < bluePrint.geoOb)
    {
        var newresources = new Resources(resources.ore - bluePrint.obOre + robots.ore, resources.clay - bluePrint.obClay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore, robots.clay, robots.obsidian + 1, robots.geode);

        var newPath = new List<string>(path); newPath.Add("Obsidian");

        var createObsidianRobot = GeodeCount_WithList(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, newPath);
        if (createObsidianRobot.Item2 > maxGeodes) { maxGeodes = createObsidianRobot.Item2; maxPath = createObsidianRobot.Item1; }
    }

    //make clay robot
    if (resources.ore >= bluePrint.clayOre && robots.clay < bluePrint.obClay)
    {
        var newresources = new Resources(resources.ore - bluePrint.clayOre + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore, robots.clay + 1, robots.obsidian, robots.geode);

        var newPath = new List<string>(path); newPath.Add("Clay");

        var createClayRobot = GeodeCount_WithList(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, newPath);
        if (createClayRobot.Item2 > maxGeodes) { maxGeodes = createClayRobot.Item2; maxPath = createClayRobot.Item1; }
    }

    // make ore robot
    if (resources.ore >= bluePrint.oreOre && robots.ore < maxOreRobots)
    {
        var newresources = new Resources(resources.ore - bluePrint.oreOre + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);
        var newRobosts = new Robots(robots.ore + 1, robots.clay, robots.obsidian, robots.geode);

        var newPath = new List<string>(path); newPath.Add("Ore");

        var createOreRobot = GeodeCount_WithList(bluePrint, minuets - 1, newresources, newRobosts, maxOreRobots, newPath);
        if (createOreRobot.Item2 > maxGeodes) { maxGeodes = createOreRobot.Item2; maxPath = createOreRobot.Item1; }
    }

    // make no robot
    if (true)
    {
        var newresources = new Resources(resources.ore + robots.ore, resources.clay + robots.clay, resources.obsidian + robots.obsidian, resources.geode + robots.geode);

        var newPath = new List<string>(path); newPath.Add("NoRobot");

        var createNoRobot = GeodeCount_WithList(bluePrint, minuets - 1, newresources, robots, maxOreRobots, newPath);
        if (createNoRobot.Item2 > maxGeodes) { maxGeodes = createNoRobot.Item2; maxPath = createNoRobot.Item1; }
    }
    

    return (maxPath, maxGeodes);
}


public static class MaxFromRemaining
{
    public static Dictionary<int, int> test()
    {
        Dictionary<int, int> newDict = new Dictionary<int, int>()
        {
            [1] = 0,
            [2] = 1,
            [3] = 3,
            [4] = 6,
            [5] = 10,
            [6] = 15,
            [7] = 21,
            [8] = 28,
            [9] = 36,
            [10] = 45,
            [11] = 55,
            [12] = 66,
            [13] = 78,
            [14] = 91,
            [15] = 106,
            [16] = 121,
        };
        return newDict;
        
    }
}





public record struct BluePrint ( int number, int oreOre, int clayOre, int obOre, int obClay, int geoOre, int geoOb );

public record struct Resources(int ore, int clay,int obsidian, int geode);

public record struct Robots(int ore, int clay, int obsidian, int geode);
























