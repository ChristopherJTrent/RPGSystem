using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public class Weapon
    {
        public Roller Dice { get; private set; }
        public int AttacksPerRound { get; private set;}
        public Weapon(Roller _dice, int _attacks_per_round) {
            Dice= _dice;
            AttacksPerRound= _attacks_per_round;
        }
    }
}
