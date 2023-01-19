using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public class StatisticsUnit
    {
        public List<int> Data { get; set; }
        public int failedTrials { get; set; }
        public StatisticsUnit() { 
            Data = new List<int>();
            failedTrials = 0;
        }
    }
}
