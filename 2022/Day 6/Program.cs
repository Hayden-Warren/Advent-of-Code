Part2();

static void Part1()
{
    var data = File.ReadAllText(@"3testdata.txt");

    var position = 3;

    while (position < data.Length)
    {
        var code = data.Substring(position - 3, 4);

        position++;

        if (code.Distinct().Count() == 4)
        {
            Console.WriteLine(code + "  " + position);
            break;
        }
    }
}

static void Part2()
{
    var data = File.ReadAllText(@"data.txt");

    var position = 13;

    while (position < data.Length)
    {
        var code = data.Substring(position - 13, 14);

        position++;

        if (code.Distinct().Count() == 14)
        {
            Console.WriteLine(code + "  " + position);
            break;
        }
    }
}