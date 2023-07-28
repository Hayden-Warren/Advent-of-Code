Part2();

static void Part1()
{
    var data = File.ReadAllLines("data.txt");

    var isVisable = new List<string>();
    var countWidth = 1;
    var countheight = 1;
    var maxwidth = data[0].Length;
    var maxheigth = data.Count();
    var treecount = maxwidth * 2 + maxheigth * 2 - 4;

    foreach (var line in data)
    {
        countWidth = 1;
        foreach (var item in line)
        {
            if (countheight == 1 || countWidth == 1 || countWidth == maxwidth || countheight == maxheigth)
            {

            }
            else
            {
                var height = Int32.Parse(item.ToString());
                var vertical = "";
                foreach (var line2 in data)
                {
                    vertical = vertical + line2[countWidth - 1];
                }
                var horizontal = line;


                var toptrees = vertical.Substring(0, countheight - 1);
                var bottomtrees = vertical.Substring(countheight);
                var lefttrees = horizontal.Substring(0, countWidth - 1);
                var righttrees = horizontal.Substring(countWidth);

                var treeData = new List<string>();
                treeData.Add(toptrees);
                treeData.Add(bottomtrees);
                treeData.Add(lefttrees);
                treeData.Add(righttrees);

                var visable = "";

                foreach (var range in treeData)
                {
                    var rangevisible = "Y";
                    foreach (var tree in range)
                    {
                        var treeheight = Int32.Parse(tree.ToString());

                        if (treeheight >= height)
                        {
                            rangevisible = "N";
                        }

                    }
                    visable = visable + rangevisible;

                }
                if (visable.Contains("Y"))
                {
                    treecount++;
                }
            }
            countWidth++;
        }
        countheight++;
    }
    Console.WriteLine(treecount);
}

static void Part2()
{
    var data = File.ReadAllLines("data.txt");

    var isVisable = new List<string>();
    var countheight = 1;
    var maxwidth = data[0].Length;
    var maxheight = data.Count();
    var maxvisablity = new List<int>(); 

    foreach (var line in data)
    {
        if (countheight == 1 ||  countheight == maxheight)
        {
            countheight++;
            continue;
        }

        var countWidth = 1;
        foreach (var item in line)
        {
            if ( countWidth == 1 || countWidth == maxwidth)
            {
                countWidth++;
                continue;
            }

            var height = Int32.Parse(item.ToString());
            var vertical = "";
            foreach (var line2 in data)
            {
                vertical = vertical + line2[countWidth - 1];
            }
            var horizontal = line;

            var toptrees = vertical.Substring(0, countheight - 1);
            char[] toparray = toptrees.ToCharArray();
            Array.Reverse(toparray);
            toptrees = new string(toparray);

            var bottomtrees = vertical.Substring(countheight);
            
            var lefttrees = horizontal.Substring(0, countWidth - 1);
            char[] leftarray = lefttrees.ToCharArray();
            Array.Reverse(leftarray);
            lefttrees=new string(leftarray);

            var righttrees = horizontal.Substring(countWidth);

            var treeData = new List<string>();
            treeData.Add(toptrees);
            treeData.Add(bottomtrees);
            treeData.Add(lefttrees);
            treeData.Add(righttrees);

            var visable = new List<int>();
            visable.Add(height);

            foreach (var direction in treeData)
            {
                var view = 0;
                foreach (var tree in direction)
                {
                    var treeheight = Int32.Parse(tree.ToString());
                    
                    if (treeheight >= height)
                    {
                        view++;
                        break;           
                    }
                    view++;
                }
                visable.Add(view);
            }

            var totalvisabliity = visable[1] * visable[2] * visable[3] * visable[4];
            visable.Add(totalvisabliity);



            if (maxvisablity.Count()<1||totalvisabliity > maxvisablity[5])
            {
                maxvisablity= visable;
            }      

            countWidth++;
        }
        countheight++;
    }
    Console.Write(maxvisablity[5]);
}