using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_13
{
    internal class Compare
    {   
        //takes a string "set" and returns a list of strings "sets"
        public static List<string> components(string set)
        {
            var setList=new List<string>();
            if (set == "[]") { return setList; }
            var bracketCount = 0;

            int start = 1;
            for (var i=0; i< set.Length; i ++)
            {
                switch (set[i])
                {
                    case '[':
                        bracketCount++;
                        break;

                    case ']':
                        bracketCount--;
                        break;

                    case ',':
                        if (bracketCount == 1) 
                        {
                            var newSet = set.Substring(start, i - start);
                            while(newSet.Count(t => t == '[')!= newSet.Count(t => t == ']')!) { newSet = '[' + newSet; }
                            setList.Add(newSet);
                            start=i+1 ; 
                        }
                        break;
                }
            }
            var new2Set = set.Substring(start, set.Length - start - 1);
            while (new2Set.Count(t => t == '[') != new2Set.Count(t => t == ']')!) { new2Set = '[' + new2Set; }
            setList.Add(new2Set);
            return setList;
        }


        //compares two strings and returns the true if they are in the "correct order"
        public static bool? run2(string left, string  right)
        {
            var leftList = Compare.components(left);
            var rightList = Compare.components(right);
            switch (leftList.Count(), rightList.Count())
            {
                case (0,0):
                    return null;
                case (>0, 0):
                    return false;
                case ( 0, >0):
                    return true;
            }

            for (var i = 0; i < Math.Min(leftList.Count(), rightList.Count()); i++)
            {
                var leftparsable = Int32.TryParse(leftList[i], out var leftresult);
                var rightparsable = Int32.TryParse(rightList[i], out var rightresult);

                switch(leftparsable,rightparsable )
                {
                    case (true, true):
                        if (leftresult < rightresult) { return true; }
                        else if (leftresult > rightresult) { return false; }
                        else if (leftresult == rightresult) { continue; }
                        break;

                    case (true, false):
                        var newLeft = '['+leftresult.ToString()+']';
                        var result= run2(newLeft, rightList[i]);
                        if (result != null) { return result; }
                        break;

                    case (false, true):
                        var newRight = '[' + rightresult.ToString() + ']';
                        result = run2(leftList[i], newRight);
                        if (result != null) { return result; }
                        break;

                    case (false, false):
                        result = run2(leftList[i], rightList[i]);
                        if (result != null) { return result; }
                        break;
                } 
            }
            if (leftList.Count() > rightList.Count())  { return false; }
            else if ( leftList.Count() < rightList.Count()) { return true; }
            else { return null; }
        }
    }    
}