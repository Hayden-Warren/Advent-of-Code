using System.Diagnostics;

Part_1();

Part_2();

void Part_1()
{
    var watch=new Stopwatch();
    watch.Start();
    var rawData = File.ReadAllLines("data.txt");

    var height=rawData.Length;
    var width = rawData[0].Length;
    var elves=new List<(int row,int column) >();

    for (int i = 0; i < height; i++)
    {
        for(int j = 0; j < width; j++)
        {
            if (rawData[i][j] == '#')
            {
                var elf = (i,j );
                elves.Add(elf);
            }
        }
    }

    var directions = Directions();
    var startDirection = 0;

    for (int round = 0; round < 10; round++)
    {
        var nextelves = new List<(int row, int column)>(elves);
        for (int elfIndex = 0; elfIndex < elves.Count; elfIndex++)
        {
            var AdjacentElf = false;
            for (int j = 0; j < 4; j++)
            {
                var testdirection = (startDirection + j) % 4;
                var testposition0 = (elves[elfIndex].row + directions[testdirection][0].row, elves[elfIndex].column + directions[testdirection][0].column);
                var testposition1 = (elves[elfIndex].row + directions[testdirection][1].row, elves[elfIndex].column + directions[testdirection][1].column);
                var testposition2 = (elves[elfIndex].row + directions[testdirection][2].row, elves[elfIndex].column + directions[testdirection][2].column);
                if (elves.Contains(testposition0) || elves.Contains(testposition1) || elves.Contains(testposition2))
                {
                    AdjacentElf = true;
                }
            }
            if (!AdjacentElf) {continue;}

            for (int j = 0; j < 4; j++)
            {
                var testdirection = (startDirection+j)%4;
                var testposition0 = (elves[elfIndex].row + directions[testdirection][0].row, elves[elfIndex].column + directions[testdirection][0].column);
                var testposition1 = (elves[elfIndex].row + directions[testdirection][1].row, elves[elfIndex].column + directions[testdirection][1].column);
                var testposition2 = (elves[elfIndex].row + directions[testdirection][2].row, elves[elfIndex].column + directions[testdirection][2].column);
                if (!elves.Contains(testposition0) && !elves.Contains(testposition1) && !elves.Contains(testposition2))
                {
                    if (nextelves.Contains(testposition1))
                    {
                        var matchingIndex = nextelves.IndexOf(testposition1);
                        nextelves[matchingIndex] = elves[matchingIndex];
                    }
                    else
                    {
                        nextelves[elfIndex] = testposition1;
                    }
                    break;
                }
            }    
        }
        elves=nextelves;
        startDirection = (startDirection + 1) % 4;
    }

    var rowMax = elves.MaxBy(x => x.row).row;
    var rowMin = elves.MinBy(x => x.row).row;
    var columnMax = elves.MaxBy(x => x.column).column;
    var columnMin = elves.MinBy(x => x.column).column;

    //Console.Clear();
    //Print(elves);
    var endWidth = rowMax - rowMin +1;
    var endHeight = columnMax - columnMin +1;

    var emptySpaces = endHeight * endWidth - elves.Count;
    Console.WriteLine($"Part 1: {emptySpaces} [{watch.ElapsedMilliseconds} ms]");
}

void Part_2()
{
    var watch = new Stopwatch();
    watch.Start();
    var rawData = File.ReadAllLines("data.txt");

    var height = rawData.Length;
    var width = rawData[0].Length;
    var elves = new List<(int row, int column)>();

    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (rawData[i][j] == '#')
            {
                var elf = (i, j);
                elves.Add(elf);
            }
        }
    }

    var directions = Directions();
    var startDirection = 0;
    var roundCount = 0;
    while(true)
    {
        var nextelves = new List<(int row, int column)>(elves);
        for (int elfIndex = 0; elfIndex < elves.Count; elfIndex++)
        {
            var AdjacentElf = false;
            for (int j = 0; j < 4; j++)
            {
                var testdirection = (startDirection + j) % 4;
                var testposition0 = (elves[elfIndex].row + directions[testdirection][0].row, elves[elfIndex].column + directions[testdirection][0].column);
                var testposition1 = (elves[elfIndex].row + directions[testdirection][1].row, elves[elfIndex].column + directions[testdirection][1].column);
                var testposition2 = (elves[elfIndex].row + directions[testdirection][2].row, elves[elfIndex].column + directions[testdirection][2].column);
                if (elves.Contains(testposition0) || elves.Contains(testposition1) || elves.Contains(testposition2))
                {
                    AdjacentElf = true;
                }
            }
            if (!AdjacentElf) { continue; }

            for (int j = 0; j < 4; j++)
            {
                var testdirection = (startDirection + j) % 4;
                var testposition0 = (elves[elfIndex].row + directions[testdirection][0].row, elves[elfIndex].column + directions[testdirection][0].column);
                var testposition1 = (elves[elfIndex].row + directions[testdirection][1].row, elves[elfIndex].column + directions[testdirection][1].column);
                var testposition2 = (elves[elfIndex].row + directions[testdirection][2].row, elves[elfIndex].column + directions[testdirection][2].column);
                if (!elves.Contains(testposition0) && !elves.Contains(testposition1) && !elves.Contains(testposition2))
                {
                    if (nextelves.Contains(testposition1))
                    {
                        var matchingIndex = nextelves.IndexOf(testposition1);
                        nextelves[matchingIndex] = elves[matchingIndex];
                    }
                    else
                    {
                        nextelves[elfIndex] = testposition1;
                    }
                    break;
                }
            }
        }
        roundCount++;

        var match = true;
        for (int elfIndex = 0; elfIndex < elves.Count; elfIndex++)
        {
            if (elves[elfIndex].row != nextelves[elfIndex].row || elves[elfIndex].column != nextelves[elfIndex].column)
            {
                match = false;
                break;
            }
        }
        if (match) { break; }
        
        elves = nextelves;
        startDirection = (startDirection + 1) % 4;
    }

    Console.WriteLine($"Part 2: {roundCount} [{watch.ElapsedMilliseconds} ms]");
}




void Print(List<(int row, int column)> elves)
{
    var rowMax = elves.MaxBy(x => x.row).row;
    var rowMin = elves.MinBy(x => x.row).row;
    var columnMax = elves.MaxBy(x => x.column).column;
    var columnMin = elves.MinBy(x => x.column).column;

    for (int i = rowMin-1; i <= rowMax+1; i++)
    {
        for (int j = columnMin-1; j <= columnMax+1; j++)
        {
            if (elves.Contains((i, j))) { Console.Write("#"); }
            else { Console.Write("."); }
        }
        Console.WriteLine();
    }
}



List<List<(int row, int column)>> Directions() 
{
    var directions = new List<List<(int, int)>>();

    List<(int, int)> north = new List<(int, int)>() { (-1, -1), (-1, 0), (-1, 1) };
    directions.Add(north);

    List<(int, int)> south = new List<(int, int)>() { (1, -1), (1, 0), (1, 1) };
    directions.Add(south);

    List<(int, int)> west = new List<(int, int)>()  {(-1, -1), (0, -1), (1, -1) };
    directions.Add(west);

    List<(int, int)> east = new List<(int, int)>()  {( -1, 1), (0, 1), (1, 1) };
    directions.Add(east);

    return directions;
}
