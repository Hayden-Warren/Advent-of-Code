using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9
{
    public class PrintList
    {
        public static void run (int[,] positionList)
        {

            var hieght = 50;
            var width = 50;
           

            char[,] grid = new char[hieght, width] ;

            var position = 9;
            while (position >= 0)
            {
                if (position == 0)
                {
                    grid[15 - positionList[position, 0], positionList[position, 1] + 15] = 'H';
                }
                else
                {
                    grid[15 - positionList[position, 0], positionList[position, 1] + 15] = (char)(position + 48);
                }
                position--;
            }

            if (grid[15,15] == 0)
            {
                grid[15, 15] = 'S';
            }



            var row = 0;
            while( row< width)
            {
                var column = 0;
                while (column < hieght)
                {
                    if (grid[row, column] == 0)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(grid[row, column]);
                    }
                    column++;

                }
                Console.WriteLine();
                row++;
            } 
        }
    }
}
