using System.Diagnostics;


Part_1();
Part_2();




void Part_1()
{
    var watch = Stopwatch.StartNew();
    var data = File.ReadAllLines("data.txt");
    var dropCoordinates = new HashSet<(int x, int y, int z)>();

    foreach (var line in data)
    {
        var split = line.Split(',');
        var x = int.Parse(split[0]);
        var y = int.Parse(split[1]);
        var z = int.Parse(split[2]);
        dropCoordinates.Add((x, y, z));
    }

    var totalSides = dropCoordinates.Count * 6;
    var Count = 0;

    foreach (var point in dropCoordinates)
    {
        if (dropCoordinates.Contains((point.x, point.y, point.z + 1))) { Count++; }
        if (dropCoordinates.Contains((point.x, point.y + 1, point.z))) { Count++; }
        if (dropCoordinates.Contains((point.x + 1, point.y, point.z))) { Count++; }
    }

    var totalExposedSides = totalSides - Count * 2;

    Console.WriteLine($"Part 1 Answer: {totalExposedSides} [{watch.ElapsedMilliseconds} ms]");
}

void Part_1singleline()
{
    var watch = Stopwatch.StartNew();
    var data = File.ReadAllLines("data.txt");
    var dropCoordinates = new HashSet<(int x, int y, int z)>();

    foreach (var line in data)
    {
        var split = line.Split(',');
        var x = int.Parse(split[0]);
        var y = int.Parse(split[1]);
        var z = int.Parse(split[2]);
        dropCoordinates.Add((x, y, z));
    }

    var totalSides = dropCoordinates.Count * 6;

    var zCount = dropCoordinates.Count(p => dropCoordinates.Contains((p.x, p.y, p.z + 1)));
    var yCount = dropCoordinates.Count(p => dropCoordinates.Contains((p.x, p.y + 1, p.z)));
    var xCount = dropCoordinates.Count(p => dropCoordinates.Contains((p.x + 1, p.y, p.z)));

    var totalExposedSides = totalSides - zCount * 2 - yCount * 2 - xCount * 2;

    Console.WriteLine($"Part 1 Answer: {totalExposedSides} [{watch.ElapsedMilliseconds} ms] (Single Line)");
}



void Part_2()
{
    var watch = Stopwatch.StartNew();
    var data = File.ReadAllLines("data.txt");
    var dropCoordinates = new HashSet<(int x, int y, int z)>();

    foreach (var line in data)
    {
        var split = line.Split(',');
        var x = int.Parse(split[0]);
        var y = int.Parse(split[1]);
        var z = int.Parse(split[2]);
        dropCoordinates.Add((x, y, z));
    }

    var maxX = dropCoordinates.MaxBy(x => x.x).x+1;
    var maxY = dropCoordinates.MaxBy(y => y.y).y+1;
    var maxZ = dropCoordinates.MaxBy(z => z.z).z+1;

    var Origin = new HashSet<(int x, int y, int z)> { (0, 0, 0) };
    var externalCoordinates = ExpoldeFromOrigin(dropCoordinates, Origin, Origin, maxX, maxY, maxZ);

    var externalFaces = 0;
    foreach (var point in dropCoordinates)
    {
        var posX = (point.x + 1, point.y, point.z);
        var posY = (point.x, point.y + 1, point.z);
        var posZ = (point.x, point.y, point.z + 1);
        var negX = (point.x - 1, point.y, point.z);
        var negY = (point.x, point.y - 1, point.z);
        var negZ = (point.x, point.y, point.z - 1);
        if (externalCoordinates.Contains(posX)) { externalFaces++; }
        if (externalCoordinates.Contains(posY)) { externalFaces++; }
        if (externalCoordinates.Contains(posZ)) { externalFaces++; }
        if (externalCoordinates.Contains(negX)) { externalFaces++; }
        if (externalCoordinates.Contains(negY)) { externalFaces++; }
        if (externalCoordinates.Contains(negZ)) { externalFaces++; }
    }

    Console.WriteLine($"Part 2 Answer: {externalFaces} [{watch.ElapsedMilliseconds} ms] ");
}

static HashSet<(int x, int y, int z)> ExpoldeFromOrigin(HashSet<(int x, int y, int z)> dropCoordinates, HashSet<(int x, int y, int z)> externalCoordinates, HashSet<(int x, int y, int z)> edgeCoordinates, int maxX, int maxY, int maxZ)
{

    var newEdgeCoordinate = new HashSet<(int x, int y, int z)>();
    foreach ( var point in edgeCoordinates)
    {
        var newPosX = (point.x+1, point.y ,point.z);
        var newPosY = (point.x ,point.y+1 ,point.z);
        var newPosZ = (point.x ,point.y ,point.z+1);
        var newNegX = (point.x - 1, point.y, point.z);
        var newNegY = (point.x, point.y - 1, point.z);
        var newNegZ = (point.x, point.y, point.z - 1);

        if (newPosX.Item1 <= maxX && !dropCoordinates.Contains(newPosX) && !externalCoordinates.Contains(newPosX)) { newEdgeCoordinate.Add(newPosX); }
        if (newPosY.Item2 <= maxY && !dropCoordinates.Contains(newPosY) && !externalCoordinates.Contains(newPosY)) { newEdgeCoordinate.Add(newPosY); }
        if (newPosZ.Item3 <= maxZ && !dropCoordinates.Contains(newPosZ) && !externalCoordinates.Contains(newPosZ)) { newEdgeCoordinate.Add(newPosZ); }
        if (newNegX.Item1 >= -1 && !dropCoordinates.Contains(newNegX) && !externalCoordinates.Contains(newNegX)) { newEdgeCoordinate.Add(newNegX); }
        if (newNegY.Item2 >= -1 && !dropCoordinates.Contains(newNegY) && !externalCoordinates.Contains(newNegY)) { newEdgeCoordinate.Add(newNegY); }
        if (newNegZ.Item3 >= -1 && !dropCoordinates.Contains(newNegZ) && !externalCoordinates.Contains(newNegZ)) { newEdgeCoordinate.Add(newNegZ); }
    }


    if (newEdgeCoordinate.Count() == 0) 
    { return externalCoordinates; }
    externalCoordinates =externalCoordinates.Concat(newEdgeCoordinate).ToHashSet();

    var nextExplosion = ExpoldeFromOrigin(dropCoordinates, externalCoordinates, newEdgeCoordinate, maxX, maxY, maxZ);
    return nextExplosion;
}






