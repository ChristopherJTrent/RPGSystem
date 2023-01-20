using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public class StatisticsUnit
    {
        public List<int> Data { get; private set; }
        public int failedTrials { get; set; }
        public double average { get; private set; }
        public int median { get; private set; }
        public StatisticsUnit() { 
            Data = new List<int>();
            failedTrials = 0;
        }
        public void UpdateDerivedValues()
        {
            average = Data.Average();
            median = sorted(Data.ToArray())[Data.Count / 2];
        }

        public void Append(int value)
        {
            Data.Add(value);
        }
        private static int[] sorted(int[] array)
        {
            int[] result = array;
            Array.Sort(result);
            return result;
        }
    }
}
