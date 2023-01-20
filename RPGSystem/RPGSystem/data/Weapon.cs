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
        public int ArmorPenetration { get; private set; }
        public Weapon(string _name, Roller _dice, int _attacks_per_round, int ArmorPenetration = 0)
        {
            Name = _name;
            Dice = _dice;
            AttacksPerRound = _attacks_per_round;
            this.ArmorPenetration = ArmorPenetration;
        }
        public int getDamage(Monster m)
        {
            int damage = Dice.roll();
            int modifiedDefenceRating = Math.Max(m.DamageResistance - ArmorPenetration, 0);
            return damage > modifiedDefenceRating ? damage - modifiedDefenceRating : 0;
        }
    }
}
