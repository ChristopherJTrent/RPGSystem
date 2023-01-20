using RPGSystem.data;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace RPGSystem
{
    internal class Program
    {
        const int TrialsPerWeapon = 10000;
        const int MaximumIterations = 20;
        const int AcceptableFailures = 250_000;
        const double TargetMinimum = 6.0d;
        const double TargetMaximum = 6.1d;

        static async Task Main(string[] args)
        {
            WeaponDefs.Init();
            List<Task> tasks = new List<Task>();
            foreach (Weapon current in WeaponDefs.weapons)
            {
                tasks.Add(Task.Run(() => SimulateWeapon(current)));

            }
            await Task.WhenAll(tasks);
            var stats = from v in await AsyncStatisticsManager.GetStats()
                        orderby v.Value.average descending
                        select v;

            foreach (var stat in stats)
            {
                Console.Write($"{stat.Key} - Average: ");
                Console.ForegroundColor =
                    stat.Value.average < TargetMinimum ?
                        ConsoleColor.Yellow :
                            stat.Value.average > TargetMaximum ?
                                ConsoleColor.Red :
                                ConsoleColor.Green;
                Console.Write($"{stat.Value.average}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"\n\tMedian: {stat.Value.median}\n");

            }
        }
        static async Task SimulateWeapon(Weapon input)
        {
            Console.WriteLine("Started Simulation for: " + input.Name);
            StatisticsUnit DataFrame = new StatisticsUnit();
            for (int i = 0; i < TrialsPerWeapon; i++)
            {
                Monster m1 = new();
                int result = await SimulateKill(input, m1);
                if (result == MaximumIterations) DataFrame.failedTrials++;
                DataFrame.Append(result);
            }
            DataFrame.UpdateDerivedValues();
            Console.WriteLine("Updated Values " + DataFrame.failedTrials);
            await AsyncStatisticsManager.AddListAsync(input.Name, DataFrame);
            if (DataFrame.failedTrials > AcceptableFailures) { Console.ForegroundColor = ConsoleColor.Red; }
            Console.WriteLine($"Completed simulation for: {input.Name}.");
            Console.WriteLine($"\tNumber of failed kills in {MaximumIterations} rounds after {TrialsPerWeapon} trials: {DataFrame.failedTrials}.");
            Console.WriteLine($"\tFailiure Rate Percentage {DataFrame.failedTrials / (float)TrialsPerWeapon * 100f}%");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static async Task<int> SimulateKill(Weapon w, Monster m)
        {
            
            return await Task.Run(() =>
            {
                int counter = 0;
                while (m.Health > 0)
                {
                    counter++;
                    for (int attacks = 0; attacks < w.AttacksPerRound; attacks++)
                    {
                        m.Health -= w.getDamage(m);
                    }
                    
                    if (counter == MaximumIterations)
                    {
                        break;
                    }
                }
                return Task.FromResult(counter);
            });
        }
    }
}

