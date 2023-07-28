using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_13
{
    public class MagicSortingHat : IComparer
    {
        public int Compare(object? x, object? y)
        {
            var result=Day_13.Compare.run2((string)x!, (string)y!);

            if ((bool)result!) { return -1; }
            if (result==null) { return 0; }
            else if (!(bool)result) { return 1; }

            throw new NotImplementedException();
        }
    }
}
