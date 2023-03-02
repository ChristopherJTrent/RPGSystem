using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public class StatisticsUnit
    {
        public string? Name { get; set; }
        public List<int> Data { get; private set; }
        public int failedTrials { get; set; }
        public double average { get; private set; }
        public int median { get; private set; }
        private Mutex mutex = new(false);
        public StatisticsUnit() { 
            Data = new List<int>();
            failedTrials = 0;
        }
        public void UpdateDerivedValues()
        {
            try
            {
                average = Data.Average();
                median = sorted(Data.ToArray())[Data.Count / 2];
            }catch (Exception)
            {
                average = 0;
                median = 0;
            }
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
        public void Print()
        {
            Console.Write($"{Name} - Average: ");
            Console.ForegroundColor =
                average < Program.TargetMinimum ?
                    ConsoleColor.Yellow :
                        average > Program.TargetMaximum ?
                            ConsoleColor.Red :
                            ConsoleColor.Green;
            Console.Write($"{average}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"\n\tMedian: {median}\n");
        }
        public async Task AppendAsync(List<int> list)
        {
            await Task.Run(() =>
            {
                mutex.WaitOne();
                Data.AddRange(list);
                mutex.ReleaseMutex();
            });
        }
        public async Task AddFailuresAsync(int failures)
        {
            await Task.Run(() =>
            {
                mutex.WaitOne();
                failedTrials += failures;
                mutex.ReleaseMutex();
            });
        }
    }
}
