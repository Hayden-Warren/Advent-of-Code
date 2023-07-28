Part2();

static void Part1()
{
    var contents = File.ReadAllLines(@"contents.txt");
    List<char> listofDupes = new List<char>();
    var index = 0;
    var prioritiesIndex = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var sum = 0;

    foreach (var line in contents)
    {
        var lineLength = line.Length;
        var compartment1 = line.Substring(0, lineLength / 2);
        var compartment2 = line.Substring(lineLength / 2);

        foreach (var item in compartment1)
        {
            if (compartment2.Contains(item))
            {
                listofDupes.Add(item);
                sum = sum + prioritiesIndex.IndexOf(listofDupes[index]) + 1;
                break;
            }
        }

        Console.WriteLine($"{compartment1}    {compartment2}     {listofDupes[index]}({prioritiesIndex.IndexOf(listofDupes[index]) + 1}) ");
        index++;
        Console.WriteLine(sum);
    }
}

static void Part2()
{
    var contents = File.ReadAllLines(@"contents.txt");
    List<char> listofBadges = new List<char>();
    var index = 0;
    var prioritiesIndex = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var sum = 0;
    var group =1;

    while (group< 300)
    {
        var elf1 = contents[group-1];
        var elf2 = contents[group];
        var elf3 = contents[group+ 1];
        List<char> common12 = new List<char>();

        foreach (var item in elf1)
        {
            if (elf2.Contains(item))
            {
                common12.Add(item);                
            }
        }

        foreach(var item in common12)
        {
            if (elf3.Contains(item))
            {
                listofBadges.Add(item);
                sum=sum+prioritiesIndex.IndexOf(item)+1;
                break;
            }
        }

        Console.WriteLine($"{listofBadges[index]}({prioritiesIndex.IndexOf(listofBadges[index]) + 1}) ");
        group=group+3;
        index++;
    }
    Console.WriteLine(sum);
}