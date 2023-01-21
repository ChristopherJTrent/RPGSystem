using RPGSystem.data;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace RPGSystem
{
    internal class Program
    {
        const int TrialsPerWeapon = 10000;
        const int MaximumIterations = 20;
        const int AcceptableFailures = TrialsPerWeapon/2;
        const double TargetMinimum = 7d;
        const double TargetMaximum = 8.9999d;

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
            StatisticsUnit DataFrame1 = new StatisticsUnit();
            StatisticsUnit DataFrame2 = new StatisticsUnit();
            for (int i = 0; i < TrialsPerWeapon; i++)
            {
                Monster m1 = new();
                Monster m2 = new(2,4);
                int result1 = await SimulateKill(input, m1);
                if (result1 == MaximumIterations) DataFrame1.failedTrials++;
                int result2 = await SimulateKill(input, m2);
                if (result2 == MaximumIterations) DataFrame2.failedTrials++;
                DataFrame1.Append(result1);
                DataFrame2.Append(result2);
            } 
            DataFrame1.UpdateDerivedValues();
            DataFrame2.UpdateDerivedValues();
            await AsyncStatisticsManager.AddListAsync(input.Name, DataFrame1);
            await AsyncStatisticsManager.AddListAsync('_'+input.Name, DataFrame2);
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

