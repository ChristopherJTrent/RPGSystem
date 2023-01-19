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
        private static Dictionary<string, List<int>> stats = new Dictionary<string, List<int>>();

        public static Task AddListAsync(string key, List<int> value)
        {
            mut.WaitOne();
            stats.Add(key, value);
            mut.ReleaseMutex();
            return Task.CompletedTask;
        }
        public static Task<Dictionary<string, List<int>>> GetStats()
        {
            mut.WaitOne();
            Dictionary<string, List<int>> kvp = stats;
            mut.ReleaseMutex();
            return Task.FromResult(kvp);
        }
    }
}
