using RPGSystem.data;
using System.ComponentModel;

namespace RPGSystem
{
    internal class Program
    {
        const int TrialsPerWeapon = 1_000_000;
        const int MaximumIterations = 15;

        static async Task Main(string[] args)
        {
            WeaponDefs.Init();
            List<Task> tasks = new List<Task>();
            foreach (Weapon current in WeaponDefs.weapons)
            {
                tasks.Add(Task.Run(() => SimulateWeapon(current)));

            }
            await Task.WhenAll(tasks);
            var stats = await AsyncStatisticsManager.GetStats();
            foreach (var stat in stats)
            {
                Console.WriteLine($"{stat.Key} - Average: {stat.Value.Data.Average()} \n Median: {sorted(stat.Value.Data.ToArray())[stat.Value.Data.Count / 2]}");
            }
        }
        static async Task SimulateWeapon(Weapon input)
        {
            Console.WriteLine("Started Simulation for: " + input.Name);
            StatisticsUnit DataFrame = new StatisticsUnit();
            for (int i = 0; i < TrialsPerWeapon; i++)
            {
                Monster m = new Monster();
                int counter = 0;
                while (m.Health > 0)
                {
                    for (int attacks = 0; attacks < input.AttacksPerRound; attacks++)
                    {
                        m.Health -= input.getDamage(m);
                    }
                    counter++;
                    if (counter == MaximumIterations)
                    {
                        DataFrame.failedTrials++;
                        break;
                    }
                }
                DataFrame.Data.Add(counter);
            }
            await AsyncStatisticsManager.AddListAsync(input.Name, DataFrame);
            Console.WriteLine($"Completed simulation for: {input.Name}. \n\tNumber of failed kills in {MaximumIterations} rounds after {TrialsPerWeapon} trials: {DataFrame.failedTrials}.\n\tNumber of failures per 100,000: {(DataFrame.failedTrials * (TrialsPerWeapon / 100_000f)) / 100_000f}");
        }
        private static int[] sorted(int[] array)
        {
            int[] result = array;
            Array.Sort(result);
            return result;
        }

    }
}

