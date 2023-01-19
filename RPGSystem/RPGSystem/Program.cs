using RPGSystem.data;
using System.ComponentModel;

namespace RPGSystem
{
    internal class Program
    {
        const int TrialsPerWeapon = 100_000;
        const int MaximumIterations = 20;

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
            foreach(var stat in stats)
            {
                Console.WriteLine($"{stat.Key} - Average: {stat.Value.Average()}");
            }
        }
        static async Task SimulateWeapon(Weapon input)
        {
            Console.WriteLine("Started Simulation for: " + input.Name);
            List<int> trials = new List<int>();
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
                    if(counter == MaximumIterations)
                    {
                        //Console.WriteLine($"{input.Name} failed to kill the monster in {MaximumIterations} rounds");
                        break;
                    }
                }
                trials.Add(counter);
            }
            await AsyncStatisticsManager.AddListAsync(input.Name, trials);
            Console.WriteLine($"Completed simulation for: {input.Name}");
        }
    }
}

