using System.Linq;
using System.Text.RegularExpressions;


Part_2();

//original Part 1
static void Part_1()
{
    var data = File.ReadAllText("data.txt");
 
    var pattern = @"Valve\s(\w+)\s+.+rate+.(\d+)+.+valves?\s(.+)\s";
    var matches = Regex.Matches(data,pattern);

    var map = new Dictionary<string, Valve>();

    // create the valves
    foreach (Match match in matches)
    {
        var name = match.Groups[1].ToString();
        var flowRate= int.Parse(match.Groups[2].ToString());
        var Valve = new Valve(name, flowRate, new Dictionary<Valve, int>());

        map.Add(name, Valve);
    }

    //populates the valves with flow rate and conection details
    foreach (Match match in matches)
    {    
        var name=match.Groups[1].ToString();
        
        var conectionsStrings = match.Groups[3].ToString().Trim().Split(", ");
        var conectionsDict= new Dictionary<Valve, int>();

        foreach(var conection in conectionsStrings)
        {
            map[name].conections.Add(map[conection], 1);
        }
    }

    //distances from every point to every point
    var distanceDictionary = DistanceDictionary_Flat(map);

    var startPath = new List<(Valve, Valve)>() { (map["AA"], map["AA"]) };

    var maxPath = PathFinder_1(distanceDictionary, startPath, 30,0);
 
    foreach(var conection in maxPath.Item1)
    {
        Console.Write($"{conection.Item2.name} => ");
    }
    Console.WriteLine(maxPath.Item2);
}
//originalpathfinding algorithium
static (List<(Valve, Valve)>, int) PathFinder_1(Dictionary<(Valve, Valve), int> distanceDictionary, List<(Valve, Valve)> path, int remainingTime, int score)
{
    var maxPath = (path, score);

    foreach (var conection in distanceDictionary.Where(x => x.Key.Item1 == path.Last().Item2))
    {
        var newRemainingTime = remainingTime - conection.Value;

        if (newRemainingTime <= 0) { continue; }

        var newScore = score + newRemainingTime * conection.Key.Item2.flowrate;
        var newPath = new List<(Valve, Valve)>(path);
        newPath.Add(conection.Key);

        var newDistanceDictionary = distanceDictionary.Where(x => x.Key.Item2 != conection.Key.Item1).ToDictionary(i => i.Key, i => i.Value);

        var testPath = PathFinder_1(newDistanceDictionary, newPath, newRemainingTime, newScore);

        if (testPath.Item2 > maxPath.score) { maxPath = testPath; }
    }
    return maxPath;
}
// original non nested dictionaty
static Dictionary<(Valve, Valve), int> DistanceDictionary_Flat(Dictionary<string, Valve> map)
{
    var valveList = map.Values;
    var distanceDictionary = new Dictionary<(Valve, Valve), int>();
    //populates all conections with 99 distance
    foreach (var valve1 in map)
    {
        foreach (var valve2 in map)
        {
            distanceDictionary.Add((valve1.Value, valve2.Value), 99);
        }
    }

    //populates "ouside Paths"
    foreach (var valve in map)
    {
        foreach (var conection in valve.Value.conections)
        {
            var distance = conection.Value;
            var fromTo = (valve.Value, conection.Key);

            distanceDictionary[fromTo] = distance;
        }
        distanceDictionary[(valve.Value, valve.Value)] = 1;
    }

    //populates "inside Paths"
    foreach (var k in valveList)
    {
        foreach (var i in valveList)
        {
            foreach (var j in valveList)
            {
                if (distanceDictionary[(i, j)] > distanceDictionary[(i, k)] + distanceDictionary[(k, j)])
                {
                    distanceDictionary[(i, j)] = distanceDictionary[(i, k)] + distanceDictionary[(k, j)];
                }
            }
        }
    }

    //modifies the distanceDictionary to imporveefficenancys
    foreach (var connection in distanceDictionary.Keys)
    {

        if (connection.Item1.flowrate == 0 && connection.Item1.name != "AA") { distanceDictionary.Remove(connection); }
        else if (connection.Item2.flowrate == 0 && connection.Item2.name != "AA") { distanceDictionary.Remove(connection); }
        else if (connection.Item2.name == "AA") { distanceDictionary.Remove(connection); }

        //removes all self conections and adds 1 to the conection time of the remaining conections
        else if (connection.Item1 == connection.Item2) { distanceDictionary.Remove(connection); }
        else { distanceDictionary[connection] = distanceDictionary[connection] + 1; }

    }

    return distanceDictionary;
}







//Part 1 with DD
static void Part_1DD()
{
    var data = File.ReadAllText("data.txt");

    var pattern = @"Valve\s(\w+)\s+.+rate+.(\d+)+.+valves?\s(.+)\s";
    var matches = Regex.Matches(data, pattern);

    var map = new Dictionary<string, Valve_2>();

    // create the valves
    foreach (Match match in matches)
    {
        var name = match.Groups[1].ToString();
        var flowRate = int.Parse(match.Groups[2].ToString());
        var conections= match.Groups[3].ToString().Trim().Split(", ").ToList();
        conections.Add(name);
        var Valve = new Valve_2(name, flowRate, conections);

        map.Add(name, Valve);
    }

    //distances from every point to every point
    var distanceDictionary = DistanceDictionary_Nested(map);

    var startPath = new List<string>() {"AA" };

    var maxPath = PathFinder_DD(distanceDictionary,map, startPath, startPath, 30, 0);

    foreach (var conection in maxPath.Item1)
    {
        Console.Write($"{conection} => ");
    }
    Console.WriteLine(maxPath.Item2);
}
//nesting the Dics(all path from AA are in a dic with the key AA) as aposed to having them on one leave with teh key (AA,BB) 
static Dictionary<string, Dictionary<string, int>> DistanceDictionary_Nested(Dictionary<string, Valve_2> map)
{
    var valveList = map.Keys;
    var distanceDictionary = new Dictionary<string, Dictionary<string, int>>();

    //populates all conections with 99 distance
    foreach (var valve1 in map)
    {
        var valveDic = new Dictionary<string, int>();
        foreach (var valve2 in map)
        {
            valveDic.Add(valve2.Key, 99);
        }

        distanceDictionary.Add(valve1.Key, valveDic);
    }

    //populates "ouside Paths"
    foreach (var valve in map)
    {
        foreach (var conection in valve.Value.conections)
        {
            distanceDictionary[valve.Key][conection] = 1;
        }
        distanceDictionary[valve.Key][valve.Key] = 1;
    }

    //populates "inside Paths"
    foreach (var Maria in valveList)
    {
        foreach (var Rose in valveList)
        {
            foreach (var Sheena in valveList)
            {
                if (distanceDictionary[Rose][Sheena] > distanceDictionary[Rose][Maria] + distanceDictionary[Maria][Sheena])
                {
                    distanceDictionary[Rose][Sheena] = distanceDictionary[Rose][Maria] + distanceDictionary[Maria][Sheena];
                }
            }
        }
    }

    //shrinks the Dic
    foreach (var valve in distanceDictionary)
    {
        if (map[valve.Key].flowrate == 0 && valve.Key != "AA") { distanceDictionary.Remove(valve.Key); continue; }

        foreach (var conection in valve.Value)
        {
            if (map[conection.Key].flowrate == 0) { distanceDictionary[valve.Key].Remove(conection.Key); }
        }
    }




    return distanceDictionary;
}
//original path finder modified to take a DD
static (List<string>, int) PathFinder_DD(Dictionary<string, Dictionary<string, int>> distanceDictionary, Dictionary<string, Valve_2> map, List<string> path, List<string> activeValves, int remainingTime, int score)
{
    var maxPath = (path, score);
    var currentValve = path.Last();

    foreach (var conection in distanceDictionary[currentValve])
    {
        if (activeValves.Contains(conection.Key)) continue;
        if (conection.Value >= remainingTime) continue;
        if (conection.Key == currentValve) continue;

        var newRemainingTime = remainingTime - conection.Value - 1;
        var newPath = new List<string>(path);
        newPath.Add(conection.Key);

        var newScore = score + newRemainingTime * map[conection.Key].flowrate;

        var newActiveValves = new List<string>(activeValves);
        newActiveValves.Add(conection.Key);

        var testPath = PathFinder_DD(distanceDictionary, map, newPath, newActiveValves, newRemainingTime, newScore);

        if (testPath.Item2 > maxPath.score) { maxPath = testPath; }
    }
    return maxPath;
}






static void Part_2()
{
    var data = File.ReadAllText("data.txt");

    var pattern = @"Valve\s(\w+)\s+.+rate+.(\d+)+.+valves?\s(.+)\s";
    var matches = Regex.Matches(data, pattern);

    var map = new Dictionary<string, Valve_2>();

    // create the valves
    foreach (Match match in matches)
    {
        var name = match.Groups[1].ToString();
        var flowRate = int.Parse(match.Groups[2].ToString());
        var Valve = new Valve_2(name, flowRate, new List<string>() { name});

        map.Add(name, Valve);
    }

    //populates the valves with flow rate and conection details
    foreach (Match match in matches)
    {
        var name = match.Groups[1].ToString();

        var conectionsStrings = match.Groups[3].ToString().Trim().Split(", ");

        foreach (var conection in conectionsStrings)
        {
            map[name].conections.Add(conection);
        }
    }

    //distances from every point to every point
    var distanceDictionary = DistanceDictionary_Nested(map);

    var startPath = new List<string>() { "AA" };

    var maxPath = PathFinder_2(distanceDictionary,map,startPath, startPath, startPath, 26, 26, 0);

    Console.WriteLine("Max Score: "+maxPath.Item3);

    foreach (var conection in maxPath.Item1)
    {
        Console.Write($"{conection} => ");
    }
    Console.WriteLine();
    foreach (var conection in maxPath.Item2)
    {
        Console.Write($"{conection} => ");
    }
}

// a DD pathfind to allow two paths to be searched at the same time
static (List<string>, List<string>, int) PathFinder_2(Dictionary<string, Dictionary<string, int>> distanceDict, Dictionary<string, Valve_2> map, List<string> activeValves, List<string> path1, List<string> path2, int time1, int time2, int score)
{
    var maxPath = (path1,path2, score);

    var leadingPath = path1; 
    var remainingLeadingTime = time1; 
    var trailingPath = path2; 
    var remainingTrailingTime = time2; 

    if (time2> time1) 
    { 
        leadingPath = path2; 
        remainingLeadingTime = time2; 
        trailingPath = path1; 
        remainingTrailingTime = time1; 
    }

    var leadingConections = distanceDict[leadingPath.Last()];

    //Leading time will always be equal to or greater than trailing time ie. it will get to its destination before or at the same time as the trailing path
    //We want to keep looping leading path/time untill leading time is less than trailing time at which points the roles will switch

    foreach (var leadingConection in leadingConections)
    {
        if (activeValves.Contains(leadingConection.Key)) continue;
        if (leadingConection.Value >= remainingLeadingTime) continue;
        if (leadingConection.Key == leadingPath.Last()) continue;

        //updates the RemainingTime, Active Valves, Score and Path of the leading path as it can travel to the destination in the remaining time
        var newRemainingLeadingTime = remainingLeadingTime - leadingConection.Value - 1;

        var newLeadingPath = new List<string>(leadingPath);
        newLeadingPath.Add(leadingConection.Key);

        var newScore = score + newRemainingLeadingTime * map[leadingConection.Key].flowrate;

        var newActiveValves = new List<string>(activeValves);
        newActiveValves.Add(leadingConection.Key);

        var testPaths = PathFinder_2(distanceDict, map, newActiveValves, newLeadingPath, trailingPath, newRemainingLeadingTime, remainingTrailingTime, newScore);

        if (testPaths.Item3 > maxPath.score) { maxPath = testPaths; }
    }

    return maxPath;
}






// an attempt at shorten the inital dictionary by removing the fly over valves, not currently working as it make the above code get the work andswer and is slower 
static Dictionary<string, Valve> Susume(  Dictionary<string, Valve> map)
{
    var newMap = new Dictionary<string, Valve>(map);

    foreach (var valve in map)
    {
        if (valve.Value.conections.Count == 2 && valve.Value.flowrate==0)
        {
            var tempDict=new Dictionary<Valve, int>(valve.Value.conections);
            tempDict.Remove(valve.Value);

            var front = tempDict.First().Key;
            
            var back = tempDict.Last().Key;

            front.conections.Add(back, front.conections[valve.Value] +1);
            front.conections.Remove(valve.Value);

            back.conections.Add(front, back.conections[valve.Value] +1);
            back.conections.Remove(valve.Value);

            newMap.Remove(valve.Key);

        }
        map=newMap;
    }
    return map;
}
//prints all valves and the conections, not uses full when using the Whatever-Fuckery algorithem but was initally usfull with Susume
static void PrintMap(Dictionary<string, Valve> map)
{
    foreach(var valve in map)
    {
        Console.WriteLine($"Valve: {valve.Key}, Flow Rate: {valve.Value.flowrate},  Conection/s:{valve.Value.conections.Count()} ");
        foreach (var Conection in valve.Value.conections)
        {
            if (Conection.Key != valve.Value)
            {
                Console.WriteLine($"Conection to Valve {Conection.Key} is {Conection.Value} unit/s Long");
            }
        }
        Console.WriteLine();
    }
}








//valve structure when using a nested Dictionary
public record struct Valve_2(string name, int flowrate, List<string> conections);

//original Valve strutcture
public record struct Valve(string name, int flowrate, Dictionary<Valve,int> conections);

public record struct Conections(string start, string end, int distance);