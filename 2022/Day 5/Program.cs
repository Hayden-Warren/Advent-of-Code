Part1();

static void Part1()
{
    var data = File.ReadAllLines(@"data.txt");
    var moves = File.ReadAllLines(@"moves.txt");
    
    var dataheight = data.Count();
    var datawidth = (data[dataheight-1].Count()+1)/4;

    List<string> dataList = new List<string>();

    for (int i = 1; i <= datawidth; i++)
    {
        dataList.Add(i.ToString());
    }

    for (int columnIndex = datawidth - 1; columnIndex >= 0; columnIndex--)
    {
        var rowIndex = dataheight - 1;
        while (rowIndex >= 0)
        {
            var container = data[rowIndex].ElementAt(1 + 4 * columnIndex);

            if (container == ' ') { break; }

            dataList[columnIndex] = dataList[columnIndex] + container;
            rowIndex--;
        }
    }

    foreach (var line in moves)
    {
        var movefrom = Int32.Parse(line.Split("from")[1].Split("to")[0]);
        var moveto = Int32.Parse(line.Split("to")[1]);
        var movedepth = Int32.Parse(line.Split("from")[0].Split("move")[1]);

        var totop = dataList[moveto - 1].Length;
        var fromtop = dataList[movefrom - 1].Length;

        var movedsection = dataList[movefrom - 1].Substring(dataList[movefrom - 1].Length - movedepth);

        char[] charArray = movedsection.ToCharArray();
        Array.Reverse(charArray);
        var reversed = new string(charArray);

        dataList[moveto - 1] = dataList[moveto - 1] + reversed;
        dataList[movefrom - 1] = dataList[movefrom - 1].Remove(dataList[movefrom - 1].Length - movedepth);
    }

    foreach (string test in dataList) { Console.Write(test.Last()); }
}

static void Part2()
{
    var data = File.ReadAllLines(@"data.txt");
    var moves = File.ReadAllLines(@"moves.txt");

    var dataheight = data.Count();
    var datawidth = (data[dataheight - 1].Count() + 1) / 4;

    List<string> dataList = new List<string>();

    for (int i = 1; i <= datawidth; i++)
    {
        dataList.Add(i.ToString());
    }

    for (int columnIndex = datawidth - 1; columnIndex >= 0; columnIndex--)
    {
        var rowIndex = dataheight - 1;
        while (rowIndex >= 0)
        {
            var container = data[rowIndex].ElementAt(1 + 4 * columnIndex);

            if (container == ' ')  { break;  }

            dataList[columnIndex] = dataList[columnIndex] + container;
            rowIndex--;
        }
    }

    foreach (var line in moves)
    {
        var movefrom = Int32.Parse(line.Split("from")[1].Split("to")[0]);
        var moveto = Int32.Parse(line.Split("to")[1]);
        var movedepth = Int32.Parse(line.Split("from")[0].Split("move")[1]);

        var totop = dataList[moveto - 1].Length;
        var fromtop = dataList[movefrom - 1].Length;

        var movedsection = dataList[movefrom - 1].Substring(dataList[movefrom - 1].Length - movedepth);

        dataList[moveto - 1] = dataList[moveto - 1] + movedsection;
        dataList[movefrom - 1] = dataList[movefrom - 1].Remove(dataList[movefrom - 1].Length - movedepth);
    }

    foreach (string test in dataList)  { Console.Write(test.Last()); }
}