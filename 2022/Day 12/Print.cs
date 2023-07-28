using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_12
{
    public class Print
    {
        public static void run(List<Positiondata> currentPositions, List<int[]> checkedPostions, int[,] map, int[] endPosition)
        {
            Console.Clear();
            var bubbleFront=new List<int[]>();
           foreach(var position in currentPositions)
            {
                int[] point = { position.vertical, position.horizontal };
                bubbleFront.Add(point);
            }

            Console.WriteLine($"Current Path Move Count{currentPositions[0].pathToPosition.Count()}");

            Console.WriteLine();

            //Print map and current position
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var ij = new int[] { i, j };
                    if (bubbleFront.Any(p => p.SequenceEqual(ij)))
                    {
                        Console.Write('#');
                    }

                    else if (checkedPostions.Any(p => p.SequenceEqual(ij)))
                    {
                        var ijsymbol = '*';

                        Console.Write(ijsymbol);
                    }

                    else { Console.Write('.'); }

                    //else { Console.Write(aplaphbet.ElementAt(map[i, j])); }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Current Path Move Count: {currentPositions[0].pathToPosition.Count()}");

            Console.WriteLine();
        }

        public static void final(Positiondata route,int[,] map)
        {
            Console.Clear();
            var bubbleFront = new List<int[]>();
            var alphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var point=new int[] {i,j};
                    if (route.pathToPosition.Any(p => p.SequenceEqual(point)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('#');
                    }
                    else
                    {
                        if (map[i, j] > 22)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else if (map[i, j] > 18)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        else if (map[i, j] > 12)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (map[i, j] > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }


                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }

                        Console.Write(alphabet[map[i,j]]);
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Shortest Valid Path Move Count: {route.pathToPosition.Count()}");
            Console.WriteLine();
        }
    }
}
