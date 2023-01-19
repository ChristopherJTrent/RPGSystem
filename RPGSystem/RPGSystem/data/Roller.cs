using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
