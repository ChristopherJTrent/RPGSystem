using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPGSystem.data
{
    public class Weapon
    {
        public string Name { get; set; }
        public Roller Dice { get; private set; }
        public int AttacksPerRound { get; private set;}
        public int DamageBonus { get; private set; }
        public Weapon(string _name, Roller _dice, int _attacks_per_round, int damageBonus = 0)
        {
            Name = _name;
            Dice = _dice;
            AttacksPerRound = _attacks_per_round;
            DamageBonus = damageBonus;
        }
        public int getDamage(Monster m)
        {
            int damage = Dice.roll() + DamageBonus;
            return damage > m.DamageResistance ? damage - m.DamageResistance : 0;
        }
    }
}
