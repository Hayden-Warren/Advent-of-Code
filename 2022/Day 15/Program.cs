using System.Linq;
using System.Text.RegularExpressions;

part_1();

static void part_1()
{
    var data = File.ReadAllLines("data.txt");

    var pattern = new Regex("[0-9]+");

    var scanners = new HashSet<ScannerData>();

    var left = 0;
    var right = 0;

    foreach (var line in data)
    {
        var macthes = pattern.Matches(line);
        var sx = int.Parse(macthes[0].ToString());
        var sy = int.Parse(macthes[1].ToString());
        var bx = int.Parse(macthes[2].ToString());
        var by = int.Parse(macthes[3].ToString());

        var r = Math.Abs(sx - bx) + Math.Abs(sy - by);

        var scanner = new ScannerData(sx, sy, r, bx, by);
        scanners.Add(scanner);

        if (sx - r < left) { left = sx - r; }
        if (sx + r > right) { right = sx + r; }
    }

    var testRow = 2000000;
    var postionCount = 0;
    for (var testX = left; testX < right; testX++)
    {
        foreach (var scanner in scanners)
        {
            var distToScanner = Math.Abs(testX - scanner.SX) + Math.Abs(testRow - scanner.SY);
            if (distToScanner <= scanner.R && !(testRow == scanner.BY && testX == scanner.BX)) { postionCount++; break; }
        }
    }
    Console.WriteLine(postionCount);
}


static void part_2()
{
    var data = File.ReadAllLines("data.txt");
    var maxgrid = 4000000;
    //var maxgrid = 20;
    var pattern = new Regex("-?[0-9]+");
    var scanners = new HashSet<ScannerData>();

    foreach (var line in data)
    {
        var macthes = pattern.Matches(line);
        var sx = int.Parse(macthes[0].ToString());
        var sy = int.Parse(macthes[1].ToString());
        var bx = int.Parse(macthes[2].ToString());
        var by = int.Parse(macthes[3].ToString());

        var r = Math.Abs(sx - bx) + Math.Abs(sy - by);

        var scanner = new ScannerData(sx, sy, r , bx, by);
        scanners.Add(scanner);
    }

    for (int y = 0; y <= maxgrid; y++)
    {
        var minlist = new List<int>();
        var maxlist=new List<int>();    
        foreach (var scanner in scanners)
        {
            if(scanner.SY-scanner.R<=y && y<=scanner.SY+scanner.R)
            {
                var diffe = scanner.R - Math.Abs( (scanner.SY - y));
                var scmin = scanner.SX - diffe;
                var scmax = scanner.SX + diffe;
                minlist.Add(scmin-1);
                maxlist.Add(scmax+1);
            }
        }
        
        var testPoints = minlist.Intersect(maxlist);
        var extents=minlist.Zip(maxlist);

        foreach(var point in testPoints)
        {
            var engulfed = extents.Any(x => x.First+1<=point&&x.Second-1>=point);
            if (!engulfed)
            {
                long answer = (long)point * 4000000 + y;
                Console.WriteLine(answer);
                return;
            }
        }
    }
}


public record struct ScannerData(int SX, int SY, int R, int BX, int BY);
public record struct Coord(int X, int Y);
