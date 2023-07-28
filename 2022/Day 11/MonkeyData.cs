using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    public class MonkeyData
    {
        public int monkeyNumber { get; set; }

        public List<int> startingItems { get; set; }
        public List<List<int>>? currentItems { get; set; }

        public string operation { get; set; }
        public int? operationNumber { get; set; }

        public int divisionCheck { get; set; }

        public int destinationOnTrue { get; set; }
        public int destinationOnFalse { get; set; }

        public int inspetionTotal { get; set; }
    }




}
