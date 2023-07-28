using System.Diagnostics;
Part_2();


void Stormcheck(HashSet<(int row, int column, char direction)> storms, int width, int height, (int row, int column, string move) currentPosition, int minute)
{
    Console.WriteLine($"Initial state:");
    Print(storms, width, height, currentPosition);
    while (true)
    {
        Console.Clear();
        storms = UpdateStorms(storms, width, height);
        Console.WriteLine($"Minute {minute}, no moves");
        Print(storms, width, height, currentPosition);

        minute++;
    }
}


void Part_1()
{
    var watch = new Stopwatch();
    watch.Start();

    var rawData = File.ReadAllLines("testdata.txt");

    HashSet<(int row, int column, char direction)> storms = new HashSet<(int, int, char)>();

    var width = rawData[0].Length;
    var height = rawData.Length;

    for (var i = 1; i < height - 1; i++)
    {
        for (var j = 1; j < width - 1; j++)
        {
            if (rawData[i][j] != '.')
            {
                storms.Add((i, j, rawData[i][j]));
            }
        }
    }
    (int row, int column, char direction) start = (0, 1, '.');
    (int row, int column, char direction) end = (height - 1, width - 2, '.');
    //storms.Add(start);
    //storms.Add(end);

    (int row, int column, int dist) startPosition = (0, 1, 0);

    var minute = 0;

    var allstorms= MaxStorms(storms,width,height);

    var StackOPositions=new HashSet<(int row, int column, int time)>() { startPosition };
    


    var minimumMinutes = Dive(allstorms, width, height, StackOPositions);

    Console.WriteLine($"Day 24 Part 1 Answer: {minimumMinutes} [{watch.ElapsedMilliseconds}]ms");

}

void Part_2()
{
    var watch = new Stopwatch();
    watch.Start();

    var rawData = File.ReadAllLines("data.txt");

    HashSet<(int row, int column, char direction)> storms = new HashSet<(int, int, char)>();

    var width = rawData[0].Length;
    var height = rawData.Length;

    for (var i = 1; i < height - 1; i++)
    {
        for (var j = 1; j < width - 1; j++)
        {
            if (rawData[i][j] != '.')
            {
                storms.Add((i, j, rawData[i][j]));
            }
        }
    }
    (int row, int column, char direction) start = (0, 1, '.');
    (int row, int column, char direction) end = (height - 1, width - 2, '.');
    //storms.Add(start);
    //storms.Add(end);

    

    var minute = 0;

    var allstorms = MaxStorms(storms, width, height);


    (int row, int column, int dist) startPosition = (0, 1, 0);

    var StackOPositions = new HashSet<(int row, int column, int time)>() { startPosition };

    var timeThere = Dive(allstorms, width, height, StackOPositions);
    Console.WriteLine($"there:{timeThere-1}");


    startPosition = (height, width-1, timeThere);

    StackOPositions = new HashSet<(int row, int column, int time)>() { startPosition };

    var timeBack = DiveBack(allstorms, width, height, StackOPositions); ;
    Console.WriteLine($"back:{timeBack}");


    startPosition = (0, 1, timeBack); ;

    StackOPositions = new HashSet<(int row, int column, int time)>() { startPosition };

    var timeThereAgain = Dive(allstorms, width, height, StackOPositions);
    Console.WriteLine($"there:{timeThereAgain-1}");



    Console.WriteLine($"Day 24 Part 2 Answer: {timeThereAgain-1} [{watch.ElapsedMilliseconds}]ms");

}



int Dive(List<HashSet<(int row, int column, char direction)>> storms, int width, int height, HashSet<(int row, int column,int time)> StackOPositions)
{
    var newStackOfPositions= new HashSet<(int row, int column, int time)>();
    var maxTime= StackOPositions.Max(x=>x.time);
   // Console.Write(maxTime);
   // Console.Write(" ");
   // Console.WriteLine(StackOPositions.Count());

    foreach (var position in StackOPositions)
    {
        var newPositions = new HashSet<(int row, int column, int time)>
        {
            (position.row, position.column+1, position.time+1),
            (position.row+1, position.column, position.time+1),

            (position.row-1, position.column, position.time+1),
            (position.row, position.column-1, position.time+1),

            (position.row, position.column, position.time+1),
        };

        foreach (var newPosition in newPositions)
        {
            if (newPosition.column == width - 2 && newPosition.row == height - 1)
            {
               // Console.WriteLine($"found the end @ {newPosition.time }");
                return newPosition.time + 1;
            }

            if (!storms[newPosition.time].Where(x=>x.row == newPosition.row).Where(x => x.column == newPosition.column).Any() && newPosition.row>0 && newPosition.row < height-1 && newPosition.column <width-1  && newPosition.column > 0 && !newStackOfPositions.Contains(newPosition))
            {
                newStackOfPositions.Add(newPosition);
            }
        }
    }
    newStackOfPositions.Add((0,1,maxTime+1));



    //Print_2(newStackOfPositions, storms[maxTime + 1], width, height);

    return Dive(storms, width, height, newStackOfPositions); ;
}

List< HashSet < (int row, int column, char direction)>> MaxStorms(HashSet<(int row, int column, char direction)> initalstorm, int width, int height)
{
    var timer = 0;

    var currentstorm = initalstorm;
    var allstorms = new List<HashSet<(int row, int column, char direction)>>();

    while (timer< width*height*3)
    {
        allstorms.Add(currentstorm);
        currentstorm = UpdateStorms(currentstorm, width, height);
        timer++;
    }

    return allstorms;
}


int DiveBack(List<HashSet<(int row, int column, char direction)>> storms, int width, int height, HashSet<(int row, int column, int time)> StackOPositions)
{
    var newStackOfPositions = new HashSet<(int row, int column, int time)>();
    var maxTime = StackOPositions.Max(x => x.time);

    foreach (var position in StackOPositions)
    {
        var newPositions = new HashSet<(int row, int column, int time)>
        {
            (position.row-1, position.column, position.time+1),
            (position.row, position.column-1, position.time+1),

            (position.row, position.column+1, position.time+1),
            (position.row+1, position.column, position.time+1),

            (position.row, position.column, position.time+1),
        };

        foreach (var newPosition in newPositions)
        {
            if (newPosition.column == 1 && newPosition.row ==0)
            {
                // Console.WriteLine($"found the end @ {newPosition.time }");
                return newPosition.time + 1;
            }

            if (!storms[newPosition.time].Where(x => x.row == newPosition.row).Where(x => x.column == newPosition.column).Any() && newPosition.row > 0 && newPosition.row < height - 1 && newPosition.column < width - 1 && newPosition.column > 0 && !newStackOfPositions.Contains(newPosition))
            {
                newStackOfPositions.Add(newPosition);
            }
        }
    }
    newStackOfPositions.Add((0, 1, maxTime + 1));



    //Print_2(newStackOfPositions, storms[maxTime + 1], width, height);

    return Dive(storms, width, height, newStackOfPositions); ;
}

HashSet<(int row, int column, char direction)> UpdateStorms(HashSet<(int row, int column, char direction)> storms, int width, int height)
{
    var newStorms = new HashSet<(int, int, char)>();
    foreach (var storm in storms)
    {
        var newStorm = storm;
        switch (storm.direction)
        {
            case ('<'):
                newStorm.column--;
                if (newStorm.column == 0)
                {
                    newStorm.column = width - 2;
                }
                break;
            case ('>'):
                newStorm.column++;
                if (newStorm.column == width - 1)
                {
                    newStorm.column = 1;
                }
                break;
            case ('^'):
                newStorm.row--;
                if (newStorm.row == 0)
                {
                    newStorm.row = height - 2;
                }
                break;
            case ('v'):
                newStorm.row++;
                if (newStorm.row == height - 1)
                {
                    newStorm.row = 1;
                }
                break;
            case ('.'):
                break;
        }
        newStorms.Add(newStorm);
    }
    return newStorms;
}

void Print(HashSet<(int row, int column, char direction)> storms, int width, int height, (int row, int column, string meh) currentPosition)
{
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            var storm = storms.Where(x => x.row == i && x.column == j);
            if (i == currentPosition.row && j == currentPosition.column)
            {
                Console.Write('E');
            }
            else if (storm.Any())
            {
                if (storm.Count() > 1)
                {
                    Console.Write(storm.Count());
                }
                else { Console.Write(storm.First().direction); }
            }
            else if (j == 0 || j == width - 1 || (i == 0&&j!=1) || (i == height - 1&&j!=width-2))
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


void Print_2(HashSet<(int row, int column, int time)> positions, HashSet<(int row, int column, char direction)> storms, int width, int height)
{
    var maxTime = positions.Max(x => x.time);
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            var storm = storms.Where(x => x.row == i && x.column == j);
            if (positions.Contains((i, j,maxTime)))
            {
                Console.Write('P');
            }
            else if (storm.Any())
            {
                if (storm.Count() > 1)
                {
                    Console.Write(storm.Count());
                }
                else { Console.Write(storm.First().direction); }
            }
            else if (j == 0 || j == width - 1 || (i == 0 && j != 1) || (i == height - 1 && j != width - 2))
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

















