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

        public static void Reset()
        {
            mut.WaitOne();
            stats.Clear();
            mut.ReleaseMutex();
        }

        public static async Task Print()
        {
            var stats = from v in await GetStats()
                        orderby v.Value.average descending
                        select v;
            foreach (var stat in stats)
            {
                stat.Value.Print();
            }
        }

        public static Task AddListAsync(string key, StatisticsUnit value)
        {
            mut.WaitOne();
            if(stats.ContainsKey(key))
            {
                stats.Remove(key);
            }
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
        public static Task WriteToDisk()
        {
            mut.WaitOne();

            return Task.CompletedTask;
        }
    }
}
