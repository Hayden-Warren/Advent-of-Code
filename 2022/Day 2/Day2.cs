Part2();

static void Part1()
{
    string[] input = File.ReadAllLines(@"PuzzleInput.txt");

    string[] letterKey = { "ABC", "XYZ", "RPS" };
    string[] winingPlays = { "RS", "SP", "PR" };


    var total = 0;


    foreach (string Line in input)
    {
        var oponentCode = Line[0];
        var oponentPosition = letterKey[0].IndexOf(oponentCode);
        var oponentPlay = letterKey[2].ElementAt(oponentPosition);

        var myCode = Line[2];
        var myPosition = letterKey[1].IndexOf(myCode);
        var myPlay = letterKey[2].ElementAt(myPosition);

        char[] plays = { myPlay, oponentPlay };
        string game = new string(plays);

        var score = 0;
        var result = "Lose";

        if (winingPlays.Contains(game))
        {
            score = 6;
            result = "Win!";
        }

        else if (myPlay == oponentPlay)
        {
            score = 3;
            result = "Draw";
        }

        if (myPlay == 'R')
        {
            score = score + 1;
        }
        else if (myPlay == 'P')
        {
            score = score + 2;
        }
        else if (myPlay == 'S')
        {
            score = score + 3;
        }

        Console.WriteLine($"{myPlay}  {oponentPlay}  score: {score}");




        total = total + score;

    }

    Console.WriteLine(total);
}

static void Part2()
{
    string[] input = File.ReadAllLines(@"PuzzleInput.txt");

    string[] letterKey = { "ABC", "XYZ", "RPS" };
    string[] resultKey = {  "SRP" ,  "RPS" ,  "PSR"  }; //opponent plays Rock,Paper,Sisors      then choose  Lose,Draw, Win!

    var total = 0;

    foreach (string Line in input)
    {
        var opponentCode = Line[0];
        var opponentPosition = letterKey[0].IndexOf(opponentCode);
        var opponentPlay = letterKey[2].ElementAt(opponentPosition);

        var gameResult= Line[2];
        var gamePosition = letterKey[1].IndexOf(gameResult);

        var myPlay = resultKey[opponentPosition].ElementAt(gamePosition);
        var myPlayposition = letterKey[2].IndexOf(myPlay);

        var score = myPlayposition+1 + (gamePosition) * 3;

        total = total + score;

        Console.WriteLine($"{myPlay}    {opponentPlay}        {score}          ");
    }

    Console.WriteLine(total);
}
