using System.Diagnostics;


Part_1();

Part_1Hashmode();

Part_2("testdata.txt");

Part_2("data.txt");

void Part_1()
{
    var watch =Stopwatch.StartNew();
    var rocks = 2022;
    var rockOption = "-+>|#";
    var wind = File.ReadAllText("data.txt");
    var windIndex = 0;
    var tower = new List<string>();


    for (int i = 0; i < rocks; i++)
    {
        var rock = rockOption[i % 5];//remainder of the i/5 to teh rock theat will fall 
        var rockAbleToFall = true;
        var rockPatern = RockPattern(rock);
        tower.AddRange(Enumerable.Repeat(".......", 7)); //add seven spaces of empty air above the tower

        var rowBellowRock = tower.Count - 5;

        while (rockAbleToFall)
        {
            var windDirection = wind[windIndex % wind.Length];

            //checks if the rock is able to be pushed Left or Right          
            var rockPushAbleLeft = true;
            var rockPushAbleRight = true;
            for (var row = 0; row < rockPatern.Count; row++)
            {
                var exmainedRockRow = rockPatern[rockPatern.Count - 1 - row];
                var firstAt = exmainedRockRow.IndexOf('@');
                var lastAt = firstAt + exmainedRockRow.Count(x=>x=='@') - 1;

                var examinedTowerRow = tower[rowBellowRock + 1 + row];

                if (firstAt == 0 || examinedTowerRow[firstAt - 1] == '#')
                {
                    rockPushAbleLeft = false;
                }
                if (lastAt == 6 || examinedTowerRow[lastAt + 1] == '#')
                {
                    rockPushAbleRight = false;
                }
            }

            // if able to push the rock left then pushes the rock left
            if (rockPushAbleLeft && windDirection == '<')
            {
                var newRockPatern = new List<string>();
                foreach (var line in rockPatern)
                {
                    var newLine = line;
                    newLine = newLine.Remove(0, 1);
                    newLine = newLine.Insert(6, ".");
                    newRockPatern.Add(newLine);
                }

                rockPatern = newRockPatern;
            }

            // if able to push the rock right then pushes the rock right
            else if (rockPushAbleRight && windDirection == '>')
            {
                var newRockPatern=new List<string>();
                foreach (var line in rockPatern)
                {
                    var newLine = line;
                    newLine= newLine.Remove(6, 1);
                    newLine = newLine.Insert(0, ".");
                    newRockPatern.Add(newLine);
                }

                rockPatern=newRockPatern;
            }

            //updates wind index for next interation
            windIndex++;

            //check if it can fall
            if (rowBellowRock ==-1) { rockAbleToFall = false; break; }//if the rock is at the bottom of the tower break out of the loop
            else if (!tower[rowBellowRock].Contains('#')) { rowBellowRock--; continue; }//if there are no rocks in the rock below the rock then drop the rock and continue to the next loop 
            else //cheack each row of the rock and each index of that row, if any part of the rock(@) has a rock(#) in same index of the tower row below it then set fallable to false and break 
            {
                for (var row = 0; row < rockPatern.Count; row++)
                {
                    var exmainedTowerRow = tower[rowBellowRock + row];
                    var examinedRockRow = rockPatern[rockPatern.Count - 1 - row];
                    for (var index = 0; index < 7; index++)
                    {
                        if (exmainedTowerRow[index] == '#' && examinedRockRow[index] == '@')
                        {
                            rockAbleToFall = false;
                            break;
                        }
                    }
                    if (!rockAbleToFall) { break; }
                }
                if (!rockAbleToFall) { break; }
                else { rowBellowRock--; }
            }
        }

        //adds the rock to the tower
        for (var row = 0; row < rockPatern.Count; row++)
        {
            var towerRowToChange = rowBellowRock + 1 + row;
            var newRow = tower[towerRowToChange];
            var rockRowToAdd = rockPatern.Count - row - 1;

            for (var index = 0; index < 7; index++)
            {                
                if (rockPatern[rockRowToAdd][index] == '@')
                {                    
                    newRow=newRow.Remove(index, 1);
                    newRow=newRow.Insert(index, "#");
                }
            }
            tower.RemoveAt(towerRowToChange);
            tower.Insert(towerRowToChange,newRow);
        }
        tower.RemoveAll(x => x == "......."); //removes all the empty rows(only exist above the top rock)


        //PrintTower(tower);
        
       //Console.Clear();
    }


    Console.WriteLine( $"Part 1 Answer: {tower.Count} [{watch.ElapsedMilliseconds}]");

}

void Part_1Hashmode()
{
    var watch = Stopwatch.StartNew();
    var rocks = 2022;
    var rockOption = "-+>|#";
    var wind = File.ReadAllText("data.txt");
    var windIndex = 0;
    var tower = new HashSet<(int,int)>();
    var towerheight = 0;



    var test = 0;

    for (var i = 0; i < rocks; i++)
    {
        var rock = rockOption[i % 5];//remainder of the i/5 to the rock theat will fall 
        var rockAbleToFall = true;
        var rockPatern = RockHash(rock, towerheight+4);

        while (rockAbleToFall)
        {
            var windDirection = wind[windIndex % wind.Length];
            var change = windDirection == '>' ? 1 : -1;

            //checks if the rock is able to be pushed Left or Right      
            var rockPushAble = true;   
            var newRockPatern = new HashSet<(int, int)>();   

            foreach(var coorodinate in rockPatern)
            {
                var check = (coorodinate.row, coorodinate.index + change);
                newRockPatern.Add(check);
                if (check.Item2==-1 || check.Item2 == 7||tower.Contains(check) ) { rockPushAble = false; }
            }

            // if able to push the rock then pushes the rock
            if (rockPushAble )
            {           
                rockPatern = newRockPatern;
            }

            //updates wind index for next interation
            windIndex++;

            //if the rock is at the bottom of the tower break out of the loop
            if (rockPatern.First().row==1) { rockAbleToFall = false; break; }

            //
            var bottomPattern=rockPatern.Select(x=>(x.row-1,x.index)).ToHashSet();            
                        
            //if the rock is able to fall then reduce the row by one and continue
            if (bottomPattern.Any(x => tower.Contains(x))) 
            {
                rockAbleToFall = false;
                break;
            }
            else { rockPatern = bottomPattern; continue; }
        }       
        
        //adds the rock to the tower        
        foreach(var coorodinate in rockPatern)
        {
            tower.Add(coorodinate);
        }

        towerheight = Math.Max(towerheight, rockPatern.Last().Item1);
    }
    
    Console.WriteLine($"Part 1 Answer: {towerheight} [{watch.ElapsedMilliseconds} ms] (Hashset)");
}

void Part_2(string datasource)
{
    var watch = Stopwatch.StartNew();
    ulong rocks = 1000000000000;
    var rockOption = "-+>|#";
    var wind = File.ReadAllText(datasource);
    var windlength = wind.Length;

    var windIndex = 0;
    
    var initalTower = new HashSet<(int, int)>();
    var topOfInitalTower = 0;

    var rockindexofonethouandthrock = new int();
    var windindexat1000throckspawn = new int();
    ulong numberOfRocksInLoop = 0;
    var heightAtRepeat = 0;
    var hightlist = new List<int>();

    // Run throught the tower untill a loop is found, start looking for a loop at the 1000ith rock to let it "settle in"
    for (ulong i = 0; i <= 5000; i++)
    {
        var rock = rockOption[((int)i) % 5];
        var rockAbleToFall = true;
        var rockPatern = RockHash(rock, topOfInitalTower + 4);

        //Colects the Infomation on the tower when the 1000th rock starts to fall
        if (i == 999) 
        { 
            rockindexofonethouandthrock = ((int)i) % 5;
            windindexat1000throckspawn = windIndex;
        }

        // list of heights from teh 100th rock and up
        if (i > 999) { hightlist.Add(topOfInitalTower); }

        //colects infomation and breaks the tower creation when a rock and wind pattern is found matching the 1000th rock
        if (((int)i) % 5 == rockindexofonethouandthrock && windIndex== windindexat1000throckspawn && i > 999) 
        { 
            numberOfRocksInLoop = i-999; 
            heightAtRepeat = topOfInitalTower; 
            break; 
        }

        //moves rock untill it comes to rest
        while (rockAbleToFall)
        {
            //create create the change based on the wind direction
            if (windIndex % wind.Length == 0) { windIndex = 0; }//trunkates windindex to stop it running away
            var windDirection = wind[windIndex];
            var change = windDirection == '>' ? 1 : -1;

            //creates the pusd block and pushes it if it is able
            var pushpattern = rockPatern.Select(x => (x.row, x.index + change)).ToHashSet();
            if (!pushpattern.Any(x => initalTower.Contains(x) || x.Item2 == -1 || x.Item2 == 7))
            {
                rockPatern = pushpattern;
            }

            //updates wind index for next interation
            windIndex++;

            //if the rock is at the bottom of the tower break out of the loop
            if (rockPatern.First().row == 1) { rockAbleToFall = false; break; }

            // creates the pattern for if the rock could fall
            var bottomPattern = rockPatern.Select(x => (x.row - 1, x.index)).ToHashSet();
            if (bottomPattern.Any(x => initalTower.Contains(x)))
            {
                rockAbleToFall = false;
                break;
            }
            else { rockPatern = bottomPattern; continue; }
        }
 
        //adds the rock to the tower        
        foreach (var coorodinate in rockPatern) { initalTower.Add(coorodinate); }      

        //updates the top of tower if the new rock pattern would increase it
        topOfInitalTower = Math.Max(topOfInitalTower, rockPatern.Last().Item1);
    }

    //calculates the number of rows in the repeating secions
    var distancebetweenrepeats = heightAtRepeat - hightlist[0];    

    //number of rocks remaining after the first 1000 rocks and the rock loops
    var remainingrocks = (rocks - 1000) % numberOfRocksInLoop;

    // number of loops required
    var goesingto = (rocks - 1000) / numberOfRocksInLoop;

    //retervies the end tower height based on how many rocks are remaining
    var endTowerHeight = hightlist[(int)remainingrocks]- hightlist[0];
    
    //calculates the total tower height
    ulong totalHeight = (ulong)hightlist[0] + goesingto * (ulong)distancebetweenrepeats + (ulong)endTowerHeight;

    Console.WriteLine($"Part 2 Answer: {totalHeight} [{watch.ElapsedMilliseconds} ms] ");
}

void PrintTower(List<string> tower)
{

    var printTower = new List<string>(tower);

    printTower.Reverse();
    foreach(var line in printTower)
    {
        Console.WriteLine("|"+line+"|");
    }
    Console.WriteLine("+-------+");
}

void PrintHash(HashSet<(int, int)> tower,int towerheight)
{
    
    for (int row = towerheight; row > 0; row--)
    {
        Console.Write("|");
        for (int index = 0; index < 7; index++)
        {
            if (tower.Contains((row, index))) { Console.Write('#'); }
            else{ Console.Write(' '); }
        }
        Console.Write("|");
        Console.WriteLine();
    }
    //Console.WriteLine("+-------+");
}

void PrintBottom(HashSet<(int, int)> tower, int towerheight)
{
    
    for (int row = towerheight; row > 0; row--)
    {
        Console.Write("|");
        for (int index = 0; index < 7; index++)
        {
            if (tower.Contains((row, index))) { Console.Write('#'); }
            else { Console.Write(' '); }
        }
        Console.Write($"| {row}");


        Console.WriteLine();
    }
    Console.WriteLine("+-------+");
}

void PrintNumberedTower(HashSet<(int, int)> tower, int towerheight)
{
    var maxRow = tower.MaxBy(x => x.Item1).Item1;

    for (int row = maxRow; row > maxRow - towerheight; row--)
    {
        Console.Write("|");
        for (int index = 0; index < 7; index++)
        {
            if (tower.Contains((row, index))) { Console.Write('#'); }
            else { Console.Write(' '); }
        }
        Console.Write($"| {row}");


        Console.WriteLine();
    }
    //Console.WriteLine("+-------+");
}

void PrintTopFour (HashSet<(int, int)> tower, int towerheight)
{

    for (int row = towerheight; row > towerheight-4; row--)
    {
        Console.Write("|");
        for (int index = 0; index < 7; index++)
        {
            if (tower.Contains((row, index))) { Console.Write('#'); }
            else { Console.Write(' '); }
        }
        Console.Write("|");
        Console.WriteLine();
    }
    Console.WriteLine("+-------+");
    Console.WriteLine();
}

static List<string> RockPattern(char rock)
{
    var rockPattern=new List<string>();
    switch (rock)
    {
        case '-':
            rockPattern.Add("..@@@@.");
            break;

        case '+':
            rockPattern.Add("...@...");
            rockPattern.Add("..@@@..");
            rockPattern.Add("...@...");
            break;

        case '>':
            rockPattern.Add("....@..");
            rockPattern.Add("....@..");
            rockPattern.Add("..@@@..");
            break;

        case '|':
            rockPattern.Add("..@....");
            rockPattern.Add("..@....");
            rockPattern.Add("..@....");
            rockPattern.Add("..@....");
            break;

        case '#':
            rockPattern.Add("..@@...");
            rockPattern.Add("..@@...");
            break;
    }
    return rockPattern;
}

static HashSet<(int row,int index)> RockHash(char rock, int spawnHeight)
{
    var rockPattern = new HashSet<(int, int)>();
    switch (rock)
    {
        case '-':
            rockPattern.Add((spawnHeight+0, 2));
            rockPattern.Add((spawnHeight+0, 3));
            rockPattern.Add((spawnHeight+0, 4));
            rockPattern.Add((spawnHeight+0, 5));
            break;

        case '+':
            rockPattern.Add((spawnHeight + 0, 3));
            rockPattern.Add((spawnHeight + 1, 2));
            rockPattern.Add((spawnHeight + 1, 3));
            rockPattern.Add((spawnHeight + 1, 4));
            rockPattern.Add((spawnHeight + 2, 3));
            break;

        case '>':
            rockPattern.Add((spawnHeight + 0, 2));
            rockPattern.Add((spawnHeight + 0, 3));
            rockPattern.Add((spawnHeight + 0, 4));
            rockPattern.Add((spawnHeight + 1, 4));
            rockPattern.Add((spawnHeight + 2, 4));
            break;

        case '|':
            rockPattern.Add((spawnHeight + 0, 2));
            rockPattern.Add((spawnHeight + 1, 2));
            rockPattern.Add((spawnHeight + 2, 2));
            rockPattern.Add((spawnHeight + 3, 2));
            break;

        case '#':
            rockPattern.Add((spawnHeight + 0, 2));
            rockPattern.Add((spawnHeight + 1, 2));
            rockPattern.Add((spawnHeight + 0, 3));
            rockPattern.Add((spawnHeight + 1, 3));
            break;
    }
    return rockPattern;
}
