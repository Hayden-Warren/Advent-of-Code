using Day_7;

Part2();

static void Part1()
{
    var data = File.ReadAllLines(@"data.txt");
    var currentdirectory = new Folder("/", null);
    var top = currentdirectory;

    var dataposition = 0;
    foreach (var line in data)
    {
        var code = line.Substring(0, 4);

        switch (code)
        {
            case "$ cd":

                if (line[5] == '/')
                {
                    break;
                }
                if (line[5] == '.')
                {
                    currentdirectory = currentdirectory.parent;
                    break;
                }
                var directoryname = line.Substring(5);
                currentdirectory = currentdirectory.folders.First(x => x.name == directoryname);
                break;

            case "$ ls":

                var incrament = 1;

                while (true)
                {
                    if ((dataposition + incrament) >= data.Length)
                    {
                        break;
                    }

                    var lowerLine = data[dataposition + incrament];


                    if (lowerLine[0] == '$')
                    {
                        break;
                    }
                    else if (lowerLine[0] == 'd')
                    {
                        var newdirectoryname = lowerLine.Substring(4);
                        var newdirectory = new Folder($"{newdirectoryname}", currentdirectory);
                        currentdirectory.folders.Add(newdirectory);
                    }
                    else
                    {
                        var splitstring = lowerLine.Split(' ');
                        currentdirectory.filesizes.Add(Int32.Parse(splitstring[0]));
                        currentdirectory.files.Add(splitstring[1]);
                    }
                    incrament++;
                }
                break;
        }
        dataposition++;
    }

    top = filesize.calculate(top);
    var listofsizes = filesize.list(top);
    var total = filesize.total(listofsizes);

    Console.WriteLine(total);
    Console.WriteLine("done!");
}

static void Part2()
{
    var data = File.ReadAllLines(@"data.txt");
    var currentdirectory = new Folder("/", null);
    var top = currentdirectory;

    var dataposition = 0;

    foreach (var line in data)
    {
        var code = line.Substring(0, 4);

        switch (code)
        {
            case "$ cd":

                if (line[5] == '/')
                {
                    break;
                }
                if (line[5] == '.')
                {
                    currentdirectory = currentdirectory.parent;
                    break;
                }
                var directoryname = line.Substring(5);
                currentdirectory = currentdirectory.folders.First(x => x.name == directoryname);
                break;

            case "$ ls":

                var incrament = 1;

                while (true)
                {
                    if ((dataposition + incrament) >= data.Length)
                    {
                        break;
                    }

                    var lowerLine = data[dataposition + incrament];


                    if (lowerLine[0] == '$')
                    {
                        break;
                    }
                    else if (lowerLine[0] == 'd')
                    {
                        var newdirectoryname = lowerLine.Substring(4);
                        var newdirectory = new Folder($"{newdirectoryname}", currentdirectory);
                        currentdirectory.folders.Add(newdirectory);
                    }
                    else
                    {
                        var splitstring = lowerLine.Split(' ');
                        currentdirectory.filesizes.Add(Int32.Parse(splitstring[0]));
                        currentdirectory.files.Add(splitstring[1]);
                    }
                    incrament++;
                }
                break;
        }
        dataposition++;
    }

    top = filesize.calculate(top);
    var dirlist = filesize.dirlist(top);
    
    var totalspace = 70000000;
    var nededspace = 30000000;
    var usedspaces = top.foldersize;
    var requiredds = nededspace + usedspaces - totalspace;
    
    var viablelist = dirlist.Where(x=> x.foldersize>requiredds).ToList();
    var deltedfile = viablelist.Min(x => x.foldersize);
    
    Console.WriteLine(deltedfile);
    Console.WriteLine("done!");
}