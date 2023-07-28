using Day_12;

Part2();

static void Part1()
{
    var data = File.ReadAllLines("data.txt");
    var alphabet = "abcdefghijklmnopqrstuvwxyz";

    //creates and populates an array with heigth based on the characters
    var dataWidth = data[0].Length;
    var dataHeight = data.Length;
    var map = new int[dataHeight, dataWidth];
    for (int i = 0; i < data.Length; i++)
    {
        var line = data[i];
        for (int j = 0; j < line.Length; j++)
        {
            var c = alphabet.IndexOf(line[j]);
            map[i, j] = c;
            if (line[j] == 'E') { map[i, j] = 25; }
            if (line[j] == 'S') { map[i, j] = 0; }


        }
    }

    // finds start position
    int[] startPosition = new int[2];
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i].Contains('S'))
        {
            startPosition = new int[] { i, data[i].IndexOf('S') };
        }
    }

    //finds end position
    int[] endPosition = new int[2];
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i].Contains('E'))
        {
            endPosition = new int[] { i, data[i].IndexOf('E') };
        }
    }

    //V>^<
    var positiondata = new Positiondata();
    positiondata.vertical = startPosition[0];
    positiondata.horizontal = startPosition[1];
    positiondata.height = 0;

    var currentPositions = new List<Positiondata>();
    currentPositions.Add(positiondata);

    var checkedPostions = new List<int[]>();
    checkedPostions.Add(startPosition);

    var finalBubleFront = PositionUpdate.run(currentPositions, checkedPostions, map, endPosition);


    var finalRoute = new Positiondata();
    foreach (var position in finalBubleFront)
    {
        if (position.vertical == endPosition[0] && position.horizontal == endPosition[1])
        {
            finalRoute = position;
        }


    }
    Print.final(finalRoute, map);
}

static void Part2()
{
    var data = File.ReadAllLines("data.txt");
    var alphabet = "abcdefghijklmnopqrstuvwxyz";

    //creates and populates an array with heigth based on the characters
    var dataWidth = data[0].Length;
    var dataHeight = data.Length;
    var map = new int[dataHeight, dataWidth];
    for (int i = 0; i < data.Length; i++)
    {
        var line = data[i];
        for (int j = 0; j < line.Length; j++)
        {
            var c = alphabet.IndexOf(line[j]);
            map[i, j] = c;
            if (line[j] == 'E') { map[i, j] = 25; }
            if (line[j] == 'S') { map[i, j] = 0; }


        }
    }

    // finds start positions
    var startPositions = new List<int[]>();
    for (int i = 0; i < data.Length; i++)
    {
        for (int j = 0; j < data[i].Length; j++)
        {
            if(data[i][j] == 'a')
            {
                var point=new int[2] { i, j };
                startPositions.Add(point);
            }
        }
    }
    //finds end position
    int[] endPosition = new int[2];
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i].Contains('E'))
        {
            endPosition = new int[] { i, data[i].IndexOf('E') };
        }
    }

    //V>^<
    var currentPositions = new List<Positiondata>();
    foreach (var point in startPositions)
    {
        var position = new Positiondata();
        position.vertical = point[0];
        position.horizontal = point[1];
        position.height = 0;
        currentPositions.Add(position);
    }

    var finalBubleFront = PositionUpdate.run(currentPositions, startPositions, map, endPosition);

    var finalRoute = new Positiondata();
    foreach (var position in finalBubleFront)
    {
        if (position.vertical == endPosition[0] && position.horizontal == endPosition[1])
        {
            finalRoute = position;
        }


    }
    Print.final(finalRoute, map);
}