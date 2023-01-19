using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public static class AsyncStatisticsManager
    {
        private static Mutex mut = new Mutex();
        private static Dictionary<string, StatisticsUnit> stats = new Dictionary<string, StatisticsUnit>();

        public static Task AddListAsync(string key, StatisticsUnit value)
        {
            mut.WaitOne();
            stats.Add(key, value);
            mut.ReleaseMutex();
            return Task.CompletedTask;
        }
        public static Task<Dictionary<string, StatisticsUnit>> GetStats()
        {
            mut.WaitOne();
            Dictionary<string, StatisticsUnit> kvp = stats;
            mut.ReleaseMutex();
            return Task.FromResult(kvp);
        }
    }
}
