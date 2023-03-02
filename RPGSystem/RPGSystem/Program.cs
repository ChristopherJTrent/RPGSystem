using Konsole;
using RPGSystem.data;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace RPGSystem
{
    class Program
    {
        public const int TrialsPerWeapon = 100_000;
        public const int UpdatesPerWeapon = 100;
        public const int TrialsPerUpdate = TrialsPerWeapon / UpdatesPerWeapon;

        public const int MaximumIterations = 12;
        public const int AcceptableFailures = TrialsPerWeapon / 2;
        public const double TargetMinimum = 7d;
        public const double TargetMaximum = 8.9999d;
        private static bool exitFlag = false;
        static async Task Main(string[] args)
        {
            WeaponDefs.Init();
            do
            {
                await Simulate();
                await updateWeapons();
                Console.Clear();
                //AsyncStatisticsManager.Reset();
            } while (!exitFlag);
        }
        public static async Task updateWeapons()
        {
            string input;
            do
            {
                Console.Clear();
                await AsyncStatisticsManager.Print();
                Console.WriteLine("Select a weapon to edit:");
                for (int i = 0; i < WeaponDefs.weapons.Count; i++)
                {
                    var w = WeaponDefs.weapons[i];
                    Console.WriteLine($"{i}: {w.Name} - ({w.AttacksPerRound}x {w.Dice.Count}d{w.Dice.Sides}+{w.ArmorPenetration}");
                }
                Console.WriteLine("q: Exit");
                Console.WriteLine("r: Rerun Simulation with new values.");
                Console.WriteLine("d: dump values to disk");
                // ?? syntax: Left Value if it's not null, right value otherwise.
                input = Console.ReadLine() ?? "q";
                if (input.ToLower() == "q") { exitFlag = true; break; }
                if (input.ToLower() == "r") { exitFlag = false;  break; }
                if (int.TryParse(input, out int selection))
                {
                    bool changes = false;
                    while (true)
                    {
                        Console.WriteLine("Select the attribute you want to edit: ");
                        Console.WriteLine("1: Attacks per Round");
                        Console.WriteLine("2: Dice (Standard notation)");
                        Console.WriteLine("3: Armor Penetration");
                        Console.WriteLine("q: exit");
                        bool exit = false;
                        
                        bool success = false;
                        switch ((Console.ReadLine() ?? "a").ToCharArray()[0])
                        {
                            case '1':
                                int updatedAPR;
                                Console.Write("Input updated attacks per round: ");
                                while (!int.TryParse(Console.ReadLine(), out updatedAPR))
                                {
                                    Console.WriteLine("Invalid attacks per round, please type a whole number.");
                                }
                                Console.WriteLine($"Selection: {updatedAPR}");
                                WeaponDefs.weapons[selection].AttacksPerRound = updatedAPR;
                                changes = true;
                                break;
                            case '2':
                                Match match;
                                do
                                {
                                    Console.WriteLine("type in the updated dice value in standard form (XdY), then press enter.");
                                    string value = Console.ReadLine() ?? $"{WeaponDefs.weapons[selection].Dice.Count}d{WeaponDefs.weapons[selection].Dice.Sides}";
                                    match = Roller.Validate(value, out success);
                                } while (!success);
                                WeaponDefs.weapons[selection].Dice = new Roller(match);
                                changes = true;
                                break;
                            case '3':
                                int updatedAP;
                                while (!int.TryParse(Console.ReadLine(), out updatedAP))
                                {
                                    Console.WriteLine("Invalid Armor Penetration, please type a whole number.");
                                }
                                WeaponDefs.weapons[selection].ArmorPenetration = updatedAP;
                                changes = true;
                                break;
                            case 'q':
                                exit = true;
                                break;
                            default:
                                break;
                        }
                        if (exit) { exitFlag = true; break; }
                    }
                    WeaponDefs.weapons[selection].updated = changes;
                }
            } while (true);
        }
        public static async Task Simulate()
        {
            List<Task> tasks = new List<Task>();
            var bars = new List<ProgressBar>();
            foreach (Weapon current in WeaponDefs.weapons)
            {
                if (!current.updated) { continue;}
                var bar = new ProgressBar(UpdatesPerWeapon);
                bars.Add(bar);
                bar.Refresh(0, current.Name);
                tasks.Add(SimulateWeapon(current, bar));
            }
            await Task.WhenAll(tasks);
        }
        static async Task SimulateWeapon(Weapon input, ProgressBar bar)
        {
            //Console.Write("Started Simulation for: " + input.Name);
            List<Task> tasks= new List<Task>(); 
            StatisticsUnit DataFrame1 = new StatisticsUnit();
            DataFrame1.Name = input.Name;
            Console.WriteLine();
            for (int iter = 0; iter < UpdatesPerWeapon; iter++)
            {
                tasks.Add(DoSimulationSegment(input, DataFrame1, bar));
            }
            await Task.WhenAll(tasks); 
            DataFrame1.UpdateDerivedValues();
            await AsyncStatisticsManager.AddListAsync(input.Name, DataFrame1);
            input.updated = false;
        }

        public static async Task DoSimulationSegment(Weapon input, StatisticsUnit unit, ProgressBar bar) {
            int failures = 0;
            List<int> Trials = new();
            for (int i = 0; i < TrialsPerUpdate; i++)
            {
                Monster m1 = new();
                int result1 = await SimulateKill(input, m1);
                if (result1 == MaximumIterations) failures++;
                Trials.Add(result1);
            }
            await unit.AppendAsync(Trials);
            await unit.AddFailuresAsync(failures);
            bar.Next(input.Name);
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
                return counter;
            });

        }
    }
}

