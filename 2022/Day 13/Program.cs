using Day_13;

Part2_3();


static void Part1_Infinte()
{
    var data = File.ReadAllLines("data.txt");
    var boolList = new List<bool?>();
    var truesum = 0;

    for (int i = 0; i < data.Length; i = i + 3)
    {
        var left = data[i];
        var right = data[i + 1];

        var result = Compare.run2(left, right);

        if ((bool)result) { truesum = truesum + i / 3 + 1; }

        Console.WriteLine($"Index:{i / 3 + 1} {result}");
    }
    Console.WriteLine($"Sum of Indices of true results: {truesum}");
}



static void Part2_CheatCounting()
{
    var data = File.ReadAllLines("data.txt");
    int[] countlist = new int[10] {0,0,0,0,0,0,0,0,0,0};
    foreach(var line in data)
    {
        if (line == "") { continue; }
        else
        {            
            for (var i = 0; i < line.Length; ++i)
            {
                var character = line[i];
                switch (character)
                {
                    case '[':
                        continue;                        
                    case ']':
                        countlist[0]++;
                        i = line.Length;
                        break;
                    default:
                        var twoDigit = line.Substring(i, 2);
                        if (int.TryParse(twoDigit.ToString(), out int value))
                        {
                            i = line.Length;
                            break;
                        }
                        else
                        {
                           int.TryParse(character.ToString(), out int value2);
                            countlist[value2]++;
                            i = line.Length;
                            break;
                        }                          
                }
            }
        }
    }

    var index2 = countlist[0] + countlist[1]+1;
    var index6 = countlist[0] + countlist[1] + countlist[2] + countlist[3] + countlist[4] + countlist[5]+2;

    var answer = index2 * index6;
    Console.WriteLine(index2);
    Console.WriteLine(index6);
    Console.WriteLine(answer);
}


static void Part2_3()
{
    var data = File.ReadAllLines("data.txt");
    var newdata= new List<string>();

    foreach (var line in data)
    {
        if (line != "") { newdata.Add(line); }
    }

    newdata.Add("[[2]]");
    newdata.Add("[[6]]");

    var anything = newdata.ToArray();
    Array.Sort(anything,new MagicSortingHat());

    var divider2position = Array.IndexOf(anything,"[[2]]")+1;
    var divider6position = Array.IndexOf(anything, "[[6]]")+1;

    var result = divider2position * divider6position;

    foreach (var line in anything)
    {
        Console.WriteLine(line);
    }
    Console.WriteLine(result);
}