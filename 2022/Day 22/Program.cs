using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

Part_1();
Console.WriteLine();
Part_2();

void Part_1()
{
    var watch = new Stopwatch();
    watch.Start();
    var rawdata = File.ReadAllText("data.txt");
    var pattern = @"\d+\w";
    var matches = Regex.Matches(rawdata, pattern);

    var distances = new List<int>();
    var rotations = new List<char>();

    foreach (Match match in matches)
    {

        distances.Add(int.Parse(match.Value.Split('L', 'R')[0]));
        rotations.Add(match.Value.Last());
    }
    rotations.RemoveAt(rotations.Count - 1);
   
    var map = File.ReadAllLines("datamap.txt");
    var arrows = ">v<^";

    var numberOfRows = map.Count();

    (int row, int column, int direction) position = (0, map[0].IndexOf('.'), 0); //0==right,1==down,2==left,3==up

    for (int i = 0; i < distances.Count; i++)
    {
        var column = "";
        foreach (var line in map)
        {
            if (line.Length <= position.column) { break; }
            column = column + line[position.column];
        }

        var row = map[position.row];

        for (int j = 0; j < distances[i]; j++)
        {
            var rockcheck = false;

            map[position.row] = map[position.row].Remove(position.column, 1);
            map[position.row] = map[position.row].Insert(position.column, arrows[position.direction].ToString());

            switch (position.direction)
            {
                case 0: //right
                    var rowStart = row.IndexOfAny(".*#<>^v".ToCharArray());

                    var rowStartItem = row[rowStart];

                    if ((position.column + 1 == row.Length || row[position.column + 1] == ' ') && rowStartItem == '#')
                    {
                        rockcheck = true;
                    }
                    else if (position.column + 1 == row.Length || row[position.column + 1] == ' ')
                    {
                        position.column = rowStart;
                    }
                    else if (row[position.column + 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        position.column = position.column + 1;
                    }

                    break;


                case 1: //down
                    var columnStart = column.IndexOfAny(".*#<>v^".ToCharArray());

                    var columnStartItem = column[columnStart];

                    if ((position.row + 1 == numberOfRows || position.row + 1 >= column.Length || column[position.row + 1] == ' ') && columnStartItem == '#')
                    {
                        rockcheck = true;
                    }
                    else if (position.row + 1 == numberOfRows || position.row + 1 >= column.Length || column[position.row + 1] == ' ')
                    {
                        position.row = columnStart;
                    }
                    else if (column[position.row + 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        position.row = position.row + 1;
                    }
                    break;

                case 2: //left  
                    var rowEndItem = row[row.Length - 1];

                    if ((position.column - 1 == -1 || row[position.column - 1] == ' ') && rowEndItem == '#')
                    {
                        rockcheck = true;
                    }
                    else if (position.column - 1 == -1 || row[position.column - 1] == ' ')
                    {
                        position.column = row.Length - 1;
                    }
                    else if (row[position.column - 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        position.column = position.column - 1;
                    }

                    break;

                case 3: //up   
                    var columnEndItem = column[column.Length - 1];

                    if ((position.row - 1 == -1 || column[position.row - 1] == ' ') && columnEndItem == '#')
                    {
                        rockcheck = true;
                    }
                    else if (position.row - 1 == -1 || column[position.row - 1] == ' ')
                    {
                        position.row = column.Length - 1;
                    }
                    else if (column[position.row - 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        position.row = position.row - 1;
                    }
                    break;
            }
            if (rockcheck) { break; }
        }
       
        if (i==distances.Count()-1) { break; }

        if (rotations[i] == 'L')
        {
            if (position.direction == 0) { position.direction = 3; }
            else { position.direction = (position.direction - 1) % 4; }
        }
        else
        {
            position.direction = (position.direction + 1) % 4;
        }
    }

    //foreach (var line in map)
    //{
    //    Console.WriteLine(line);
    //}

    Console.WriteLine($"Part 1: {1000 * (position.row + 1) + 4 * (position.column + 1) + position.direction} [{watch.ElapsedMilliseconds} ms]");
}

void Part_2()
{
    var watch2 = new Stopwatch();
    watch2.Start();
    var rawdata = File.ReadAllText("data.txt");
    var pattern = @"\d+\w";
    var matches = Regex.Matches(rawdata, pattern);

    var distances = new List<int>();
    var rotations = new List<char>();

    foreach (Match match in matches)
    {
        distances.Add(int.Parse(match.Value.Split('L', 'R')[0]));
        rotations.Add(match.Value.Last());
    }
    rotations.RemoveAt(rotations.Count - 1);

    var map = File.ReadAllLines("datamap.txt");
    var arrows = ">v<^";

    var numberOfRows = map.Count();

    (int row, int column, int direction) position = (0, map[0].IndexOf('.'), 0); //0==right,1==down,2==left,3==up

    for (int i = 0; i < distances.Count; i++)
    {
        for (int j = 0; j < distances[i]; j++)
        {
            var rockcheck = false;

            map[position.row] = map[position.row].Remove(position.column, 1);
            map[position.row] = map[position.row].Insert(position.column, arrows[position.direction].ToString());

            var row = map[position.row];

            var column = "";
            foreach (var line in map)
            {
                if (line.Length <= position.column) { break; }
                column = column + line[position.column];
            }
            var newPosition = position;

            switch (position.direction)
            {
                case 0: //right
                    if (position.column + 1 == row.Length || row[position.column + 1] == ' ')
                    {
                        newPosition = overEdge(position, map);
                    }
                    else if (row[position.column + 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        newPosition.column = position.column + 1;
                    }
                    break;

                case 1: //down
                    if (position.row + 1 == numberOfRows || position.row + 1 >= column.Length || column[position.row + 1] == ' ')
                    {
                        newPosition = overEdge(position, map);
                    }
                    else if (column[position.row + 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        newPosition.row = position.row + 1;
                    }
                    break;

                case 2: //left   
                    if (position.column - 1 == -1 || row[position.column - 1] == ' ')
                    {
                        newPosition = overEdge(position, map);
                    }
                    else if (row[position.column - 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        newPosition.column = position.column - 1;
                    }
                    break;

                case 3: //up  
                    if (position.row - 1 == -1 || column[position.row - 1] == ' ')
                    {
                        newPosition = overEdge(position,map);
                    }
                    else if (column[position.row - 1] == '#')
                    {
                        rockcheck = true;
                    }
                    else
                    {
                        newPosition.row = position.row - 1;
                    }
                    break;
            }
            position = newPosition;
            if (rockcheck) { break; }
        }

        if (i == distances.Count() - 1) { break; }

        if (rotations[i] == 'L')
        {
            if (position.direction == 0) { position.direction = 3; }
            else { position.direction = (position.direction - 1) % 4; }
        }
        else
        {
            position.direction = (position.direction + 1) % 4;
        }
    }

    //Print(map);

    Console.WriteLine($"Part 2: 1000 * {position.row + 1} + 4 * {position.column +1} + { position.direction} = {1000 * (position.row + 1) + 4 * (position.column + 1) + position.direction} [{watch2.ElapsedMilliseconds} ms]");

}

(int row, int column, int direction) overEdge((int row, int column, int direction) position, string[] map)
{
    (int row, int column, int direction) newPosition = (0, 0, 0);
    switch (position.row)
    {
        case < 50:
            switch (position.direction)
            {
                case 0: //right
                    newPosition.row = 149 - position.row;
                    newPosition.column = 99;
                    newPosition.direction = 2;
                    break;

                case 1: //down
                    newPosition.row = position.column - 50;
                    newPosition.column = 99;
                    newPosition.direction = 2;
                    break;

                case 2: //left     
                    newPosition.row = 149 - position.row;
                    newPosition.column = 0;
                    newPosition.direction = 0;
                    break;

                case 3: //up                  
                    if (position.column < 100)
                    {
                        newPosition.row = 100 + position.column;
                        newPosition.column = 0;
                        newPosition.direction = 0;
                    }
                    else
                    {
                        newPosition.row = 199;
                        newPosition.column = position.column - 100;
                        newPosition.direction = 3;
                    }
                    break;
            }
            break;

        case < 100:
            switch (position.direction)
            {
                case 0: //right
                    newPosition.row = 49;
                    newPosition.column = position.row + 50;
                    newPosition.direction = 3;
                    break;

                case 1: //down, can't be reahced

                    break;

                case 2: //left     
                    newPosition.row = 100;
                    newPosition.column = position.row - 50;
                    newPosition.direction = 1;
                    break;

                case 3: //up, can't be reahced                  

                    break;
            }
            break;

        case < 150:
            switch (position.direction)
            {
                case 0: //right
                    newPosition.row = 149 - position.row;
                    newPosition.column = 149;
                    newPosition.direction = 2;
                    break;

                case 1: //down
                    newPosition.row = 100 + position.column;
                    newPosition.column = 49;
                    newPosition.direction = 2;
                    break;

                case 2: //left     
                    newPosition.row = 149 - position.row;
                    newPosition.column = 50;
                    newPosition.direction = 0;
                    break;

                case 3: //up          
                    newPosition.row = 50 + position.column;
                    newPosition.column = 50;
                    newPosition.direction = 0;
                    break;
            }
            break;

        case < 200:
            switch (position.direction)
            {
                case 0: //right
                    newPosition.row = 149;
                    newPosition.column = position.row - 100;
                    newPosition.direction = 3;
                    break;

                case 1: //down
                    newPosition.row = 0;
                    newPosition.column = position.column + 100;
                    newPosition.direction = 1;
                    break;

                case 2: //left     
                    newPosition.row = 0;
                    newPosition.column = position.row - 100;
                    newPosition.direction = 1;
                    break;

                case 3: //up, can't be reahced          

                    break;
            }

            break;
    }

    if (map[newPosition.row][newPosition.column] == '#') { return position; }

    return newPosition;
}

void Print(string[] map)
{
    foreach (var line in map)
    {
        foreach (var character in line)
        {
            if ("<>^v".Contains(character)) { Console.ForegroundColor = ConsoleColor.Red; }
            else { Console.ForegroundColor = ConsoleColor.White; }
            if (character == '.') { Console.Write("  "); }
            else{ Console.Write(character + " ");  }
        }
        Console.WriteLine();  
    }
}
