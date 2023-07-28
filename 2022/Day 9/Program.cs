using Day_9;


Part2();



static void Part1()
{
    var data = File.ReadAllLines("data.txt");
    //V,H
    var startposition = new int[] { 0, 0 };
    var headposition = new int[] { 0, 0 };
    //var tailposition= new int[] { 0, 0 };
    var tailTrail = new List<int[]>();

    tailTrail.Add(startposition);



    foreach (var line in data)
    {
        var direction = line.Split(' ')[0];
        var distance = Int32.Parse(line.Split(' ')[1]);

        Console.WriteLine($"=={direction}{distance}==");
        while (distance > 0)
        {



            var tailposition = tailTrail.Last();





            if (distance == 0)
            {
                throw new Exception();
            }


            switch (direction)
            {
                case "U":
                    headposition[0]++;
                    break;
                case "D":
                    headposition[0]--;
                    break;
                case "L":
                    headposition[1]--;
                    break;
                case "R":
                    headposition[1]++;
                    break;
            }

            var verticaldifference = headposition[0] - tailposition[0];
            var horizontaldifference = headposition[1] - tailposition[1];
            var newtailv = tailposition[0];
            var newtailh = tailposition[1];



            if (Math.Abs(verticaldifference) + Math.Abs(horizontaldifference) == 3)
            {
                if (Math.Abs(verticaldifference) == 2)
                {
                    newtailv = tailposition[0] + (verticaldifference / Math.Abs(verticaldifference));
                    newtailh = tailposition[1] + (horizontaldifference / Math.Abs(horizontaldifference));
                }
                else if (Math.Abs(horizontaldifference) == 2)
                {
                    newtailh = tailposition[1] + (horizontaldifference / Math.Abs(horizontaldifference));
                    newtailv = tailposition[0] + (verticaldifference / Math.Abs(verticaldifference));
                }
            }
            else if (Math.Abs(verticaldifference) == 2)
            {
                newtailv = tailposition[0] + (verticaldifference / Math.Abs(verticaldifference));
            }
            else if (Math.Abs(horizontaldifference) == 2)
            {
                newtailh = tailposition[1] + (horizontaldifference / Math.Abs(horizontaldifference));
            }


            int[] newtailposition = new int[2] { newtailv, newtailh };


            Console.WriteLine($" {newtailposition[0]}   {newtailposition[1]}");
            //Console.WriteLine($"{headposition[0]}   {headposition[1]}");
            distance--;
            tailTrail.Add(newtailposition);
        }

    }

    Console.WriteLine($"------------------------------");


    var uniqueList = new List<int[]>();
    uniqueList.Add(startposition);



    foreach (var position in tailTrail)
    {
        var dummylist = new List<int[]>();

        var uniqueCount = 0;
        foreach (var unique in uniqueList)
        {

            if (position[0] == unique[0] && position[1] == unique[1])
            {
                uniqueCount++;
            }
        }

        if (uniqueCount == 0) uniqueList.Add(position);

    }


    Console.WriteLine(uniqueList.Count());
}

static void Part2()
{
    var data = File.ReadAllLines("data.txt");
    //V,H
    var startposition = new int[] { 0, 0 };
    var positionList= new int[,] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };
    var tailTrail = new List<int[]>();




    tailTrail.Add(startposition);

    var count = 0;

    foreach (var line in data)
    {
        
        var direction = line.Split(' ')[0];
        var distance = Int32.Parse(line.Split(' ')[1]);

       
        Console.WriteLine(count);
        //Console.WriteLine($"=={direction}{distance}==");
        while (distance > 0)
        {
            switch (direction)
            {
                case "U":
                    positionList[0,0]++;
                    break;
                case "D":
                    positionList[0,0]--;
                    break;
                case "L":
                    positionList[0,1]--;
                    break;
                case "R":
                    positionList[0,1]++;
                    break;
            }
            distance--;
          
            



            var ropePosition = 1;
            
            while (ropePosition <= 9)
            {
                
                
                var forwardposition = new int[] { 0, 0 };
                forwardposition[0] = positionList[ropePosition-1, 0];
                forwardposition[1] = positionList[ropePosition - 1, 1];
                var lagposition = new int[] { 0, 0 };
                lagposition[0] = positionList[ropePosition, 0];
                lagposition[1] = positionList[ropePosition, 1];


                var verticaldifference = forwardposition[0] - lagposition[0];
                var horizontaldifference = forwardposition[1] - lagposition[1];

                var verab = Math.Abs(verticaldifference);
                var horab = Math.Abs(horizontaldifference);
                var nextto = verab == 1 && horab == 0 || horab == 1 && verab == 0;
                var diagonal = (horab + verab == 2)&& (horab==verab);



                if (nextto||diagonal)
                { 
                    break; 
                }


                var newlagv = lagposition[0];
                var newlagh = lagposition[1];



                if (Math.Abs(verticaldifference) + Math.Abs(horizontaldifference) >= 3)
                {
                    if (Math.Abs(verticaldifference) == 2)
                    {
                        newlagv = lagposition[0] + (verticaldifference / Math.Abs(verticaldifference));
                        newlagh = lagposition[1] + (horizontaldifference / Math.Abs(horizontaldifference));
                    }
                    else if (Math.Abs(horizontaldifference) == 2)
                    {
                        newlagh = lagposition[1] + (horizontaldifference / Math.Abs(horizontaldifference));
                        newlagv = lagposition[0] + (verticaldifference / Math.Abs(verticaldifference));
                    }
                }
                else if (Math.Abs(verticaldifference) == 2)
                {
                    newlagv = lagposition[0] + (verticaldifference / Math.Abs(verticaldifference));
                }
                else if (Math.Abs(horizontaldifference) == 2)
                {
                    newlagh = lagposition[1] + (horizontaldifference / Math.Abs(horizontaldifference));
                }

                positionList[ropePosition,0]=newlagv;
                positionList[ropePosition,1]=newlagh;

                               
                
                if(ropePosition==9)
                {
                    int[] newtailposition = new int[2] { newlagv, newlagh };


                    //Console.WriteLine($" {newtailposition[0]}   {newtailposition[1]}");
                   
                    tailTrail.Add(newtailposition);
                }
                ropePosition++;

                //Console.Clear();
                //Console.WriteLine($"=={direction}{distance}==");
                //PrintList.run(positionList);
            }
           
            


        }
        //PrintList.run(positionList);
        count++;
    }

    Console.WriteLine($"------------------------------");


    var uniqueList = new List<int[]>();
    uniqueList.Add(startposition);



    foreach (var position in tailTrail)
    {
        var dummylist = new List<int[]>();

        var uniqueCount = 0;
        foreach (var unique in uniqueList)
        {

            if (position[0] == unique[0] && position[1] == unique[1])
            {
                uniqueCount++;
            }
        }

        if (uniqueCount == 0) uniqueList.Add(position);

    }


    Console.WriteLine(uniqueList.Count());
}