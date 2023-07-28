using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_12
{

    public class Positiondata
    {
        new public int vertical { get; set; }
        new public int horizontal { get; set; }
        new public int height { get; set; }
        new public List<int[]>? pathToPosition { get; set; } = new List<int[]>();
    }



    public class PositionUpdate
    {
        public static List<Positiondata> run(List<Positiondata> currentPositions, List<int[]> checkedPostions, int[,] map, int[] endPosition)
        {
            var result = new List<Positiondata>();
            foreach (var position in currentPositions)
            {
                if (position.vertical == endPosition[0] && position.horizontal == endPosition[1])
                {
                    return currentPositions;
                }



                var pointsToCheck = new List<int[]>();

                var up = new int[2];
                up[0] = position.vertical - 1;
                up[1] = position.horizontal;
                pointsToCheck.Add(up);

                var down = new int[2];
                down[0] = position.vertical + 1;
                down[1] = position.horizontal;
                pointsToCheck.Add(down);

                var left = new int[2];
                left[0] = position.vertical;
                left[1] = position.horizontal - 1;
                pointsToCheck.Add(left);

                var right = new int[2];
                right[0] = position.vertical;
                right[1] = position.horizontal + 1;
                pointsToCheck.Add(right);


                foreach (var point in pointsToCheck)
                {
                    if (point[0] < 0 || point[1] < 0 || point[0] >= map.GetLength(0) || point[1] >= map.GetLength(1) || checkedPostions.Any(p => p.SequenceEqual(point)))
                    {
                        continue;
                    }
                    var checkedPointHeight = map[point[0], point[1]];
                    if (checkedPointHeight- position.height < 2)
                    {
                        var newposition = new Positiondata();
                        newposition.pathToPosition = new List<int[]>(position.pathToPosition);
                        newposition.pathToPosition.Add(point);
                        newposition.height = map[point[0], point[1]];
                        newposition.vertical = point[0];
                        newposition.horizontal = point[1];

                        result.Add(newposition);
                        checkedPostions.Add(point);
                    }
                }
            }

            //Print.run(result, checkedPostions, map, endPosition);


            var nextlevl= PositionUpdate.run(result, checkedPostions, map, endPosition);

            return nextlevl;

        }

     
    }
}




//feed list of positions and checked postions

