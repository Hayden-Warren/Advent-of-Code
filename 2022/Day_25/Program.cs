
Part_1();

void Part_1()
{
    var rawdata = File.ReadAllLines("data.txt");

    var total = snafuToDecimal(rawdata);

    var snafu = decimalToSnafu(total);

    Console.WriteLine(snafu);
}

long snafuToDecimal( string[] rawData)
{
    long sum = 0;
    foreach (var snafu in rawData)
    {
        var reverseSnafu = snafu.Reverse().ToArray();
        double decimall = 0;

        for (int i = 0; i < reverseSnafu.Length; i++)
        {
            if (int.TryParse(reverseSnafu[i].ToString(), out int number))
            {
                decimall = decimall + (number * Math.Pow(5, i));
            }
            else if (reverseSnafu[i] == '-')
            {
                decimall = decimall + (-1 * Math.Pow(5, i));
            }
            else if (reverseSnafu[i] == '=')
            {
                decimall = decimall + (-2 * Math.Pow(5, i));
            }
            else if (reverseSnafu[i] == ' ')
            {

            }

            else { Console.WriteLine("something went wrong"); }
        }

        //Console.WriteLine($"{snafu}        {decimall}");

        sum = sum + (long)decimall;

    }

    Console.WriteLine();
    Console.WriteLine($"Total={sum}");

    return sum;
}

string decimalToSnafu ( long total)
{
    var remainder = new long();

    List<(long multiple,long power)> mulituplePower= new ();

    while (total > 0)
    {
        var power = 0;
        var multiple = 1;

        while (total / (long)Math.Pow(5, power) > 0)
        {
            power++;
        }
        power--;
        while (total / (long)(multiple * Math.Pow(5, power)) > 0)
        {
            multiple++;
        }
        multiple--;
        total = total - (long)(multiple * Math.Pow(5, power));
        mulituplePower.Add((multiple, power));

        //Console.WriteLine($"{multiple}*5^{power} = {(long)(multiple * Math.Pow(5, power))}       {total}");
    }

    mulituplePower.Reverse();
    var maxpower=mulituplePower.MaxBy(x =>x.power).power;
    var snafunumber = "";
    var overflow = 0;

    for(int currentPower=0; currentPower <= maxpower; currentPower++)
    {
        long currentMultiple = mulituplePower.Find(x => x.power == currentPower).multiple;

        if (currentMultiple==0 && snafunumber.Length<=currentPower && overflow != 1)
        {
            snafunumber=snafunumber +"0";
            overflow = 0;
        }
        else if (currentMultiple == 0 && snafunumber.Length <= currentPower && overflow == 1)
        {
            snafunumber = snafunumber + "1";
            overflow = 0;
        }
        else if(currentMultiple!=0 && overflow!=1)
        {
            if(currentMultiple==1)
            {
                snafunumber = snafunumber + "1";
                overflow = 0;
            }
            else if (currentMultiple == 2)
            {
                snafunumber = snafunumber + "2";
                overflow = 0;
            }
            else if (currentMultiple == 3)
            {
                snafunumber = snafunumber + "=";
                overflow = 1;
            }
            else if (currentMultiple == 4)
            {
                snafunumber = snafunumber + "-";
                overflow = 1;
            }
        }
        else if (currentMultiple != 0 && overflow == 1)
        {
            if (currentMultiple == 1)
            {
                snafunumber = snafunumber + "2"; 
                overflow = 0;
            }
            else if (currentMultiple == 2)
            {
                snafunumber = snafunumber + "=";
                overflow = 1;
            }
            else if (currentMultiple == 3)
            {
                snafunumber = snafunumber + "-";
                overflow = 1;
            }
            else if (currentMultiple == 4)
            {
                snafunumber = snafunumber + "0";
                overflow = 1;
            }
        }

    }


    char[] stringArray = snafunumber.ToCharArray();
    Array.Reverse(stringArray);
    string reversedStr = new string(stringArray);



    return reversedStr;
}

