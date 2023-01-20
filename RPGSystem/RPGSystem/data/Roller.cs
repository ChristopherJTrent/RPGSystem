using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public class Roller
    {
        public int Count { get; private set; }
        public int Sides { get; private set; }
        private Random generator = new Random();
        public Roller(int _count = 1, int _sides = 6)
        {
            Count = _count;
            Sides = _sides;
        }
        //@param _definition the d notation form of the desired die. eg 1d6
        public Roller (string _definition)
        {
            Regex regex = new Regex(@"^(?<count>\d+)d(?<sides>\d+)$");
            Match match = regex.Match(_definition);
            if (match.Success)
            {
                int _count;
                int _sides;
                if (int.TryParse(match.Groups["count"].Value, out _count) 
                    && int.TryParse(match.Groups["sides"].Value, out _sides))
                {
                    Count = _count;
                    Sides = _sides;
                    Console.WriteLine($"{_count}d{_sides}");
                }
                else
                {
                    Console.WriteLine($"Invalid input '{_definition}.");
                    Count = 0;
                    Sides = 0;
                }
            }

        }
        public int roll()
        {
            int total = 0;
            for (int i = 0; i < Count; i++)
            {
                total += generator.Next(1, Sides);
            }
            return total;
        }
    }
}
