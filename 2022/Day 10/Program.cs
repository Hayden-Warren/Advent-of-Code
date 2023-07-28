Part2();

static void Part1()
{
    var data = File.ReadAllLines(@"data.txt");
    var operationList = new List<int>() { 1} ;
    var addx = 1;

    foreach (var line in data)
    {
        operationList.Add(addx);
        if (line.Length > 4)
        {
            addx = addx + Int32.Parse(line.Split(' ')[1]);
            operationList.Add(addx);
        }
    }

    var printlist = new List<int>() { 20, 60, 100, 140, 180, 220 };
    var sum = 0;

    foreach (var print in printlist)
    {
        sum = sum + print * operationList[print-1];
        Console.WriteLine($"{print}  {operationList[print-1]}   {sum}");
    }
}


static void Part2()
{
    var data = File.ReadAllLines(@"data.txt");
    var operationList = new List<int>() { 1 };
    var addx = 1;

    foreach (var line in data)
    {
        operationList.Add(addx);
        if (line.Length > 4)
        {
            addx = addx + Int32.Parse(line.Split(' ')[1]);
            operationList.Add(addx);
        }
    }

    var columCount=0;
    var cycle = 1;

    while (cycle < 241)
    {
        if (Math.Abs(columCount - operationList[cycle-1]) <2)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
        columCount++;
        if (cycle % 40 == 0)
        {
            columCount = 0;
            Console.WriteLine();
        }
        cycle++;
    }
}

