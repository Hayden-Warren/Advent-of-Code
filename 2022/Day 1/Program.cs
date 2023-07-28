// See https://aka.ms/new-console-template for more information



// create elfSumList
// elfSum=0
// for each line in txt

// if not null add line to elfSum
// if null add elfSum to elfSumList then zzero elfSum
// return position of max from elfsumTotal



List<int> elfSumList = new List<int>();
var elfsum=0;
var path = @".\ElfCalories.txt";



string[] caloriesArray = File.ReadAllLines(path);

foreach (var cal in caloriesArray)
{
    if (cal is "")
    {
        elfSumList.Add(elfsum);
        elfsum = 0;
    }
    else
    {
    int calInt = Int32.Parse(cal);
    elfsum = elfsum +  calInt;
    }
}


elfSumList.Sort();
elfSumList.Reverse();
var top3total = elfSumList[0] + elfSumList[1] + elfSumList[2]; 


Console.WriteLine(top3total);


