
main();

static void main()
{
    Precentbar(33, 10);
    Precentbar(67, 10);
    Precentbar(37, 04);
    Precentbar(63, 04);
    Precentbar(50, 03);
}

static void Precentbar(int percentage, int barLength)
{
    var fullSection = new string('#', (int)Math.Round(percentage/100.0*barLength-0.0001)) + new string('~', (int)Math.Round((100-percentage) / 100.0 * barLength, 0));

    Console.WriteLine( $"Progress Bar {percentage}% [{barLength}]: [{fullSection}]");
    Console.WriteLine();
}


