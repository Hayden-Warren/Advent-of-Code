using System.Diagnostics;

Part_1();

Part_2();

void Part_1()
{
    var watch=new Stopwatch();
    watch.Start();
    var rawdata = File.ReadAllLines("data.txt");

    var codeList = new List<Code>();

    var count = 0;
    foreach (var code in rawdata)
    {
        var newCode = new Code();
        newCode.initalpostion = count;
        newCode.value = int.Parse(code);
        codeList.Add(newCode);
        count++;
    }

    var listLength = codeList.Count;

    //Console.WriteLine("Inital arrangement:");
    //print(codeList);

    for (int i = 0; i < listLength; i++)
    {
        var code = codeList.Find(x => x.initalpostion == i);
        var currentPostion = codeList.FindIndex(x => x.initalpostion == i);

        var newPosition = currentPostion + code.value;

        while (newPosition < 0 || newPosition >= listLength)
        {
            if (newPosition > (listLength - 1))
            {
                newPosition = newPosition - listLength + 1;
            }
            else if (newPosition < 0)
            {
                newPosition = newPosition + listLength - 1;
            }
        }

        codeList.RemoveAt(currentPostion);
        if (newPosition == 0 && code.value != 0) { codeList.Add(code); }
        else { codeList.Insert((int)newPosition, code); }

        //var numBefore = codeList[newPosition + 1].value;
        //var numAfter = new int();
        //if (newPosition != 0) { numBefore = codeList[newPosition - 1].value; }
        //else { numBefore = codeList[0].value; }
        //Console.WriteLine($"{code.value} moves between {numBefore} and {numAfter}:");
       //print(codeList);
    }

    while (codeList.Count < 5000)
    {
        codeList.AddRange(codeList);
    }

    Console.WriteLine("Part 1 Answer: ");

    var zeroPosition = codeList.FindIndex(x => x.value == 0);
    var oneThousand = codeList[zeroPosition + 1000].value;
    var twoThousand = codeList[zeroPosition + 2000].value;
    var threeThousand = codeList[zeroPosition + 3000].value;

    Console.WriteLine($"{oneThousand} + {twoThousand} + {threeThousand} = {oneThousand + twoThousand + threeThousand}  [{watch.ElapsedMilliseconds} ms]");
    Console.WriteLine();
}


void Part_2()
{
    var watch = new Stopwatch();
    watch.Start();
    var rawdata = File.ReadAllLines("data.txt");

    var codeList = new List<LongCode>();
    long decriptionKey = 811589153;
    var count = 0;
    var listLength = rawdata.Length;

    foreach (var code in rawdata)
    {
        var newCode = new LongCode();
        newCode.initalpostion = count;
        newCode.value = (int.Parse(code) * decriptionKey);
        codeList.Add(newCode);
        count++;
    }

    Console.WriteLine("Part 2 Answer: ");
    //Console.WriteLine("Initial arrangement:");
    //printLong(codeList);

    for (int j = 1; j <= 10; j++)
    {
        for (int i = 0; i < listLength; i++)
        {
            var code = codeList.Find(x => x.initalpostion == i);
            var currentPostion = codeList.FindIndex(x => x.initalpostion == i);

            var newPosition = currentPostion + code.value;
            newPosition = newPosition % (listLength-1);

            while (newPosition < 0 || newPosition >= listLength)
            {
                if (newPosition > (listLength - 1))
                {
                    newPosition = newPosition - listLength + 1;
                }
                else if (newPosition < 0)
                {
                    newPosition = newPosition + listLength - 1;
                }
            }            

            codeList.RemoveAt(currentPostion);

            if (newPosition == 0 && code.value != 0) { codeList.Add(code); }
            else { codeList.Insert((int)newPosition, code); }

        }
        //Console.WriteLine($"After {j} round of mixing:");
        //printLong(codeList);
    }

    while (codeList.Count < 4999)
    {
        codeList.AddRange(codeList);
    }

    var zeroPosition = codeList.FindIndex(x => x.value == 0);
    var oneThousand = codeList[zeroPosition + 1000].value;
    var twoThousand = codeList[zeroPosition + 2000].value;
    var threeThousand = codeList[zeroPosition + 3000].value;

    Console.WriteLine($"{oneThousand} + {twoThousand} + {threeThousand} = {oneThousand + twoThousand + threeThousand}  [{watch.ElapsedMilliseconds} ms]");
}


static void printLong(List<LongCode> codeList)
{
    foreach (var code in codeList)
    {
        Console.Write(code.value + ", ");
    }
    Console.WriteLine();
    Console.WriteLine();

}

static void print(List<Code> codeList)
{
    for (var i=0;i<codeList.Count-1;i++)
    {
        var code = codeList[i];
        Console.Write(code.value + ", ");
    }
    Console.Write(codeList[codeList.Count-1].value);

   Console.WriteLine();
    Console.WriteLine();

}

public struct Code { public int initalpostion; public int value; };
public struct LongCode { public int initalpostion; public long value; };
