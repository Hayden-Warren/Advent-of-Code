using Day_11;
using System.Text.RegularExpressions;
using System.Numerics;

Part2();

static void Part1()
{
    var data = File.ReadAllText("data.txt");

    var matches = Regex.Matches(data, @"Monkey\s(\d):\s+.+\:\s(.+)\s+.+=\sold\s([*+])\s(.+)\s+.+by\s(\d+)\s+.+monkey\s(\d+)\s+.+monkey\s(\d+)");
    var monkeyList = new List<MonkeyData>();

    foreach (Match match in matches)
    {
        var newMonkey = new MonkeyData();

        newMonkey.monkeyNumber = int.Parse(match.Groups[1].Value);
        var itemsStirng = match.Groups[2].Value.Split(",");
        var startingItems = new List<int>();
        foreach (var item in itemsStirng)
        {
            var itemInt = int.Parse(item);
            startingItems.Add(itemInt);
        }
        newMonkey.startingItems = startingItems;

        newMonkey.operation = match.Groups[3].Value;
        if (int.TryParse(match.Groups[4].Value, out var operationNumber))
        {
            newMonkey.operationNumber = operationNumber;
        }

        newMonkey.divisionCheck = int.Parse(match.Groups[5].Value);
        newMonkey.destinationOnTrue = int.Parse(match.Groups[6].Value);
        newMonkey.destinationOnFalse = int.Parse(match.Groups[7].Value);

        monkeyList.Add(newMonkey);
    }

    var round = 1;

    while (round <= 20)
    {
        foreach (var monkey in monkeyList)
        {
            if (monkey.operation == "*" && monkey.operationNumber == null)
            {
                var newstaringItems = new List<int>(monkey.startingItems);
                foreach (var item in newstaringItems)
                {
                    var newitem = (item * item) / 3;
                    var boolTest = newitem % monkey.divisionCheck == 0;
                    if (boolTest)
                    {
                        monkeyList[monkey.destinationOnTrue].startingItems.Add(newitem);
                    }
                    else monkeyList[monkey.destinationOnFalse].startingItems.Add(newitem);
                    monkey.startingItems.Remove(item);
                    monkey.inspetionTotal++;
                }
            }
            else if ((monkey.operation == "*" && monkey.operationNumber != null))
            {
                var newstaringItems = new List<int>(monkey.startingItems);
                foreach (var item in newstaringItems)
                {
                    var newitem = (item * monkey.operationNumber) / 3;
                    var boolTest = newitem % monkey.divisionCheck == 0;
                    if (boolTest)
                    {
                        monkeyList[monkey.destinationOnTrue].startingItems.Add((int)newitem);
                    }
                    else monkeyList[monkey.destinationOnFalse].startingItems.Add((int)newitem);
                    monkey.startingItems.Remove(item);
                    monkey.inspetionTotal++;
                }
            }
            else if ((monkey.operation == "+" && monkey.operationNumber == null))
            {
                var newstaringItems = new List<int>(monkey.startingItems);
                foreach (var item in newstaringItems)
                {
                    var newitem = (item + item) / 3;
                    var boolTest = newitem % monkey.divisionCheck == 0;
                    if (boolTest)
                    {
                        monkeyList[monkey.destinationOnTrue].startingItems.Add(newitem);
                    }
                    else monkeyList[monkey.destinationOnFalse].startingItems.Add(newitem);
                    monkey.startingItems.Remove(item);
                    monkey.inspetionTotal++;
                }
            }
            else if ((monkey.operation == "+" && monkey.operationNumber != null))
            {
                var newstaringItems = new List<int>(monkey.startingItems);
                foreach (var item in newstaringItems)
                {
                    var newitem = (item + monkey.operationNumber) / 3;
                    var boolTest = newitem % monkey.divisionCheck == 0;
                    if (boolTest)
                    {
                        monkeyList[monkey.destinationOnTrue].startingItems.Add((int)newitem);
                    }
                    else monkeyList[monkey.destinationOnFalse].startingItems.Add((int)newitem);
                    monkey.startingItems.Remove(item);
                    monkey.inspetionTotal++;
                }
            }
            else Console.WriteLine("ERROR");
        }
        Console.WriteLine($"After round {round}, the monkeys are holding items with these worry levels:");
        foreach (var monkey in monkeyList)
        {
            Console.Write($"Monkey {monkey.monkeyNumber}:");
            foreach (var item in monkey.startingItems)
            {
                Console.Write($" {item},");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        round++;
    }

    foreach (var monkey in monkeyList)
    {
        Console.WriteLine($"Monkey {monkey.monkeyNumber} inspected items {monkey.inspetionTotal} times.");
    }

    Console.WriteLine();
    monkeyList = monkeyList.OrderByDescending(x => x.inspetionTotal).ToList();
    var monkeyBusiness = monkeyList[0].inspetionTotal * monkeyList[1].inspetionTotal;
    Console.WriteLine($"Level of Monkey Business: {monkeyBusiness}");
}

static void Part2()
{
    var data = File.ReadAllText("data.txt");

    var matches = Regex.Matches(data, @"Monkey\s(\d):\s+.+\:\s(.+)\s+.+=\sold\s([*+])\s(.+)\s+.+by\s(\d+)\s+.+monkey\s(\d+)\s+.+monkey\s(\d+)");
    var monkeyList = new List<MonkeyData>();

    foreach (Match match in matches)
    {
        var newMonkey = new MonkeyData();

        newMonkey.monkeyNumber = int.Parse(match.Groups[1].Value);
        var itemsStirng = match.Groups[2].Value.Split(",");
        var startingItems = new List<int>();
        foreach (var item in itemsStirng)
        {
            var itemInt = int.Parse(item);
            startingItems.Add(itemInt);
        }
        newMonkey.startingItems = startingItems;

        newMonkey.operation = match.Groups[3].Value;
        if (int.TryParse(match.Groups[4].Value, out var operationNumber))
        {
            newMonkey.operationNumber = operationNumber;
        }

        newMonkey.divisionCheck = int.Parse(match.Groups[5].Value);
        newMonkey.destinationOnTrue = int.Parse(match.Groups[6].Value);
        newMonkey.destinationOnFalse = int.Parse(match.Groups[7].Value);

        monkeyList.Add(newMonkey);
    }

    var primeDivisorList = new List<int>();
    foreach (var monkey in monkeyList)
    {
        primeDivisorList.Add(monkey.divisionCheck);
    }

    // populate currentItem (items are lists of remainders) from the items in starting items
    //starting items is not touched after this
    var tempMonkeyList = new List<MonkeyData>(monkeyList);
    foreach (var monkey in tempMonkeyList)
    {
        var currentItems = new List<List<int>>();
        foreach (var item in monkey.startingItems)
        {
            var itemRemainders = new List<int>();
            foreach (var prime in primeDivisorList)
            {
                itemRemainders.Add(item % prime);
            }
            currentItems.Add(itemRemainders);
        }
        var position = tempMonkeyList.IndexOf(monkey);
        monkeyList[position].currentItems = currentItems;
    }

    var round = 0;
    while (round <= 10000)
    {
        //print 
        {

           if (round <= 0)
            {
               Console.WriteLine();
                Console.WriteLine($"== Items Worry Levels Round {round} ==");
                foreach (var monkey in monkeyList)
               {
                   Console.Write($"Monkey {monkey.monkeyNumber} has the follwing items: ");
                    var position = primeDivisorList.IndexOf(monkey.divisionCheck);
                   foreach (var item in monkey.currentItems)
                   {
                      Console.Write($" {item[position]} ");
                  }
                   Console.WriteLine();
                }
            }
           var printRounds = new List<int> { 0, 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };



          if (printRounds.Contains(round))
          {
               Console.WriteLine();
               Console.WriteLine($"== After round {round} ==");
               foreach (var monkey in monkeyList)
               {
                    Console.WriteLine($"Monkey {monkey.monkeyNumber} inspected items {monkey.inspetionTotal} times.");
               }
               Console.WriteLine();
                var orderedmonkeyList = monkeyList.OrderByDescending(x => x.inspetionTotal).ToList();
                BigInteger monkeyBusiness = BigInteger.Parse(orderedmonkeyList[0].inspetionTotal.ToString()) * BigInteger.Parse(orderedmonkeyList[1].inspetionTotal.ToString());
               Console.WriteLine($"Level of Monkey Business: {monkeyBusiness}");
               Console.WriteLine();
          }
        }


        if (round == 10000) break;


        //update all items in currentItems based on the monkeys operation
        foreach (var monkey in monkeyList)
        {
            monkey.inspetionTotal = monkey.inspetionTotal + monkey.currentItems.Count();
            var divisorpostion = primeDivisorList.IndexOf(monkey.divisionCheck);

            if (monkey.operation == "*")
            {
                if (monkey.operationNumber == null)//squaring
                {
                    var tempItemList = new List<List<int>>(monkey.currentItems);
                    foreach (var item in tempItemList)
                    {
                        var newitem = new List<int>();
                        var itemcount = 0;
                        foreach (var remainder in item)
                        {
                            var newremainder = (remainder * remainder) % primeDivisorList[itemcount];
                            newitem.Add(newremainder);
                            itemcount++;
                        }
                        if (newitem[divisorpostion] == 0)
                        {
                            monkeyList[monkey.destinationOnTrue].currentItems.Add(newitem);
                        }
                        else
                        {
                            monkeyList[monkey.destinationOnFalse].currentItems.Add(newitem);
                        }
                        monkey.currentItems.Remove(item);
                    }
                }
                else //multiplication
                {
                    var tempItemList = new List<List<int>>(monkey.currentItems);
                    foreach (var item in tempItemList)
                    {
                        var newitem = new List<int>();
                        var itemcount = 0;
                        foreach (var remainder in item)
                        {
                            var newremainder = (remainder * monkey.operationNumber) % primeDivisorList[itemcount];
                            newitem.Add((int)newremainder);
                            itemcount++;
                        }
                        if (newitem[divisorpostion] == 0)
                        {
                            monkeyList[monkey.destinationOnTrue].currentItems.Add(newitem);
                        }
                        else
                        {
                            monkeyList[monkey.destinationOnFalse].currentItems.Add(newitem);
                        }
                        monkey.currentItems.Remove(item);
                    }
                }
            }

            else if (monkey.operation == "+")//addition
            {
                var tempItemList = new List<List<int>>(monkey.currentItems);
                foreach (var item in tempItemList)
                {
                    var newitem = new List<int>();
                    var itemcount = 0;
                    foreach (var remainder in item)
                    {
                        var newremainder = (remainder + monkey.operationNumber) % primeDivisorList[itemcount];
                        newitem.Add((int)newremainder);
                        itemcount++;
                    }
                    if (newitem[divisorpostion] == 0)
                    {
                        monkeyList[monkey.destinationOnTrue].currentItems.Add(newitem);
                    }
                    else
                    {
                        monkeyList[monkey.destinationOnFalse].currentItems.Add(newitem);
                    }
                    monkey.currentItems.Remove(item);
                }
            }
            else Console.WriteLine("ERROR");
        }




        round++;
    }
}


